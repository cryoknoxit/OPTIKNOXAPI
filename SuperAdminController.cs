using Azure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client.Extensions.Msal;
using Newtonsoft.Json;
using OptiKnoxAPI.Models;
using OptiKnoxAPI.Services;
using System.Data;
using System.IO;

namespace OptiKnoxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperAdminController : ControllerBase
    {
        private readonly SuperAdmin objResult = new SuperAdmin();
        
        private readonly User objRoleResult = new User();
        private readonly Employee objEmployee = new Employee();

       // private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IAzureStorage _storage;
        public SuperAdminController(IConfiguration configuration, IAzureStorage storage, IWebHostEnvironment webHostEnvironment)
        {
          //  _configuration = configuration;
            _hostingEnvironment = webHostEnvironment;
            _storage= storage;
        }

        #region Master Data
        [HttpGet]
        [Route("get-master-Departments")]
        public ActionResult<DataTable> GetMasterDepartments()
        {
            var dtMasterDepartments = objResult.MasterDepartments();
            return Ok(JsonConvert.SerializeObject(dtMasterDepartments, new JsonSerializerSettings()));
        }
        [HttpGet]
        [Route("get-master-roles")]
        public ActionResult<DataTable> GetMasterRoles()
        {
            var dtMasterRoles = objResult.MasterRoles();
            return Ok(JsonConvert.SerializeObject(dtMasterRoles, new JsonSerializerSettings()));
        }

        [HttpGet]
        [Route("get-master-Branch")]
        public ActionResult<DataTable> GetMasterBranch(string Organizationid)
        {
            var dtMasterRoles = objResult.MasterBranch(Organizationid);
            return Ok(JsonConvert.SerializeObject(dtMasterRoles, new JsonSerializerSettings()));
        }

        [HttpGet]
        [Route("get-Active-Users")]
        public ActionResult<DataTable> GetActiveusers(string BranchId, string Organizationid)
        {
            var dtusers = objRoleResult.GetActiveUsers(Organizationid);
            return Ok(JsonConvert.SerializeObject(dtusers, new JsonSerializerSettings()));
        }


        [HttpGet]
        [Route("get-Master-Employee")]
        public ActionResult<DataTable> GetMasterEmployee(string BranchId, string Organizationid)
        {
            var dtEmployee = objEmployee.GetMasterEmployee(BranchId,Organizationid);
            return Ok(JsonConvert.SerializeObject(dtEmployee, new JsonSerializerSettings()));
        }
        [HttpGet]
        [Route("get-Master-Employee-by-Branch")]
        public ActionResult<DataTable> GetMasterEmployeebybranch(string BranchId, string Organizationid)
        {
            var dtEmployee = objEmployee.GetMasterEmployeebyBranch(BranchId, Organizationid);
            return Ok(JsonConvert.SerializeObject(dtEmployee, new JsonSerializerSettings()));
        }

        [HttpGet]
        [Route("get-Employee-Designation")]
        public ActionResult<DataTable> GetEmployeeDesignation()
        {
            var dtDesignation = objEmployee.GetEmployeeDesignation();
            return Ok(JsonConvert.SerializeObject(dtDesignation, new JsonSerializerSettings()));
        }

        [HttpGet]
        [Route("get-organization-Image")]
        public async Task<ActionResult<DataTable>> GetOrgImage(string FileName,string FolderName)
        {
            string filename = FolderName + "/" + FileName;
            BlobDto response = await _storage.DownloadAsync(filename);
            var result = response.Uri;
            return Ok(JsonConvert.SerializeObject(result, new JsonSerializerSettings()));
        }

        #endregion


        #region Create Branch
        [HttpPost]
        [Route("Branch-Creation")]
        public ActionResult<DataTable> BranchCreation(Branch obj)
        {
            var dtBranchDetails = obj.AddBranchDetails(obj);
            return Ok(JsonConvert.SerializeObject(dtBranchDetails, new JsonSerializerSettings()));
        }

        #endregion

        #region Create Employee
        [HttpPost]
        [Route("Employee-Creation")]
        public ActionResult<DataTable> EmployeeCreation(Employee obj)
        {
            var dtEmployeedetals = objEmployee.AddEmployeeDetails(obj);
            return Ok(JsonConvert.SerializeObject(dtEmployeedetals, new JsonSerializerSettings()));
        }

        #endregion




        #region Create Role

        [HttpPost]
        [Route("User-Creation")]
        public ActionResult<DataTable> UserCreation(User obj)
        {
            var dtUserDetails = obj.AddUserDetails(obj);
            return Ok(JsonConvert.SerializeObject(dtUserDetails, new JsonSerializerSettings()));
        }

        [HttpPost]
        [Route("User-Update")]
        public ActionResult<DataTable> UserUpdate(User obj)
        {
            var dtUserDetails = obj.UpdateUserDetails(obj);
            return Ok(JsonConvert.SerializeObject(dtUserDetails, new JsonSerializerSettings()));
        }

        [HttpPost]
        [RequestSizeLimit(long.MaxValue)]
        [Route("Orgnization-file-Upload")]
        
        public async Task<ActionResult<DataTable>> worklogUploadfiles([FromForm] FileModel fileModel)
        {
            string filename = ""; string Responcestatus = "";
            foreach (var file in fileModel.TileImages)
            {
                filename = fileModel.Name + "/" + fileModel.OrdId;
                BlobResponseDto response = await _storage.UploadAsync(file, filename, fileModel.OrdId);
                Responcestatus = response.Status;
                
            }
            return Ok(JsonConvert.SerializeObject(Responcestatus, new JsonSerializerSettings()));

        }

        [HttpGet]
        [Route("GetOrg-Profile")]
        public ActionResult<DataTable>  GetOrganizationProfile(string imageName)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            string imagePath = "images/"+ imageName; 
            string fullPath = Path.Combine(webRootPath, imagePath);
            return Ok(JsonConvert.SerializeObject(fullPath, new JsonSerializerSettings()));
        }

        [HttpGet]
        [Route("Update-Record-Status")]
        public ActionResult<DataTable> UpdateRecordStatus(string BranchId, string Organizationid,string TableName,string StatusID,string ID)
        {
            User objuser=new User();
            var dtstatus = objuser.UpdateRecordStatus(BranchId, Organizationid, TableName, StatusID, ID);
            return Ok(JsonConvert.SerializeObject(dtstatus, new JsonSerializerSettings()));
        }
        #endregion
    }
}
