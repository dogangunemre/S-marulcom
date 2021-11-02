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
using System.Net;
using System.Drawing.Imaging;


namespace vericekme
{
    public class resimindirme
    {
        public void productTest()
        {
            var contex = new ProductContext();

            Product productModel = new Product();
            IWebDriver driver = new ChromeDriver();

            string yazi = "https://www.marul.com/tr/urun/zek-tokat-yapragi-750-gr-2/irmaklar-market-hadimkoy/317/7495/istanbul-avrupa/arnavutkoy---cilingir-mah";

            driver.Navigate().GoToUrl(yazi);

            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("Siteye Gidildi!");
            Thread.Sleep(2000);
            List<IWebElement> productimg = driver.FindElements(By.XPath("//*[@id='product-details-form']/div/div[1]/div[1]/div")).ToList();
           
            Regex r = new Regex(@"https(.*?)thumbs\/[0-9]+\/(?<katCode>\d*)_(.*?)\.[\w]+");
            
                
                foreach (IWebElement kategori in productimg)
                {
                    IWebElement fLi = kategori;
                    string katURL = fLi.FindElement(By.TagName("IMG")).GetAttribute("SRC");
                    string katCode = null;
                    if (r.Match(katURL).Success)
                    {
                        katCode = r.Match(katURL).Groups["katCode"].Value;
                    }
                    Console.WriteLine("productCode:   " + katCode);
                    productModel.Code = katCode.ToString();
                    Console.WriteLine("productUrlIMG:    " + katURL);

                    Models.File file = new Models.File();
                    file.Path = katURL.ToString();
                    file.State = true;
                    file.ProductID = productModel.ID;
                    
                string url = katURL.ToString();
                string uzanti = Path.GetExtension(url);
                System.Net.WebClient wc = new System.Net.WebClient();
                wc.DownloadFile(url, String.Concat(@"C:\Users\DELL E6420\source\repos\vericekme\vericekme\Images\", katCode, ".jpg"));
                string relativePath = katCode + ".jpg";
                Console.WriteLine(relativePath);
                file.RelativePath = relativePath;
                AddFile addFile = new AddFile();
                    addFile.addFile2(file);
                }
            }
                   
          
        }     
   

    }


