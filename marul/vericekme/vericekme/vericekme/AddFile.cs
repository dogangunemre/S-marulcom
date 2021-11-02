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
    public class AddFile
    {
        public void addFile2(Models.File file)
        {
            using (var context = new ProductContext())
            {
                context.Files.Add(file);
                context.SaveChanges();
            }
        }
    }
}
