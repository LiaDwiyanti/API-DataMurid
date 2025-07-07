using Manajemen_Data_Murid.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Manajemen_Data_Murid.Controllers
{
    public class Data_MuridController : Controller
    {
        private string __constr;

        public Data_MuridController(IConfiguration configuration)
        {
            __constr = configuration.GetConnectionString("WebApiDatabase");
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("api/murid"), Authorize(Roles = "User")]
        public ActionResult<Data_Murid> ListDataMurid()
        {
            Data_MuridContext context = new Data_MuridContext(this.__constr);
            List<Data_Murid> listMurid = context.ListDataMurid();
            return Ok(listMurid);
        }

        [HttpPost("api/murid/add"), Authorize(Roles = "Admin")]
        public IActionResult CreateDataMurid([FromBody] Data_Murid murid)
        {
            murid.muridID = null;

            Data_MuridContext context = new Data_MuridContext(this.__constr);
            context.addDataMurid(murid);
            return Ok(new { message = "Data murid berhasil ditambahkan!" });
        }

        [HttpPut("api/murid/update/{muridID}"), Authorize(Roles = "Admin")]
        public IActionResult UpdateDataMurid(int muridID, [FromBody] Data_Murid murid)
        {
            Data_MuridContext context = new Data_MuridContext(this.__constr);
            context.updateDataMurid(muridID, murid);
            return Ok(new { message = "Data murid berhasil diperbarui!" });
        }

        [HttpDelete("api/murid/delete/{muridID}"), Authorize(Roles = "Admin")]
        public IActionResult DeleteDataMurid(int muridID)
        {
            Data_MuridContext context = new Data_MuridContext(this.__constr);
            context.delDataMurid(muridID);
            return Ok(new { message = "Data murid berhasil dihapus!" });
        }
    }
}
