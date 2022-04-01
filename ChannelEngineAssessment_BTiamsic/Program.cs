using Helper;
using Helper.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ChannelEngineAssessment_BTiamsic
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            ApiHelper.Initialize();
            string[] status = {"IN_PROGRESS"};
            var orders = await OrderProcessor.GetOrders(status);
            var lineOrders = OrderProcessor.GetMostSoldProducts(orders);

            var firstLineOrder = lineOrders.First();
            List<ProductModel> productToUpdate = new List<ProductModel>();
            productToUpdate.Add(new ProductModel
            {
                MerchantProductNo = firstLineOrder.MerchantProductNo,
                Name = firstLineOrder.Description,
                Stock = 25
            });


            //var updatedProduct = await OrderProcessor.UpdateProduct(productToUpdate);
            DisplayToConsole(orders, lineOrders, "");
        }

        public static void DisplayToConsole(List<OrderModel> orders, List<LineOrderModel> lineItems, string updateProductResult)
        {
            Console.WriteLine("These orders are still in progress.");
            foreach(OrderModel order in orders)
            {
                Console.WriteLine($"Id: {order.Id} || {order.Status}");
            }

            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("These are the most ordered products");
            foreach(LineOrderModel lineOrder in lineItems)
            {
                Console.WriteLine($"MerchantProductNo.: {lineOrder.MerchantProductNo}\r\n Description: {lineOrder.Description} \r\n Total Ordered: {lineOrder.Quantity}");
            }

            Console.WriteLine("---------------------------------------------");

            Console.WriteLine($"Product Update Status:  {updateProductResult}");
        }
    }
}
