using BikeStoreDB2.Models;
using BikeStoreDB2.ViewModel.HomeModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BikeStoreDB2.Controllers
{
    public class HomeController : Controller
    {
        #region basis
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        #endregion basis

        #region brand
        public ActionResult BrandList()
        {
            BikeStoresEntities1 ent = new BikeStoresEntities1();
            List<brands> model = new List<brands>();

            model = ent.brands.ToList();

            return View(model);

        }
        #endregion brand
        #region products

        public ActionResult ProductList()
        {
            BikeStoresEntities1 ent = new BikeStoresEntities1();
            List<products> model = new List<products>();

            model = ent.products.ToList();

            return View(model);
        }
        [HttpPost]
        public ActionResult CreateProduct(products model)
        {
            BikeStoresEntities1 ent = new BikeStoresEntities1();

            ent.products.Add(model);
            ent.SaveChanges();
            return RedirectToAction("ProductList");
        }

        public ActionResult CreateProduct()
        {
            BikeStoresEntities1 ent = new BikeStoresEntities1();
            ProductModel model = new ProductModel();
            List<brands> brandList = new List<brands>();
            List<categories> categoryList = new List<categories>();
            try
            {
                //brandList = ent.brands.ToList();
                //categoryList = ent.categories.ToList();

                ViewData["brandList"] = brandList;
                ViewBag.categoryList = categoryList;

                //TempData["brandList"] = brandList;

                model.brandList = ent.brands.ToList();
                model.categoryList = ent.categories.ToList();


            }
            catch (Exception)
            { throw; }
            return View(model);
        }


        public ActionResult DeleteProduct(int id)
        {
            BikeStoresEntities1 ent = new BikeStoresEntities1();
            products model = new products();

            model = ent.products.Find(id);
            ent.products.Remove(model);
            ent.SaveChanges();

            return RedirectToAction("ProductList");
        }


        public ActionResult CategoryList()
        {
            BikeStoresEntities1 ent = new BikeStoresEntities1();
            List<categories> model = new List<categories>();

            model = ent.categories.ToList();
            
            return View(model);

        }
        #endregion
        #region Customer
        public ActionResult CustomerList()
        {
            BikeStoresEntities1 ent = new BikeStoresEntities1();
            List<customers> model = new List<customers>();

            model = ent.customers.ToList();

            return View(model);

        }
        public ActionResult CreateCustomer()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CreateCustomer(customers model)
        {
            BikeStoresEntities1 ent = new BikeStoresEntities1();
            ent.customers.Add(model);
            ent.SaveChanges();

            return RedirectToAction("CustomerList");
        }
        public ActionResult DeleteCustomer(int? id)
        {
            BikeStoresEntities1 ent = new BikeStoresEntities1();
            customers model = new customers();
            model = ent.customers.Find(id);

            return View(model);
        }
        [HttpPost]
        public ActionResult DeleteCustomer(int id)
        {
            BikeStoresEntities1 ent = new BikeStoresEntities1();
            customers model = new customers();
            model = ent.customers.Find(id);
            ent.customers.Remove(model);
            ent.SaveChanges();
            return RedirectToAction("CustomerList");
        }

        public ActionResult EditCustomer(int id)
        {
            BikeStoresEntities1 ent = new BikeStoresEntities1();
            customers model = new customers();
            model = ent.customers.Find(id);

            return View(model);
        }
        [HttpPost]
        public ActionResult EditCustomer(customers model)
        {
            BikeStoresEntities1 ent = new BikeStoresEntities1();
            customers customerModel = new customers();
            customerModel = ent.customers.Find(model.customer_id);
            customerModel = model;
            ent.SaveChanges();
            return RedirectToAction("CustomerList");
        }
        #endregion customer
        

        #region order
        public ActionResult OrderList()
        {
            BikeStoresEntities1 ent = new BikeStoresEntities1();
            List<orders> model = new List<orders>();

            try
            {
                model = ent.orders.OrderByDescending(q => q.order_id).ToList();
            }
            catch (Exception)
            { throw; }

            return View(model);
        }

        public ActionResult CreateOrder()
        {
            BikeStoresEntities1 ent = new BikeStoresEntities1();
            OrderModel model = new OrderModel();
            List<products> productList = new List<products>();
            List<customers> customerList = new List<customers>();
            List<staffs> staffList = new List<staffs>();
            List<stores> storeList = new List<stores>();
            try
            {
                model.productList = ent.products.ToList();
                model.customerList = ent.customers.ToList();
                model.staffList = ent.staffs.ToList();
                model.storeList = ent.stores.ToList();
            }
            catch (Exception)
            { throw; }
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateOrder(orders model,int quantity, int product_id)
        {
            BikeStoresEntities1 ent = new BikeStoresEntities1();
            order_items order_item = new order_items();
            order_item.quantity = quantity;
            order_item.product_id = product_id;
            model.order_date = DateTime.Now;
            model.required_date = DateTime.Now.AddDays(3);
            model.order_status = 1;
            ent.orders.Add(model);
            ent.order_items.Add(order_item);
            ent.SaveChanges();
            return RedirectToAction("OrderList");
        }
        public ActionResult DeleteOrder(int id)
        {
            BikeStoresEntities1 ent = new BikeStoresEntities1();
            orders orderModel = new orders();

            orderModel = ent.orders.Find(id);

            ent.orders.Remove(orderModel);
            ent.SaveChanges();
            return RedirectToAction("OrderList");
        }

        public ActionResult DetailOrder(int id)
        {
            BikeStoresEntities1 ent = new BikeStoresEntities1();
            OrderModel orderModel = new OrderModel();
            order_items orderItem = new order_items();
            orders order = new orders();
            order = ent.orders.Find(id);

            if (ent.order_items.Where(q => q.order_id.Equals(order.order_id)).ToList().Count > 0)
            {
                orderItem = ent.order_items.Where(q => q.order_id.Equals(order.order_id)).ToList()[0];
            }

            orderModel.customerList = ent.customers.Where(q => q.customer_id.Equals((int)order.customer_id)).ToList();
            orderModel.storeList = ent.stores.Where(q => q.store_id.Equals(order.store_id)).ToList();
            orderModel.staffList = ent.staffs.Where(q => q.staff_id.Equals(order.staff_id)).ToList();
            if (orderItem != null && orderItem.order_id > 0)
            {
                orderModel.productList = ent.products.Where(q => q.product_id.Equals(orderItem.product_id)).ToList();
                orderModel.quantity = orderItem.quantity;
            }
            return View(orderModel);
        }

        public ActionResult EditOrder(int? id, int? customerId, int? staffId, int? storeId)
        {
            BikeStoresEntities1 ent = new BikeStoresEntities1();
            OrderModel orderModel = new OrderModel();
            orders order = new orders();
            order_items orderItem = new order_items();
            order = ent.orders.Find(id);

            if (ent.order_items.Where(q => q.order_id.Equals(order.order_id)).ToList().Count > 0)
            {
                orderItem = ent.order_items.Where(q => q.order_id.Equals(order.order_id)).ToList()[0];
            }

            try
            {
                orderModel.productList = ent.products.ToList();
                orderModel.customerList = ent.customers.ToList();
                orderModel.staffList = ent.staffs.ToList();
                orderModel.storeList = ent.stores.ToList();
                orderModel.selectedCustomerId = customerId;
                orderModel.selectedStaffId = staffId;
                orderModel.selectedStoreId = storeId;
                if (orderItem != null && orderItem.order_id > 0)
                {
                    orderModel.quantity = orderItem.quantity;
                    orderModel.selectedProductId = orderItem.product_id;
                }
            }
            catch (Exception)
            { throw; }
            return View(orderModel);
        }
        [HttpPost]
        public ActionResult EditOrder(orders model)
        {
            BikeStoresEntities1 ent = new BikeStoresEntities1();
            orders orderModel = new orders();
            orderModel = ent.orders.Find(model.order_id);
            orderModel = model;
            ent.SaveChanges();
            return RedirectToAction("OrderList");
        }
        #endregion order


        public ActionResult StaffList()
        {
            BikeStoresEntities1 ent = new BikeStoresEntities1();
            List<staffs> model = new List<staffs>();

            model = ent.staffs.ToList();

            return View(model);

        }
        public ActionResult DeleteStaff(int id)
        {
            BikeStoresEntities1 ent = new BikeStoresEntities1();
            staffs model = new staffs();

            model = ent.staffs.Find(id);
            model.active = 0;
            ent.SaveChanges();
            return RedirectToAction("StaffList");

        }

        public ActionResult CreateStaff()
        {
            BikeStoresEntities1 ent = new BikeStoresEntities1();
            StaffModel model = new StaffModel();
            try
            {
                model.storeList = ent.stores.ToList();
                model.managerList = ent.staffs.ToList();
            }
            catch (Exception)
            { throw; }
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateStaff(staffs model)
        {
            BikeStoresEntities1 ent = new BikeStoresEntities1();

            model.active = 1;
            ent.staffs.Add(model);
            ent.SaveChanges();
            return RedirectToAction("StaffList");
        }

        public ActionResult DetailStaff(int id)
        {
            BikeStoresEntities1 ent = new BikeStoresEntities1();
            StaffModel staff = new StaffModel();
            staff.staff = ent.staffs.Find(id);
            if (staff.staff != null)
            {
                if (staff.staff.store_id > 0)
                {
                    staff.storeList = ent.stores.Where(q => q.store_id.Equals((int)staff.staff.store_id)).ToList();
                }
                if (staff.staff.manager_id > 0)
                {
                    staff.managerList = ent.staffs.Where(q => q.store_id.Equals((int)staff.staff.manager_id)).ToList();
                }
            }

            return View(staff);
        }
        public ActionResult EditStaff(int id)
        {
            BikeStoresEntities1 ent = new BikeStoresEntities1();
            StaffModel staff = new StaffModel();
            staff.staff = ent.staffs.Find(id);
            staff.storeList = ent.stores.ToList();
            staff.managerList = ent.staffs.ToList();

            return View(staff);
        }
    }
}