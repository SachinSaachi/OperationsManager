using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OManager_Core.businessLogic;

namespace OperationsManager.Controllers
{
	public class EnterIssueController : Controller
	{
        private IBLItissue _bLItissue;
        public EnterIssueController(IBLItissue bLItissue)
        {
            _bLItissue = bLItissue;
        }
        // GET: EnterIssueController
        public ActionResult Index()
		{
            FillDepartmentMaster();
            FillCompanyMaster();
            FillLocationMaster(1);
            FillTypeMaster();
            FillCategoryMaster(0, 0, 0, 0);
            FillSubCategoryMaster(4);
            FillProblemMaster(1, 5);

            return View();
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

        public void FillCompanyMaster()
        {
            try
            {
                ViewBag.CompanyMaster = _bLItissue.GetCompanyMasterList();
                
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
                ViewBag.Locationlist = _bLItissue.GetLocationMasterList(intDepartmentid);
              
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void FillTypeMaster()
        {
            try
            {
               ViewBag.TypeMasterlist = _bLItissue.GetTypeMasterList("4");//CR-45938 BOE Log Ticket
             
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FillCategoryMaster(int type, int UserTypeID, int companyid, int DepartID)
        {
            try
            {
               ViewBag.CategoryMasterList = _bLItissue.GetCategoryMasterList(type, UserTypeID, companyid, DepartID);
                

            }
            catch (Exception ex)
            {
               throw new Exception(ex.Message);
            }
        }
        public void FillSubCategoryMaster(int CategoryId)
        {
            try
            {
                ViewBag.SubCategoryMasterList = _bLItissue.GetSubCategoryMasterList(CategoryId);
               
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void FillProblemMaster(int CategoryId, int SubCategoryId)
        {
            try
            {
               ViewBag.ProblemMasterList = _bLItissue.GetProblemMasterList(CategoryId, SubCategoryId);
                

               // Session["ProblemList"] = oBOProblemMasterList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
