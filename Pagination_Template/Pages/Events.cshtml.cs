using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pagination_Template.Data;
using Pagination_Template.Models;
using Pagination_Template.Special;

namespace Pagination_Template.Pages
{
    public class EventsModel : PageModel
    {
		private readonly SchoolContext _context;

		public EventsModel(SchoolContext context)
		{
			_context = context;
		}

		[BindProperty]
		public PaginatedList<Event> Events { get; set; }

        public async Task<IActionResult> OnGetAsync(int? pageNumber, string sortOrder, string sortField)
        {
            int pageSize = 10;
            var events = _context.Events.AsNoTracking();

            switch (sortField)
            {
                case "title":
                    events = sortOrder == "asc" ? events.OrderBy(e => e.Title) : events.OrderByDescending(e => e.Title);
                    break;
                case "start_date":
                    events = sortOrder == "asc" ? events.OrderBy(e => e.StartDate) : events.OrderByDescending(e => e.StartDate);
                    break;
                case "end_date":
                    events = sortOrder == "asc" ? events.OrderBy(e => e.EndDate) : events.OrderByDescending(e => e.EndDate);
                    break;
                default:
                    events = sortOrder == "asc" ? events.OrderBy(e => e.Id) : events.OrderByDescending(e => e.Id); // Default sorting by id
                    break;
            }

            Events = await PaginatedList<Event>.CreateAsync(events, pageNumber ?? 1, pageSize);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return new JsonResult(new
                {
                    paginatedList = new
                    {
                        items = Events,
                        totalPages = Events.TotalPages,
                        pageIndex = Events.PageIndex,
                        hasPreviousPage = Events.HasPreviousPage,
                        hasNextPage = Events.HasNextPage
                    }
                });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var eventToDelete = await _context.Events.FindAsync(id);

            if (eventToDelete == null)
            {
                return NotFound();
            }

            _context.Events.Remove(eventToDelete);
            await _context.SaveChangesAsync();

            return new JsonResult(new { success = true, message = "Event deleted successfully." });
        }
    }
}
