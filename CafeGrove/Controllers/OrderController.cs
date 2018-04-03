using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CafeGrove.Models;

namespace CafeGrove.Controllers
{
    [RoutePrefix("Order")]
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Order
        [Route("TakeOrder/{tableNo}", Name = "TakeOrder")]
        public ActionResult TakeOrder(string tableNo)
        {
            ViewBag.tableNo = tableNo;
            return View(db.Categories.ToList());
        }

        [Route("Tables", Name = "Tables")]
        public ActionResult Tables()
        {

            return View();
        }

        // GET: Order/Details/5
        [HttpGet]
        [Route("AddToOrder/{tableNo}/{productId}")]
        public ActionResult AddToOrder(string tableNo, int productId)
        {
            var productOrder = db.Orders.FirstOrDefault(x => x.TableNo == tableNo && x.ProductId == productId);
            if (productOrder == null)
            {
                db.Orders.Add(new Order
                {
                    ProductId = productId,
                    Quantity = 1,
                    TableNo = tableNo
                });
                db.SaveChanges();
            }
            else
            {
                productOrder.Quantity = productOrder.Quantity + 1;
                db.SaveChanges();
            }
            return PartialView("_OrderPatialView", db.Orders.Include("Product").Where(x => x.TableNo == tableNo).ToList());
        }

        [HttpGet]
        [Route("DeleteProductFromOrder/{tableNo}/{productId}")]
        public ActionResult DeleteProductFromOrder(string tableNo, int productId)
        {
            var productOrder = db.Orders.FirstOrDefault(x => x.TableNo == tableNo && x.ProductId == productId);
            if (productOrder != null)
            {

                db.Orders.Remove(productOrder);
                db.SaveChanges();
            }
            return PartialView("_OrderPatialView", db.Orders.Include("Product").Where(x => x.TableNo == tableNo).ToList());
        }

        [HttpGet]
        [Route("ChangeProductQuantity/{tableNo}/{productId}/{quantity}")]
        public ActionResult ChangeProductQuantity(string tableNo, int productId,int quantity)
        {
            var productOrder = db.Orders.FirstOrDefault(x => x.TableNo == tableNo && x.ProductId == productId);
            if (productOrder != null)
            {
                if (productOrder.Quantity == 1 && quantity == -1)
                {
                    db.Orders.Remove(productOrder);
                 
                }
                else
                {
                    productOrder.Quantity += quantity;
                    db.Entry(productOrder).State = EntityState.Modified;
                }

                db.SaveChanges();
            }
            return PartialView("_OrderPatialView", db.Orders.Include("Product").Where(x => x.TableNo == tableNo).ToList());
        }
        [Route("CompleteOrder/{tableNo}", Name = "CompleteOrder")]
        public ActionResult CompleteOrder(string tableNo)
        {
            ViewBag.tableNo = tableNo;

            foreach (var order in db.Orders.Where(x => x.TableNo == tableNo).ToList())
            {
                db.Orders.Remove(order);
            }

            db.SaveChanges();
            return PartialView("_OrderPatialView", db.Orders.Include("Product").Where(x => x.TableNo == tableNo).ToList());
        }

        [HttpGet]
        [Route("GetOrder/{tableNo}")]
        public ActionResult GetOrder(string tableNo)
        {
            return PartialView("_OrderPatialView", db.Orders.Include("Product").Where(x => x.TableNo == tableNo).ToList());
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
