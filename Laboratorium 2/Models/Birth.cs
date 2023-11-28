using System.Globalization;
using System;
using System.Data;

namespace Laboratorium_2.Models
{
    public class Birth
    {

        public string x { get; set; }
        public DateTime y { get; set; }





        public bool IsValid()
        {
            return x != null && y < DateTime.Now;
        }

        public int age
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - y.Year;

                if (y.Date > today.AddYears(-age))
                {
                    age--;
                }
                return age;
            }

        }
    }
}