using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

public static class DataSeeder{
    public static void Initialize(IServiceProvider serviceProvider){
        using (var context = new Context(
            serviceProvider.GetRequiredService<DbContextOptions<Context>>()
        )){
            if (context.User.Any()){
                return;
            }

            context.Users.AddRange(
                new User
                {
                    Name = "Alice Johnson",
                    Email = "alice@example.com"
                },
                new User
                {
                    Name = "Bob Smith",
                    Email = "bob@example.com"
                },
                new User
                {
                    Name = "Charlie Brown",
                    Email = "charlie@example.com"
                }
            );

            context.SaveChanges();
        }
    }
}