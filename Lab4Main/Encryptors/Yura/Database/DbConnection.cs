using Newtonsoft.Json;

namespace Lab4Main
{
    public class DbConnection
    {
        public string server;
        public string uid;
        public string pwd;
        public string database;

        public DbConnection()
        {
            using (StreamReader r = new StreamReader(@"../../../../dbCredentials.json"))
            {
                string json = r.ReadToEnd();
                var credentials = JsonConvert.DeserializeObject<DbCredentials>(json);

                if (credentials == null)
                {
                    throw new Exception("Please provide a json file with database credentials");
                }

                server = "localhost";
                uid = credentials.uid;
                pwd = credentials.pwd;
                database = "lab4tw";
            }
        }

        public DbConnection(string server, string uid, string pwd, string database)
        {
            this.server = server;
            this.uid = uid;
            this.pwd = pwd;
            this.database = database;
        }

        public DbConnection(string dbName): this()
        {
            database = dbName;
        }

        public string GetConnectionString()
        {
            return $"server={server};uid={uid};Port=3306;pwd={pwd};database={database};Allow User Variables=True;default command timeout=30";
        }
    }
}
