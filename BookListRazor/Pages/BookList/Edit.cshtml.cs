using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class EditModel : PageModel
    {
        private ApplicationDbContext _db;

        [TempData]
        public string Message { get; set; }

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

       

        [BindProperty]
        public Book Book { get; set; }

        public async Task OnGet(int id)
        {
            Book = await _db.Book.FindAsync(id);
            //Book = _db.Book.Where(b => b.Id == id).FirstOrDefault();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var BookFromDb = await _db.Book.FindAsync(Book.Id);
                BookFromDb.Name = Book.Name;
                BookFromDb.Autor = Book.Autor;
                BookFromDb.ISBN = Book.ISBN;

                await _db.SaveChangesAsync();
                Message = "Book has been updated succesfully.";
                return RedirectToPage("Index");
            }

            return RedirectToPage();
        }
    }
}