using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LicensesDataAccess;

namespace LicensesBusinessLayer
{
    public class clsPeople
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public string ImagePath { get; set; }


        private clsPeople(int ID,string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, byte Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            this.ID                   = ID; 
            this.NationalNo           = NationalNo;
            this.FirstName            = FirstName;
            this.SecondName           = SecondName;
            this.ThirdName            = ThirdName;
            this.LastName             = LastName;
            this.DateOfBirth          = DateOfBirth;
            this.Gendor               = Gendor;
            this.Address              = Address;
            this.Phone                = Phone;
            this.Email                = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath            = ImagePath;

            Mode = enMode.Update;
        }


        public clsPeople()
        {
            this.ID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Gendor = 0;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = -1;
            this.ImagePath = "";

            Mode = enMode.AddNew;
        }


        private bool _AddNewPerson()
        {
            this.ID = clsPeopleData.AddNewPerson(this.NationalNo, this.FirstName, this.SecondName,
                this.ThirdName,this.LastName, this.DateOfBirth,this.Gendor,this.Address,
                this.Phone,this.Email,this.NationalityCountryID,this.ImagePath);

            return (this.ID != -1);
        }


        private bool _UpdatePerson()
        {
            return clsPeopleData.UpdatePerson(this.ID,this.NationalNo, this.FirstName, this.SecondName,
                this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor, this.Address,
                this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
        }


        public static DataTable GetAllPeople()
        {
            return clsPeopleData.GetAllPeople();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if(_AddNewPerson())
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

                    return _UpdatePerson();

            }

            return false;
        }

        public static clsPeople Find(int ID)
        {
            byte Gendor = 0; int NationalityCountryID = -1;
            DateTime DateOfBirth = DateTime.Now;
            string NationalNo = "",  FirstName ="",  SecondName ="",ThirdName = "",
            LastName = "", Address = "", Phone = "", Email = "", ImagePath = "";

            if (clsPeopleData.GetPersonByID(ID, ref NationalNo, ref FirstName, ref SecondName,
           ref ThirdName, ref LastName, ref DateOfBirth,
          ref Gendor, ref Address, ref Phone, ref Email,
          ref NationalityCountryID, ref ImagePath))

                return new clsPeople(ID, NationalNo, FirstName, SecondName,
             ThirdName, LastName, DateOfBirth,
            Gendor, Address, Phone, Email,
            NationalityCountryID, ImagePath);

            else
                return null;

        }


        public static clsPeople FindByNationalNo(string NationalNo)
        {
            byte Gendor = 0; int NationalityCountryID = -1, ID = -1;
            DateTime DateOfBirth = DateTime.Now;
            string FirstName = "", SecondName = "", ThirdName = "",
            LastName = "", Address = "", Phone = "", Email = "", ImagePath = "";

            if (clsPeopleData.GetPersonByNationalNo(ref ID,  NationalNo, ref FirstName, ref SecondName,
           ref ThirdName, ref LastName, ref DateOfBirth,
          ref Gendor, ref Address, ref Phone, ref Email,
          ref NationalityCountryID, ref ImagePath))

                return new clsPeople(ID, NationalNo, FirstName, SecondName,
             ThirdName, LastName, DateOfBirth,
            Gendor, Address, Phone, Email,
            NationalityCountryID, ImagePath);

            else
                return null;

        }

        public static bool isExistPerson(int ID)
        {
            return clsPeopleData.IsPersonExist(ID);
        }

        public static bool DeletePerson(int ID)
        {
            return clsPeopleData.DeletePerson(ID);
        }
    }
}
