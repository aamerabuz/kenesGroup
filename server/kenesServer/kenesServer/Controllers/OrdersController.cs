using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;

namespace kenesServer.Controllers
{
    public class OrdersController : Controller
    {
        [HttpGet]
        [Route("api/GetOrders")]
        public async Task<ActionResult<List<int>>> GetOrders()
        {
            var orders = new List<int>();
            try
            {
                string jsonContent = GetJsonData();
                dynamic jsonObject = JsonConvert.DeserializeObject(jsonContent);
                if (jsonObject != null)
                {
                    foreach (var order in jsonObject)
                    {
                        int id = order.order_id;
                        orders.Add(id);
                    }
                }
                return Ok(orders);
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately (e.g., log or return an error response)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("api/GetOrderById/{id}", Name = "GetOrderById")]
        public async Task<ActionResult<string>> GetOrderById(int id)
        {
            var order_ = new Object();
            try
            {
                string jsonContent = GetJsonData();
                dynamic jsonObject = JsonConvert.DeserializeObject(jsonContent);
                if (jsonObject != null)
                {
                    foreach (var order in jsonObject)
                    {
                        if (order.order_id == id)
                        {
                            Console.WriteLine("Hello World!");
                            order_ = order;
                        }
                    }
                }
                return Ok(JsonConvert.SerializeObject(order_));
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately (e.g., log or return an error response)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        public string GetJsonData()
        {
            try
            {
                string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "orders.json");
                string jsonContent = System.IO.File.ReadAllText(jsonFilePath);
                return jsonContent;
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately (e.g., log or return an error response)
                return $"Internal server error: {ex.Message}";
            }
        }
    }
}
