using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMockSubterraneo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiMockForecast : ControllerBase
    {
        [HttpGet]
        public IActionResult GetForecast()
        {
            string responseApiMock = ApiMockSubterraneo.Helper.Helper.ObtenerRespuestaMock("Forecast");
            return Ok(JObject.Parse(responseApiMock).ToString());
        }
    }
}
