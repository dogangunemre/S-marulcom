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
    public class Products
    {
        public void productTest(string yazi)
        {
            var contex = new ProductContext();

            Product productModel = new Product();
            IWebDriver driver = new ChromeDriver();



            driver.Navigate().GoToUrl(yazi);

            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("Siteye Gidildi!");
            Thread.Sleep(2000);

            if ("https://www.marul.com/tr/" == driver.Url)
            {
                Console.WriteLine("urun silinmiş");
                driver.Close();
            }
          
            else
            {

                //detayları çekme
                IReadOnlyCollection<IWebElement> productname = driver.FindElements(By.XPath("//*[@id='product-details-form']/div/div/div[2]/div[1]/div[1]/h1"));
                List<IWebElement> productprice = driver.FindElements(By.ClassName("product-price")).ToList();
                List<IWebElement> productUnit = driver.FindElements(By.ClassName("unit-type")).ToList();
                IReadOnlyCollection<IWebElement> productbarcodcode = driver.FindElements(By.XPath("//*[@id='product-details-form']/div/div/div[2]/div[1]/div[2]/div/div/span[2]"));
                IReadOnlyCollection<IWebElement> productdescription = driver.FindElements(By.XPath("//*[@id='product-details-form']/div/div/div[1]/div"));
                List<IWebElement> productimg = driver.FindElements(By.XPath("//*[@id='product-details-form']/div/div[1]/div[1]/div")).ToList();
                IReadOnlyCollection<IWebElement> productBrand = driver.FindElements(By.ClassName("manufacturers"));
                IReadOnlyCollection<IWebElement> productcat = driver.FindElements(By.XPath("/html/body/div[6]/div[4]/div/div[1]/div/div[1]/ul/li[3]/span[2]/a/span"));

                Regex r = new Regex(@"https(.*?)thumbs\/[0-9]+\/(?<katCode>\d*)_(.*?)\.[\w]+");
                Regex r4 = new Regex(@"^sku-(?<katCode>.*?)$");
                //detayları yazdirma
                if (productbarcodcode != null)
                {
                    foreach (IWebElement itemsss in productbarcodcode)
                    {

                        string SKU = itemsss.GetAttribute("ID");
                        string katURL = " ";
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

                if (driver.FindElements(By.XPath("//*[@id='product-details-form']/div/div/div[2]/div[1]/div[1]/h1")) != null)
                {
                    foreach (IWebElement items in productname)
                    {
                        Console.WriteLine("ProductName:" + items.Text);
                        productModel.Name = items.Text;
                    }
                }
                else
                {
                    productModel.Name = null;
                }

                if (driver.FindElements(By.XPath("//*[@id='product-details-form']/div/div/div[1]/div")) != null)
                {
                    foreach (IWebElement item2 in productdescription)
                    {
                        Console.WriteLine(item2.Text);
                        productModel.Description = item2.Text.ToString();
                    }
                }
                else
                {
                    productModel.Description = null;
                }

                productModel.State = true;

                if (driver.FindElements(By.XPath("//*[@id='product-details-form']/div/div[1]/div[1]/div")) != null)
                {
                    foreach (IWebElement kategori in productimg)
                    {
                        IWebElement fLi = kategori;
                        string katURL = fLi.FindElement(By.TagName("IMG")).GetAttribute("SRC");
                        string proCode = null;
                        if (r.Match(katURL).Success)
                        {
                            proCode = r.Match(katURL).Groups["katCode"].Value;
                            productModel.Code = proCode.ToString();
                        }
                        Console.WriteLine("productCode:   " + proCode);

                    }
                }
                else
                {
                    productModel.Code = null;
                }

                productModel.Address = yazi;

                if (driver.FindElements(By.ClassName("product-price")) != null)
                {
                    foreach (IWebElement itemss in productprice)
                    {
                        Console.WriteLine("ProductPrice:" + itemss.Text);
                        string PriceName = itemss.FindElement(By.TagName("SPAN")).GetAttribute("CONTENT").ToString();
                        decimal aa = Convert.ToDecimal(PriceName);
                        aa = aa / 100;
                        productModel.Price = aa;
                    }
                }
                else
                {
                    productModel.Price = 0;
                }

                if (driver.FindElements(By.ClassName("unit-type")) != null)
                {
                    foreach (IWebElement itemss in productUnit)
                    {
                        Unit u = contex.Unit.FirstOrDefault(o => o.Name == itemss.Text);
                        if (u == null)
                        {
                            u = new Unit();
                            u.Name = itemss.Text;
                            contex.Unit.Add(u);
                            contex.SaveChanges();
                        }
                        productModel.UnitID = u.ID;
                    }
                }
                else
                {
                    productModel.UnitID = 0;
                }


                productModel.BrandID = 4401;
                if (productBrand != null)
                {
                    foreach (IWebElement item in productBrand)
                    {

                        IWebElement brandcode2 = driver.FindElement(By.XPath("//*[@id='product-details-form']/div/div/div[2]/div[2]/span[2]"));
                        Regex r3 = new Regex(@"\/tr\/marka\/(?<katCode>.*?)\/");

                        string brandcode2URL3 = brandcode2.FindElement(By.TagName("A")).GetAttribute("href");
                        string brandcode2URLName = brandcode2.FindElement(By.TagName("A")).Text;
                        string brandcode2Code3 = null;
                        if (r3.Match(brandcode2URL3).Success)
                        {
                            brandcode2Code3 = r3.Match(brandcode2URL3).Groups["katCode"].Value;
                        }

                        Brand b = contex.Brands.FirstOrDefault(o => o.Description == brandcode2Code3);
                        if (b == null)
                        {
                            b = new Brand();
                            b.Name = brandcode2URLName;
                            b.Address = brandcode2URL3;
                            b.Description = brandcode2Code3;
                            b.State = true;
                            contex.Brands.Add(b);
                            contex.SaveChanges();
                        }
                        productModel.BrandID = b.ID;
                    }
                }



                //--------CategoryID yazdirma---------

                IWebElement produccat1 = null;
                IWebElement produccat1code = null;

                IWebElement MenuElement = driver.FindElement(By.XPath("/html/body/div[6]/div[4]/div/div[1]/div/div[1]/ul"));
                IWebElement currentMenu = MenuElement.FindElement(By.CssSelector("li:last-of-type"));

                if (currentMenu != null)
                {
                    produccat1 = currentMenu.FindElement(By.XPath("span[2]/a/span"));
                    produccat1code = currentMenu.FindElement(By.XPath("span[2]"));
                }

                Regex r2 = new Regex(@"\/tr\/kategori\/(?<katCode>.*?)\/");



                string katURL1 = produccat1code.FindElement(By.TagName("A")).GetAttribute("href");
                string katName = produccat1code.FindElement(By.TagName("A")).GetAttribute("innerHtml");
                string katCode1 = null;
                if (r2.Match(katURL1).Success)
                {
                    katCode1 = r2.Match(katURL1).Groups["katCode"].Value;
                }



                if (produccat1 != null)
                {


                    Category c = contex.Categories.FirstOrDefault(o => o.Description == katCode1);
                    if (c == null)
                    {
                        c = new Category();
                        c.Name = katName;
                        c.Address = katURL1;
                        c.Description = katCode1;
                        c.State = true;
                        contex.Categories.Add(c);
                        contex.SaveChanges();
                    }
                    productModel.CategoryID = c.ID;
                }
                //-----------------------------
                Product d = contex.Products.FirstOrDefault(o => o.Barcode == productModel.Barcode);

                if (d == null)
                {
                    contex.Products.Add(productModel);
                    contex.SaveChanges();
                    foreach (IWebElement kategori in productimg)
                    {
                        IWebElement fLi = kategori;
                        string katURL = fLi.FindElement(By.TagName("IMG")).GetAttribute("SRC");
                        string katCode = null;
                        if (r.Match(katURL).Success)
                        {
                            katCode = r.Match(katURL).Groups["katCode"].Value;
                            productModel.Code = katCode.ToString();

                        }
                        Console.WriteLine("productCode:   " + katCode);
                        Console.WriteLine("productUrlIMG:    " + katURL);

                        Models.File file = new Models.File();
                        file.Path = katURL.ToString();
                        file.State = true;
                        file.ProductID = productModel.ID;
                        if ("https://www.marul.com/Content/Images/no-img.jpg" != katURL)
                        {
                            string url = katURL.ToString();
                            string uzanti = Path.GetExtension(url);
                            try
                            {
                            System.Net.WebClient wc = new System.Net.WebClient();
                            wc.DownloadFile(url, String.Concat(@"C:\Users\DELL E6420\source\repos\vericekme\vericekme\Images\", katCode, ".jpg"));
                            }
                            catch (Exception)
                            {

                                Console.WriteLine("resim indirlemedi");
                            }
                            
                            string relativePath = katCode + ".jpg";
                            Console.WriteLine(relativePath);
                            file.RelativePath = relativePath;
                            AddFile addFile = new AddFile();
                            addFile.addFile2(file);
                        }
                        else
                        {
                            Console.WriteLine("resim yok");
                        }

                    }
                }

                //file ekleme

                Console.WriteLine("successful added");
                driver.Close();

            }
        }
    }
}
