using LicensesDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicensesBusinessLayer
{
    public class clsUsers
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int UserID {  get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        private clsUsers(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
            Mode = enMode.Update;
        }

        public clsUsers()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = false;
            Mode = enMode.AddNew;
        }

        private  bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName,
              this.Password, this.IsActive);
            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID, this.PersonID, this.UserName,
              this.Password, this.IsActive);
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew :
                {
                   if(_AddNewUser())
                   {
                        Mode = enMode.Update;
                        return true;
                   }
                   else
                   {
                        return false;
                   }
                }
                    case enMode.Update :
                    return _UpdateUser();
            }
            return false;
        }

        public static clsUsers Find(int PersonID)
        {
            int UserID = -1; string UserName = "", Password = "";
            bool IsActive = false;

            if (clsUserData.GetUserByID(ref UserID,  PersonID, ref UserName, ref Password, ref IsActive))
                return new clsUsers(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;
        }

        public static clsUsers FindByUserID(int UserID)
        {
            int PersonID = -1; string UserName = "", Password = "";
            bool IsActive = false;

            if (clsUserData.GetUserByUserID( UserID, ref PersonID, ref UserName, ref Password, ref IsActive))
                return new clsUsers(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;
        }

        public static clsUsers Find(string UserName ,string Password)
        {
            int PersonID = -1 , UserID = -1;
            bool IsActive = false;

            if (clsUserData.GetUserByUserNameAndUserName(ref UserID, ref PersonID,  UserName,  Password, ref IsActive))
                return new clsUsers(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;
        }


        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }

        public static bool isExistUser(int PersonID)
        {
            return clsUserData.IsUserExist(PersonID);
        }

        

    }
}
