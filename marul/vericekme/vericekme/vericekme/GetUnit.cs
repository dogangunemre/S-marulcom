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
    public class GetUnit
    {
        public void bb()
        {



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

        }
    }



}
