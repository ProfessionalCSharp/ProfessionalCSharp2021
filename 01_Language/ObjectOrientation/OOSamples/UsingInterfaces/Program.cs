using System;
using UsingInterfaces;

IBankAccount venusAccount = new SaverAccount();
IBankAccount jupiterAccount = new GoldAccount();

venusAccount.PayIn(200);
venusAccount.Withdraw(100);
Console.WriteLine(venusAccount.ToString());

jupiterAccount.PayIn(500);
jupiterAccount.Withdraw(600);
jupiterAccount.Withdraw(100);
Console.WriteLine(jupiterAccount.ToString());