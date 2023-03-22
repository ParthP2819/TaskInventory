namespace TaskInventory.Models
{
    public class InputData
    {
        public string ProductCode { get; set; }
        public double EventType { get; set; }
        public double Quantity { get; set; }
        public double PricePerQuantity { get; set; }
        public string PurchaseQty { get; set; }
        public string SaleQty { get; set; }
        public string TotalPurchaseAmount { get; set; }
        public string TotalSaleAmount { get; set; }
        public string ClosingQty { get; set; }
        public string OpeningStock { get; set; }
        public string ProfitLoss { get; set; }
        public DateTime Date { get; set; }
    }
}
