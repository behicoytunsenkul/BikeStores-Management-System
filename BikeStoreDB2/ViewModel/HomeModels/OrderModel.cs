using BikeStoreDB2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BikeStoreDB2.ViewModel.HomeModels
{
    public class OrderModel
    {
        public List<products> productList { get; set; }

        public List<customers> customerList { get; set; }

        public List<staffs> staffList { get; set; }

        public List<stores> storeList { get; set; }
        public int? quantity { get; set; }

        public int? selectedCustomerId { get; set; }
        public int? selectedStaffId { get; set; }
        public int? selectedStoreId { get; set; }
        public int? selectedProductId { get; set; }
    }
}