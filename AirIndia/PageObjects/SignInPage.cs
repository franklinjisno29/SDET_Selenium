using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirIndia.PageObjects
{
    internal class SignInPage
    {
        IWebDriver driver;
        public SignInPage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver));//if the driver is null exception thrown
            PageFactory.InitElements(driver, this);//for optimizing the code we write this inside the constructor
        }

        [FindsBy(How = How.Id, Using = "createAccountButton")]
        private IWebElement? JoinNowButton { get; set; } 

        public UserDetailsPage ClickJoinNow()
        {
            JoinNowButton?.Click();
            return new UserDetailsPage(driver);

        }
    }
}
