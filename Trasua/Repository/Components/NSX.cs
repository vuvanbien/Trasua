using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trasua.Models;


namespace Trasua.Repository.Components
{
    public class NSX : ViewComponent
    {
        private readonly TrasuaContext _context;

        public NSX(TrasuaContext context)
        {
            _context = context;
        }



        public async Task<IViewComponentResult> InvokeAsync() => View(await _context.NhaSanXuats.ToListAsync());
    }
}
