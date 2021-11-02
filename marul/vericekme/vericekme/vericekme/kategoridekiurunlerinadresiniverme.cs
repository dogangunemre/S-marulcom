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
    public class kategoridekiurunlerinadresiniverme
    {
        public void urun()
        {
            Urldekiurunlerilistele urldekiurunlerilistele = new Urldekiurunlerilistele();

            string[] CategoryCode = new string[2000];
            string[] CategoryAddress = new string[2000];
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
                        CategoryAddress[sayac] = AllCategory[sayac].Address;
                        Console.WriteLine(CategoryAddress[sayac]);
                        sayac++;
                    }
                }
            }
            catch (Exception) { throw; }

            for (int i = 0; i < 13; i++)
            {
                urldekiurunlerilistele.urunlist(CategoryAddress[i]);

            }
            
        }
    }
}
