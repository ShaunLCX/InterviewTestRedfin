using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    public class FunctionController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Results = loopFunction(15);
            return View();
        }

        public List<string> loopFunction(int n) 
        {
            List<string> results = new List<string>();
            for (int i = 1; i <= n; i++)
            {
                if (i % 3 == 0 && i % 5 == 0)
                {
                    results.Add("fizzbuzz");
                }
                else if (i % 3 == 0)
                {
                    results.Add("buzz");
                }
                else if (i % 5 == 0)
                {
                    results.Add("buzz");
                }
                else
                {
                    results.Add(i.ToString());
                }
            }
            return results;

        }
    }
}
