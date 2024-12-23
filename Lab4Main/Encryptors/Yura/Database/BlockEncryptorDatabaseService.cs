namespace Lab4Main
{
    public class BlockEncryptorDatabaseService
    {
        private DbConnection dbConfig;

        public BlockEncryptorDatabaseService()
        {
            dbConfig = new DbConnection();
        }

        public BlockEncryptorDatabaseService(DbConnection dbConfig)
        {
            this.dbConfig = dbConfig;
        }

        public async Task<string> GetKey(int id)
        {
            using (var context = new ApplicationContext(dbConfig))
            {
                return context.Keys.Find(id).KeyText;
            }
        }

        public async Task AddKey(int id, string key)
        {
            using (var context = new ApplicationContext(dbConfig))
            {
                context.Keys.Add(new Key { Id = id, KeyText = key});
                context.SaveChanges();
            }
        }
    }
}
