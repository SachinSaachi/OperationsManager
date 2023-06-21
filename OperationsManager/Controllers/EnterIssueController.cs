using Common.SessionExtensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OManager_Core.businessLogic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using static Common.Models.BOIssue;
using static Common.Models.BOLogin;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace OperationsManager.Controllers
{
    public class EnterIssueController : Controller
	{
#pragma warning disable IDE0044 // Add readonly modifier
        private IHttpContextAccessor context;
#pragma warning restore IDE0044 // Add readonly modifier

        private IBLItissue _bLItissue;
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        IssueOut oBOIssueOut = new IssueOut();
        string attach = "";
        public EnterIssueController(IBLItissue bLItissue,  IHttpContextAccessor httpContextAccessor, IConfiguration maxFileSize, IHostingEnvironment hostingEnvironment)
        {
            _bLItissue = bLItissue;
            context= httpContextAccessor;
            _configuration = maxFileSize;
            _hostingEnvironment = hostingEnvironment;
        }
        // GET: EnterIssueController
        public ActionResult Index()
		{
            FillDepartmentMaster();
            FillLocationMaster(1);
            FillCircleMaster();
            FillTypeMaster();


            return View("_EnterIssue");
		}

        // GET: EnterIssueController/Details/5
        public void FillDepartmentMaster()
        {
            try
            {

                ViewBag.deparmentMaster = _bLItissue.GetDepartmentMasterList(HttpContext.Session.GetInt32("UsertypeID").ToString());
               
            }
            catch (Exception ex)
            {
               throw ex;
            }
        }

        public void FillLocationMaster(int intDepartmentid)
        {
            try
            {
                var Locationlist = _bLItissue.GetLocationMasterList(intDepartmentid);
                HttpContext.Session.SetObject("Locationlist", Locationlist);
                ViewBag.Locationlist = Locationlist;  


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FillTypeMaster()
        {
            try
            {
                ViewBag.TypeMasterlist = _bLItissue.GetTypeMasterList(context.HttpContext?.Session.GetInt32("UsertypeID").ToString());//CR-45938 BOE Log Ticket
             
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CategoryMasterList FillCategoryMaster(int type, int companyid, int DepartID)
        {
            try
            {
                return _bLItissue.GetCategoryMasterList(type, 4, 2, DepartID);
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }
        public SubCategoryMasterList FillSubCategoryMaster(int CategoryId)
        {
            try
            {
               return _bLItissue.GetSubCategoryMasterList(CategoryId);
               
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ProblemMasterList FillProblemMaster(int CategoryId, int SubCategoryId)
        {
            try
            {
               return _bLItissue.GetProblemMasterList(CategoryId, SubCategoryId);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FillCircleMaster()
        {
            try
            {
               ViewBag.Circlelist = _bLItissue.GetCircleMasterList();
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public JsonResult GetCategoryMaster(int TypeId, int DeparmentId)
        {
            CategoryMasterList categoryMasters = new CategoryMasterList();
            try
            {
                categoryMasters = FillCategoryMaster(TypeId, Convert.ToInt32(context.HttpContext?.Session.GetInt32("CompanyID")), DeparmentId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(data: JsonSerializer.Serialize(categoryMasters));
        }
        [HttpPost]
        public JsonResult GetSubCategoryList(int Category)
        {
            SubCategoryMasterList subcategorymaster = new SubCategoryMasterList();

            try
            {
                subcategorymaster = FillSubCategoryMaster(Category);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(data: JsonSerializer.Serialize(subcategorymaster));

        }
        [HttpPost]
        public JsonResult GetProblemList(int Category, int SubCategory)
        {
            ProblemMasterList problemMasters = new ProblemMasterList();

            try
            {
               
                    problemMasters = FillProblemMaster(Category, SubCategory);
                    
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(data:JsonSerializer.Serialize(problemMasters));

        }
       [HttpPost]
        public ActionResult SvaeIssues(IssueLog oBOIssueLog, IFormFile postedFile)
        {
            try
            {
                if (string.IsNullOrEmpty(oBOIssueLog.problem_details_html))
                {
                    TempData["problem"] = "Please Enter Problem Details !";
                    return RedirectToAction("Index");
                }
                else
                { TempData["problem"] = ""; }
                oBOIssueLog.CategoryID = Convert.ToInt32(context.HttpContext?.Session.GetInt32("CompanyID"));

                Int32? lngUploadFileSize = context.HttpContext?.Session.GetInt32("fileSize");
                 
                //File Size check implemented by Sachin Saxena on 14 Jan 2021
            
                if (postedFile.Length > lngUploadFileSize)
                {

                   ViewBag.massage = "File Size is greater than 1 MB. Please select file less than 1 MB";
                    return RedirectToAction("Index");
                }
                //File Size check implemented by Sachin Saxena on 14 Jan 2021
                string Filenae = postedFile.FileName;
                string ext = Path.GetExtension(Filenae);
                if (postedFile.FileName.Length != 0)
                {
                    attach = "Yes";
                    if (!(_configuration["FileTypes"].ToLower().Contains(ext.ToLower())))
                    {
                        ViewBag.FileExt = "javascript:alert('Invalid File.Please upload a valid File. Only .bmp, .gif, .png, .jpg, .jpeg, .doc,.docx, .xls, .pdf, .xlsx, .msg, .zip, .rar are allowed');";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    attach = "No";
                }
                if (attach == "Yes")
                {
                    if (postedFile.Length != 0)
                    {

                        string filename = postedFile.FileName;
                        //var extention = System.IO.Path.GetExtension(filename);
                        //if (extention == "msg")
                        //{
                        //    FlUpload.PostedFile.ContentType = "application/vnd.ms-outlook";
                        //}
                        //string[] d = file.Split('.');
                        string folderPath  = _hostingEnvironment.WebRootPath+"/Uploaded";
                        string contentRootPath = _hostingEnvironment.ContentRootPath;
                        //Check whether Directory (Folder) exists.
                        if (!Directory.Exists(folderPath))
                        {
                            //If Directory (Folder) does not exists. Create it.
                            Directory.CreateDirectory(folderPath);
                        }
                        //Save the File to the Directory (Folder).                   
                        string fullpath = folderPath + filename;
                        using (FileStream stream = new FileStream(Path.Combine(folderPath, filename), FileMode.Create))
                        {
                            postedFile.CopyTo(stream);
                        }
                        oBOIssueLog.File_Name= filename;
                        oBOIssueLog.File_Type= filename;
                        oBOIssueLog.attachment = attach;
                        oBOIssueLog.UrlPath = fullpath;
                    }
                }
                string problemWithHTML = oBOIssueLog.problem_details_html;
                problemWithHTML = Regex.Replace(problemWithHTML, @"\s*(<[^>]+>)\s*", "$1", RegexOptions.Singleline);
                string probDetails = (RemoveHTMLTags(oBOIssueLog.problem_details_html.Trim()));
                int nOfUser = 0;
                int.TryParse(oBOIssueLog.usersaffected.ToString(), out nOfUser);
                int complaintreanid = 0;
                oBOIssueLog.attachment = attach;
                oBOIssueLog.ComplaintTranID = complaintreanid;
                oBOIssueLog.problem_details = probDetails;
                oBOIssueLog.problem_details_html = problemWithHTML;//OR-18251 by nazia
                oBOIssueLog.ReasonToLog = "Do not have any option/tool to resolve the issue";
                oBOIssueLog.status_id = 1;
                int dept_type = 1;
                if (context.HttpContext?.Session.GetString("department") == "Collection")
                    dept_type = 2;
                oBOIssueLog.user_id = (int)context.HttpContext?.Session.GetInt32("user_id");
                oBOIssueLog.usersaffected = nOfUser;
                oBOIssueLog.dept_type = dept_type;
                // CR : 10971 Start
                string locationValue = "";
                var location = JsonConvert.DeserializeObject<LocationMasterList>(HttpContext.Session.GetString("Locationlist")).ToList();
                oBOIssueLog.Location_Name= location.Where(m => m.LocationID == oBOIssueLog.Location_Id).FirstOrDefault().LocationName;
                // locationValue = Locationlist.Select(x => x.locaton.ToString()).ToArray();

                //End
                oBOIssueOut = _bLItissue.SubmitIssue(oBOIssueLog);

                

            }
            catch (Exception ex)
            {
             throw ex;
            }
          return  RedirectToAction("Index");
        }
        public static string RemoveHTMLTags(string content)
        {
            var cleaned = string.Empty;
            try
            {
                string textOnly = string.Empty;
                Regex tagRemove = new Regex(@"<[^>]*(>|$)");
                Regex compressSpaces = new Regex(@"[\s\r\n]+");
                textOnly = tagRemove.Replace(content, string.Empty);
                textOnly = compressSpaces.Replace(textOnly, " ");
                cleaned = textOnly;
            }
            catch
            {
                //A tag is probably not closed. fallback to regex string clean.
            }
            return cleaned;
        }
        public ValidationResult IsValid(
             object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                if (file.Length > Convert.ToInt32(_configuration["UploadFileSize"]))
                {
                    return new ValidationResult("Fail");
                }
            }

            return ValidationResult.Success;
        }
    }
}
