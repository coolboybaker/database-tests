using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4UiTest.test1
{
    public class DiffieHelmanUiTest : IDisposable
    {
        private readonly IWebDriver _driver;
        private string url = "http://localhost:5098";

        public DiffieHelmanUiTest()
        {
            _driver = new ChromeDriver();
        }

        private void Pause(int seconds)
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 0, seconds));
            var delay = new TimeSpan(0, 0, 0, seconds);
            var timestamp = DateTime.Now;
            wait.Until(webDriver => (DateTime.Now - timestamp) > delay);
        }

        private void Pause(double seconds)
        {
            int milliseconds = (int)(seconds * 1000);
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 0, 0, milliseconds));
            var delay = new TimeSpan(0, 0, 0, milliseconds);
            var timestamp = DateTime.Now;
            wait.Until(webDriver => (DateTime.Now - timestamp) > delay);
        }

        [Fact]
        public void EncryptTest()
        {
            _driver.Navigate().GoToUrl(url);

            Pause(1);

            _driver.FindElement(By.Id("evgeny-encrypt-input")).SendKeys("hello");
            _driver.FindElement(By.Id("evgeny-encrypt-button")).Click();

            Pause(1);

            string expected = "107 104 111 111 114";//////////////////////
            string actual = _driver.FindElement(By.Id("evgeny-decrypt-input")).GetAttribute("value");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DecryptTest()
        {
            _driver.Navigate().GoToUrl(url);

            Pause(1);

            _driver.FindElement(By.Id("evgeny-decrypt-input")).SendKeys("107 104 111 111 114");/////////////
            _driver.FindElement(By.Id("evgeny-decrypt-button")).Click();

            Pause(1);

            string expected = "hello";
            string actual = _driver.FindElement(By.Id("evgeny-encrypt-input")).GetAttribute("value");

            Assert.Equal(expected, actual);
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
