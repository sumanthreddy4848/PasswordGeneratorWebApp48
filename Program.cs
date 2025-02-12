using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
 
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
 
app.UseDefaultFiles(); // Serve index.html by default
app.UseStaticFiles(); // To serve CSS and JS if needed
 
app.MapGet("/generatepassword", (int length) => GeneratePassword(length));
 
app.Run();
 
static string GeneratePassword(int length)
{
    const string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    const string lowerCase = "abcdefghijklmnopqrstuvwxyz";
    const string numbers = "1234567890";
    const string specialChars = "!@#$%^&*()";
 
    string allChars = upperCase + lowerCase + numbers + specialChars;
    Random random = new Random();
 
    // Ensure that password contains at least one of each required character type
    string password = new string(Enumerable.Repeat(allChars, length)
        .Select(s => s[random.Next(s.Length)]).ToArray());
 
    // Replace first 4 characters to meet the criteria
    password = password
        .Remove(0, 1).Insert(0, upperCase[random.Next(upperCase.Length)].ToString())
        .Remove(1, 1).Insert(1, lowerCase[random.Next(lowerCase.Length)].ToString())
        .Remove(2, 1).Insert(2, numbers[random.Next(numbers.Length)].ToString())
        .Remove(3, 1).Insert(3, specialChars[random.Next(specialChars.Length)].ToString());
 
    return password;
}
