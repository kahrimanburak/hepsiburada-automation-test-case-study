using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium.Interactions;

namespace HepsiburadaTestProject
{
    class Test
    {
        static void Main(string[] args)
        {

        }

        IWebDriver driver = new FirefoxDriver();

        [SetUp]
        public void Initialize()
        {
            driver.Url = "https://www.hepsiburada.com/asus-fx504gd-58050-intel-core-i5-8300h-8gb-1tb-8gb-ssd-gtx1050-freedos-15-6-fhd-tasinabilir-bilgisayar-p-HBV00000EBOMR";
        }

        [Test]
        public void TestMethod()
        {

            productShowPage();
            cardPage();
            memberPage();
            deliveryPage();
            paymentPage();
            orderPage();

            driver.Close();

        }

        public void productShowPage()
        {
            Thread.Sleep(1000);
            //Sepetime ekle butonuna tıklandığında “Sepetim” sayfasına geçiş yapılmalı.
            driver.FindElement(By.Id("addToCart")).Click();
            Thread.Sleep(3000);
        }

        public void cardPage()
        {
            Thread.Sleep(1000);
            //Alışverişi Tamamla” butonuna tıklayarak “Üye girişi” sayfasına geçiş yapılmalı.
            driver.FindElement(By.XPath("//button[contains(@class, 'btn-primary')]")).Click();
            Thread.Sleep(3000);
        }

        public void memberPage()
        {
            Thread.Sleep(1000);
            //Üye olmadan devam et seçeneği seçilmeli
            driver.FindElement(By.XPath("//*[contains(@for, 'select-guest')]")).Click();
            Thread.Sleep(3000);

            //E-mail boş bir şekilde giriş yapıldığında uyarı mesajı görünmeli.
            driver.FindElement(By.XPath("//*[contains(@data-track, 'giris, click, alisverisi-tamamla')]")).Click();
            Thread.Sleep(2000);
            string bodyText_giris = driver.FindElement(By.TagName("body")).Text;
            Assert.IsTrue(bodyText_giris.Contains("Lütfen e-posta adresinizi girin."));
            Thread.Sleep(2000);

            //Geçersiz bir e-mail adresi girildiğinde uyarı mesajı görünmeli.
            driver.FindElement(By.Id("lazy-email")).SendKeys("gecersizemail" + Keys.Enter);
            driver.FindElement(By.XPath("//*[contains(@data-track, 'giris, click, alisverisi-tamamla')]")).Click();
            Assert.IsTrue(bodyText_giris.Contains("Lütfen e-posta adresinizi girin."));
            Thread.Sleep(2000);

            //Geçerli bir e-mail adresi girildikten sonra “Sonraki adım” butonuna tıklanarak “Teslimat Bilgileri” sayfasına geçiş yapılmalı.
            driver.FindElement(By.Id("lazy-email")).Clear();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("lazy-email")).SendKeys("kahriman.burak7@gmail.com");
            int checkmark = driver.FindElements(By.XPath("//*[contains(@class, 'checkmark draw')]")).Count;
            Assert.AreEqual(checkmark, 1);
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//*[contains(@data-track, 'giris, click, alisverisi-tamamla')]")).Click();
            Thread.Sleep(2000);
        }

        public void deliveryPage()
        {
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;

            //Adres alanları boş iken “Devam et” butonuna tıklandığında doldurulması zorunlu alanlarla ilgili uyarı mesajları görünmeli
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//button[contains(@class, 'btn-primary')]")).Click();
            Thread.Sleep(2000);
            string bodyText_adres = driver.FindElement(By.TagName("body")).Text;
            Assert.IsTrue(bodyText_adres.Contains("Lütfen adınızı girin."));
            Assert.IsTrue(bodyText_adres.Contains("Lütfen soyadınızı girin."));
            Assert.IsTrue(bodyText_adres.Contains("Lütfen ilçe seçin."));
            Assert.IsTrue(bodyText_adres.Contains("Lütfen adres girin."));
            Assert.IsTrue(bodyText_adres.Contains("Lütfen adres adı oluşturun."));
            Assert.IsTrue(bodyText_adres.Contains("Lütfen telefon numaranızı girin."));

            //Adres alanları tek tek doldurularak “Ödeme Bilgileri” sayfasına geçiş yapılmalı.
            driver.FindElement(By.Id("first-name")).SendKeys("Burak");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("last-name")).SendKeys("Kahriman");
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//*[contains(@data-id, 'country')]")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Türkiye")).Click();
            Thread.Sleep(2000);
            
            js.ExecuteScript("window.scrollBy(0,300);");
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//*[contains(@data-id, 'city')]")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.LinkText("İstanbul")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//*[contains(@data-id, 'town')]")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.LinkText("ATAŞEHİR")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//*[contains(@data-id, 'district')]")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.LinkText("İÇERENKÖY")).Click();
            Thread.Sleep(2000);
            
            js.ExecuteScript("window.scrollBy(0,200);");
            Thread.Sleep(2000);

            driver.FindElement(By.Id("address")).SendKeys("Eski Üsküdaryolu caddesi Malatya sokak No:3");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("address-name")).SendKeys("Ev Adresim");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("phone")).SendKeys("534 873 43 59");
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//button[contains(@class, 'btn-primary')]")).Click();
            Thread.Sleep(3000);

        }

        public void paymentPage()
        {
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;

            //Kredi/Banka Kartı seçeneği seçilebilmeli,
            driver.FindElement(By.XPath("//*[contains(@class, 'accordion-item credit-card')]")).Click();
            Thread.Sleep(3000);
            js.ExecuteScript("window.scrollBy(0,200);");
            Thread.Sleep(2000);

            //Kart bilgileri doldurulmadan “Devam et” butonuna tıklandığında doldurulması zorunlu olan alanlarla ilgili uyarı mesajları görünmeli.
            driver.FindElement(By.XPath("//button[contains(@class, 'btn-primary')]")).Click();
            Thread.Sleep(2000);
            int error_class_count = driver.FindElements(By.XPath("//*[contains(@class, 'control-group error')]")).Count;
            Assert.AreEqual(error_class_count, 3);
            //Not: cvc'nin uyarı clası farklı olduğu için ayrıca test edildi.
            int last_error_class_count = driver.FindElements(By.XPath("//*[contains(@class, 'control-group last error')]")).Count;
            Assert.AreEqual(last_error_class_count, 1);
            Thread.Sleep(2000);

            //Tooltip’lere tıklandığında gerekli mesajlar görünmeli.
            Actions card_no = new Actions(driver);
            IWebElement card_no_tooTip = driver.FindElement(By.XPath("//i[@for= 'card-no']"));
            card_no.MoveToElement(card_no_tooTip).Click().Build().Perform();
            Thread.Sleep(2000);
            Actions holder_name = new Actions(driver);
            IWebElement holder_name_tooTip = driver.FindElement(By.XPath("//i[@for= 'holder-Name']"));
            card_no.MoveToElement(holder_name_tooTip).Click().Build().Perform();
            Thread.Sleep(2000);
            Actions expire_year = new Actions(driver);
            IWebElement expire_year_tooTip = driver.FindElement(By.XPath("//i[@for= 'expireYear']"));
            card_no.MoveToElement(expire_year_tooTip).Click().Build().Perform();
            Thread.Sleep(2000);
            Actions cvc = new Actions(driver);
            IWebElement cvc_tooTip = driver.FindElement(By.XPath("//i[@for= 'cvc']"));
            card_no.MoveToElement(cvc_tooTip).Click().Build().Perform();
            Thread.Sleep(2000);
            Actions cvc_info = new Actions(driver);
            IWebElement cvc_info_tooTip = driver.FindElement(By.XPath("//div[@class= 'cvc-info popover']"));
            card_no.MoveToElement(cvc_info_tooTip).Click().Build().Perform();
            Thread.Sleep(2000);
            int cvc_info_class_count = driver.FindElements(By.XPath("//*[contains(@class, 'hb-ui-popover show')]")).Count;
            Assert.AreEqual(cvc_info_class_count, 1);
            Thread.Sleep(2000);

            //Geçersiz bir kart numarası girildiğinde uyarı mesajı görünmeli.
            driver.FindElement(By.Id("card-no")).SendKeys("4824894728063010");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("holder-Name")).SendKeys("Burak Kahriman");
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//*[contains(@title, 'Ay')]")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.LinkText("10")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//*[contains(@title, 'Yıl')]")).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.LinkText("2020")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("cvc")).SendKeys("123");
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//button[contains(@class, 'btn-primary')]")).Click();
            Thread.Sleep(2000);
            string bodyText_gecerli_kard = driver.FindElement(By.TagName("body")).Text;
            Assert.IsTrue(bodyText_gecerli_kard.Contains("Lütfen geçerli bir kredi kartı girin."));
            Thread.Sleep(2000);

            //Geçerli kart bilgileri girildiğinde taksit seçenekleri görünmeli.
            driver.FindElement(By.Id("card-no")).Clear();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("card-no")).SendKeys("375622005485014");
            Thread.Sleep(3000);
            Boolean installment = driver.FindElement(By.XPath("//*[contains(@class, 'installment-list')]")).Displayed;
            Assert.AreEqual(true, installment);
            Thread.Sleep(2000);

            //Geçerli bilgiler girildikten sonra “Devam et” butonuna tıklandığında “Sipariş Özeti” sayfasına geçiş yapılmalı.
            driver.FindElement(By.XPath("//button[contains(@class, 'btn-primary')]")).Click();
            Thread.Sleep(3000);
            
        }

        public void orderPage()
        {
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;

            //Ödeme bilgileri sayfasında seçtiğimiz “Peşin ödeme” durumu sipariş özeti sayfasında görüntülenmeli.
            js.ExecuteScript("window.scrollBy(0,200);");
            Thread.Sleep(2000);
            string odeme_sekli_pesin = driver.FindElement(By.XPath("//*[contains(@class,'info-detail')]")).FindElement(By.XPath("//*[contains(@data-bind,'text: installmentText')]")).Text;
            Assert.AreEqual("Peşin", odeme_sekli_pesin);
            Thread.Sleep(2000);

            //Taksit seçeneğini denemek amacıyla “Ödeme Bilgileri” linkine tıklanarak ödeme bilgileri sayfasına geçiş yapılmalı.
            driver.FindElement(By.LinkText("Ödeme Bilgileri")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//*[contains(@class, 'accordion-item credit-card')]")).Click();
            Thread.Sleep(3000);
            js.ExecuteScript("window.scrollBy(0,300);");
            Thread.Sleep(2000);

            //Ödeme bilgileri sayfasında kart bilgileri tekrar doldurulup taksit seçeneklerinden “Taksit” seçeneği seçilerek “Devam Et” butonuna tılandığında sipariş özetine geçiş yapılabilmeli.
            driver.FindElement(By.Id("card-no")).SendKeys("375622005485014");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("holder-Name")).SendKeys("Burak Kahriman");
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//*[contains(@title, 'Ay')]")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.LinkText("10")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//*[contains(@title, 'Yıl')]")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.LinkText("2020")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("cvc")).SendKeys("123");
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//*[contains(@for, 'installment-3')]")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//button[contains(@class, 'btn-primary')]")).Click();
            Thread.Sleep(3000);

            //Ödeme bilgileri sayfasında seçmiş olduğumuz taksit seçeneği sipariş özeti sayfasında görünmeli.
            js.ExecuteScript("window.scrollBy(0,300);");
            Thread.Sleep(2000);
            string odeme_sekli_taksit = driver.FindElement(By.XPath("//*[contains(@class,'info-detail')]")).FindElement(By.XPath("//*[contains(@data-bind,'text: installmentText')]")).Text;
            Assert.AreEqual("3", odeme_sekli_taksit);
            Thread.Sleep(2000);
                    
        }

       


    }
}
