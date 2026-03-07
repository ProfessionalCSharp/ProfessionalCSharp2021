using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredProceduresWithEFCore.Extensions;
public static class DbContextExtensions
{
    public static async Task<IList<IList>> QueryStoredProcedureWithMultipleResults(
        this DbContext dbContext,
        List<Type> resultSetMappingTypes,
        string storedProcedureName,
        params object[] parameters
    )
    {
        var resultSets = new List<IList>();

        var connection = dbContext.Database.GetDbConnection();
        var parameterGenerator = dbContext.GetService<IParameterNameGeneratorFactory>()
            .Create();
        var commandBuilder = dbContext.GetService<IRelationalCommandBuilderFactory>()
            .Create();

        foreach (var parameter in parameters)
        {
            var generatedName = parameterGenerator.GenerateNext();
            if (parameter is DbParameter dbParameter)
            {
                commandBuilder.AddRawParameter(generatedName, dbParameter);
            }
            else
            {
                commandBuilder.AddParameter(generatedName, generatedName);
            }
        }

        await using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = storedProcedureName;
        command.Connection = connection;
        for (var i = 0; i < commandBuilder.Parameters.Count; i++)
        {
            var relationalParameter = commandBuilder.Parameters[i];
            relationalParameter.AddDbParameter(command, parameters[i]);
        }

        if (connection.State == ConnectionState.Closed)
        {
            await connection.OpenAsync();
        }

        await using var reader = await command.ExecuteReaderAsync();

        int resultIndex = 0;
        do
        {
            Type type = resultSetMappingTypes[resultIndex];

            var resultSetValues = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(type))!;
            var columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
            while (reader.Read())
            {
                var obj = Activator.CreateInstance(type);
                if (obj is null)
                {
                    throw new Exception($"Cannot create object from type '{type}'");
                }

                foreach (var column in columns)
                {
                    var value = reader[column] == DBNull.Value ? null : reader[column];
                    obj!.GetType().GetProperty(column)?.SetValue(obj, value);
                }

                resultSetValues!.Add(obj);
            }

            resultSets.Add(resultSetValues);
            resultIndex++;
        } while (reader.NextResult());

        return resultSets;
    }

    public static async Task<(IReadOnlyCollection<T1> FirstResultSet, IReadOnlyCollection<T2> SecondResultSet)>
        QueryStoredProcedureWithMultipleResults<T1, T2>(
            this DbContext dbContext,
            string storedProcedureName,
            params object[] parameters
        )
    {
        List<Type> resultSetMappingTypes = new() { typeof(T1), typeof(T2) };

        var resultSets =
            await QueryStoredProcedureWithMultipleResults(dbContext, resultSetMappingTypes, storedProcedureName,
                parameters);

        return ((IReadOnlyCollection<T1>)resultSets[0], (IReadOnlyCollection<T2>)resultSets[1]);
    }

    public static async
        Task<(IReadOnlyCollection<T1> FirstResultSet, IReadOnlyCollection<T2> SecondResultSet,
            IReadOnlyCollection<T3>
            ThirdResultSet)> QueryStoredProcedureWithMultipleResults<T1, T2, T3>(
            this DbContext dbContext,
            string storedProcedureName,
            params object[] parameters
        )
    {
        List<Type> resultSetMappingTypes = new() { typeof(T1), typeof(T2), typeof(T3) };

        var resultSets =
            await QueryStoredProcedureWithMultipleResults(dbContext, resultSetMappingTypes, storedProcedureName,
                parameters);

        return ((IReadOnlyCollection<T1>)resultSets[0], (IReadOnlyCollection<T2>)resultSets[1],
            (IReadOnlyCollection<T3>)resultSets[2]);
    }
}
