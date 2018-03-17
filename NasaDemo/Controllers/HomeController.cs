using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NasaDemo.Models;
using RestSharp;

namespace NasaDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult NasaImages(string StartDate = "", string EndDate = "", string Orden = "")
        {
            DateTime startDate;
            DateTime endDate;

            if (StartDate == string.Empty || !DateTime.TryParse(StartDate, out startDate))
            {
                StartDate = DateTime.Now.ToString("yyyy-MM-dd");
                startDate = DateTime.Now;
            }

            if (EndDate == string.Empty || !DateTime.TryParse(EndDate, out endDate))
            {
                EndDate = DateTime.Now.ToString("yyyy-MM-dd");
                endDate = DateTime.Now;
            }

            ViewBag.StartDate = startDate.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate.ToString("yyyy-MM-dd");

            var key = "VFpwMJIVaLWhuSQc9cG386d0zlWOb5PZPpc9jFa7";
            var parameters = $"&start_date={StartDate}&end_date={EndDate}";
            var client = new RestClient($@"https://api.nasa.gov/planetary/apod?api_key={key}&{parameters}");
            var response = client.ExecuteAsGet<List<NasaAPODViewModel>>(new RestRequest(), "GET");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (Orden == string.Empty)
                {
                    Orden = "Ascendente";
                }

                ViewBag.Orden = Orden;
                switch (Orden)
                {
                    case "Ascendente":
                        return View(response.Data.OrderBy(g => g.Date).ToList());
                    case "Descendente":
                        return View(response.Data.OrderByDescending(g => g.Date).ToList());
                    default:
                        return View(response.Data);
                }
            }
            else
            {
                ViewBag.Message(response.ErrorMessage);
                return View();
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
