using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using vericekme.Context;
using vericekme.Models;

namespace vericekme
{
    class kategori
    {
        public void aa()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.marul.com/tr/market/irmaklar-market-hadimkoy/317/7495/istanbul-avrupa/arnavutkoy---cilingir-mah");
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("Siteye Gidildi!");
            Thread.Sleep(5000);


            int sayac = 1;
            List<Category> categories = new List<Category>();
            string[] kategoria = new string[200];
            IReadOnlyCollection<IWebElement> kategories = driver.FindElements(By.XPath("/html/body/div[6]/div[1]/div[4]/div/div[1]/ul/li"));
            int counts = kategories.Count;
            foreach (IWebElement kategori in kategories)
            {
                Category category = new Category();
                category.Name = kategori.Text;
                category.State = true;
                
                using (var context = new ProductContext())
                {
                    context.Categories.AddRange(categories);

                    context.SaveChanges();
                }
            }





        }
    }
}
