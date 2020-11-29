﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public BooksController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET
        public IActionResult Index()
        {
            var books = _context.Books.ToList();
            return View(books);
        }

        public IActionResult Show(int id)
        {
            return View(_context.Books.Find(id));
        }
        
        // GET: Customer/Create
        public IActionResult AddOrEdit(int id = 0)
        {
            return View(id == 0
                ? new Book()
                : _context.Books.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(Book book)
        {
            if (ModelState.IsValid)
            {    
                if (book.MyImage != null)
                {
                    var uniqueFileName = GetUniqueFileName(book.MyImage.FileName);
                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                    var filePath = Path.Combine(uploads,uniqueFileName);
                    book.MyImage.CopyTo(new FileStream(filePath, FileMode.Create));

                    book.ImageFileName = uniqueFileName;
                }
                
                if (book.Id == 0)
                    _context.Add(book);
                else
                    _context.Update(book);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        public IActionResult Upload(Book model)
        {
            // do other validations on your model as needed
            if (model.MyImage != null)
            {
                var uniqueFileName = GetUniqueFileName(model.MyImage.FileName);
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                var filePath = Path.Combine(uploads,uniqueFileName);
                model.MyImage.CopyTo(new FileStream(filePath, FileMode.Create)); 

                //to do : Save uniqueFileName  to your db table   
            }
            // to do  : Return something
            return RedirectToAction("Index","Home");
        }
        
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return  Path.GetFileNameWithoutExtension(fileName)
                    + "_" 
                    + Guid.NewGuid().ToString().Substring(0, 4) 
                    + Path.GetExtension(fileName);
        }
    }
}