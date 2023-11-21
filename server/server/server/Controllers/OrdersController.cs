using System;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Xml.Linq;

namespace server.Controllers
{
    public class OrdersController : Controller
    {
        //Get: api/Tasks
        [HttpGet]
        [Route("api/GetOrders")]
        public async  Task<ActionResult<List<int>>> GetOrders()
        {
            var orders = new List<int>();
            try{
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

        // GET: api/Orders/5
        [HttpGet("{id}", Name = "GetOrderById")]
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
                        if(order.order_id == id)
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
                //dynamic jsonObject = JsonConvert.DeserializeObject(jsonContent);
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




// Define a model class to represent the structure of your JSON file
public class NumbersModel
{
    public List<int> Numbers { get; set; }
}
