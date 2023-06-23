using Microsoft.EntityFrameworkCore;
using VillaAPI.Models;

namespace VillaAPI.Data
{
    // translator for code and DataBase
    public class AppDBContext : DbContext
    {
        public DbSet<Villa> Villas { get; set; }
    }
}
