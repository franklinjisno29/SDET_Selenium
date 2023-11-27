using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunnyCart.PageObjects
{
    internal class ProductPage
    {
        IWebDriver driver;
        public ProductPage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver)); ;
            PageFactory.InitElements(driver, this);
        }

        //Arrange

        [FindsBy(How = How.ClassName, Using = "qty-inc")]
        private IWebElement? InQtyLink { get; set; }

        [FindsBy(How = How.Id, Using = "product-addtocart-button")]
        private IWebElement? AddToCartBtn { get; set; }

        //Act
        public string? GetProductUrll()
        {
            string url = driver.Url;
            return url;
        }
        public void ClickInQtyLink()
        {
            InQtyLink?.Click();
        }
        public void ClickAddToCartBtn()
        {
            AddToCartBtn?.Click();
        }
        
    }
}
