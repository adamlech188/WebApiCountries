using System;
using System.Collections.Generic;

namespace CountriesLibrary.Models
{
    public partial class TblCountries
    {
        public int Countryid { get; set; }
        public string Countryname { get; set; }
        public string Twocharcountrycode { get; set; }
        public string Threecharcountrycode { get; set; }
    }
}
