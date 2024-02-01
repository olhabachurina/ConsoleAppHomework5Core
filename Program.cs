using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ConsoleAppHomework5Core;

class Program
{
    static void Main()
    {
        using (var db = new ApplicationContext())
        {
            //db.Database.EnsureCreated();
            //// Добавление пользователей и их настроек
            //var user1 = new User { Username = "User1", Email = "user1@ukr.net", Settings = new UserSettings { Language = "English", Theme = "Light" } };
            //var user2 = new User { Username = "User2", Email = "user2@ukr.net", Settings = new UserSettings { Language = "Russian", Theme = "Dark" } };
            //var user3 = new User { Username = "User3", Email = "user3@ukr.net", Settings = new UserSettings { Language = "Spanish", Theme = "Light" } };

            //db.Users.AddRange(user1, user2, user3);
            //db.SaveChanges();

            // Получение пользователя с Id = 2 и его настроек
            //var userWithId2 = db.Users.Include(u => u.Settings).SingleOrDefault(u => u.UserId == 2);

            //if (userWithId2 != null)
            //{
            //    Console.WriteLine($"Пользователь с Id = 2: {userWithId2.Username}");
            //    Console.WriteLine($"Настройки пользователя: Language - {userWithId2.Settings.Language}, Theme - {userWithId2.Settings.Theme}");
            //}
            //else
            //{
            //    Console.WriteLine("Пользователь с Id = 2 не найден");
            //}

            // Удаление пользователя с Id = 3
            //var userToDelete = db.Users.Include(u => u.Settings).SingleOrDefault(u => u.UserId == 3);
            //if (userToDelete != null)
            //{
            //    db.Users.Remove(userToDelete);
            //    db.SaveChanges();
            //    Console.WriteLine($"Пользователь с Id = 3 удален");
            //}
            //else
            //{
            //    Console.WriteLine("Пользователь с Id = 3 не найден");
            //}
        }
    }
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public UserSettings Settings { get; set; }
    }

    public class UserSettings
    {
        [Key]
        public int UserId { get; set; }
        public string Language { get; set; }
        public string Theme { get; set; }
        public User User { get; set; }
    }
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-4PCU5RA\\SQLEXPRESS;Database=UserSet;Trusted_Connection=True;TrustServerCertificate=True;");
                optionsBuilder.LogTo(e => Debug.WriteLine(e), new[] { RelationalEventId.CommandExecuted });
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Settings)
                .WithOne(s => s.User)
                .HasForeignKey<UserSettings>(s => s.UserId);
        }
    }
}


