using System.Numerics;
namespace Lab4Main
{
    public class RSA
    {
        public RSA()
        {
            Array.Clear(encryptedText, 0, encryptedText.Length);
            Array.Clear(decryptedText, 0, decryptedText.Length);
        }
        public RSA(int p, int q, int n, int t, int e, int d)
        {
            this.P = p;
            this.Q = q; 
            this.N = n; 
            this.T = t; 
            this.E = e;
            this.D = d;
            Array.Clear(encryptedText, 0, encryptedText.Length);
            Array.Clear(decryptedText, 0, decryptedText.Length);
        }
        public long P { get; set; }
        public long Q { get; set; }
        public BigInteger T { get; set; }
        public BigInteger D { get; set; }
        public BigInteger N { get; set; }
        public BigInteger E { get; set; }

        public BigInteger[] encryptedText = new BigInteger[100];

        public BigInteger[] decryptedText = new BigInteger[100];
        public int sizE { get; set; }

        public static bool IsPrime(long prime)
        {
            if (prime <= 2) return false;
            BigInteger j = (BigInteger)Math.Sqrt(prime);

            for (BigInteger i = 2; i <= j; i++)
            {
                if (prime % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static BigInteger CalculateE(BigInteger t)
        {
            for (BigInteger e = 2; e < t; e++)
            {
                if (GreatestCommonDivisor(e, t) == 1)
                {
                    return e;
                }
            }

            return -1;
        }

        public static BigInteger GreatestCommonDivisor(BigInteger e, BigInteger t)
        {
            while (e > 0)
            {
                BigInteger temp = e;
                e = t % e;
                t = temp;
            }

            return t;
        }

        static BigInteger CalculateD(BigInteger e, BigInteger t)
        {
            BigInteger d;
            BigInteger k = 1;

            while (true)
            {
                k = k + t;

                if (k % e == 0)
                {
                    d = (k / e);
                    return d;
                }
            }
        }

        static BigInteger Encrypt(BigInteger i, BigInteger e, BigInteger n)
        {
            BigInteger current = i - 97;
            BigInteger result = 1;

            for (BigInteger j = 0; j < e; j++)
            {
                result = result * current;
                result = result % n;
            }

            return result;
        }

        static BigInteger Decrypt(BigInteger i, BigInteger d, BigInteger n)
        {
            i %= n;
            BigInteger result = 1;
            while (d > 0)
            {
                if ((d & (BigInteger)1) == 1) result = (result * i) % n;
                i = (i * i) % n;
                d >>= 1;
            }
            return result + 97;
        }

        public bool GenerateKeys()
        {
            P = GeneratePrime();
            Q = GeneratePrime();

            N = P * Q;
            T = (P - 1) * (Q - 1);
            E = CalculateE(T);
            D = CalculateD(E, T);
            return true;
        }
        public static long GeneratePrime()
        {
            long maxValue = (long)Math.Pow(2, 20);
            long minValue = (long)Math.Pow(2, 12);
            long prime = new Random().NextInt64(minValue, maxValue);
            while (!IsPrime(prime))
            {
                prime = new Random().NextInt64(minValue, maxValue);
            }
            return prime;
        }
        public string Encrypt(string msg)
        {
            string ans = "";
            sizE = msg.Length;
            for (int i = 0; i < msg.Length; i++)
            {
                encryptedText[i] = Encrypt(msg[i], E, N);
            }


            for (int i = 0; i < msg.Length; i++)
            {
                ans += encryptedText[i].ToString();
            }
            return ans;
        }

        public string Decrypt()
        {
            string ans = "";
            for (int i = 0; i < sizE; i++)
            {
                decryptedText[i] = Decrypt(encryptedText[i], D, N);
            }


            for (int i = 0; i < sizE; i++)
            {
                ans += (char)decryptedText[i];
            }
            return ans;
        }
    }
}
