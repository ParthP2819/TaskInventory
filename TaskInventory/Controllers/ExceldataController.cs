using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using TaskInventory.Models;

namespace TaskInventory.Controllers
{
    public class ExceldataController : Controller
    {

        static dynamic Tdata;
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormFile file)
        {
            List<OutputData> Output = new List<OutputData>();
            List<InputData> Input = new List<InputData>();

            var productCode = "";
            double PurchasePrice = 0;
            double SalePrice = 0;
            var PurchaseQty = "";
            double TotalPurchaseAmt = 0;
            double SaleQty = 0;
            double TotalSaleAmt = 0;
            double ProfitLoss = 0;
            double ClosingQty = 0;
            double OpenngStock = 0;
         
            string path = Directory.GetCurrentDirectory();
            string FileName = Path.Combine(path, "wwwroot", "inevtorytask.xlsx");
            var data = new List<InputData>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = file.OpenReadStream())//System.IO.File.Open(FileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        data.Add(new InputData()
                        {
                            ProductCode = reader.GetValue(0).ToString(),
                            EventType = reader.GetDouble(1),
                            Quantity = reader.GetDouble(2),
                            PricePerQuantity = reader.GetDouble(3),
                            Date = reader.GetDateTime(4),
                        });
                    }
                }
            }

            var monthwise = data.GroupBy(x => x.Date.Month).ToList();

            foreach (var month in monthwise)
            {
                var product = month.GroupBy(x => x.ProductCode).ToList();
                foreach (var type in product)
                {
                    var date = DateTime.Now;
                    var ProductCode = "";
                    PurchaseQty = "";
                    PurchasePrice = 0;
                    TotalPurchaseAmt = 0;
                    SaleQty = 0;
                    SalePrice = 0;
                    TotalSaleAmt = 0;
                    ProfitLoss = 0;
                    ClosingQty = 0;
                    OpenngStock = 0;
                    foreach (var item in type)
                    {
                        date = item.Date;
                        ProductCode = item.ProductCode;
                        if (item.EventType == 1)
                        {
                            PurchaseQty = item.Quantity.ToString();
                            PurchasePrice = item.PricePerQuantity;
                            TotalPurchaseAmt = Convert.ToDouble(PurchaseQty) * (PurchasePrice);
                        }
                        else if (item.EventType == 2)
                        {
                            SaleQty = item.Quantity;
                            SalePrice = item.PricePerQuantity;
                            TotalSaleAmt = Convert.ToDouble(SaleQty) * (SalePrice);
                        }
                        ProfitLoss = Convert.ToInt32(TotalSaleAmt) - Convert.ToInt32(SaleQty * PurchasePrice);
                        //ClosingQty = Convert.ToInt32(PurchaseQty) - Convert.ToInt32(SaleQty);



                    }
                    Output.Add(new OutputData
                    {
                        Date = date.ToString("MMMM/yyyy"),
                        ProductCode = ProductCode,
                        PurchaseQty = PurchaseQty,
                        PurchasePrice = PurchasePrice.ToString(),
                        TotalPurchaseAmount = TotalPurchaseAmt.ToString(),
                        SaleQty = SaleQty.ToString(),
                        SalePrice = SalePrice.ToString(),
                        TotalSaleAmount = TotalSaleAmt.ToString(),
                        ProfitLoss = ProfitLoss.ToString(),
                        ClosingQty = ClosingQty.ToString(),
                        OpeningStock = OpenngStock.ToString(),
                    });
                }
            }
           //Tdata = InuptData.ToList();

            return View("Excel",Output);
        }
        [HttpGet]
        public IActionResult Excel(OutputData output)
        {
            return View();
        }
    }
}
