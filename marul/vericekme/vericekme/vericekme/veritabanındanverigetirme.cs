using System;
using System.Collections.Generic;
using System.Text;
using vericekme.Context;
using vericekme.Models;
using System.Linq;
using System.Threading.Tasks;
namespace vericekme
{
    public class veritabanındanverigetirme
    {
        public  List<Category> GetAllCategory()
        {
            string[] CategoryName = new string[2000];
            int[] CategoryID = new int[2000];
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
                        sayac++;
                    }
                    return AllCategory;
                    }
                }catch (Exception){throw;}
        }
    }
}