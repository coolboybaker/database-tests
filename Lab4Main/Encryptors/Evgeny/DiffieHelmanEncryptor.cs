using System.Numerics;
using System.Text;

namespace Lab4Main.Encryptors.Evgeny
{
    public class DiffieHelmanEncryptor
    {

        public DiffieHelmanEncryptor()
        {

        }

        public int GenerateNumber()
        {
            Random rnd = new Random();
            return rnd.Next(1000000, (int)Math.Pow(2, 20));
        }

        public bool IsNumberSimple(int number)
        {
            if (number <= 1) return false; // 0 и 1 не простые числа
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                    return false;
            }
            return true;
        }
        public int GetPrimeNumber20X()
        {
            int number;
            do
            {
                number = GenerateNumber();
            } while (!IsNumberSimple(number));
            return number;
        }

        public BigInteger FindPublicKey(string x, string y, string priv)
        {
            BigInteger X = BigInteger.Parse(x);
            BigInteger Y = BigInteger.Parse(y);
            int Priv = int.Parse(priv);
            BigInteger number = BigInteger.ModPow(X, Priv, Y);
            return number;
        }

        public BigInteger GetSecretKey(string pubKey, string privKey, string y)
        {
            BigInteger PK = BigInteger.Parse(pubKey);
            BigInteger PRK = BigInteger.Parse(privKey);
            BigInteger Y = int.Parse(y);
            BigInteger number = 0;
            number = BigInteger.ModPow(PK, PRK, Y);
            return number;
        }


        public string EncryptData(string data, string secKey)
        {
            string[] words = data.Split(' ');
            StringBuilder result = new StringBuilder();
            int SecKey = int.Parse(secKey);
            foreach (var word in words)
            {
                byte[] asciiBytes = Encoding.ASCII.GetBytes(word);
                foreach (var letter in asciiBytes)
                {
                    result.Append(letter + SecKey);
                    result.Append(' ');
                }
                result.Append(" ");
            }
            return result.ToString().TrimEnd(); // Удаляем последний пробел
        }

        public string DecryptData(string data, string secKey)
        {
            StringBuilder result = new StringBuilder();
            string[] words = data.Split();
            int SecKey = int.Parse(secKey);
            foreach (var word in words)
            {
                if (word != "," && word != "")
                {
                    int Word = int.Parse(word);
                    int asciWord = Word - SecKey;
                    string letter = Encoding.ASCII.GetString(new byte[] { (byte)asciWord });
                    result.Append(letter);
                }
                else
                {
                    result.Append(" ");
                }
            }
            return result.ToString();
        }
    }
}
