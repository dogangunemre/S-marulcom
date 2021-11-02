using System;
using System.Collections.Generic;
using System.Text;
using vericekme.Context;
using vericekme.Models;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace vericekme
{
    public class urundenkategoriıdbulma
    {
        public void CategoryIDBulma()
        {
            string[] CategoryCode = new string[2000];
            int[] CategoryID = new int[2000];
            int sayac = 0;
            try
            {
                using (var contex = new ProductContext())
                {
                    List<Category> AllCategory = contex.Categories.ToList();
                    foreach (var item in AllCategory)
                    {
                        CategoryID[sayac] = AllCategory[sayac].ID;
                        CategoryCode[sayac] = AllCategory[sayac].Description;

                        sayac++;
                    }
                }
            }
            catch (Exception) { throw; }

            Product productModel = new Product();
            IWebDriver driver = new ChromeDriver();

            string yazi = System.IO.File.ReadAllText(@"C:\Users\DELL E6420\source\repos\vericekme\vericekme\url.txt");

            driver.Navigate().GoToUrl(yazi);

            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("Siteye Gidildi!");
            Thread.Sleep(2000);

            //detayları çekme
            IWebElement produccat3 = driver.FindElement(By.XPath("/html/body/div[6]/div[4]/div/div[1]/div/div[1]/ul/li[4]/span[2]/a/span"));
            IWebElement produccat2 = driver.FindElement(By.XPath("/html/body/div[6]/div[4]/div/div[1]/div/div[1]/ul/li[3]/span[2]/a/span"));
            IWebElement produccat1 = driver.FindElement(By.XPath("/html/body/div[6]/div[4]/div/div[1]/div/div[1]/ul/li[2]/span[2]/a/span"));
            
            IWebElement produccat3code = driver.FindElement(By.XPath("/html/body/div[6]/div[4]/div/div[1]/div/div[1]/ul/li[4]/span[2]"));
            IWebElement produccat2code = driver.FindElement(By.XPath("/html/body/div[6]/div[4]/div/div[1]/div/div[1]/ul/li[3]/span[2]"));
            IWebElement produccat1code = driver.FindElement(By.XPath("/html/body/div[6]/div[4]/div/div[1]/div/div[1]/ul/li[2]/span[2]"));


            Regex r2 = new Regex(@"\/tr\/kategori\/(?<katCode>.*?)\/");

            string katURL3 = produccat3code.FindElement(By.TagName("A")).GetAttribute("href");
            string katCode3 = null;
            if (r2.Match(katURL3).Success)
            {
                katCode3 = r2.Match(katURL3).Groups["katCode"].Value;
            }

            string katURL2 = produccat2code.FindElement(By.TagName("A")).GetAttribute("href");
            string katCode2 = null;
            if (r2.Match(katURL2).Success)
            {
                katCode2 = r2.Match(katURL2).Groups["katCode"].Value;
            }

            string katURL1 = produccat1code.FindElement(By.TagName("A")).GetAttribute("href");
            string katCode1 = null;
            if (r2.Match(katURL1).Success)
            {
                katCode1 = r2.Match(katURL1).Groups["katCode"].Value;
            }

            
            
            if (produccat1 != null)
            {

                if (produccat2 != null)
                {
                    if (produccat3 != null)
                    {
                        Console.WriteLine(produccat3.Text);
                        string katname = produccat3.Text;
                        for (int i = 0; i < sayac; i++)
                        {
                            if (katCode3 == CategoryCode[i])
                            {
                                //productModel.CategoryID = CategoryID[i];
                                Console.WriteLine(CategoryID[i].ToString());
                            }

                        }
                    }
                    else { 
                    Console.WriteLine(produccat2.Text);
                    string katname = produccat2.Text;
                    for (int i = 0; i < sayac; i++)
                    {
                        if (katCode2 == CategoryCode[i])
                        {
                            //productModel.CategoryID = CategoryID[i];
                            Console.WriteLine(CategoryID[i].ToString());
                        }

                    }
                    }
                }

                else { 
                Console.WriteLine(produccat1.Text);
                string katname = produccat1.Text;
                for (int i = 0; i < sayac; i++)
                {
                    if (katCode1 == CategoryCode[i])
                    {
                        //productModel.CategoryID = CategoryID[i];
                        Console.WriteLine(CategoryID[i].ToString());
                    }

                }
                }

            }
        }
    }
}
