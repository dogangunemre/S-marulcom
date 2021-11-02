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
    public class productaddresslericekme
    {
        public void productaddressleri()
        {
            
            List<ProductAddress> GetAllProductAddress = new List<ProductAddress>();
            string[] ProductAddressPath = new string[200000];
            int[] ProductAddressSource = new int[200000];
            int ProductAddresssayac = 0;
            try
            {
                using (var contex = new ProductContext())
                {
                    List<ProductAddress> AllBrand = contex.ProductAddresses.ToList();
                    foreach (var item in AllBrand)
                    {
                        ProductAddressPath[ProductAddresssayac] = AllBrand[ProductAddresssayac].Path;
                        ProductAddressSource[ProductAddresssayac] = AllBrand[ProductAddresssayac].Source;
                        Console.WriteLine(ProductAddressPath[ProductAddresssayac]);
                        Console.WriteLine(ProductAddressSource[ProductAddresssayac]);
                        if (AllBrand[ProductAddresssayac].State==true)
                        {
                            Products products2 = new Products();
                            products2.productTest(ProductAddressPath[ProductAddresssayac]);
                            AllBrand[ProductAddresssayac].State = false;
                            contex.SaveChanges();
                            Thread.Sleep(2000);
                        }
                       
                        ProductAddresssayac++;

                    }
                }
            }
            catch (Exception ex) { }
        }
    }
}
