using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trasua.Models;


namespace Trasua.Repository.Components
{
    public class Products : ViewComponent
    {
        private readonly TrasuaContext _context;

        public Products(TrasuaContext context)
        {
            _context = context;
        }



        public async Task<IViewComponentResult> InvokeAsync() => View(await _context.SanPhams.ToListAsync());
    }
}
