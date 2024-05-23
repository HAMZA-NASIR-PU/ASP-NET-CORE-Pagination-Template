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
		public PaginatedList<Event> Products { get; set; }

		public async Task<IActionResult> OnGetAsync(int? pageNumber)
		{
			int pageSize = 10;
			var products = _context.Events.AsNoTracking();
			Products = await PaginatedList<Event>.CreateAsync(products, pageNumber ?? 1, pageSize);

			if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
			{
				return new JsonResult(new
				{
					paginatedList = new
					{
						items = Products,
						totalPages = Products.TotalPages,
						pageIndex = Products.PageIndex,
						hasPreviousPage = Products.HasPreviousPage,
						hasNextPage = Products.HasNextPage
					}
				});
			}

			return Page();
		}
	}
}
