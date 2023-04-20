using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Controllers
{
    public class RandomHashController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly Random _random = new Random();

        [HttpGet("get-hash")]
        public async Task<ActionResult<string>> GetHash()
        {
            await Task.Delay(1000); // Wait for 1 second
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
            var hashBytes = sha256.ComputeHash(bytes);
            var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            return hashString;
        }

        [HttpGet("request-hash")]
        public async Task<IActionResult> RequestHash()
        {
            var lastChar = ' ';
            string hashString="";
            while (!Char.IsDigit(lastChar) || (int.Parse(lastChar.ToString()) % 2 == 0))
            {
                var hash = await GetStringHash();
                lastChar = hash.Substring(hash.Length - 1)[0];
                await Task.Delay(1000);
                hashString = hash;
            }
            return Content(hashString);
        }

        private static async Task<string> GetStringHash()
        {
            var apiUrl = "https://localhost:7193/get-hash";
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(apiUrl);
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
        }
    }
}
