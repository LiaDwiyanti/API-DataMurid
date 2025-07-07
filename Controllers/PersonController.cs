using Manajemen_Data_Murid.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Manajemen_Data_Murid.Controllers
{
    public class PersonController : Controller
    {
        private string __constr;

        public IActionResult Index()
        {
            return View();
        }

        public PersonController(IConfiguration configuration)
        {
            __constr = configuration.GetConnectionString("koneksi");
        }



        [HttpGet("api/person"), Authorize (Roles = "Admin")]
        public ActionResult<Person> ListPerson()
        {
            PersonContext context = new PersonContext(this.__constr);

            List<Person> ListPerson = context.ListPerson();

            return Ok(ListPerson);
        }
    }
}