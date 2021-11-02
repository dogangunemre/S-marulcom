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
    public class Brands
    {
        public void bb(string url)
        {


            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("Siteye Gidildi!");
            Thread.Sleep(2000);



            List<IWebElement> Brandlist = driver.FindElements(By.XPath("//*[@id='manufacturerList']/div/div/div/div")).ToList();
            foreach (IWebElement brandlist in Brandlist)
            {
                Regex r = new Regex(@"\w+\/tr\/marka-detay\/(?<katCode>.*?)$");

                IWebElement katLi = brandlist;
                string katURL = katLi.Text;
                string katCode = null;
                string brandurl = katLi.FindElement(By.TagName("A")).GetAttribute("href");

                if (r.Match(brandurl).Success)
                {
                    katCode = r.Match(brandurl).Groups["katCode"].Value;
                }
                Console.WriteLine(katCode.ToString());
                Console.WriteLine(katURL.ToString());
                Brand brand = new Brand();
                brand.Name = katURL.ToString();
                brand.Address = brandurl.ToString();
                brand.State = true;
                brand.Description = katCode.ToString();

                using (var context = new ProductContext())
                {
                    context.Brands.AddRange(brand);
                    context.SaveChanges();
                }



            }

            IWebElement nextPageEl = driver.FindElement(By.XPath("/html/body/div[6]/div[4]/div/div/div/div[3]/div[3]/ul/li[6]"));

            if (nextPageEl != null)
            {
                List<IWebElement> nextPageEl2 = driver.FindElements(By.XPath("/html/body/div[6]/div[4]/div/div/div/div[3]/div[3]/ul/li[6]")).ToList();

                foreach (IWebElement kategori in nextPageEl2)
                {
                    IWebElement katLi = kategori;
                    string katURL = katLi.FindElement(By.TagName("A")).GetAttribute("href");
                    Console.WriteLine(katURL.ToString());
                    driver.Close();

                    this.bb(katURL);
                }

            }

        }
    }



}
