﻿using BusinessLayer;
using EntiyLayers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PresentationLayer.Controllers
{
    public class CommentController : Controller
    {
        private NoteManager noteManager = new NoteManager();
        private CommentManager commentManager = new CommentManager();
        // GET: Comment
        public ActionResult ShowNoteComments(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteManager.ListQueryable().Include("Comments").FirstOrDefault(x => x.Id == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialComments", note.Comments);
        }
        
       [HttpPost]
      public ActionResult Edit(int? id, string text) //Rooting id // text ise ajaxta yakalasın diye.
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = commentManager.Find(x => x.Id == id);
            if (comment == null)
            {
                return new HttpNotFoundResult();
            }
            comment.Text = text;
            if (commentManager.Update(comment)>0)
            {
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }
    }
}