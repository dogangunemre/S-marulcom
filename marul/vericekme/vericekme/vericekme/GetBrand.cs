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
    public class GetBrand
    {
        public void getBrand()
        {
            List<Brand> GetAllBrand = new List<Brand>();
            string[] BrandName = new string[2000];
            string[] BrandDescription = new string[2000];
            int[] BrandID = new int[2000];
            int sayac = 0;
            try
            {
                using (var contex = new ProductContext())
                {
                    List<Brand> AllBrand = contex.Brands.ToList();
                    foreach (var item in AllBrand)
                    {
                        BrandName[sayac] = AllBrand[sayac].Name;
                        BrandID[sayac] = AllBrand[sayac].ID;
                        BrandDescription[sayac] = AllBrand[sayac].Description;
                        Console.WriteLine(AllBrand[sayac].Name);
                        Console.WriteLine(AllBrand[sayac].ID);
                        Console.WriteLine(AllBrand[sayac].Description);

                        sayac++;
                    }
                }
            }
            catch (Exception) { }

        }
    }
}
