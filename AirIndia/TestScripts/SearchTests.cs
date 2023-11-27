using BunnyCart.PageObjects;
using BunnyCart.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunnyCart.TestScripts
{
    internal class SearchTests : CoreCodes
    {
        [Test]
        [TestCase("Water Poppy","2")]
        //[TestCaseSource(nameof(productData))]

        public void SearchProductAndAddToCart(string searchtext, string pId)
        {
            //IWebElement modal = new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("(//div[@class='modal-inner-wrap'])[position()=2]")));

            BCHomePage bchp = new BCHomePage(driver);
            var searchespage = bchp?.TypeSearchText(searchtext);
            Thread.Sleep(3000);
            Assert.That(searchtext.Contains(searchespage?.GetProductSelect()));
            var productpage = searchespage?.ClickProduct(pId);
            Thread.Sleep(3000);
            string? check = productpage?.GetProductUrll();
            Assert.That(check.Contains("water-poppy"));
            productpage.ClickInQtyLink();
            productpage.ClickAddToCartBtn();
            Thread.Sleep(3000);
        }
        static object[] productData()
        {
            return new object[]
            {
                new object[]  { "5" }
            };
        }

    }
}
