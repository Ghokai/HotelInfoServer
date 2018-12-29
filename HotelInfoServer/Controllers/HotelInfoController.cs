using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using HotelInfoServer.Managers;

namespace HotelInfoServer.Controllers
{
    [Route("api/[controller]")]
    public class HotelInfoController : Controller
    {
        private IHotelInfoFileManager _manager;
        //IHotelInfoFileManager will be injected from Configured Services from startup.cs
        public HotelInfoController(IHotelInfoFileManager manager)
        {
            _manager = manager;
        }

        [Route("[action]")]
        [HttpGet]
        public string Test()
        {
            return "it's working:)";
        }

        //reads file,
        //parses into models,
        //validates(validation types are configurable from appsettings.json ),
        //models sorted by field and it's direction by given information
        //write input file and output format files (which are configurable from appsettings.json )
        //we can manage validation rules and  output formats from appsettings (without changing a single line of code) at runtime
        [Route("[action]")]
        [HttpPost]
        public IActionResult Upload(IFormCollection form)
        {
            try
            {
                _manager.ProcessFile(form);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //reads file,
        //parses into models,
        //validates(validation types are configurable from appsettings.json ),
        //models sorted by field and it's direction by given information
        //returns models to client as json
        [Route("[action]")]
        [HttpPost]
        public IActionResult Validate(IFormCollection form)
        {
            try
            {
                var items = _manager.ParseHotelInfoToList(form);
                return Json(items);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}