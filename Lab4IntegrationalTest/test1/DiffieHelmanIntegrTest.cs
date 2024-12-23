using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Lab4Main;
using Lab4Main.Encryptors.test1;
using Lab4Main.Encryptors.test1.Database;
using Lab4Main.Encryptors.test1.Database.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Lab4IntegrationalTest.test1
{
    public class DiffieHelmanIntegrTest : IDisposable
    {
        public DbConnection dbConfig;

        public DiffieHelmanIntegrTest()
        {
            dbConfig = new DbConnection("lab4tw_test_test1");



            using (var context = new DiffieHelmanApplicationContext(dbConfig))
            {
                context.Database.EnsureCreated();
            }
        }


        [Fact]
        public void EncryptDataSaveResultInDatabase()
        { 

            using (DiffieHelmanApplicationContext db = new DiffieHelmanApplicationContext(dbConfig))
            {

                var encryptor = new DiffieHelmanEncryptor();
                string originalData = "hello";
                string secKey = "3";

                string encrypted = encryptor.EncryptData(originalData, secKey);
                var encryptedData = new EncryptedData
                {
                    OriginalData = originalData,
                    EncryptedText = encrypted,
                    DecryptedData = encryptor.DecryptData(encrypted, secKey),
                    SecretKey = secKey
                };

                db.EncryptedDatas.Add(encryptedData);
                db.SaveChanges();

                var savedEntry = db.EncryptedDatas.Find(encryptedData.Id);
                Assert.NotNull(savedEntry);
                Assert.Equal(originalData, savedEntry.OriginalData);
                Assert.Equal(encrypted, savedEntry.EncryptedText);
                Assert.Equal(originalData, savedEntry.DecryptedData);
                Assert.Equal(secKey, savedEntry.SecretKey);
            }
        }

        [Fact]
        public void DecryptData_ShouldSaveResultInDatabase()
        {
            using (DiffieHelmanApplicationContext db = new DiffieHelmanApplicationContext(dbConfig))
            {

                var encryptor = new DiffieHelmanEncryptor();
                string originalData = "world";
                string secKey = "2";
                string encryptedData = encryptor.EncryptData(originalData, secKey);

                string decryptedData = encryptor.DecryptData(encryptedData, secKey);
                var encryptionResult = new EncryptedData
                {
                    OriginalData = originalData,
                    EncryptedText = encryptedData,
                    DecryptedData = decryptedData,
                    SecretKey = secKey
                };

                db.EncryptedDatas.Add(encryptionResult);
                db.SaveChanges();

                var savedEntry = db.EncryptedDatas.Find(encryptionResult.Id);
                Assert.NotNull(savedEntry);
                Assert.Equal(originalData, savedEntry.OriginalData);
                Assert.Equal(encryptedData, savedEntry.EncryptedText);
                Assert.Equal(originalData, savedEntry.DecryptedData);
                Assert.Equal(secKey, savedEntry.SecretKey);
            }
        }

        [Fact]
        public void GetSecretKey_ShouldReturnValidKeyAndSaveInDatabase()
        {
            using (DiffieHelmanApplicationContext db = new DiffieHelmanApplicationContext(dbConfig))
            {
                var encryptor = new DiffieHelmanEncryptor();
                string pubKey = "123456789";
                string privKey = "987654321";
                string y = "100";

                BigInteger secretKey = encryptor.GetSecretKey(pubKey, privKey, y);
                var encryptionResult = new EncryptedData
                {
                    SecretKey = secretKey.ToString()
                };

                db.EncryptedDatas.Add(encryptionResult);
                db.SaveChanges();

                var savedEntry = db.EncryptedDatas.Find(encryptionResult.Id);
                Assert.NotNull(savedEntry);
                Assert.Equal(secretKey.ToString(), savedEntry.SecretKey);
            }
                
        }
        public void Dispose()
        {
            using (var context = new ApplicationContext(dbConfig))
            {
                context.Database.EnsureDeleted();
            }
        }
    }






}
