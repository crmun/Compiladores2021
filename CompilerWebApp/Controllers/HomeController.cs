using RegexToNFAProgramApp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompilerWebApp.Controllers
{
    public class HomeController : Controller
    {

        public string Analizar(String regex)
        {
            RegexToNFA regexToNFA = new RegexToNFA();
            Graph graph = regexToNFA.processRegexSequential(regex);
            //Graph graph = regexToNFA.processRegexSequential("(ab)*c*");
            //Graph graph = regexToNFA.processRegexSequential("a|b|c|d"); //NO SOPORTADO
            //Graph graph = regexToNFA.processRegexSequential("a|b");

            string strGraph = JsonConvert.SerializeObject(graph);
            return strGraph;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}