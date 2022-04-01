using System;
using Xunit;
using Helper.Models;
using Helper;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UnitTestChannelEngineAssessment
{
    public class ChannelEngineTest
    {
        [Fact]
        public async void CompareMostProductsSold()
        {
            //Arrange
            ApiHelper.Initialize();
            string[] status = { "IN_PROGRESS" };
            var orders = await OrderProcessor.GetOrders(status);

            List<LineOrderModel> lineOrdersArrange = new List<LineOrderModel>();
            lineOrdersArrange.Add(new LineOrderModel { Description = "T-shirt met lange mouw BASIC petrol: S", MerchantProductNo = "001201-S", GTin = "8719351029609", Quantity = 4 });
            lineOrdersArrange.Add(new LineOrderModel { Description = "T-shirt met lange mouw BASIC petrol: M", MerchantProductNo = "001201-M", GTin = "8719351029609", Quantity = 4 });
            lineOrdersArrange.Add(new LineOrderModel { Description = "T-shirt met lange mouw BASIC petrol: XL", MerchantProductNo = "001201-XL", GTin = "8719351029609", Quantity = 1 });
            lineOrdersArrange.Add(new LineOrderModel { Description = "T-shirt met lange mouw BASIC petrol: L", MerchantProductNo = "001201-L", GTin = "8719351029609", Quantity = 1 });
            
            //Actual
            var mostSold = OrderProcessor.GetMostSoldProducts(orders);

            var actual = JsonConvert.SerializeObject(mostSold);
            var statLines = JsonConvert.SerializeObject(lineOrdersArrange);
            //Assert
            Assert.Equal(actual, statLines);


        }
    }
}
