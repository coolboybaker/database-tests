using Lab4Main.Encryptors.test1;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab4UnitTest.test1
{
    public class DiffieHelmanUnitTest
    {
        [Theory]
        [InlineData(1000000, 1048575)] // 2^20 - 1
        public void GenerateNumberTest(int min, int max)
        {
            var encryptor = new DiffieHelmanEncryptor();
            int generatedNumber = encryptor.GenerateNumber();
            Assert.InRange(generatedNumber, min, max);
        }

        [Theory]
        [InlineData(2, true)]    // 2 - простое число
        [InlineData(3, true)]    // 3 - простое число
        [InlineData(4, false)]   // 4 - не простое число
        [InlineData(5, true)]    // 5 - простое число
        [InlineData(10, false)]  // 10 - не простое число
        [InlineData(13, true)]   // 13 - простое число
        [InlineData(17, true)]   // 17 - простое число
        [InlineData(18, false)]  // 18 - не простое число
        [InlineData(19, true)]   // 19 - простое число
        [InlineData(1, false)]    // 1 - не простое число
        [InlineData(0, false)]    // 0 - не простое число
        [InlineData(-5, false)]   // отрицательное число - не простое
        public void IsNumberSimpleTest(int number, bool expected)
        {
            var encryptor = new DiffieHelmanEncryptor();
            bool result = encryptor.IsNumberSimple(number);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetPrimeNumber20XTest()
        {
            var encryptor = new DiffieHelmanEncryptor();
            int result = encryptor.GetPrimeNumber20X();
            Assert.True(encryptor.IsNumberSimple(result), "The generated number should be prime.");
        }

        [Fact]
        public void GetPrimeNumber20XRangeTest()
        {
            var encryptor = new DiffieHelmanEncryptor();
            int result = encryptor.GetPrimeNumber20X();
            Assert.InRange(result, 1000000, (int)Math.Pow(2, 20) - 1);
        }

        [Theory]
        [InlineData("3", "7", "4", 4)] // 3^4 mod 7 = 4
        [InlineData("5", "13", "3", 8)] // 5^3 mod 13 = 8
        [InlineData("10", "6", "2", 4)] // 10^2 mod 6 = 4
        [InlineData("2", "5", "3", 3)] // 2^3 mod 5 = 3
        [InlineData("7", "19", "5", 11)] // 7^5 mod 19 = 11
        public void FindPublicKeyTest(string x, string y, string priv, long expected)
        {
            var encryptor = new DiffieHelmanEncryptor();
            BigInteger result = encryptor.FindPublicKey(x, y, priv);
            Assert.Equal(expected, (long)result);
        }

        [Theory]
        [InlineData("3", "4", "7", 4)] // 3^4 mod 7 = 4
        [InlineData("5", "3", "13", 8)] // 5^3 mod 13 = 8
        [InlineData("10", "2", "6", 4)] // 10^2 mod 6 = 4
        [InlineData("2", "3", "5", 3)] // 2^3 mod 5 = 3
        [InlineData("7", "5", "19", 11)] // 7^5 mod 19 = 11
        public void GetSecretKeyTest(string pubKey, string privKey, string y, long expected)
        {
            var encryptor = new DiffieHelmanEncryptor();
            BigInteger result = encryptor.GetSecretKey(pubKey, privKey, y);
            Assert.Equal(expected, (long)result);
        }

        [Theory]
        [InlineData("hello", "3", "107 104 111 111 114")]
        [InlineData("test", "1", "117 102 116 117")]
        [InlineData("data", "0", "100 97 116 97")]
        public void EncryptDataTest(string data, string secKey, string expected)
        {
            var encryptor = new DiffieHelmanEncryptor();
            string result = encryptor.EncryptData(data, secKey);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("107 104 111 111 114", "3", "hello")]
        [InlineData("117 102 116 117", "1", "test")]
        [InlineData("100 97 116 97", "0", "data")]
        public void DecryptDataTest(string data, string secKey, string expected)
        {
            var encryptor = new DiffieHelmanEncryptor();
            string result = encryptor.DecryptData(data, secKey);
            Assert.Equal(expected, result);
        }
    }
}
