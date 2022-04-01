namespace Helper.Models
{
    public class LineOrderModel
    {
        //public int OrderId { get; set; }
        public string GTin { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }

        public string MerchantProductNo { get; set; }

        //public Product ProductItem { get; set; }
    }

    public class StockLocation
    {
        public int Id { get; set; }
        public string  Name { get; set; }
    }
}
