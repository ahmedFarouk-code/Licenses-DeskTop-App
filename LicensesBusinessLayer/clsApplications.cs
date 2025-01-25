using LicensesDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicensesBusinessLayer
{
    public class clsApplications
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ID { get; set; }

        public static DataTable GetAllApplications()
        {
            return clsApplicationsData.GetAllApplications();
        }
    }
}
