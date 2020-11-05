using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectASPNETMVC.Models;

namespace ProjectASPNETMVC.Controllers
{
    public class GuestBookController : Controller
    {
        private GuestBookEntities db = new GuestBookEntities();

        // GET: GuestBook
        public ActionResult Index()
        {
            return View(db.GB.ToList());
        }

        //自訂區
        public ActionResult List()
        {
            //return View("_List",db.GB.ToList());
            return View("_List", sortOrder());
        }
        public Object sortOrder()
        {
            var guestBooks = db.Set<GB>();
            foreach (GB item in guestBooks)
            {
                System.Diagnostics.Trace.WriteLine(item.ID);
                System.Diagnostics.Trace.WriteLine(item.Title);
                System.Diagnostics.Trace.WriteLine(item.Content);
                System.Diagnostics.Trace.WriteLine(item.PostTime);
                System.Diagnostics.Trace.WriteLine("");
            }
            var result = guestBooks.OrderByDescending(m => m.PostTime);
            return result.ToList();
        }
        //自訂區

        // GET: GuestBook/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GB gB = db.GB.Find(id);
            if (gB == null)
            {
                return HttpNotFound();
            }
            return View(gB);
        }

        // GET: GuestBook/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GuestBook/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Content,PostTime")] GB gB)
        {
            if (ModelState.IsValid)
            {
                gB.ID = Guid.NewGuid();
                gB.PostTime = DateTime.Now;
                db.GB.Add(gB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gB);
        }

        // GET: GuestBook/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GB gB = db.GB.Find(id);
            if (gB == null)
            {
                return HttpNotFound();
            }
            return View(gB);
        }

        // POST: GuestBook/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Content,PostTime")] GB gB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gB).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("List");
            }
            return View(gB);
        }

        // GET: GuestBook/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GB gB = db.GB.Find(id);
            if (gB == null)
            {
                return HttpNotFound();
            }
            return View(gB);
        }

        // POST: GuestBook/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            GB gB = db.GB.Find(id);
            db.GB.Remove(gB);
            db.SaveChanges();
            return RedirectToAction("Index");
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
