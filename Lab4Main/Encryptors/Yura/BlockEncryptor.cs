namespace Lab4Main
{
    public class BlockEncryptor
    {
        private const int SYMBOL_SIZE = 8;

        private const int BLOCK_SIZE = 8;
        private const int SYMBOLS_IN_BLOCK = BLOCK_SIZE / SYMBOL_SIZE;

        private const int KEY_SIZE = 16;
        private const int SYMBOLS_IN_KEY = KEY_SIZE / SYMBOL_SIZE;

        public ulong StringToUlong(string str)
        {
            ulong result = 0;
            for (int i = str.Count() - 1; i >= 0; --i)
            {
                result = result << SYMBOL_SIZE;
                result += str[i];
            }
            return result;
        }

        public string UlongToString(ulong ul)
        {
            string str = "";
            for (int i = 0; i < SYMBOLS_IN_BLOCK; ++i)
            {
                str += (char)ul;
                ul = ul >> SYMBOL_SIZE;
            }
            return str;
        }

        public string PadString(string str)
        {
            //while (str.Count() % SYMBOLS_IN_BLOCK != 0)
            //{
            //    str = str + " ";
            //}
            return str;
        }

        public string EncryptBlock(string block, string key)
        {
            ulong ulongBlock = StringToUlong(block);
            ulong k = StringToUlong(key);
            ulongBlock = ulongBlock ^ StringToUlong(key);
            block = UlongToString(ulongBlock);
            return block;
        }

        public string Encrypt(string text, string key)
        {
            string result = "";
            text = PadString(text);
            int count = text.Length / SYMBOLS_IN_BLOCK;
            for (int i = 0; i < count; ++i)
            {
                int start = i * SYMBOLS_IN_BLOCK;
                result += EncryptBlock(text.Substring(start, SYMBOLS_IN_BLOCK), key);
            }
            return result;
        }

        public string Decrypt(string text, string key)
        {
            string result = "";
            text = PadString(text);
            int count = text.Length / SYMBOLS_IN_BLOCK;
            for (int i = 0; i < count; ++i)
            {
                int start = i * SYMBOLS_IN_BLOCK;
                result += EncryptBlock(text.Substring(start, SYMBOLS_IN_BLOCK), key);
            }
            return result;
        }

        public string GenerateKey()
        {
            var random = new Random();
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string key = "";

            for (int i = 0; i < SYMBOLS_IN_KEY; ++i)
            {
                char curChar = alphabet[random.Next(alphabet.Length)];
                key += curChar;
            }

            return key;
        }
    }
}
