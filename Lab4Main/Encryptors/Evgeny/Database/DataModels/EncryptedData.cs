namespace Lab4Main.Encryptors.Evgeny.Database.DataModels
{
    public class EncryptedData
    {
        public int Id { get; set; }
        public string OriginalData { get; set; } = "";
        public string EncryptedText { get; set; } = "";
        public string DecryptedData { get; set; } = "";
        public string SecretKey { get; set; }
    }
}
