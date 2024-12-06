using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryJulesVerne.Models;
using LibraryJulesVerne.Context;

namespace LibraryJulesVerne.Models
namespace LibraryJulesVerne.Pages
{
    public class SearchModel : PageModel
    {
        private readonly LibraryJulesVerneContext _context;

        public List<Reader> Readers { get; set; }
        public bool HasSearched { get; set; }

        public SearchModel(LibraryJulesVerneContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                Readers = await _context.Readers
                    .Where(r => r.FirstName.Contains(name) || r.LastName.Contains(name))
                    .ToListAsync();
                HasSearched = true;
            }
        }
    }
}