using Microsoft.EntityFrameworkCore;

namespace L01_2021CO601_2019AC603.Models
{
    public class blogDBContext : DbContext
    {

        public blogDBContext(DbContextOptions<blogDBContext> options) : base(options)
        {

        }

        public DbSet<usuarios> usuarios { get; set; }
        public DbSet<publicaciones> publicaciones { get; set; }
        public DbSet<comentarios> comentarios { get; set; }

    }
}
