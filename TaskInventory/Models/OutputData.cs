namespace TaskInventory.Models
{
    //VM
    public class OutputData
    {
        public string ProductCode { get; set; }
        public string EventType { get; set; }
        public string SalePrice { get; set; }
        public string PurchasePrice { get; set; }
        public string PurchaseQty { get; set; }
        public string SaleQty { get; set; }
        public string TotalPurchaseAmount { get; set; }
        public string TotalSaleAmount { get; set; }
        public string ClosingQty { get; set; }
        public string OpeningStock { get; set; }
        public string ProfitLoss { get; set; }
        public string Date { get; set; }
    }
}
