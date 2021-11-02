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
    class pricedeneme
    {
        public void pricedenemeTest()
        {


            

            Product productModel = new Product();
            IWebDriver driver = new ChromeDriver();

            string yazi = "https://www.marul.com/tr/urun/basaran-et-pilic-but-kusbasi/basaran-et-kasap/317/5843/istanbul-avrupa/fatih-cankurtaran-mah";

            driver.Navigate().GoToUrl(yazi);

            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("Siteye Gidildi!");
            Thread.Sleep(2000);

            //detayları çekme
            List<IWebElement> productUnit = driver.FindElements(By.XPath("/html/body/div[6]/div[4]/div/div/div/div[1]/h1")).ToList();


            foreach (IWebElement itemss in productUnit)
            {
                Console.WriteLine("ProductPrice:" + itemss.Text);

            }

            
            }
        }
    }

