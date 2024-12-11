using Microsoft.EntityFrameworkCore;

namespace Test.Models
{
    public class DBContext: DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<UserDetail> userDetails{ get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
