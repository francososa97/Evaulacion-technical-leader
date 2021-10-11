using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMockBackup.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiMockAlerts : ControllerBase
    {

        [HttpGet]
        public IActionResult GetAlerta()
        {
            string responseApiMock = ApiMockSubterraneo.Helper.Helper.ObtenerRespuestaMock("Alerta");
            return Ok(JObject.Parse(responseApiMock).ToString());
        }

    }
}
