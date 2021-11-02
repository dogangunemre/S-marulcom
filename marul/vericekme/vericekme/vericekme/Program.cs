using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Hosting;
using vericekme.Context;
using vericekme.Models;

namespace vericekme
{
    public class Program
    {
        public static void Main(string[] args)
        {
            kategori b = new kategori();
            altkategori b2 = new altkategori();
            Products productdeneme = new Products();
            veritabanındanverigetirme d = new veritabanındanverigetirme();
            altkategori2 b22 = new altkategori2();
            altkategori3 b33 = new altkategori3();
            urunlisteleme list = new urunlisteleme();
            Brands brands = new Brands();
            GetBrand getBrand = new GetBrand();
            pricedeneme pricedeneme2 = new pricedeneme();
            urundenkategoriıdbulma urundenkategoriıdbulma = new urundenkategoriıdbulma();
            GetUnit getUnit = new GetUnit();
            kategoridekiurunlerinadresiniverme kategoridekiurunlerinadresiniverme = new kategoridekiurunlerinadresiniverme();
            Urldekiurunlerilistele urldekiurunlerilistele = new Urldekiurunlerilistele();
            ProductData productData = new ProductData();
            productaddresslericekme productaddresslericekme2 = new productaddresslericekme();
            Barkodcekme barkodcekme = new Barkodcekme();
            resimindirme resimindirme = new resimindirme();
            productaddresslericekme2.productaddressleri();
                }

    }
}
    

