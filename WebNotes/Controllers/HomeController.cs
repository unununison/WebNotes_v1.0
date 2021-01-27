using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebNotes.Data;
using WebNotes.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebNotes.Controllers
{
    public class HomeController : Controller
    {
        private DBContent db;
        public HomeController(DBContent context){
            db = context;
        }
        public IActionResult Index(){
            return View();
        }

        /*-------------------------------------------ЗАМЕТКИ-------------------------------------------*/
        /*Отображение всех*/
        public ActionResult Note(){
            var notes = db.Notes.Include(p => p.Tag);
            return View(notes.ToList());
        }
        /*Создание*/
        [HttpGet]
        public ActionResult Create(){
            SelectList tags = new SelectList(db.Tags, "Id", "Tag_text");
            ViewBag.Tags = tags;
            return View();
        }
        [HttpPost]
        public ActionResult Create(Note note){
            db.Notes.Add(note);
            db.SaveChanges();
            return RedirectToAction("Note");
        }
        /*Редактирование*/
        [HttpGet]
        public ActionResult Edit(int? id){
            if (id != null){
                Note note = db.Notes.FirstOrDefault(p => p.Id == id);
                SelectList tags = new SelectList(db.Tags, "Id", "Tag_text");
                ViewBag.Tags = tags;
                if (note != null)
                    return View(note);
            }
            return NotFound();
        }
        [HttpPost]
        public ActionResult Edit(Note note){
            db.Notes.Update(note);
            db.SaveChanges();
            return RedirectToAction("Note");
        }
        /*Удаление*/
        [HttpGet]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int? id){ 
            if (id != null){
                Note note = db.Notes.FirstOrDefault(p => p.Id == id);
                if (note != null)
                    return View(note);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult Delete(int? id){
            if (id != null){
                Note note = db.Notes.FirstOrDefault(p => p.Id == id);
                if (note != null){
                    db.Notes.Remove(note);
                    db.SaveChanges();
                    return RedirectToAction("Note");
                }
            }
            return NotFound();
        }
        /*-------------------------------------------НАПОМИНАНИЯ-------------------------------------------*/
        /*Отображение всех*/
        public async Task<IActionResult> Notification(){
            return View(await db.Notifications.ToListAsync());
        }
        /*Создание*/

        public IActionResult NotificationCreate(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>NotificationCreate(Notification notification){
            db.Notifications.Add(notification);
            await db.SaveChangesAsync();
            return RedirectToAction("Notification");
        }
        /*Редактирование*/
        public async Task<IActionResult> NotificationEdit(int? id){
            if (id != null){
                Notification notification = await db.Notifications.FirstOrDefaultAsync(p => p.Id == id);
                if (notification != null)
                    return View(notification);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> NotificationEdit(Notification notification){
            db.Notifications.Update(notification);
            await db.SaveChangesAsync();
            return RedirectToAction("Notification");
        }
        /*Удаление*/
        [HttpGet]
        [ActionName("NotificationDelete")]
        public async Task<IActionResult> ConfirmDeleteNotification(int? id){
            if (id != null){
                Notification notification = await db.Notifications.FirstOrDefaultAsync(p => p.Id == id);
                if (notification != null)
                    return View(notification);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> NotificationDelete(int? id){
            if (id != null){
                Notification notification = await db.Notifications.FirstOrDefaultAsync(p => p.Id == id);
                if (notification != null){
                    db.Notifications.Remove(notification);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Notification");
                }
            }
            return NotFound();
        }
        /*-------------------------------------------ТЕГИ-------------------------------------------*/
        /*Отображение всех*/
        public ActionResult Tag(){
            return View(db.Tags.ToList());
        }
        /*Создание*/

        public ActionResult TagCreate(){
            return View();
        }

        [HttpPost]
        public ActionResult TagCreate(Tag tag){
            db.Tags.Add(tag);
            db.SaveChanges();
            return RedirectToAction("Tag");
        }
     
        /*Удаление*/
        [HttpGet]
        [ActionName("TagDelete")]
        public async Task<IActionResult> ConfirmDeleteTag(int? id){
            if (id != null){
                Tag tag = await db.Tags.FirstOrDefaultAsync(p => p.Id == id);                
                if (tag != null)
                    return View(tag);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> TagDelete(int? id){
            if (id != null){
                Tag tag = await db.Tags.FirstOrDefaultAsync(p => p.Id == id);
                Note note = await db.Notes.FirstOrDefaultAsync(p => p.TagId == id);
                if (tag != null){
                    db.Tags.Remove(tag);
                    if (note != null){
                        note.TagId = null;
                        db.Notes.Update(note);
                    }
                    await db.SaveChangesAsync();
                    return RedirectToAction("Tag");
                }
            }
            return NotFound();
        }

    }
}
