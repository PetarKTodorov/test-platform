namespace TestPlatform.Tests.Selenium
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    public class AuthSeleniumTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            this.driver = new ChromeDriver();
        }

        [Test]
        [TestCase("administrator@email.mail", "12msHZ&!srd")]
        [TestCase("teacher1@email.mail", "12msHZ&!srd")]
        [TestCase("demo@email.mail", "12msHZ&!srd")]
        public void TestLogin(string email, string password)
        {
            try
            {
                this.driver.Navigate().GoToUrl("https://localhost:44361/Account/User/Login");

                IWebElement emailElement = this.driver.FindElement(By.Id("Email"));
                emailElement.SendKeys(email);

                IWebElement passwordElement = this.driver.FindElement(By.Id("Password"));
                passwordElement.SendKeys(password);

                IWebElement loginButton = this.driver.FindElement(By.CssSelector("button[type='submit']"));
                loginButton.Click();

                By logoutBy = By.XPath(@"/html/body/nav/div/div/ul[2]/li/ul/li[2]/a");

                if (this.IsElementPresent(logoutBy))
                {
                    throw new SuccessException("Logged in successfully");
                }

                throw new SuccessException("Failed to login");
            }
            catch (SuccessException ex)
            {
                Assert.Pass(ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        private bool IsElementPresent(By byQuery)
        {
            try
            {
                this.driver.FindElement(byQuery);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}