﻿

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ilibrary.DataAccess.Data;
using Ilibrary.Models;
using Ilibrary.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Ilibrary.Utility;

namespace IlibraryWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin) ]

    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork, ApplicationDbContext context)
        { 
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public IActionResult Index()
        {
            //List<Section> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            List<Ilibrary.Models.Type> objCategoryList = _context.Type.ToList();

            return View(objCategoryList);
        }

        //-------------------------------Create-----------------------------------
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Section obj)
        {
            //  custom validation
            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            //}
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Created successfully";
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
            /*way 1*/
            Section? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            //way 2  //Category categoryFromD1 = _db.categories.FirstOrDefault(u => u.Id==id);
            //way 3 //Category categoryFromDb2 = _db.categories.Where(u => u.Id == id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Section obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Updated successfully";
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
            /*way 1*/
            Section? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            //way 2  //Category categoryFromD1 = _db.categories.FirstOrDefault(u => u.Id==id);
            //way 3 //Category categoryFromDb2 = _db.categories.Where(u => u.Id == id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {

            Section? obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
