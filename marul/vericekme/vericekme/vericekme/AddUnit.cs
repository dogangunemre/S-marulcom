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
    public class AddUnit
    {
        public void addUnit2(Unit unit)
        {
            using (var context = new ProductContext())
            {
                context.Unit.Add(unit);
                context.SaveChanges();
            }
        }
    }
}
