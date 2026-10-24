using System;
using System.Security.Cryptography;
using System.Text.Json;
using API.DTOs;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.data;

public class Seed
{
  public static async Task SeedUsers(AppDbContext context)
  {
    if (await context.Users.AnyAsync()) return;
    var membersData = await File.ReadAllTextAsync("data/UserSeedData.json");
    var members = JsonSerializer.Deserialize<List<SeedUserDto>>(membersData);
    if(members == null)
        {
            Console.WriteLine("No members found in the seed data.");
            return;
        } 
    foreach (var member in members)
    {
              using var hmac = new HMACSHA512();

     var user = new AppUser
     {
         
        Id = member.Id,
        
        Email = member.Email,
        PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("Pa$$w0rd")),
        PasswordSalt = hmac.Key,

        ImageUrl = member.ImageUrl,
        DisplayName = member.DisplayName,
        Member = new Member
        {
            Id = member.Id,
            DateOfBirth = member.DateOfBirth,
            ImageUrl = member.ImageUrl,
            DisplayName = member.DisplayName,
            Created = member.Created,
            LastActive = member.LastActive,
            Gender = member.Gender,
            Description = member.Description,
            City = member.City,
            Country = member.Country
        }
        
     };
     user.Member.Photos.Add(new Photo
     {
        Url = member.ImageUrl,
        PublicId = member.Id
     });

     context.Users.Add(user);
    }

    await context.SaveChangesAsync();
  }
}
