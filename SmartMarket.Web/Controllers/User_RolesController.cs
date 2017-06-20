using BusinessEntities;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MadintyFacebook.Controllers
{
    [Authorize(Roles = "Admin")]
    public class User_RolesController : Controller
    {
        private readonly SmartMarketDB _db = new SmartMarketDB();

        [Authorize(Roles = "Admin")]
        // GET: User_Roles
        public ActionResult Index()
        {
            var user_Roles = _db.UserRoles.Include(u => u.Role).Include(u => u.User);
            return View(user_Roles.ToList());
        }

        [Authorize(Roles = "Admin")]
        // GET: User_Roles/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole user_Roles = _db.UserRoles.Find(id);
            if (user_Roles == null)
            {
                return HttpNotFound();
            }
            return View(user_Roles);
        }

        [Authorize(Roles = "Admin")]
        // GET: User_Roles/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(_db.Roles, "ID", "Roles");
            ViewBag.UserId = new SelectList(_db.Users, "ID", "UserName");
            return View();
        }

        // POST: User_Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,RoleId")] UserRole user_Roles)
        {
            if (ModelState.IsValid)
            {
                _db.UserRoles.Add(user_Roles);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(_db.Roles, "ID", "Roles", user_Roles.RoleId);
            ViewBag.UserId = new SelectList(_db.Users, "ID", "UserName", user_Roles.UserId);
            return View(user_Roles);
        }

        [Authorize(Roles = "Admin")]
        // GET: User_Roles/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole user_Roles = _db.UserRoles.Find(id);
            if (user_Roles == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(_db.Roles, "ID", "Roles", user_Roles.RoleId);
            ViewBag.UserId = new SelectList(_db.Users, "ID", "UserName", user_Roles.UserId);
            return View(user_Roles);
        }

        // POST: User_Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,RoleId")] UserRole user_Roles)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(user_Roles).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(_db.Roles, "ID", "Roles", user_Roles.RoleId);
            ViewBag.UserId = new SelectList(_db.Users, "ID", "UserName", user_Roles.UserId);
            return View(user_Roles);
        }

        [Authorize(Roles = "Admin")]
        // GET: User_Roles/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole user_Roles = _db.UserRoles.Find(id);
            if (user_Roles == null)
            {
                return HttpNotFound();
            }
            return View(user_Roles);
        }

        // POST: User_Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            UserRole user_Roles = _db.UserRoles.Find(id);
            _db.UserRoles.Remove(user_Roles);
            _db.SaveChanges();
            return RedirectToAction("Index");
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
