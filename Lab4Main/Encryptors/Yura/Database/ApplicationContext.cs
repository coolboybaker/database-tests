using Microsoft.EntityFrameworkCore;

namespace Lab4Main
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Key> Keys { get; set; }
        public DbSet<Decoded> DecodedTexts { get; set; }
        public DbSet<Encoded> EncodedTexts { get; set; }

        private string dbName;
        private DbConnection dbConfig;

        public ApplicationContext()
        {
            dbConfig = new DbConnection();
            Database.EnsureCreated();
        }

        public ApplicationContext(DbConnection dbConfig)
        {
            this.dbConfig = dbConfig;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(dbConfig.GetConnectionString(),
                new MySqlServerVersion(new Version(8, 0, 32)));
        }
    }
}
