using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMockSubterraneo.Helper
{
    public class Helper
    {
        public static string ObtenerRespuestaMock(string nombreApi)
        {
            string ubicacionApi = nombreApi == "Forecast" ? @"ResponseMock\ResponseServiceForecastGTFS.json" : @"ResponseMock\ResponseServiceAlerts.json";
            string response = File.ReadAllText(ubicacionApi);
            return response;
        }
    }
}
