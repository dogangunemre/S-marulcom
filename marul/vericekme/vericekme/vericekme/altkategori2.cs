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
   public class altkategori2
    {
        public void bb()
        {
            List<Category> GetAllCategory = new List<Category>();
            string[] CategoryName = new string[200];
            int[] CategoryID = new int[200];
            int sayac = 0;
            try
            {
                using (var contex = new ProductContext())
                {
                    List<Category> AllCategory = contex.Categories.ToList();
                    foreach (var item in AllCategory)
                    {
                        CategoryName[sayac] = AllCategory[sayac].Name;
                        CategoryID[sayac] = AllCategory[sayac].ID;
                        Console.WriteLine(AllCategory[sayac].Name);
                        Console.WriteLine(AllCategory[sayac].ID);      
                        sayac++;
                    }
                }
            }catch (Exception){}

            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.marul.com/tr/market/irmaklar-market-hadimkoy/317/7495/istanbul-avrupa/arnavutkoy---cilingir-mah");
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("Siteye Gidildi!");
            Thread.Sleep(2000);

            //kategorileri, Listeleme Start

            sayac = 0;

            string[] kategoria = new string[200];
            //IReadOnlyCollection<IWebElement> kategories = driver.FindElements(By.CssSelector(".with-subcategories"));
            List<IWebElement> kategoriler = driver.FindElements(By.XPath("/html/body/div[6]/div[1]/div[4]/div/div[1]/ul/li")).ToList();

            foreach (IWebElement kategori in kategoriler)
            {
                Regex r = new Regex(@"\/tr\/kategori\/(?<katCode>.*?)\/");

                IWebElement katLi = kategori;
                string katURL = katLi.FindElement(By.TagName("A")).GetAttribute("href");
                string katCode = null;
                if (r.Match(katURL).Success)
                {
                    katCode = r.Match(katURL).Groups["katCode"].Value;
                }
                string katName = katLi.FindElement(By.TagName("SPAN")).Text;
                List<IWebElement> subCats = katLi.FindElements(By.XPath("div/ul/li")).ToList();
                Console.WriteLine(sayac + "> " + katCode);
                Console.WriteLine(sayac + "> " + katURL);
                Console.WriteLine(sayac + "> " + katName);
                if (katCode == null)
                {
                    continue;
                }
                else
                {
                    kategoria[sayac] = katCode.ToString();
                    kategoria[sayac] = katURL.ToString();
                    kategoria[sayac] = katName.ToString();
                }

                foreach (IWebElement x in subCats)
                {
                    string subkatURL = x.FindElement(By.TagName("A")).GetAttribute("href");
                    string subkatCode = null;
                    if (r.Match(subkatURL).Success)
                    {
                        subkatCode = r.Match(subkatURL).Groups["katCode"].Value;
                    }
                    string subkatName = x.FindElement(By.TagName("A")).FindElement(By.TagName("SPAN")).GetAttribute("innerHTML");
                    List<IWebElement> subCats2 = x.FindElements(By.XPath("div/ul/li")).ToList();

                    Console.WriteLine(sayac + "-> " + subkatCode);
                    Console.WriteLine(sayac + "-> " + subkatURL);
                    Console.WriteLine(sayac + "-> " + subkatName);

                    List<Category> categories = new List<Category>();
                    Category category = new Category();
                    category.Name = subkatName.ToString();
                    category.State = true;
                    category.Description = subkatCode;
                    category.Address = subkatURL.ToString();
                    string CategoryName2 = CategoryName[sayac];
                    if (katName.ToString() == CategoryName2)
                    {
                        category.ParentCategoryID = CategoryID[sayac];
                    }
                    else
                    {
                        category.ParentCategoryID = 1;
                    }

                    using (var context = new ProductContext())
                    {
                        context.Categories.AddRange(category);

                        context.SaveChanges();
                    }
                }
                sayac++;
            }
            Console.ReadLine();
        }
    }
}
