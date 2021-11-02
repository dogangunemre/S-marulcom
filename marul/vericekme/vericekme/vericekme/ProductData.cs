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
    public class ProductData
    {
         public void productTest()
            {

            string yazi= "https://www.marul.com/tr/urun/dana-sote-kg/irmaklar-market-hadimkoy/317/7495/istanbul-avrupa/arnavutkoy---cilingir-mah";
            //-----------------Brandleri çekme------------


            List<Brand> GetAllBrand = new List<Brand>();
            string[] BrandName = new string[20000];
            string[] BrandDescription = new string[20000];
            int[] BrandID = new int[20000];
            int brandsayac = 0;
            try
            {
                using (var contex = new ProductContext())
                {
                    List<Brand> AllBrand = contex.Brands.ToList();
                    foreach (var item in AllBrand)
                    {
                        BrandName[brandsayac] = AllBrand[brandsayac].Name;
                        BrandID[brandsayac] = AllBrand[brandsayac].ID;
                        BrandDescription[brandsayac] = AllBrand[brandsayac].Description;

                        brandsayac++;
                    }
                }
            }
            catch (Exception) { }

            //-----------------------------
            //------------KategoriÇekme-----------------

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

            //-----------------------------
            //------------GetUnit----------------

            List<Unit> GetAllUnit = new List<Unit>();
            string[] UnitName = new string[20000];
            string[] UnitDescription = new string[20000];
            int[] UnitID = new int[20000];
            int Unitsayac = 0;
            try
            {
                using (var contex = new ProductContext())
                {
                    List<Unit> AllUnit = contex.Unit.ToList();
                    foreach (var item in AllUnit)
                    {
                        UnitName[Unitsayac] = AllUnit[Unitsayac].Name;
                        UnitID[Unitsayac] = AllUnit[Unitsayac].ID;
                        UnitDescription[Unitsayac] = AllUnit[Unitsayac].Description;
                        Console.WriteLine(UnitName[Unitsayac]);
                        Unitsayac++;
                    }
                }
            }
            catch (Exception) { }
            //-----------------------------

            Product productModel = new Product();
            IWebDriver driver = new ChromeDriver();



            driver.Navigate().GoToUrl(yazi);

            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("Siteye Gidildi!");
            Thread.Sleep(2000);

            //detayları çekme
            IReadOnlyCollection<IWebElement> productname = driver.FindElements(By.XPath("//*[@id='product-details-form']/div/div/div[2]/div[1]/div[1]/h1"));
            List<IWebElement> productprice = driver.FindElements(By.ClassName("product-price")).ToList();
            List<IWebElement> productUnit = driver.FindElements(By.ClassName("unit-type")).ToList();
            IReadOnlyCollection<IWebElement> productbarcodcode = driver.FindElements(By.XPath("//*[@id='product-details-form']/div/div/div[2]/div[1]/div[2]/div/div"));
            IReadOnlyCollection<IWebElement> productdescription = driver.FindElements(By.XPath("//*[@id='product-details-form']/div/div/div[1]/div"));
            List<IWebElement> productimg = driver.FindElements(By.XPath("//*[@id='product-details-form']/div/div[1]/div[1]/div")).ToList();
            IReadOnlyCollection<IWebElement> productBrand = driver.FindElements(By.ClassName("manufacturers"));
            IReadOnlyCollection<IWebElement> productcat = driver.FindElements(By.XPath("/html/body/div[6]/div[4]/div/div[1]/div/div[1]/ul/li[3]/span[2]/a/span"));

            Regex r = new Regex(@"https(.*?)thumbs\/[0-9]+\/(?<katCode>\d*)_(.*?)\.[\w]+");

            //detayları yazdirma

            if (driver.FindElements(By.XPath("//*[@id='product-details-form']/div/div/div[2]/div[1]/div[2]/div/div")) != null)
            {
                foreach (IWebElement itemsss in productbarcodcode)
                {
                    Console.WriteLine("Product" + itemsss.Text);
                    productModel.Barcode = itemsss.Text.ToString();
                }
            }
            else
            {
                productModel.Barcode = null;
            }
            productModel.SKU = null;

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
                    }
                    Console.WriteLine("productCode:   " + proCode);
                    productModel.Code = proCode.ToString();
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
                    string uname = itemss.Text;
                    int deger = 0;
                    for (int i = 0; i < Unitsayac; i++)
                    {
                        if (uname.ToString() == UnitName[i])
                        {
                            productModel.UnitID = UnitID[i];
                            deger = 1;
                        }
                    }
                    if (deger == 0)
                    {
                        Unit unit = new Unit();
                        unit.Name = uname.ToString();
                        AddUnit addUnit = new AddUnit();
                        addUnit.addUnit2(unit);
                        productModel.UnitID = unit.ID;
                    }

                }
            }
            else
            {
                productModel.UnitID = 0;
            }

            productModel.SKU = null;


            if (productBrand != null)
            {
                if (productBrand.Count == 0)
                {
                    productModel.BrandID = 4401;
                }
                else
                {


                    foreach (IWebElement item in productBrand)
                    {

                        IWebElement brandcode2 = item.FindElement(By.TagName("SPAN[2]"));
                        Regex r3 = new Regex(@"\/tr\/marka\/(?<katCode>.*?)\/");

                        string brandcode2URL3 = brandcode2.FindElement(By.TagName("A")).GetAttribute("href");
                        string brandcode2Code3 = null;
                        if (r3.Match(brandcode2URL3).Success)
                        {
                            brandcode2Code3 = r3.Match(brandcode2URL3).Groups["katCode"].Value;
                        }

                        foreach (IWebElement item1 in productBrand)
                        {
                            for (int i = 0; i < brandsayac; i++)
                            {
                                if (brandcode2Code3 == BrandDescription[i])
                                {
                                    productModel.BrandID = BrandID[i];
                                }
                                else
                                    continue;
                            }
                        }
                    }
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
            string katCode1 = null;
            if (r2.Match(katURL1).Success)
            {
                katCode1 = r2.Match(katURL1).Groups["katCode"].Value;
            }



            if (produccat1 != null)
            {
                    Console.WriteLine(produccat1.Text);
                    string katname = produccat1.Text;
                    for (int i = 0; i < sayac; i++)
                    {
                        if (katCode1 == CategoryCode[i])
                        {
                            productModel.CategoryID = CategoryID[i];
                        }
                    }
            }
            //-----------------------------

            using (var context = new ProductContext())
            {
                context.Products.Add(productModel);
                context.SaveChanges();
            }

            //file ekleme
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

                AddFile addFile = new AddFile();
                addFile.addFile2(file);
            }
            Console.ReadLine();
        }
        }
    }