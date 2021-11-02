using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using vericekme.Context;
using vericekme.Models;

namespace vericekme
{
    public class urunlisteleme
    {

        Products products1 = new Products();
        public void urun()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.marul.com/tr/kategori/online-meyve-siparisi/irmaklar-market-hadimkoy/317/7495/istanbul-avrupa/arnavutkoy---cilingir-mah");
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("Siteye Gidildi!");
            Thread.Sleep(2000);

            string[] kategoria = new string[200];
            //IReadOnlyCollection<IWebElement> kategories = driver.FindElements(By.XPath("//*[@id='item-list-mode']"));
            List<IWebElement> products = driver.FindElements(By.XPath("//*[@id='item-list-mode']/div/div")).ToList();

            int counts = products.Count;
            foreach (IWebElement kategori in products)
            {
                IWebElement katLi = kategori;
                string katURL = katLi.FindElement(By.TagName("A")).GetAttribute("href");
                System.IO.File.WriteAllLines(@"C:\Users\DELL E6420\source\repos\vericekme\vericekme\product.txt", katURL.Split());
                Console.WriteLine(katURL);
            }
        }
    }
}
