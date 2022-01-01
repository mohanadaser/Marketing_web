using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Marketing.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Marketing.Controllers
{
    public class ReportsController : Controller
    {

        private MyDbcontext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public ReportsController(MyDbcontext context,IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

       
        public IActionResult ProductsReports()
        {
            

            string mintype = "";
            int Extention = 1;

            var path = $"{this._webHostEnviroment.WebRootPath}\\Reports\\Report1.rdlc";


            LocalReport localreport = new LocalReport(path);
            localreport.AddDataSource("DataSet1", GetProducts());
            var result = localreport.Execute(RenderType.Pdf, Extention, null, mintype);
            return File(result.MainStream, "application/pdf");




        }

        public DataTable GetProducts()
        {
            List<Products> products = _context.products.ToList();
            var dt = new DataTable();
            dt.Columns.Add("pro_id");
            dt.Columns.Add("proname");
            dt.Columns.Add("typeid");
            dt.Columns.Add("price");

            foreach (var item in products)
            {
                dt.Rows.Add(
                item.pro_id.ToString(),
                item.proname.ToString(),
                item.typeid.ToString(),
                item.price.ToString() + "ج . م"

               );


            }
            return dt;
        }


    }
}
