using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListRazor.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly Model.ApplicationDBContext _db;
        public BookController( Model.ApplicationDBContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Book.ToListAsync() });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb =await _db.Book.FirstOrDefaultAsync(u => u.ID == id);
            if (bookFromDb == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }
            _db.Book.Remove(bookFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete Successfull" });
        }
    }

}
