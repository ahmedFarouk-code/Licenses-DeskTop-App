using LicensesDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicensesBusinessLayer
{
    public class clsCountry
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ID { get; set; }
        public string CountryName { get; set; }
        


        private clsCountry(int ID, string CountryName)
        {
            this.ID = ID;
            this.CountryName = CountryName;
            

            Mode = enMode.Update;
        }


        public clsCountry()
        {
            this.ID = -1;
            this.CountryName = "";
            

            Mode = enMode.AddNew;
        }


        private bool _AddNewCountry()
        {
            this.ID = clsCountryData.AddNewCountry(this.CountryName);

            return (this.ID != -1);
        }


        private bool _UpdateCountry()
        {
            return clsCountryData.UpdateCountry(this.ID, this.CountryName);
        }


        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewCountry())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case enMode.Update:

                    return _UpdateCountry();

            }

            return false;
        }

        public static clsCountry Find(int ID)
        {

            string CountryName = ""; 

            if (clsCountryData.GetCountryByID(ID, ref CountryName))

                return new clsCountry(ID, CountryName);

            else
                return null;

        }

        public static clsCountry Find(string CountryName)
        {

            int ID = -1;

            if (clsCountryData.GetCountryByName(ref ID,  CountryName))

                return new clsCountry(ID, CountryName);

            else
                return null;

        }

        public static bool isExistCountry(int ID)
        {
            return clsCountryData.IsCountryExist(ID);
        }

        public static bool DeleteCountry(int ID)
        {
            return clsCountryData.DeleteCountry(ID);
        }
    }
}
