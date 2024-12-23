using Lab4Main.Encryptors.Evgeny.Database.DataModels;
using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Lab4Main.Encryptors.Evgeny.Database
{
    public class DiffieHelmanApplicationContext:DbContext
    {
        public DbSet<EncryptedData> EncryptedDatas { get; set; }
        private DbConnection dbConfig;

        public DiffieHelmanApplicationContext()
        {
            dbConfig = new DbConnection();
            Database.EnsureCreated();
        }

        public DiffieHelmanApplicationContext(DbConnection dbConfig)
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
