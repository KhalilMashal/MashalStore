using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ilibrary.DataAccess.Data;
using Ilibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Ilibrary.Utility;

namespace IlibraryWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class TypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Ilibrary.Models.Type> objTypeList = _context.Type.ToList();
            return View(objTypeList);
        }

        //-------------------------------Create-----------------------------------
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Ilibrary.Models.Type obj)
        {
            if (ModelState.IsValid)
            {
                _context.Type.Add(obj);
                _context.SaveChanges();
                TempData["success"] = "Type created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        //------------------------------Edit---------------------------------------------
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Ilibrary.Models.Type? typeFromDb = _context.Type.FirstOrDefault(u => u.Id == id);
            if (typeFromDb == null)
            {
                return NotFound();
            }
            return View(typeFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Ilibrary.Models.Type obj)
        {
            if (ModelState.IsValid)
            {
                _context.Type.Update(obj);
                _context.SaveChanges();
                TempData["success"] = "Type updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        //------------------------------Delete---------------------------------------------
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Ilibrary.Models.Type? typeFromDb = _context.Type.FirstOrDefault(u => u.Id == id);
            if (typeFromDb == null)
            {
                return NotFound();
            }
            return View(typeFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Ilibrary.Models.Type? obj = _context.Type.FirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _context.Type.Remove(obj);
            _context.SaveChanges();
            TempData["success"] = "Type deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
