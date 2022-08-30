global using BooksAPI.Models;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();
builder.Services.AddSqlServer<BooksContext>(builder.Configuration.GetConnectionString("BooksConnection"));
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Books API v1"));
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();

app.Run();
