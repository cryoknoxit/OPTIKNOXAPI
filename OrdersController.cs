using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OptiKnoxAPI.Models;
using OptiKnoxAPI.Services;
using System.Data;
namespace OptiKnoxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly PurchaseOrder objPurchase = new PurchaseOrder();
        private readonly salesOrder objSales = new salesOrder();
        private readonly EyeTesting objEyetesting = new EyeTesting();
        //private readonly IConfiguration _configuration;
        //private readonly IWebHostEnvironment _hostingEnvironment;
        //private readonly IAzureStorage _storage;
        public OrdersController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            //_configuration = configuration;
            //_hostingEnvironment = webHostEnvironment;
        }
        [HttpGet]
        [Route("get-Orders-Status")]
        public ActionResult<DataTable> GetOrderStatus()
        {
            var dtOrder = objSales.GetOrderStatus();
            return Ok(JsonConvert.SerializeObject(dtOrder, new JsonSerializerSettings()));
        }

        [HttpGet]
        [Route("get-Purchase-Orders")]
        public ActionResult<DataTable> GetActiveusers(string BranchId, string Organizationid)
        {
            var dtPurchase = objPurchase.GetPurchaseOrders(BranchId, Organizationid);
            return Ok(JsonConvert.SerializeObject(dtPurchase, new JsonSerializerSettings()));
        }

        [HttpGet]
        [Route("get-Purchase-Invoice")]
        public ActionResult<DataTable> GetPurchaseInvoice(string BranchId, string Organizationid,string PurchaseId)
        {
            var dtPurchaseInvoice = objPurchase.GetPurchaseInvoice(BranchId, Organizationid, PurchaseId);
            return Ok(JsonConvert.SerializeObject(dtPurchaseInvoice, new JsonSerializerSettings()));
        }

        [HttpGet]
        [Route("get-all-Sales-Invoice")]
        public ActionResult<DataTable> GetSaleInvoice(string BranchId, string Organizationid)
        {
            var dtPurchaseInvoice = objSales.GetAllSalesInvoice(BranchId, Organizationid);
            return Ok(JsonConvert.SerializeObject(dtPurchaseInvoice, new JsonSerializerSettings()));
        }

        [HttpGet]
        [Route("get-Sales-Order-Form")]
        public ActionResult<DataTable> GetSalesInvoice(string BranchId, string Organizationid, string SalesId)
        {
            var dtPurchaseInvoice = objSales.GetSalesInvoiceDetails(BranchId, Organizationid, SalesId);
            return Ok(JsonConvert.SerializeObject(dtPurchaseInvoice, new JsonSerializerSettings()));
        }

        [HttpGet]
        [Route("get-Purchase-Inventory-Barcode")]
        public ActionResult<DataTable> GetPurchaseInventoryBarcode(string BranchId, string Organizationid, string PurchaseId)
        {
            var dtPurchaseBarcodes = objPurchase.GetPurchaseInventoryBarcodes(BranchId, Organizationid, PurchaseId);
            return Ok(JsonConvert.SerializeObject(dtPurchaseBarcodes, new JsonSerializerSettings()));
        }

        [HttpGet]
        [Route("Update-Inventory-Stock-Status")]
        public ActionResult<DataTable> GetPurchaseInventoryBarcode(string BranchId, string Organizationid, string InventoryID, string Status)
        {
            var dtPurchaseBarcodes = objPurchase.UpdateInventoryStockStatus(BranchId, Organizationid, InventoryID, Status);
            return Ok(JsonConvert.SerializeObject(dtPurchaseBarcodes, new JsonSerializerSettings()));
        }
        [HttpGet]
        [Route("get-Inventory-Stock")]
        public ActionResult<DataTable> GetInventoryStock(string BranchId, string Organizationid)
        {
            var dtInventorySorck = objPurchase.GetInventeryStock(BranchId, Organizationid);
            return Ok(JsonConvert.SerializeObject(dtInventorySorck, new JsonSerializerSettings()));
        }
        [HttpGet]
        [Route("get-Product-Detasils-by-Barcode")]
        public ActionResult<DataTable> GetProductDetasilsbyBarcode(string BranchId, string Organizationid,string ProductCode)
        {
            var dtInventorySorck = objPurchase.GetProductDetasilsbyBarcode(BranchId, Organizationid,ProductCode);
            return Ok(JsonConvert.SerializeObject(dtInventorySorck, new JsonSerializerSettings()));
        }


        [HttpGet]
        [Route("get-Eyetesting-Master-Data")]
        public ActionResult<DataTable> GetEyetestingMasterData(string BranchId, string Organizationid)
        {
            var dtInventorySorck = objEyetesting.GetEyeTestingMasterData(BranchId, Organizationid);
            return Ok(JsonConvert.SerializeObject(dtInventorySorck, new JsonSerializerSettings()));
        }

        [HttpGet]
        [Route("get-Eyetesting-Invoice-Data")]
        public ActionResult<DataTable> GetEyetestingInvoiceData(string BranchId, string Organizationid,string CaseId)
        {
            var dtInventorySorck = objEyetesting.GetEyeTestingInvoiceData(BranchId, Organizationid, CaseId);
            return Ok(JsonConvert.SerializeObject(dtInventorySorck, new JsonSerializerSettings()));
        }

        [HttpPost]
        [Route("Purchase-Order-Creation")]
        public ActionResult<DataTable> PurchaseOrderCreation(PurchaseOrder obj)
        {
            string ResultMessage = "";
            ResultMessage = obj.SavePurchaseOrders(obj);
            return Ok(JsonConvert.SerializeObject(ResultMessage, new JsonSerializerSettings()));
        }

        [HttpPost]
        [Route("EyeTesting-MasterData-Creation")]
        public ActionResult<DataTable> EyeTestingMasterDataCreation(EyeTesting obj)
        {
            DataTable Dt = obj.SaveEyeTesting(obj);
            return Ok(JsonConvert.SerializeObject(Dt, new JsonSerializerSettings()));
        }

        [HttpPost]
        [Route("EyeTesting-Prescription")]
        public ActionResult<DataTable> EyeTestingPrescription(EyeTestingPrescription obj)
        {
            var dtPrescription = obj.SavePrescription(obj);
            return Ok(JsonConvert.SerializeObject(dtPrescription, new JsonSerializerSettings()));
        }

        [HttpPost]
        [Route("EyeTesting-ContactLens-Prescription")]
        public ActionResult<DataTable> EyeTestingContactLensPrescription(EyeTestingContactLensPrescriptionPrescription obj)
        {
            var dtPrescription = obj.SavePrescription(obj);
            return Ok(JsonConvert.SerializeObject(dtPrescription, new JsonSerializerSettings()));
        }
        [HttpPost]
        [Route("EyeTesting-Transpose-Prescription")]
        public ActionResult<DataTable> EyeTestingTransposePrescription(EyeTestingTransposePrescriptionPrescription obj)
        {
            var dtPrescription = obj.SavePrescription(obj);
            return Ok(JsonConvert.SerializeObject(dtPrescription, new JsonSerializerSettings()));
        }

        [HttpPost]
        [Route("EyeTesting-Payment")]
        public ActionResult<DataTable> EyeTestingPayment(EyeTestingPayment obj)
        {
            var dtPayment = obj.SaveEyeTestingPayment(obj);
            return Ok(JsonConvert.SerializeObject(dtPayment, new JsonSerializerSettings()));
        }

        [HttpPost]
        [Route("Sales-Order-Creation")]
        public ActionResult<DataTable> SalesOrderMasterDataCreation(salesOrder obj)
        {
            string ResultMessage = "";
            ResultMessage = obj.SavesalesOrder(obj);
            return Ok(JsonConvert.SerializeObject(ResultMessage, new JsonSerializerSettings()));
        }

        [HttpPost]
        [Route("Sales-EyeOrder-Presecption")]
        public ActionResult<DataTable> SalesOrderMasterDataCreation(SalesEyeTestingPrescription obj)
        {
            salesOrder objsales = new salesOrder();
            bool resultMessage = false;
            resultMessage = objsales.SavePrescription(obj);
            return Ok(JsonConvert.SerializeObject(resultMessage, new JsonSerializerSettings()));
        }


    }
}
