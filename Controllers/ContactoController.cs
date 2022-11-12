using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HeladeriaTAMS.Models;
using HeladeriaTAMS.Data;
using HeladeriaTAMS.Integration.Sendgrid;
 

namespace HeladeriaTAMS.Controllers
{
  
    public class ContactoController : Controller
    {
        private readonly ILogger<ContactoController> _logger;
        private readonly SendMailIntegration _sendgrid;

//agregamos nueva sentencia
        private readonly ApplicationDbContext _context;
     

        public ContactoController(ILogger<ContactoController> logger, ApplicationDbContext context,SendMailIntegration sendgrid)
        {
            _logger = logger;
             _context = context; //AGREGAMOS
             _sendgrid = sendgrid;
            
        }

        public IActionResult Index()
        {
            return View("Index");
        }
 

        [HttpPost]
        public async Task<IActionResult> Create(Contacto objContacto)
        {
                _context.Add(objContacto);
                _context.SaveChanges();
                await _sendgrid.SendMail(objContacto.Email,
                objContacto.Name,
                objContacto.Subject,
                objContacto.Comment,
                SendMailIntegration.SEND_SENDGRID);
                ViewData["Message"] = "Se registro el contacto";
                return View("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}