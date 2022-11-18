using BikeStoreDB2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BikeStoreDB2.ViewModel.HomeModels
{
    public class StaffModel
    {
        public List<staffs> managerList { get; set; }
        public List<stores> storeList { get; set; }

        public staffs staff { get; set; }
    }
}