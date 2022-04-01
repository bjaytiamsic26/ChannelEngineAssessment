using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Linq;
using Helper.Models;
using Newtonsoft.Json;
using System.Text;

namespace Helper
{
    public class OrderProcessor
    {
        public static async Task<List<OrderModel>> GetOrders(string[] status)
        {
            string url = ApiHelper.UrlBuilder("v2/orders");

            try
            {
                using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {

                        //var order = await response.Content.ReadAsAsync<List<OrderModel>>();

                        var orderContent = await response.Content.ReadAsAsync<OrderHeaderModel>();
                        //var orderStr = await response.Content.ReadAsStringAsync();
                        var orders = new List<OrderModel>();
                        foreach(var order in orderContent.Content)
                        {
                            orders.Add(order);
                        }

                        return orders;
                    }
                    else
                    {
                        throw new Exception();// response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public static List<LineOrderModel> GetMostSoldProducts(List<OrderModel> orders)
        {
            var lineOrders = new List<LineOrderModel>();
            foreach(OrderModel order in orders)
            {
                if (order.Lines.Count > 0)
                {
                    lineOrders.AddRange(order.Lines);
                }
            }

            if(lineOrders.Count > 0)
            {
                var topProducts =
                   (from top5 in
                       (from item in lineOrders
                        group item by item.MerchantProductNo into i

                        select new LineOrderModel
                        {
                            GTin = i.First().GTin,
                            Quantity = i.Sum(x => x.Quantity),
                            MerchantProductNo = i.First().MerchantProductNo,
                            Description = i.First().Description
                        })
                    orderby top5.Quantity descending
                    select top5).Take(5);
                        ;

                return topProducts.ToList();
            }
            else
            {
                throw new ArgumentNullException("No data available");
            }
        }

        public static async Task<string> UpdateProduct(List<ProductModel> product)
        {
            string url = Constants.BaseUri+"v2/products";
            var content = JsonConvert.SerializeObject(product);
            HttpContent cont = new StringContent(content, Encoding.UTF8, "application/json");

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress= new Uri(url + "?ignoreStock=false");
                    //client.DefaultRequestHeaders.Add("api-key",Constants.ApiKey);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("api-key", Constants.ApiKey);

                    var updateResult =  await client.PostAsync("",cont);

                    if (updateResult.IsSuccessStatusCode)
                    {
                        return updateResult.RequestMessage.ToString();
                    }
                    else
                    {
                        throw new Exception(updateResult.ReasonPhrase.ToString());
                    }
                   
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
