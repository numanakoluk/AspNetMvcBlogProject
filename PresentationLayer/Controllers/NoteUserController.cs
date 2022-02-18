using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EntiyLayers;
using PresentationLayer.Data;

namespace PresentationLayer.Controllers
{
    public class NoteUserController : Controller
    {

        // GET: NoteUser
        public ActionResult Index()
        {
            return View(db.NoteUsers.ToList());
        }

        // GET: NoteUser/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoteUser noteUser = db.NoteUsers.Find(id);
            if (noteUser == null)
            {
                return HttpNotFound();
            }
            return View(noteUser);
        }

        // GET: NoteUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NoteUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Surname,UserName,Email,Password,ProfileImageFileName,IsActive,IsAdmin,ActivateGuid,CreatedOn,ModifiedOn,ModifiedUserName")] NoteUser noteUser)
        {
            if (ModelState.IsValid)
            {
                db.NoteUsers.Add(noteUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(noteUser);
        }

        // GET: NoteUser/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoteUser noteUser = db.NoteUsers.Find(id);
            if (noteUser == null)
            {
                return HttpNotFound();
            }
            return View(noteUser);
        }

        // POST: NoteUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Surname,UserName,Email,Password,ProfileImageFileName,IsActive,IsAdmin,ActivateGuid,CreatedOn,ModifiedOn,ModifiedUserName")] NoteUser noteUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(noteUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(noteUser);
        }

        // GET: NoteUser/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoteUser noteUser = db.NoteUsers.Find(id);
            if (noteUser == null)
            {
                return HttpNotFound();
            }
            return View(noteUser);
        }

        // POST: NoteUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NoteUser noteUser = db.NoteUsers.Find(id);
            db.NoteUsers.Remove(noteUser);
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
