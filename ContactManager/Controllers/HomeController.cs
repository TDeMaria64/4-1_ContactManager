using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ContactManager.Models;
using System.Threading.Tasks;

namespace ContactManager.Controllers
{
    public class HomeController : Controller
    {
        private ContactContext Context { get; set; }
        public HomeController(ContactContext ctx)
        {
            Context = ctx;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var contacts = from c in Context.Contacts
                           .Include(c => c.Category)
                           .OrderBy(c => c.Lastname)
                           select c;

            if (!string.IsNullOrEmpty(searchString))
            {
                contacts = contacts.Where(s => s.Lastname!.Contains(searchString));
            }

            return View(await contacts.ToListAsync());
        }
    }
}
