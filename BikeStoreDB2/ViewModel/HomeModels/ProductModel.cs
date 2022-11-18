using BikeStoreDB2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BikeStoreDB2.ViewModel.HomeModels
{
    public class ProductModel
    {
        public List<brands> brandList { get; set; }
        public List<categories> categoryList { get; set; }
    }
}