using BusinessEntities;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SmartMarket.Web.Controllers
{
    public class OrderItemsController : Controller
    {
        private SmartMarketDB db = new SmartMarketDB();

        // GET: OrderItems
        public ActionResult Index(Guid orderId)
        {
            var orderItems = db.OrderItems.Include(o => o.Order).Include(o => o.Product).Where(o => o.OrderId == orderId);
            ViewBag.orderId = orderId;
            return View(orderItems.ToList());
        }

        // GET: OrderItems/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderItem = db.OrderItems.Find(id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            return View(orderItem);
        }

        // GET: OrderItems/Create
        public ActionResult Create(Guid orderId)
        {
            var orderItem = new OrderItem() { OrderId = orderId };
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "Id");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "ProductName");
            orderItem.Count = 1;
            return View(orderItem);
        }

        // POST: OrderItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderId,ProductId,Count,PricePerItem")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                orderItem.PricePerItem = db.Products.SingleOrDefault(p => p.Id == orderItem.ProductId).Price;
                orderItem.Id = Guid.NewGuid();
                db.OrderItems.Add(orderItem);
                db.SaveChanges();
                return Json(new { success = true });
            }

            ViewBag.OrderId = new SelectList(db.Orders, "Id", "Id", orderItem.OrderId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "ProductName", orderItem.ProductId);
            return PartialView(orderItem);
        }

        // GET: OrderItems/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderItem = db.OrderItems.Find(id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "Id", orderItem.OrderId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "ProductName", orderItem.ProductId);
            return PartialView(orderItem);
        }

        // POST: OrderItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderId,ProductId,Count,PricePerItem")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderItem).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true });
            }
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "Id", orderItem.OrderId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "ProductName", orderItem.ProductId);
            return PartialView(orderItem);
        }

        [HttpGet]
        // GET: OrderItems/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderItem = db.OrderItems.Find(id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            return Json(new { success = true, Message = "" });
        }

        // POST: OrderItems/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            OrderItem orderItem = db.OrderItems.Find(id);
            db.OrderItems.Remove(orderItem);
            db.SaveChanges();
            return Json(new { success = true, Message = "" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
