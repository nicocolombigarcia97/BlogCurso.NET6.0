using AppWeb1.Models;
using AppWeb1.Rules;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppWeb1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var rule = new PublicacionRule(_configuration);
            var posts = rule.GetOnePostHome();
            return View(posts);
        }

        public IActionResult Post(string id)
        {
            var rule = new PublicacionRule(_configuration);
            var post = rule.GetPostById(int.Parse(id));
            if (post == null)
            {
                return View("Index");
            }
            return View(post);
        }

        public IActionResult Contacto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contacto(Contacto contacto)
        {
            if (!ModelState.IsValid)
            {
                return View("Contacto", contacto);
            }
            var rule = new ContactoRule(_configuration);
            var mensaje = @"<h1>Gracias por contactarnos</h1>
                            <p>A la brevedad nos pondremos en contacto</p>
                            <hr /><p>Saludos</p><p><b>Blog El Mundo</b></p>";
            rule.SendEmail(contacto.Email, mensaje, "Mensaje recibido", "Blog El Mundo");

            return View("contacto");
        }
        public IActionResult AcercaDe()
        {
            return View();
        }
        public IActionResult Suerte()
        {
            var rule = new PublicacionRule(_configuration);
            var post = rule.GetOnePostRandom();
            return View(post);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}