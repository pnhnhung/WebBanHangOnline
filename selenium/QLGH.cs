using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;

namespace Intergrated_QLDH
{
    public class QLGH
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://localhost:44374/Home/Index");
            Thread.Sleep(3000);
        }

        private void Login(string username, string password)
        {
            IWebElement myAccount = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[@href='#' and contains(text(),'My Account')]")));
            Actions actions = new Actions(driver);
            actions.MoveToElement(myAccount).Perform();
            Thread.Sleep(1000);

            IWebElement signInButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/account/login' and contains(text(),'Sign In')]")));
            signInButton.Click();
            Thread.Sleep(3000);

            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("UserName"))).SendKeys(username);
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Password"))).SendKeys(password);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='loginForm']/form/div[4]/div/input"))).Click();
        }

        private void LoginAdmin(string username, string password)
        {
            driver.Navigate().GoToUrl("https://localhost:44374/Admin/Account/Login");
            Thread.Sleep(3000);

            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("UserName"))).SendKeys(username);
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Password"))).SendKeys(password);

            // Tìm và nhấn vào nút Sign In
            IWebElement signInButton = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//div[@class='col-4']/button[@type='submit' and contains(@class, 'btn-primary')]")
            ));

            // Cuộn xuống nút đăng nhập nếu bị che
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", signInButton);
            Thread.Sleep(500);

            // Dùng Actions để click nếu cần
            Actions actions = new Actions(driver);
            actions.MoveToElement(signInButton).Click().Perform();

            Thread.Sleep(3000); // Chờ sau khi đăng nhập

            // Chuyển hướng về trang Admin Dashboard
            driver.Navigate().GoToUrl("https://localhost:44374/admin/order");
            Thread.Sleep(2000);
        }

        [Test]
        public void Test_DatHangThanhCong()
        {
            Login("Nhung", "123456789");

            driver.Navigate().GoToUrl("https://localhost:44374/products");
            Thread.Sleep(3000);

            // Tìm sản phẩm "PISTA GP RR E2206 DOT - ORO"
            IWebElement productElement = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//h6[@class='product_name']/a[contains(text(), 'PISTA GP RR E2206 DOT - ORO')]")
            ));
            string productName = productElement.Text;

            // Hover vào sản phẩm để hiển thị nút "Add to Cart"
            Actions actions = new Actions(driver);
            actions.MoveToElement(productElement).Perform();
            Thread.Sleep(1000);

            // Tìm và click vào nút "Add to Cart"
            IWebElement addToCartButton = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//div[contains(@class,'product')]//a[contains(@class, 'btnAddToCart') and @data-id='99']")
            ));

            // Scroll đến nút "Add to Cart" nếu cần
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", addToCartButton);
            Thread.Sleep(500);

            // Click vào nút "Add to Cart"
            actions.MoveToElement(addToCartButton).Click().Perform();
            Thread.Sleep(2000);

            // Kiểm tra nếu có alert
            try
            {
                WebDriverWait waitForAlert = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                waitForAlert.Until(ExpectedConditions.AlertIsPresent()).Accept();
                Thread.Sleep(2000);
            }
            catch (NoAlertPresentException) { }

            // Vào giỏ hàng
            driver.Navigate().GoToUrl("https://localhost:44374/gio-hang");
            Thread.Sleep(1000);

            IWebElement payButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div[3]/div/div/div[2]/div[2]/div/a[2]")));
            payButton.Click();

            // Nhập thông tin khách hàng
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='myForm']/div[1]/input"))).SendKeys("Đàm hòa Giaiiiiii");
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='myForm']/div[2]/input"))).SendKeys("0774162631");
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='myForm']/div[3]/input"))).SendKeys("50 Lò Siêu");
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='myForm']/div[4]/input"))).SendKeys("damhoagiai456@gmail.com");

            IWebElement paymentDropdown = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='myForm']/div[5]/select")));
            SelectElement selectPayment = new SelectElement(paymentDropdown);
            selectPayment.SelectByIndex(1);

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='myForm']/div[6]/button"))).Click();

            // Kiểm tra đặt hàng thành công
            bool isOrderPlaced = driver.FindElements(By.XPath("//*[@id='load_data']/h1")).Count > 0;
            Thread.Sleep(2000);
            Assert.That(isOrderPlaced, Is.True, "Đặt hàng không thành công!");

            // Đăng nhập admin
            LoginAdmin("nishida", "123456");
            Thread.Sleep(3000);


            // Truy cập danh sách đơn hàng
            driver.Navigate().GoToUrl("https://localhost:44374/admin/order");
            Thread.Sleep(3000);

            // Kiểm tra thông tin đơn hàng mới nhất
            IWebElement firstOrderRow = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//table[@class='table table-bordered']/tbody/tr[1]")));

            string actualCustomerName = firstOrderRow.FindElement(By.XPath("./td[3]")).Text.Trim();
            string actualPhone = firstOrderRow.FindElement(By.XPath("./td[4]")).Text.Trim();
            string actualAddress = firstOrderRow.FindElement(By.XPath("./td[5]")).Text.Trim();
            string actualPrice = firstOrderRow.FindElement(By.XPath("./td[6]")).Text.Trim();    
            string actualStatus = firstOrderRow.FindElement(By.XPath("./td[7]")).Text.Trim();

            Assert.That(actualCustomerName, Is.EqualTo("Đàm hòa Giaiiiiii"), "Tên khách hàng không khớp!");
            Assert.That(actualPhone, Is.EqualTo("0774162631"), "Số điện thoại không khớp!");
            Assert.That(actualAddress, Is.EqualTo("50 Lò Siêu"), "Địa chỉ không khớp!");
            Assert.That(actualPrice, Is.EqualTo("$1700"), "Giá trị đơn hàng không khớp!");
            Assert.That(actualStatus, Is.EqualTo("Đã thanh toán"), "Trạng thái đơn hàng không khớp!");

            Console.WriteLine("Thông tin đơn hàng khớp với đơn đã đặt!");
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}
