using CountriesLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CountryLibraryTest
{
    [TestClass]
    public class CountryDBTest
    {
        [TestMethod]
        public void TestDbCoonection()
        {
            using (var dbContext = new CountriesDBContext()) {
                var countries = dbContext.TblCountries;
                Assert.IsTrue(countries.Count() > 100);
            }
        }
        [TestMethod]
        public void Test_Add_Country() {
            using (var dbContext = new CountriesDBContext())
            {
              var countryName =    dbContext.TblCountries.FromSqlRaw("SELECT * FROM public.tbl_countries").Select( r => r.Countryname).FirstOrDefault();
              System.Diagnostics.Debug.WriteLine(countryName);
           }

        }

        [TestMethod]
        public void Test_Search_Country() {
            using (var dbContext = new CountriesDBContext())
            {
               var countries = dbContext.TblCountries.Where(c => c.Countryname.StartsWith("P")).Select( c => c.Countryname);
               var countrList =  countries.ToList();
               Assert.IsTrue(countrList.Contains("Poland"));
           }
        }
    }
}
