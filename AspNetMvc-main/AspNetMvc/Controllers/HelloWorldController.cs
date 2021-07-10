using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvc.Controllers
{
    public class HelloWorldController : Controller
    {
        public string Index()
        {
            return "This is my default action...";
        }

        public string Welcome()
        {
            return "This is the welcome method";
        }

        // https://localhost:{PORT}/HelloWorld/Welcome?name=Rick&numtimes=4
        public string Welcome1(string name, int numTimes = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
        }

        // https://localhost:{PORT}/HelloWorld/Welcome/3?name=Rick
        public string Welcome2(string name, int id = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, ID: {id}");
        }
    }
}