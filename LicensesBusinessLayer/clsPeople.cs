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

        public static DataTable GetAllPeople()
        {
            return clsPeopleData.GetAllPeople();
        }
    }
}
