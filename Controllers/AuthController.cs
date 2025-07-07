using Manajemen_Data_Murid.Helper;
using Manajemen_Data_Murid.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Claims;
using System.Text;

namespace Manajemen_Data_Murid.Controllers
{
    public class AuthController : Controller
    {
        private readonly string __constr;
        private readonly IConfiguration _config;
        public IActionResult Index()
        {
            return View();
        }

        public AuthController(IConfiguration config)
        {
            _config = config;
            __constr = _config.GetConnectionString("WebApiDatabase");
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] Person registerData)
        {

            if (string.IsNullOrEmpty(registerData.Email) || string.IsNullOrEmpty(registerData.Password))
            {
                return BadRequest(new { message = "Email dan password harus diisi" });
            }

            PersonContext context = new PersonContext(__constr);


            Person existingPerson = context.GetPersonByEmail(registerData.Email);
            if (existingPerson != null)
            {
                return BadRequest(new { message = "Email sudah terdaftar" });
            }

            string selectedRole = registerData.Role?.ToLower();
            if (selectedRole != "user" && selectedRole != "admin")
            {
                return BadRequest(new { message = "Peran tidak valid. Hanya bisa memilih 'User' atau 'Admin'." });
            }

            registerData.Role = selectedRole == "admin" ? "Admin" : "User";

            bool isRegistered = context.RegisterPerson(registerData);

            if (isRegistered)
            {
                return Ok(new { message = "Registrasi berhasil" });
            }
            else
            {
                return StatusCode(500, new { message = "Registrasi gagal" });
            }
        }



        [HttpPost("login")]
        public IActionResult Login([FromBody] Login loginData)
        {
            PersonContext context = new PersonContext(__constr);
            Person person = context.GetPersonByEmail(loginData.Email);

            if (person == null || person.Password != loginData.Password)
            {
                return Unauthorized(new { message = "Email atau password salah" });
            }

            JwtHelper jwtHelper = new JwtHelper(_config);
            var token = jwtHelper.GenerateToken(person);

            return Ok(new
            {
                token = token,
                person = new
                {
                    id = person.Id,
                    name = person.Name,
                    email = person.Email,
                    role = person.Role
                }
            });
        }

    }
}