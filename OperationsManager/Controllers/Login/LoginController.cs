using DocumentFormat.OpenXml.Office.CustomUI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OManager_Core.businessLogic;
using System.Diagnostics.Metrics;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using static Common.Models.BOIssue;
using static Common.Models.BOLogin;

namespace OperationsManager.Controllers
{
	
	public class LoginController : Controller
	{
		
		private IBLItissue _bLItissue;
		public LoginController(IBLItissue bLItissue)
		{
			_bLItissue= bLItissue;
		}
		UserDetail oBOUserDetail = new UserDetail();
		LoginDetail oBOLoginDetail = new LoginDetail();
		PendingUser objPendingUser = new PendingUser();	
		//_bLItissue. oBOItIssue = new BLItIssue();
		//ITIssue.BusinessRuleLayer.Common.BLMomUser oBOMOMItIssue = new ITIssue.BusinessRuleLayer.Common.BLMomUser();

		//// CR :10971 
		//ITIssue.BusinessObject.Common.UserTypeMaster oBOUserTypeMaster = new ITIssue.BusinessObject.Common.UserTypeMaster();
		UserTypeMasterList oBOUserTypeMasterList = new UserTypeMasterList();
		public void FillUserTypeMaster()
		{
			try
			{
				oBOUserTypeMasterList = _bLItissue.GetUserTypeMasterList();
				var productsList = (from product in _bLItissue.GetUserTypeMasterList()
									select new SelectListItem()
									{
										Text = product.UserShortName,
										Value = product.UserType.ToString(),
									}).ToList();

				productsList.Insert(0, new SelectListItem()
				{
					Text = string.Empty,
					Value = "User Type" 
				});

				ViewBag.UserTypeMasterList = productsList;
			}
			catch (Exception ex)
			{
				throw ;
			}
		}
		public IActionResult Index()
		{
			FillUserTypeMaster();
			
			return View();
		}
		public string GetMACAddress()
		{
			NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
			String sMacAddress = string.Empty;
			foreach (NetworkInterface adapter in nics)
			{
				if (sMacAddress == String.Empty)
				{
					IPInterfaceProperties properties = adapter.GetIPProperties();
					sMacAddress = adapter.GetPhysicalAddress().ToString();
				}
			}
			return sMacAddress;
		}
		public ActionResult SaveLogin(string UserName ,string Password,string UserType)
		{
			
			try
			{
				var remote = this.HttpContext.Connection.RemoteIpAddress;
				int UsertypeID;
				// CR :10971 Start
						oBOUserTypeMasterList = _bLItissue.GetUserTypeMasterList();
						ViewBag.UserTypeList = oBOUserTypeMasterList;
					
					oBOUserTypeMasterList = ViewBag.UserTypeList;
					UsertypeID = Convert.ToInt32(oBOUserTypeMasterList.Where(m => m.UserShortName == UserType).FirstOrDefault().UserTypeID);
					HttpContext.Session.SetInt32("UsertypeID", UsertypeID);
					// CR :10971
					oBOLoginDetail.Type = UserType;
					oBOLoginDetail.Username = UserName;
					oBOLoginDetail.Password = Password;
					oBOLoginDetail.Domain = "SITI"; //// CR :10971 
					
						oBOLoginDetail.CompanyId = 2;
				HttpContext.Session.SetInt32("User_CompanyID", 2);//OR - 19012
					
					if (oBOLoginDetail.Type.Equals("MOMU") || oBOLoginDetail.Type.Equals("MOMA"))
					{
						oBOUserDetail = _bLItissue.GetUserDetailByLogin(oBOLoginDetail, UsertypeID);
					}
					else
					{
						oBOUserDetail = _bLItissue.GetUserDetailByLogin(oBOLoginDetail, UsertypeID);
					}
				HttpContext.Session.SetString("UserType", oBOUserDetail.employee_type);
				HttpContext.Session.SetInt32("user_id", oBOUserDetail.user_id);
				HttpContext.Session.SetString("employee_name", oBOUserDetail.employee_name);
				HttpContext.Session.SetString("employee_type", oBOUserDetail.employee_type);
				HttpContext.Session.SetString("login_name", oBOUserDetail.login_name);
				HttpContext.Session.SetString("department", oBOUserDetail.department);
				HttpContext.Session.SetString("contact", oBOUserDetail.contact);
				HttpContext.Session.SetString("extension", oBOUserDetail.extension);
				HttpContext.Session.SetString("emailid", oBOUserDetail.email_id);
				HttpContext.Session.SetInt32("centerid", oBOUserDetail.centerid);
				HttpContext.Session.SetInt32("UserGroup", oBOUserDetail.UGID);
				HttpContext.Session.SetString("IsAvailable", oBOUserDetail.IsAvailable);
				HttpContext.Session.SetString("guiuniqueid",Guid.NewGuid().ToString().Trim());
				HttpContext.Session.SetString("EmpCode", oBOUserDetail.Empcode);
				
					//CR-44380 To Capture IP address and Lat/Lng
					//if (System.Configuration.ConfigurationManager.AppSettings["isCaptureLatLon"].ToString() == "1")
					//{
					//	if ((hdnlat.Value == "" || hdnlng.Value == "") && hdnLocationStatus.Value != "-100")
					//	{
					//		string scriptText = "javascript:alert('Please allow location tracking.');";
					//		ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", scriptText, true);
					//		return;
					//	}
					//}

					////this.Session["Latitude"] = hdnlat.Value;
				   //	this.Session["Longitude"] = hdnlng.Value;
					//CR-44380 To Capture IP address and Lat/Lng


					if (HttpContext.Session.GetInt32("user_id") != null)
					{
						if ((HttpContext.Session.GetString("UserType") != "MOMU" && HttpContext.Session.GetString("UserType") != "MOMA") & _bLItissue.CheckLoginStatus(HttpContext.Session.GetInt32("user_id")))
						{
						return RedirectToAction("Dashbord", "Login");
					}
						else
						{
							if (HttpContext.Session.GetInt32("user_id") != null)
							{
							if (_bLItissue.InsertLoginCheck(HttpContext.Session.GetInt32("user_id"), HttpContext.Session.GetString("guiuniqueid"), remote.ToString(), GetMACAddress()))
								{
									if (HttpContext.Session.GetString("UserType") == "CCE" || HttpContext.Session.GetString("employee_type") == "UEMP")   // CR : 10971
									{
									return RedirectToAction("Dashbord", "Login");

								}
									else if (HttpContext.Session.GetString("UserType") == "ITTL")// add by shiv 21-06-2016
									{
									//Response.Redirect("RCA.aspx");
									return RedirectToAction("Dashbord", "Login");
								}
									else if (HttpContext.Session.GetString("UserType") == "MOMU" || HttpContext.Session.GetString("UserType") == "MOMA")
									{
										Response.Redirect("Pages/Mom/Index.aspx");
									}
								}
							}
						}
					}
			}
			catch (Exception ex)
			{
				throw ex; 
			}
			 return RedirectToAction("Dashbord", "Login"); ;

	
		}
		public ActionResult Dashbord()
		{
			if ((HttpContext.Session.GetString("guiuniqueid") == null) || (HttpContext.Session.GetString("guiuniqueid").ToString() == ""))
			{
				 return RedirectToAction("Index", "Login");
			}
			oBOLoginDetail = _bLItissue.GetLoginStatus(HttpContext.Session.GetInt32("user_id"), HttpContext.Session.GetString("guiuniqueid").ToString());
			if ((oBOLoginDetail.name == "False"))
			{
				
				return  RedirectToAction("Index", "Login");
			}
			
			if (((HttpContext.Session.GetString("employee_type") != null || HttpContext.Session.GetInt32("user_id") != null || HttpContext.Session.GetInt32("login_name") != null)))
			{
				
					FillDashboard(HttpContext.Session.GetInt32("user_id"));
				
				
				////CR-45938 BOE Log Ticket
				//if (Session["UserType"].ToString() == "RMW")
				//{
				//	row2.Visible = false;
				//	RT.Visible = false;
				//	WIPT.Visible = false;
				//	RST.Visible = false;


				//}
				//else
				//{
				//	row2.Visible = true;
				//	RT.Visible = true;
				//	WIPT.Visible = true;
				//	RST.Visible = true;

				//}
				//if (Session["UserType"].ToString() == "HELP")
				//{
				//	EngIns.Visible = false;
				//	dvIssueLogs.Visible = true;
				//	DT.Visible = true;
				//}
				//else
				//{
				//	EngIns.Visible = false;
				//	dvIssueLogs.Visible = false;
				//	DT.Visible = false;
				//}
			}
			else
			{
				Response.Redirect("Default.aspx");
			}
			return View("_Dashbord");
		}

		public void FillDashboard(int? UserId)
		{
			objPendingUser = _bLItissue.GetDashboardDetails(UserId);
			ViewBag.TotalTicket = objPendingUser.TotalTicket.ToString();
			ViewBag.ResolvedTicket = objPendingUser.ResolvedTicket.ToString();
			//lblWip.Text = objBOPendingUser.WipTicket.ToString();
			//lbl.Text = objBOPendingUser.UnresolvedTicket.ToString();
			ViewBag.WipTicket = objPendingUser.WipTicket.ToString();
			ViewBag.PendingTicket = objPendingUser.PendingTicket.ToString();
			ViewBag.BreachedSLA = objPendingUser.BreachedSLA.ToString();
			ViewBag.OpenIssuesWithinSLA = objPendingUser.OpenIssuesWithinSLA.ToString();
			ViewBag.AbouttoBreachSLA = objPendingUser.AbouttoBreachSLA.ToString();
			ViewBag.ReOpenTicket = objPendingUser.ReOpenTicket.ToString();
			ViewBag.DevelopmentTaskCount = objPendingUser.DevelopmentTaskCount.ToString();
			ViewBag.IssueLogCount = objPendingUser.IssueLogCount.ToString();


		}
	}
}
