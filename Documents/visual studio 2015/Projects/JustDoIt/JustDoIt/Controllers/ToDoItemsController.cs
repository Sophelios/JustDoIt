using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JustDoIt.Models;

namespace JustDoIt.Controllers
{
    public class ToDoItemsController : Controller
    {
        private JustDoItContext db = new JustDoItContext();

        // GET: ToDoItems
        public ActionResult Index()
        {
            var toDoItems = db.ToDoItems.Include(t => t.TaskList);
            return View(toDoItems.ToList());
        }

        // GET: ToDoItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoItem toDoItem = db.ToDoItems.Find(id);
            if (toDoItem == null)
            {
                return HttpNotFound();
            }
            return View(toDoItem);
        }

        // GET: ToDoItems/Create
        public ActionResult Create()
        {
            ViewBag.ToDoListID = new SelectList(db.TaskLists, "ToDoListID", "Title");
            return View();
        }

        // POST: ToDoItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ToDoItemID,ItemName,ItemNote,Priority,DateDue,ToDoListID")] ToDoItem toDoItem)
        {
            toDoItem.DateCreated = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.ToDoItems.Add(toDoItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ToDoListID = new SelectList(db.TaskLists, "ToDoListID", "Title", toDoItem.ToDoListID);
            return View(toDoItem);
        }

        // GET: ToDoItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoItem toDoItem = db.ToDoItems.Find(id);
            if (toDoItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ToDoListID = new SelectList(db.TaskLists, "ToDoListID", "Title", toDoItem.ToDoListID);
            return View(toDoItem);
        }

        // POST: ToDoItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ToDoItemID,ItemName,ItemNote,Priority,DateDue,ToDoListID")] ToDoItem toDoItem)
        {
            //lookup current item.
            var existingToDoItem = db.ToDoItems.Find(toDoItem.ToDoItemID);

            if (existingToDoItem == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                //db.Entry(toDoItem).State = EntityState.Modified;
                existingToDoItem.ItemName = toDoItem.ItemName;
                existingToDoItem.ItemNote = toDoItem.ItemNote;
                existingToDoItem.Priority = toDoItem.Priority;
                existingToDoItem.DateDue = toDoItem.DateDue;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ToDoListID = new SelectList(db.TaskLists, "ToDoListID", "Title", toDoItem.ToDoListID);
            return View(toDoItem);
        }
        //POST TodoItems/Finish/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Finish(int? id)
        {
            //lookup current item.
            var existingToDoItem = db.ToDoItems.Find(id);

            if (existingToDoItem == null)
            {
                return HttpNotFound();
            }

            existingToDoItem.DateCompleted=DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }
        //POSTL TodoItems/Finish/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UnFinish(int? id)
        {
            //lookup current item.
            var existingToDoItem = db.ToDoItems.Find(id);

            if (existingToDoItem == null)
            {
                return HttpNotFound();
            }

            existingToDoItem.DateCompleted = null;
            db.SaveChanges();
            return RedirectToAction("Index");


        }

        // GET: ToDoItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoItem toDoItem = db.ToDoItems.Find(id);
            if (toDoItem == null)
            {
                return HttpNotFound();
            }
            return View(toDoItem);
        }

        // POST: ToDoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ToDoItem toDoItem = db.ToDoItems.Find(id);
            db.ToDoItems.Remove(toDoItem);
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
