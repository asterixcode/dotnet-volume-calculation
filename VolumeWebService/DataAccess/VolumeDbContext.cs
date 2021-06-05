using Microsoft.EntityFrameworkCore;
using VolumeWebService.Models;

namespace VolumeWebService.DataAccess
{
    public class VolumeDbContext : DbContext
    {
        public DbSet<VolumeResult> VolumeResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                @"Data Source = C:\Users\lucas\Desktop\DNP1ExamExample-300709\VolumeWebService\Volume.db");
        }
    }
}