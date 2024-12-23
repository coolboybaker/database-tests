using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Lab4Main.Encryptors.Mihail.Database
{
    public class RSAApplicationContext : DbContext
    {
        public DbSet<EncryptedData> EncryptedDatas { get; set; }
        private DbConnection dbConfig;

        public RSAApplicationContext()
        {
            dbConfig = new DbConnection();
            Database.EnsureCreated();
        }

        public RSAApplicationContext(DbConnection dbConfig)
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
