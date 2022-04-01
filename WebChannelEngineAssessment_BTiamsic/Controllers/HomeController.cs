using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Helper;
using Newtonsoft.Json.Serialization;
using WebChannelEngineAssessment_BTiamsic.Models;
using Helper.Models;

namespace WebChannelEngineAssessment_BTiamsic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            string[] status = { "IN_PROGRESS" };
            var orders = await  OrderProcessor.GetOrders(status);
            var mostSold = OrderProcessor.GetMostSoldProducts(orders);
            var product = new ProductModel
            {
                MerchantProductNo = mostSold.First().MerchantProductNo,
                Name = mostSold.First().Description,
                Stock = 25
            };

            var combined = new ListOrderMostSoldItemModeL
            {
                Orders = orders,
                Lines = mostSold,
                UpdatingProduct = product
            };

            return View(combined);
        }

        public async Task<IActionResult> UpdateProduct(ProductModel product)
        {
            List<ProductModel> products = new List<ProductModel>();
            products.Add(product);
            var result = await OrderProcessor.UpdateProduct(products);

            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
