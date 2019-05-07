using System;
using System.Collections.Generic;
using System.Text;

namespace DCSLibrary
{
    public class SalesStaff : Employee
    {
        public SalesStaff(int id, string name, string store)
        {
            this.id = id;
            this.name = name;
            this.store = store;
            this.title = EmployeeRank.SALES_STAFF;
        }
    }
}
