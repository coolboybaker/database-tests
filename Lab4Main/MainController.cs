using Lab4Main.Encryptors.Evgeny;
using Microsoft.AspNetCore.Mvc;

namespace Lab4Main
{
    public class WrappedString
    {
        public string Text { get; set; }

    }

    [ApiController]
    [Route("api/request")]
    public class MainController : ControllerBase
    {
        BlockEncryptor blockEncryptor = new BlockEncryptor();
        DiffieHelmanEncryptor dhEncryptor = new DiffieHelmanEncryptor(); 
        RSA RSAEncryptor = new RSA();

        [HttpPost("BlockEncrypt")]
        public async Task<IActionResult> BlockEncrypt([FromBody] WrappedString text)
        {
            var res = blockEncryptor.Encrypt(text.Text, "1234");
            return Ok(res);
        }

        [HttpPost("BlockDecrypt")]
        public async Task<IActionResult> BlockDecrypt([FromBody] WrappedString text)
        {
            var res = blockEncryptor.Decrypt(text.Text, "1234");
            return Ok(res);
        }

        [HttpPost("DHEncrypt")]
        public async Task<IActionResult> DHEncrypt([FromBody] WrappedString text)
        {
            var res = dhEncryptor.EncryptData(text.Text, "3");
            return Ok(res);
        }

        [HttpPost("DHDecrypt")]
        public async Task<IActionResult> DHDecrypt([FromBody] WrappedString text)
        {
            var res = dhEncryptor.DecryptData(text.Text, "3");
            return Ok(res);
        }

        [HttpPost("MishaEncrypt")]
        public async Task<IActionResult> MishaEncrypt([FromBody] WrappedString text)
        {
            RSAEncryptor.GenerateKeys();
            var res = RSAEncryptor.Encrypt(text.Text);
            return Ok(res);
        }

        [HttpPost("MishaDecrypt")]
        public async Task<IActionResult> MishaDecrypt([FromBody] WrappedString text)
        {
            RSAEncryptor.GenerateKeys();
            var res = RSAEncryptor.Decrypt();
            return Ok(res);
        }
    }
}

