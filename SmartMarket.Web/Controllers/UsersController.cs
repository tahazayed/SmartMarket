using BusinessEntities;
using SmartMarket.Web.Helpers;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;

namespace MadintyFacebook.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly SmartMarketDB _db = new SmartMarketDB();

        [Authorize(Roles = "Admin")]
        // GET: Users
        public ActionResult Index()
        {
            return View(_db.Users.ToList());
        }

        [Authorize(Roles = "Admin")]
        // GET: Users/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            List<string> userRoles = (from r in _db.Roles
                                      join ur in _db.UserRoles on r.Id equals ur.RoleId
                                      join u in _db.Users on ur.UserId equals u.Id
                                      select r.Roles).ToList();
            ViewBag.MemberOf = "";
            if (userRoles.Count > 0)
            {
                foreach (var role in userRoles)
                {
                    ViewBag.MemberOf += role + ",";
                }

            }

            return View(user);
        }

        [Authorize(Roles = "Admin")]
        // GET: Users/Create
        public ActionResult Create()
        {
            var user = new User();
            return View(user);
        }

        [AllowAnonymous]
        public ActionResult SignUp()
        {
            var user = new User { UserType = UserType.Customer };
            return View(user);
        }
        [HttpPost]
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        public ActionResult SignUp([Bind(Include = "ID,UserName,Password,Active,Email,Address,Phone,UserType")] User user)
        {
            user.UserType = UserType.Customer;
            if (ModelState.IsValid)
            {

                if (_CreateUser(user))
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(user);
        }
        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(
            [Bind(Include = "ID,UserName,Password,Active,Email,Address,Phone,UserType")] User user)
        {
            if (ModelState.IsValid)
            {

                if (_CreateUser(user))
                {
                    RedirectToAction("Index");
                }
            }

            return View(user);
        }

        protected bool _CreateUser(User user)
        {
            using (var dbContextTransaction = _db.Database.BeginTransaction())
            {
                try
                {
                    string encodedPassword = TextEncoding.EncodeString(user.Password);
                    user.Password = encodedPassword;
                    user = _db.Users.Add(user);
                    _db.SaveChanges();
                    string roleName = "";
                    switch (user.UserType)
                    {
                        case UserType.Company:
                            roleName = "Company";
                            break;
                        case UserType.Customer:
                            roleName = "Customer";
                            break;
                        case UserType.EStore:
                            roleName = "Admin";
                            break;
                    }
                    if (!string.IsNullOrEmpty(roleName))
                    {
                        var role = _db.Roles.SingleOrDefault(r => r.Roles == roleName);
                        var userRole = new UserRole
                        {
                            Role = role,
                            User = user
                        };
                        _db.UserRoles.Add(userRole);
                    }
                    _db.SaveChanges();
                    if (user.UserType == UserType.Customer)
                    {
                        var customer = new Customer
                        {
                            User = user,
                            Gender = Gender.Male,
                            Nikename = user.UserName
                        };
                        _db.Customers.Add(customer);
                        _db.SaveChanges();
                    }
                    dbContextTransaction.Commit();


                    return true;

                }
                catch
                {
                    dbContextTransaction.Rollback();
                }
            }
            return false;
        }

        [Authorize(Roles = "Admin")]
        // GET: Users/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _db.Users.Find(id);
            if (user == null || user.IsSystem)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ID,UserName,Password,Active,Email,Address,Phone,UserType")] User user)
        {
            if (ModelState.IsValid && user.UserName.ToLower() != "admin")
            {
                using (var dbContextTransaction = _db.Database.BeginTransaction())
                {
                    try
                    {

                        string encodedPassword = TextEncoding.EncodeString(user.Password);
                        user.Password = encodedPassword;
                        _db.Entry(user).State = EntityState.Modified;
                        _db.SaveChanges();
                        foreach (var ur in _db.UserRoles.Where(u => u.UserId == user.Id).ToList())
                        {
                            _db.UserRoles.Remove(ur);
                        }
                        _db.SaveChanges();
                        string roleName = "";
                        switch (user.UserType)
                        {
                            case UserType.Company:
                                roleName = "Company";
                                break;
                            case UserType.Customer:
                                roleName = "Customer";
                                break;
                            case UserType.EStore:
                                roleName = "Admin";
                                break;
                        }
                        if (!string.IsNullOrEmpty(roleName))
                        {
                            var role = _db.Roles.SingleOrDefault(r => r.Roles == roleName);
                            var userRole = new UserRole
                            {
                                Role = role,
                                User = user
                            };
                            _db.UserRoles.Add(userRole);

                        }
                        _db.SaveChanges();
                        dbContextTransaction.Commit();
                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        // GET: Users/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var oUser = _db.Users.Find(id);
            if (oUser.IsSystem)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You can not delete system admin");
            }
            if (oUser == null)
            {
                return HttpNotFound();
            }
            return View(oUser);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            var oUser = _db.Users.Find(id);
            if (!oUser.IsSystem)
            {
                _db.Users.Remove(oUser);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                ViewBag.ReturnUrl = returnUrl;
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(User user, string returnUrl)
        {

            //if (ModelState.IsValid)
            {
                //string IP = Request.ServerVariables["REMOTE_ADDR"];
                string IP = Request.UserHostAddress;
                SmartMarket.Web.Business.User _user = new SmartMarket.Web.Business.User();
                if (_user.Authenticate(user.UserName, user.Password, IP))
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    if (!string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "");
                }
            }
            return View(user);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            //FormsAuthentication.RedirectToLoginPage();
            return RedirectToAction("Login", "Users");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
