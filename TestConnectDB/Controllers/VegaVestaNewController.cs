using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using TestConnectDB.Models;

namespace TestConnectDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VegaVestaNewController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public VegaVestaNewController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public string GetVegaVesta()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("VegaVestaNewCon").ToString());
            SqlDataAdapter da= new SqlDataAdapter("SELECT * FROM VegaBrand", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<VegaVesta>vegaVestaList = new List<VegaVesta>();
            Response response = new Response();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                { 
                    VegaVesta vegaVesta = new VegaVesta();
                    vegaVesta.ID =Convert.ToInt32(dt.Rows[i]["ID"]);
                    vegaVesta.VegaBrandName = Convert.ToString(dt.Rows[i]["VegaBrandName"]);
                    vegaVestaList.Add(vegaVesta);
                }
            }
            if (vegaVestaList.Count > 0)
                return JsonConvert.SerializeObject(vegaVestaList);
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "Not found";
                return JsonConvert.SerializeObject(response);
            }
        }
    }
}
