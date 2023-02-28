using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V108.DOM;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium;

namespace AmazonTests.StepDefinitions
{
    [Binding]
    public sealed class StepDefinitions
    {
        private IWebDriver driver;
        public StepDefinitions(IWebDriver driver)
        {
            this.driver = driver;
        }

        [Given(@"Open Browser")]
        public void GivenOpenTheBrowser()
        {
        }

        [When(@"Search a refrigerator & Add to the cart a mabe refrigerator without protection")]
        public void SearchRefrigerator()
        {
            driver.Url = "https://www.amazon.com.mx/";
        }

        [Then(@"Verify if the refrigerator is added to the cart correctly")]
        public void AddToCartMabeRefrigerator()
        {
            RefrigeratorSearch();
            driver.FindElement(By.LinkText("Mabe")).Click();
            driver.FindElement(By.XPath("//div//*[text()='Frigobar 4 pies³ Mabe Blanco RMF032PYMXB Dos puertas']")).Click();
            int priceOfMabeRefri = ClickAddCart();
            Assert.IsTrue(priceOfMabeRefri < 10000);
        }

        [Then(@"Verify if the Samsung refrigerator is added to the cart correctly")]
        public void AddToCartSamsungRefrigerator()
        {
            RefrigeratorSearch();
            driver.FindElement(By.LinkText("SAMSUNG")).Click();
            driver.FindElement(By.XPath("//div//*[text()='Refrigerador Samsung Top Mount 11 cu.ft con Digital Inverter']")).Click();
            int priceOfSamsungRefri = ClickAddCart();
            Assert.IsTrue(priceOfSamsungRefri > 10000);
        }


        /// <summary>
        /// search the word "refrigerator " on the search box
        /// </summary>
        public void RefrigeratorSearch()
        {
            driver.FindElement(By.Id("twotabsearchtextbox")).SendKeys("Refrigerador");
            driver.FindElement(By.Id("nav-search-submit-button")).Click();
        }

        /// <summary>
        /// Clicks at the "add cart" button and then click on the "No Thanks" button 
        /// </summary>
        public int ClickAddCart()
        {
            driver.FindElement(By.CssSelector("input[id='add-to-cart-button']")).Click();
            Assert.IsTrue(ElementIsPresent(driver, By.Id("attach-desktop-sideSheet")));
            driver.FindElement(By.XPath("//span[@id='attachSiNoCoverage']//input")).Click();
            ElementIsPresent(driver, By.CssSelector("nav-cart-text-container"));
            driver.FindElement(By.Id("nav-cart-text-container")).Click();

            string amountOfArticleString = driver.FindElement(By.XPath("//*[@id='sc-subtotal-amount-activecart']//span")).Text;

            string finalAmount = amountOfArticleString.Replace("$", string.Empty).Replace(",", string.Empty).Replace(".00", string.Empty);

            int amountOfArticle = Int32.Parse(finalAmount.ToString());
            return amountOfArticle;
        }

        /// <summary>
        /// Verify if the element is visible on the page with a timeout for 10 seconds
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        /// <returns></returns>
        public static bool ElementIsPresent(IWebDriver driver, By locator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                wait.Until(drv => drv.FindElement(locator));
                return true;
            }

            catch { }
            return false;
        }

    }
}

