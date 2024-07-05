using Api_Login_Auth_ASPNET.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Login_Auth_ASPNET.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base (options)
        {

        }

        public DbSet<UsuarioModel> Usuario { get; set; }
    }
}
