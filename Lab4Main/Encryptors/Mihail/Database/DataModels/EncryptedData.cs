namespace Lab4Main.Encryptors.Mihail.Database
{
    public class EncryptedData
    {
        public int Id { get; set; }
        public string OriginalData { get; set; } = "";
        public string EncryptedText { get; set; } = "";
        public string DecryptedData { get; set; } = "";
        public string P { get; set; }
        public string Q { get; set; }
        public string N { get; set; }
        public string T { get; set; }
        public string E { get; set; }
        public string D { get; set; }
    }
}
