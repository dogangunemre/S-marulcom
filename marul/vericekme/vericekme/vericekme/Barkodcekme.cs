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
    public class Barkodcekme
        {
        public void productTest()
        {
            var contex = new ProductContext();

            Product productModel = new Product();
            IWebDriver driver = new ChromeDriver();

            string yazi = "https://www.marul.com/tr/urun/portakal-finike/irmaklar-market-hadimkoy/317/7495/istanbul-avrupa/arnavutkoy---cilingir-mah";

            driver.Navigate().GoToUrl(yazi);

            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("Siteye Gidildi!");
            Thread.Sleep(2000);

            //detayları çekme
            IReadOnlyCollection<IWebElement> productbarcodcode = driver.FindElements(By.XPath("//*[@id='product-details-form']/div/div/div[2]/div[1]/div[2]/div/div/span[2]"));
            Regex r4 = new Regex(@"^sku-(?<katCode>.*?)$");

            //detayları yazdirma
            if (productbarcodcode != null)
            {
                foreach (IWebElement itemsss in productbarcodcode)
                {

                    string SKU = itemsss.GetAttribute("ID");
                    string katURL =" ";
                    if (r4.Match(SKU).Success)
                    {
                        katURL = r4.Match(SKU).Groups["katCode"].Value;
                    }
                  
                    productModel.Barcode = itemsss.Text.ToString();
                    productModel.SKU = katURL;
                    Console.WriteLine(productModel.Barcode);
                    Console.WriteLine(productModel.SKU);
                }
            }
            else
            {
                productModel.Barcode = null;
                productModel.SKU = null;
            }
           
            Console.WriteLine("successful added");
            driver.Close();
        }
    }
}
