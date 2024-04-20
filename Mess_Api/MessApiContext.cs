using System.Collections.Generic;
using System.Reflection.Emit;
using Mess_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Mess_Api
{
    public class MessApiContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public MessApiContext(DbContextOptions<MessApiContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Дополнительная конфигурация моделей
        }
    }
}
