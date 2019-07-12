using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ReembolsoComMediatr.Web.Controllers
{
    [Authorize]
    public class PagamentosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}