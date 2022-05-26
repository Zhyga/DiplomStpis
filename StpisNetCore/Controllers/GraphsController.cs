using Microsoft.AspNetCore.Mvc;
using StpisNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace StpisNetCore.Controllers
{
    public class GraphsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ModelsContext _dbContext;
        private FileInfo FileInfo;
        private List<tblRegions> regions;
        private List<tblProducts> products;

        public GraphsController(ModelsContext dbContext)
        {
            _dbContext = dbContext;
            regions = _dbContext.region.ToList();
            products = _dbContext.products.ToList();
        }
        public IActionResult Index()
        {
            ViewData["Clients"] = _dbContext.clients.ToList();
            List<tblStatus> Statuses = _dbContext.status.ToList();
            foreach(var status in Statuses)
            {
                status.Status.Trim();
            }
            ViewData["Status"] = Statuses;
            List<tblDeliveries> deliveries = _dbContext.deliveries.ToList();
            List<tblRegions> regions = _dbContext.region.ToList();
            foreach(var region in regions)
            {
                foreach(var delivery in deliveries)
                {
                    if(delivery.RegionId == region.Id)
                    {
                        delivery.Region = region;
                    }
                }
            }
            ViewData["Deliveries"] = deliveries;
            ViewData["Regions"] = regions;
            return View();
        }

        public IActionResult CreateDocument()
        {
            List<tblDeliveries> deliveries = _dbContext.deliveries.ToList();
            deliveries.ForEach(d => d.Region = _dbContext.region.Find(d.RegionId));
            List<KeyValuePair<string, string>> RegionAmount = new List<KeyValuePair<string, string>>();
            foreach (tblRegions Region in regions)
            {
                List<tblDeliveries> temp = deliveries.FindAll(d => d.RegionId == Region.Id);
                RegionAmount.Add(new KeyValuePair<string, string>(Region.RegionName.Substring(0,15).ToString(), temp.Count.ToString()));
            }
            string LowDate = deliveries[0].DeliveryDate.ToString();
            string HighDate = deliveries[0].DeliveryDate.ToString();
            for (int i = 1; i < deliveries.Count; i++)
            {
                if(deliveries[i-1].DeliveryDate < deliveries[i].DeliveryDate)
                {
                    LowDate = deliveries[i - 1].DeliveryDate.ToString();
                }
                if (deliveries[i - 1].DeliveryDate < deliveries[i].DeliveryDate)
                {
                    HighDate = deliveries[i].DeliveryDate.ToString();
                }
            }

            List<tblOrders> orders = _dbContext.orders.ToList();
            int amount = 0;
            foreach(tblOrders order in orders)
            {
                amount += order.ProductAmount;
            }

            List<KeyValuePair<string, string>> ProductAmount = new List<KeyValuePair<string, string>>();
            List<tblOrders> productAmount = _dbContext.deliveries.Join(_dbContext.orders, d => d.OrderId, o => o.Id,
                    (d, o) => new tblOrders
                    {
                        ProductAmount = o.ProductAmount,
                        ProductId = o.ProductId,
                    }).ToList();
            foreach (tblProducts product in products)
            {
                int counter = 0;
                foreach(tblOrders orders1 in productAmount) {
                    if (product.Id == orders1.ProductId)
                    {
                        counter++;
                    }
                }
                ProductAmount.Add(new KeyValuePair<string, string>(product.Title.Substring(0,20), counter.ToString()));
            }
            ProductAmount.OrderBy(p => p.Value);

            string FileName = "Template.docx";
            if (System.IO.File.Exists(FileName))
            {
                FileInfo = new FileInfo(FileName);
                var Items = new Dictionary<string, string>
                {
                    { "<DATE1>" , LowDate},
                    { "<DATE2>" , HighDate},
                    { "<AMOUNT>" , amount.ToString()},
                    { "<REGION1>" , RegionAmount[0].Key},
                    { "<REGION2>" , RegionAmount[1].Key},
                    { "<REGION3>" , RegionAmount[2].Key},
                    { "<REGION4>" , RegionAmount[3].Key},
                    { "<REGION5>" , RegionAmount[4].Key},
                    { "<REGION6>" , RegionAmount[5].Key},
                    { "<REGION7>" , RegionAmount[6].Key},
                    { "<AMOUNT1>" , RegionAmount[0].Value},
                    { "<AMOUNT2>" , RegionAmount[1].Value},
                    { "<AMOUNT3>" , RegionAmount[2].Value},
                    { "<AMOUNT4>" , RegionAmount[3].Value},
                    { "<AMOUNT5>" , RegionAmount[4].Value},
                    { "<AMOUNT6>" , RegionAmount[5].Value},
                    { "<AMOUNT7>" , RegionAmount[6].Value},
                    { "<PRODUCT1>" , ProductAmount[0].Key},
                    { "<PRODUCT2>" , ProductAmount[1].Key},
                    { "<PRODUCT3>" , ProductAmount[2].Key},
                    { "<PRODUCT4>" , ProductAmount[3].Key},
                    { "<PRODUCTAMOUNT1>" , ProductAmount[0].Value},
                    { "<PRODUCTAMOUNT2>" , ProductAmount[1].Value},
                    { "<PRODUCTAMOUNT3>" , ProductAmount[2].Value},
                    { "<PRODUCTAMOUNT4>" , ProductAmount[3].Value}
                };

                

                Process(Items);
            }
            else
            {
                throw new ArgumentException("File not found");
            }

            return RedirectToAction("Index");
        }

        public bool Process(Dictionary<string, string> Items)
        {
            Word.Application app = null;
            try
            {
                app = new Word.Application();
                Object file = FileInfo.FullName;
                Object missing = Type.Missing;

                app.Documents.Open(file);

                foreach(var item in Items)
                {
                    Word.Find find = app.Selection.Find;
                    find.Text = item.Key;
                    find.Replacement.Text = item.Value;

                    Object wrap = Word.WdFindWrap.wdFindContinue;
                    Object replace = Word.WdReplace.wdReplaceAll;

                    find.Execute(FindText: Type.Missing,
                        MatchCase: false,
                        MatchWholeWord: false,
                        MatchWildcards: false,
                        MatchSoundsLike: missing,
                        MatchAllWordForms: false,
                        Forward: true,
                        Wrap: wrap,
                        Format: false,
                        ReplaceWith: missing, Replace: replace);
                }

                Object newFilePath = Path.Combine(FileInfo.DirectoryName, DateTime.Now.ToString("yyyyMMdd HHmmss") + FileInfo.Name);
                app.ActiveDocument.SaveAs2(newFilePath);
                app.ActiveDocument.Close();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (app != null)
                {
                    app.Quit();
                }
            }
            return false;
        }
    }
}
