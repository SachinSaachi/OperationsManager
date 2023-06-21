using Common;
using Common.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using OManager_Core.businessLogic;
using OperationsManager.Models;
using System.Security.Policy;

namespace OperationsManager.Controllers
{
    public class LogIssueController : Controller
    {
        private IBLItissue _bLItissue;
        public LogIssueController(IBLItissue bLItissue)
        {
            _bLItissue = bLItissue;
        }
        public IActionResult Index()
        {
            FilTicketDashboard();
            return View();
        }

        [HttpPost]
        public JsonResult GetLogSheet(string FromDate, string ToDate, string TicketType, string Status, string Group, string ComplaintIds)
        {
            string emplyeeType = "ENG";
            int userId = 198425;
            int centerId = 0;

            if (ComplaintIds != null && ComplaintIds != "")
            {
                ComplaintIds = ComplaintIds.Trim().Replace("\t", "").Replace("\n", "");
            }
            else
            { ComplaintIds = string.Empty; }
           
            var objBOPendingUserLst = _bLItissue.GetLogSheet(Status, emplyeeType, centerId, userId, FromDate, ToDate, ComplaintIds, Group, Constants.Brand);
            return Json(objBOPendingUserLst);
        }
        public string ConvertDate(string dt)
        {
            string[] d = dt.Split('/');
            string dt1 = d[2] + "-" + d[1] + "-" + d[0];
            return dt1;
        }

        public JsonResult BindStatus()
        {
            string queryStr = HttpContext.Request.Query["S"].ToString();
            var status = _bLItissue.GetStatusList();
            status.Insert(0, new Common.Models.Commondata { ID = 0, Name = "All" });

            return Json(status);
        }
        public JsonResult GetGroup()
        {
            string CompanyId = "2";
            var groups = _bLItissue.GetGroupListRoleWise("HELP", Convert.ToInt32(198260), CompanyId);
            groups.Insert(0, new GroupMaster { GroupID = 0, GroupName = "All" });
            return Json(groups);
        }

        public void FilTicketDashboard()
        {
            string emplyeeType = "ENG";
            int userId = 198425;
            var allTicket = _bLItissue.GetDashboardDetails(userId);
            ViewBag.TotalTicket = allTicket.TotalTicket;
            ViewBag.UnresolvedTicket = allTicket.UnresolvedTicket;
            ViewBag.WipTicket = allTicket.WipTicket;
            ViewBag.ClosedTicket = allTicket.ClosedTicket;
            ViewBag.ResolvedTicket = allTicket.ResolvedTicket;
            ViewBag.PendingTicket = allTicket.PendingTicket;
           
        }

    }
}
