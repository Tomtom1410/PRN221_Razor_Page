using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebRazor.Pages.Cart
{
    public class InvoiceModel : PageModel
    {
        public Models.Order order { get; set; }
        public void OnGet()
        {
        }
    }
}
