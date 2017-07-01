using BusinessEntities;
using SmartMarket.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SmartMarket.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        SmartMarketDB db = new SmartMarketDB();
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            // Return the view
            return View(viewModel);
        }
        //
        // GET: /Store/AddToCart/5
        [HttpPost]
        public ActionResult AddToCart(Guid id)
        {
            // Retrieve the album from the database
            var addedAlbum = db.Products
                .Single(album => album.Id == id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedAlbum);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(addedAlbum.ProductName) +
                    " has been added to your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = cart.GetCountByProductId(id),
                DeleteId = id
            };
            return Json(results);
        }
        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(Guid id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation
            string albumName = db.Products
                .Single(item => item.Id == id).ProductName;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(albumName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
        //
        //// GET: /ShoppingCart/CartSummary
        //[ChildActionOnly]
        //public ActionResult CartSummary()
        //{
        //    var cart = ShoppingCart.GetCart(this.HttpContext);

        //    ViewData["CartCount"] = cart.GetCount();
        //    return PartialView("CartSummary");
        //}

        [HttpPost]
        public ActionResult AddOrder()
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var lstItems = cart.GetCartItems();
            if (lstItems.Count > 0)
            {
                var order = new Order();
                var oUser = new SmartMarket.Web.Business.User();
                long userId = oUser.GetUserId(User.Identity.Name);
                var oCustomer = db.Customers.SingleOrDefault(c => c.UserId == userId);
                if (oCustomer != null)
                {
                    order.CustomerId = oCustomer.Id;
                    order.OrderDate = DateTime.Now;
                    order = db.Orders.Add(order);
                    db.SaveChanges();
                    if (cart.CreateOrder(order) == order.Id)
                    {
                        return Json(new { success = true, Message = "", OrderId = order.Id });
                    }
                    db.Orders.Remove(order);
                    db.SaveChanges();
                    return Json(new { success = false, Message = "Failed to create order Items", OrderId = -1 });

                }
                else
                {
                    return Json(new { success = false, Message = "not a customer", OrderId = -1 });
                }
            }
            return Json(new { success = false, Message = "empty cart", OrderId = -1 });


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