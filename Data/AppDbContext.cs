using BankTask.Models;
using Microsoft.EntityFrameworkCore;

namespace BankTask.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
            //Database.EnsureDeleted();   // Удаляем бд со старой схемой
            Database.EnsureCreated();   // Создаем бд с новой схемой
        }
        
        public DbSet<Client> Clients { set; get; }
        
        public DbSet<Founder> Founders { set; get; }
    }
}