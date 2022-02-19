﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using BusinessLayer.Results;
using EntiyLayers;

namespace PresentationLayer.Controllers
{
    public class NoteUserController : Controller
    {
        private NoteUserManager noteUserManager = new NoteUserManager();
        public ActionResult Index()
        {
            return View(noteUserManager.List());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoteUser noteUser = noteUserManager.Find(x=>x.Id == id.Value);
            if (noteUser == null)
            {
                return HttpNotFound();
            }
            return View(noteUser);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteUser noteUser)
        {
            if (ModelState.IsValid)
            {
                //BusinessLayerResult<NoteUser> res = noteUserManager.Insert(noteUser);
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
            NoteUser noteUser = noteUserManager.Find(x => x.Id == id.Value);
            if (noteUser == null)
            {
                return HttpNotFound();
            }
            return View(noteUser);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NoteUser noteUser)
        {
            if (ModelState.IsValid)
            {
                //Todo: Düzenlenecek
                return RedirectToAction("Index");
            }
            return View(noteUser);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoteUser noteUser = noteUserManager.Find(x => x.Id == id.Value);
            if (noteUser == null)
            {
                return HttpNotFound();
            }
            return View(noteUser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NoteUser noteUser = noteUserManager.Find(x => x.Id == id);
            noteUserManager.Delete(noteUser); 
            return RedirectToAction("Index");
        }
    }
}