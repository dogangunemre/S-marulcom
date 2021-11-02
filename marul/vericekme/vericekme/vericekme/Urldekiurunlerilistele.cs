using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using vericekme.Models;
using vericekme.Context;
using System.Threading.Tasks;

namespace vericekme
{
    public class Urldekiurunlerilistele
    {
        public void urunlist(string url)
        {
            var contex = new ProductContext();





            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("Siteye Gidildi!");
            Thread.Sleep(2000);

            List<IWebElement> product = driver.FindElements(By.XPath("//*[@id='item-list-mode']/div/div/div[1]")).ToList();
            foreach (IWebElement item in product)
            {

                IWebElement katLi = item;
                string katURL = katLi.FindElement(By.TagName("A")).GetAttribute("href");
                Console.WriteLine(katURL);
                ProductAddress productAddress = new ProductAddress();
                productAddress.Path = katURL;
                using (var context = new ProductContext())
                {
                    context.ProductAddresses.Add(productAddress);
                    context.SaveChanges();
                }

            }

            //IWebElement nextPageEl = driver.FindElement(By.ClassName("next-page"));
            try
            {
                if (driver.FindElement(By.CssSelector("div.pager > ul > li.next-page")) != null)
                {
                    IWebElement nextPageEl = driver.FindElement(By.CssSelector("div.pager > ul > li.next-page"));

                    string katURL = nextPageEl.FindElement(By.TagName("A")).GetAttribute("href");
                    Console.WriteLine(katURL.ToString());
                    driver.Close();
                    this.urunlist(katURL);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("******the end*****the end**********the end****the end******the end********");



                driver.Close();

            }

            Console.ReadLine();
        }

    }
}
