using HRMS_Core.DataAccessLayer.DatabaseHelper.Datablase.Common;
using HRMS_Core.DataAccessLayer.DatabaseHelper.Datablase.DatabaseHelper;
using System.Data;
using System.DirectoryServices;
using System.IO;
using System.Reflection.PortableExecutable;
using static Common.Models.BOIssue;
using static Common.Models.BOLogin;

namespace OManager_Core.DataAccessLayer
{

	public interface IDLItIssue
	{
		UserTypeMasterList GetUserTypeMasterList();
		UserDetail GetUserDetailByLogin(LoginDetail oBOLoginDetail, int UsertypeID);
		bool CheckLoginStatus(int? userrid);
		bool InsertLoginCheck(int? userrid, string guiuniqueid, string LoginIP, string MacId);
		LoginDetail GetLoginStatus(int? Userid, string GUIIniqueid);
		PendingUser GetDashboardDetails(int? UserId);
		DepartmentMasterList GetDepartmentMasterList(string userTypeID);
		CompanyMasterList GetCompanyMasterList();
		LocationMasterList GetLocationMasterList(int intDepartmentid);
		TypeMasterList GetTypeMasterList(string userTypeID);
		CategoryMasterList GetCategoryMasterList(int type, int? UserTypeID, int companyid, int DepartID);
		SubCategoryMasterList GetSubCategoryMasterList(int CategoryId);
		ProblemMasterList GetProblemMasterList(int CategoryId, int SubCategoryId);
        CircleMasterList GetCircleMasterList();
        IssueOut SubmitIssue(IssueLog oBOIssue);

    }
	public class DLItIssue: IDLItIssue
	{
		private IExecuteQuery _executequery;
		public DLItIssue(IExecuteQuery executequery) { 
		_executequery= executequery;
		}

		public static string GetProperty(SearchResult searchResult, string PropertyName)
		{
			if (searchResult.Properties.Contains(PropertyName))
			{
				return searchResult.Properties[PropertyName][0].ToString();
			}
			else
			{
				return string.Empty;
			}
		}
		public UserTypeMasterList GetUserTypeMasterList()
		{
			DataTableReader dr = null;
			UserTypeMasterList oboUserTypeMasterList = new UserTypeMasterList();
			UserTypeMaster oBOUserTypeMaster;
			String SQL = String.Empty;
			ParameterList paramList = new ParameterList();
			try
			{
				SQL = "usp_ITissueLog_GetUserTypeMaster";
				dr = _executequery.ExecuteReader(SQL);
				while (dr.Read())
				{
					oBOUserTypeMaster = new UserTypeMaster();
					oBOUserTypeMaster.UserTypeID = Convert.ToInt32(dr["UserTypeID"]);
					oBOUserTypeMaster.UserType = Convert.ToString(dr["UserType"]);
					oBOUserTypeMaster.UserShortName = Convert.ToString(dr["UserShortName"]);
					oboUserTypeMasterList.Add(oBOUserTypeMaster);
				}
				dr.Close();
				return oboUserTypeMasterList;
			}
			catch (Exception)
			{ throw; }
		}
		public UserDetail ValidateLogin(string UserName, string Password, string Domain)
		{
			UserDetail oBOUserDetail = new UserDetail();
			try
			{



				System.DirectoryServices.DirectoryEntry entry = new System.DirectoryServices.DirectoryEntry("LDAP://" + Domain, UserName, Password);
				Object obj = entry.NativeObject;
				DirectorySearcher search = new DirectorySearcher(entry);
				search.Filter = "(SAMAccountName=" + UserName + ")";
				search.PropertiesToLoad.Add("cn");
				search.PropertiesToLoad.Add("department");
				search.PropertiesToLoad.Add("role");
				search.PropertiesToLoad.Add("samaccountname");
				search.PropertiesToLoad.Add("mail");
				search.PropertiesToLoad.Add("displayname");//first name
				search.PropertiesToLoad.Add("telephoneNumber");//first name
				search.PropertiesToLoad.Add("Mobile");//first name              
				search.PropertiesToLoad.Add("st");
				search.PropertiesToLoad.Add("Office");//first name
				search.PropertiesToLoad.Add("physicaldeliveryofficename");//Empcode
				SearchResult result = search.FindOne();
				if (null == result)
				{
					oBOUserDetail = null;
					return oBOUserDetail;
				}
				oBOUserDetail.employee_name = GetProperty(result, "cn");
				oBOUserDetail.department = GetProperty(result, "department");
				oBOUserDetail.login_name = GetProperty(result, "samaccountname");
				oBOUserDetail.employee_name = GetProperty(result, "cn");
				oBOUserDetail.login_password = Password;
				oBOUserDetail.email_id = GetProperty(result, "mail");
				oBOUserDetail.contact = GetProperty(result, "Mobile");
				oBOUserDetail.state = GetProperty(result, "st");
				oBOUserDetail.Address = GetProperty(result, "Office");
				oBOUserDetail.Lan_ID = GetProperty(result, "samaccountname");
				oBOUserDetail.Empcode = GetProperty(result, "physicaldeliveryofficename");

				return oBOUserDetail;

			}
			catch (Exception ex)
			{
				oBOUserDetail = null;
				ex.ToString();
			}

			return oBOUserDetail;
		}

		public UserDetail GetUserDetailByLogin(LoginDetail oBOLoginDetail, int UsertypeID)
		{
			DataTableReader dr = null;
			UserDetail oBOUserDetail = new UserDetail();
			String SQL = String.Empty;
			ParameterList paramList = new ParameterList();
			try
			{
				oBOUserDetail = ValidateLogin(oBOLoginDetail.Username.Trim(), oBOLoginDetail.Password.Trim(), oBOLoginDetail.Domain);

				if (oBOUserDetail != null)
				{

					SQL = "usp_ItIssueLog_UserLoginWithLanID";
					paramList.Add(new SQLParameter("@employee_name", oBOUserDetail.employee_name.Trim()));
					paramList.Add(new SQLParameter("@login_name", oBOUserDetail.login_name.Trim()));
					paramList.Add(new SQLParameter("@login_password", oBOUserDetail.login_password.Trim()));
					paramList.Add(new SQLParameter("@email_id", oBOUserDetail.email_id.Trim()));
					paramList.Add(new SQLParameter("@contact", oBOUserDetail.contact.Trim()));
					paramList.Add(new SQLParameter("@department", oBOUserDetail.department.Trim()));
					paramList.Add(new SQLParameter("@Address", oBOUserDetail.Address.Trim()));
					paramList.Add(new SQLParameter("@state", oBOUserDetail.state.Trim()));
					paramList.Add(new SQLParameter("@employee_type", oBOLoginDetail.Type));
					paramList.Add(new SQLParameter("@Lan_ID", oBOUserDetail.Lan_ID.Trim()));
					paramList.Add(new SQLParameter("@User_type", Convert.ToString(UsertypeID)));
					paramList.Add(new SQLParameter("@Companyid", oBOLoginDetail.CompanyId));
				}
				else
				{
					oBOUserDetail = new UserDetail();
					SQL = "usp_ItIssueLog_UserLogin";
					paramList.Add(new SQLParameter("@Type", oBOLoginDetail.Type.Trim()));
					paramList.Add(new SQLParameter("@Companyid", oBOLoginDetail.CompanyId));
					paramList.Add(new SQLParameter("@Username", oBOLoginDetail.Username.Trim()));
					paramList.Add(new SQLParameter("@Password", oBOLoginDetail.Password.Trim()));
				}
				dr = _executequery.ExecuteReader(SQL, paramList);
				while (dr.Read())
				{
					oBOUserDetail.user_id = Convert.ToInt32(dr["user_id"]);
					oBOUserDetail.employee_name = dr["employee_name"] == null ? string.Empty : Convert.ToString(dr["employee_name"]);
					oBOUserDetail.login_name = dr["login_name"] == null ? string.Empty : Convert.ToString(dr["login_name"]);
					oBOUserDetail.login_password = dr["login_password"] == null ? string.Empty : Convert.ToString(dr["login_password"]);
					oBOUserDetail.email_id = dr["email_id"] == null ? string.Empty : Convert.ToString(dr["email_id"]);
					oBOUserDetail.department = dr["department"] == null ? string.Empty : Convert.ToString(dr["department"]);
					oBOUserDetail.contact = dr["contact"] == null ? string.Empty : Convert.ToString(dr["contact"]);
					oBOUserDetail.extension = dr["extension"] == null ? string.Empty : Convert.ToString(dr["extension"]);
					oBOUserDetail.last_visited = dr["last_visited"] == null ? string.Empty : Convert.ToString(dr["last_visited"]);
					oBOUserDetail.designation = dr["designation"] == null ? string.Empty : Convert.ToString(dr["designation"]);
					oBOUserDetail.direct_contact = dr["direct_contact"] == null ? string.Empty : Convert.ToString(dr["direct_contact"]);
					oBOUserDetail.Address = dr["Address"] == null ? string.Empty : Convert.ToString(dr["Address"]);
					oBOUserDetail.city = dr["city"] == null ? string.Empty : Convert.ToString(dr["city"]);
					oBOUserDetail.state = dr["state"] == null ? string.Empty : Convert.ToString(dr["state"]);
					oBOUserDetail.admin = dr["admin"] == null ? string.Empty : Convert.ToString(dr["admin"]);
					oBOUserDetail.engineer = dr["engineer"] == null ? string.Empty : Convert.ToString(dr["engineer"]);
					oBOUserDetail.cce = dr["cce"] == null ? string.Empty : Convert.ToString(dr["cce"]);
					oBOUserDetail.employee_type = dr["employee_type"] == null ? string.Empty : Convert.ToString(dr["employee_type"]);
					oBOUserDetail.centerid = dr["CenterID"] == null ? 0 : Convert.ToInt32(dr["CenterID"].ToString());
					oBOUserDetail.UGID = dr["UGID"] == null ? 0 : Convert.ToInt32(dr["UGID"].ToString());
					oBOUserDetail.IsAvailable = dr["IsAvailable"] == null ? string.Empty : Convert.ToString(dr["IsAvailable"]);
				}
				dr.Close();
				return oBOUserDetail;
			}
			catch (Exception)
			{ throw; }
		}

		public bool CheckLoginStatus(int? userrid)
		{
			DataTableReader dr = null;

			String SQL = String.Empty;
			ParameterList paramList = new ParameterList();
			try
			{
				SQL = "USP_CheckLoginStatus";

				paramList.Add(new SQLParameter("@Userrid", userrid));
				dr = _executequery.ExecuteReader(SQL, paramList);
				while (dr.Read())
				{

					return Convert.ToBoolean(dr["status"]);

				}
				dr.Close();
				return false;
			}
			catch (Exception)
			{ throw; }
		}

		public bool InsertLoginCheck(int? userrid, string guiuniqueid, string LoginIP, string MacId)
		{


			String SQL = String.Empty;
			ParameterList paramList = new ParameterList();
			try
			{
				SQL = "USP_Login_Insert";

				paramList.Add(new SQLParameter("@Userrid", userrid));
				paramList.Add(new SQLParameter("@Uniqueid", guiuniqueid));
				paramList.Add(new SQLParameter("@LoginIP", LoginIP));
				paramList.Add(new SQLParameter("@MacId", MacId));

				_executequery.ExecuteNonQuery(SQL, paramList);

				return true;
			}
			catch (Exception)
			{ throw; }
		}

		public LoginDetail GetLoginStatus(int? Userid, string GUIIniqueid)
		{

			DataTableReader dr = null;
			LoginDetail oBOCurrentStatus = new LoginDetail();

			String SQL = String.Empty;
			ParameterList paramList = new ParameterList();
			try
			{
				SQL = "USP_GetLoginStatus";

				paramList.Add(new SQLParameter("@Userrid", Userid));
				paramList.Add(new SQLParameter("@Uniqueid", GUIIniqueid));

				dr = _executequery.ExecuteReader(SQL, paramList);
				while (dr.Read())
				{

					oBOCurrentStatus.value = dr["value"] == null ? string.Empty : Convert.ToString(dr["value"]);
					oBOCurrentStatus.name = dr["name"] == null ? string.Empty : Convert.ToString(dr["name"]);

				}
				dr.Close();
				return oBOCurrentStatus;
			}
			catch (Exception)
			{ throw; }
		}


		public PendingUser GetDashboardDetails(int? UserId)
		{
			DataTableReader dr = null;
			PendingUserList oboPendingUserList = new PendingUserList();
			PendingUser oBOPendingUser = new PendingUser();
			String SQL = String.Empty;
			ParameterList paramList = new ParameterList();
			try
			{
				SQL = "USP_ITISSUELOG_GETDASHBOARDDATA";
				paramList.Add(new SQLParameter("@USER_ID", UserId));
				dr = _executequery.ExecuteReader(SQL, paramList);
				while (dr.Read())
				{
					oBOPendingUser.TotalTicket = Convert.ToInt32(dr["TOTAL"].ToString());
					oBOPendingUser.UnresolvedTicket = Convert.ToInt32(dr["UNRESOLVED"].ToString());
					oBOPendingUser.WipTicket = Convert.ToInt32(dr["WIP"].ToString());
					oBOPendingUser.ResolvedTicket = Convert.ToInt32(dr["RESOLVED"].ToString());
					oBOPendingUser.PendingTicket = Convert.ToInt32(dr["Reassigned"].ToString());
					oBOPendingUser.ClosedTicket = Convert.ToInt32(dr["CLOSED"].ToString());
					oBOPendingUser.AbouttoBreachSLA = Convert.ToInt32(dr["OpenIssueAbouttoBreachSLA"].ToString());
					oBOPendingUser.BreachedSLA = Convert.ToInt32(dr["OpenIssueBreachedSLA"].ToString());
					oBOPendingUser.OpenIssuesWithinSLA = Convert.ToInt32(dr["OpenIssueWithinSLA"].ToString());
					oBOPendingUser.ReOpenTicket = Convert.ToInt32(dr["ReOpen"].ToString());
					oBOPendingUser.DevelopmentTaskCount = Convert.ToInt32(dr["DevelopmentTaskCount"].ToString());
					oBOPendingUser.IssueLogCount = Convert.ToInt32(dr["IssueLogCount"].ToString());


				}
				dr.Close();
				return oBOPendingUser;
			}
			catch (Exception)
			{ throw; }
		}
        public DepartmentMasterList GetDepartmentMasterList(string userTypeID)
        {
            DataTableReader dr = null;
            DepartmentMasterList oboDepartmentMasterList = new DepartmentMasterList();
            DepartmentMaster oBODepartmentMaster;
            String SQL = String.Empty;
            ParameterList paramList = new ParameterList();
            try
            {
                SQL = "usp_ITissueLog_GetDeparatmentMaster";
                paramList.Add(new SQLParameter("@UserTypeID", userTypeID));//CR-45938 BOE Log Ticket
                dr = _executequery.ExecuteReader(SQL, paramList);
                while (dr.Read())
                {
                    oBODepartmentMaster = new DepartmentMaster();
                    oBODepartmentMaster.DepartmentID = Convert.ToInt32(dr["DeptID"]);
                    oBODepartmentMaster.DepartmentName = Convert.ToString(dr["DeptName"]);
                    oboDepartmentMasterList.Add(oBODepartmentMaster);
                }
                dr.Close();
                return oboDepartmentMasterList;
            }
            catch (Exception)
            { throw; }
        }

        public CompanyMasterList GetCompanyMasterList()
        {
            DataTableReader dr = null;
           CompanyMasterList oboCompanyMasterList = new CompanyMasterList();
            CompanyMaster oBOCompanyMaster;
            String SQL = String.Empty;
            ParameterList paramList = new ParameterList();
            try
            {
                SQL = "usp_ITissueLog_GetCompanyMaster";
                dr = _executequery.ExecuteReader(SQL);
                while (dr.Read())
                {
                    oBOCompanyMaster = new CompanyMaster();
                    oBOCompanyMaster.CompanyID = Convert.ToInt32(dr["CompanyID"]);
                    oBOCompanyMaster.CompanyName = Convert.ToString(dr["CompanyName"]);
                    oboCompanyMasterList.Add(oBOCompanyMaster);
                }
                dr.Close();
                return oboCompanyMasterList;
            }
            catch (Exception)
            { throw; }
        }

        public LocationMasterList GetLocationMasterList(int intDepartmentid)
        {
            DataTableReader dr = null;
            LocationMasterList oboLocationMasterList = new LocationMasterList();
           LocationMaster oBOLocationMaster;
            String SQL = String.Empty;
            ParameterList paramList = new ParameterList();
            try
            {
                SQL = "usp_ITissueLog_GetLocationMaster";
                dr = _executequery.ExecuteReader(SQL, paramList);
                while (dr.Read())
                {
                    oBOLocationMaster = new LocationMaster();
                    oBOLocationMaster.LocationID = Convert.ToInt32(dr["LocationID"]);
                    oBOLocationMaster.LocationName = Convert.ToString(dr["LocationName"]);
                    oboLocationMasterList.Add(oBOLocationMaster);
                }
                dr.Close();
                return oboLocationMasterList;
            }
            catch (Exception)
            { throw; }
        }

        public TypeMasterList GetTypeMasterList(string userTypeID)
        {
            DataTableReader dr = null;
            TypeMasterList oboTypeMasterList = new TypeMasterList();
           TypeMaster oBOTypeMaster;
            String SQL = String.Empty;
            ParameterList paramList = new ParameterList();
            try
            {
                SQL = "usp_ITissueLog_GetTypeMaster";
                paramList.Add(new SQLParameter("@UserTypeID", userTypeID));//CR-45938 BOE Log Ticket
                dr = _executequery.ExecuteReader(SQL, paramList);
                while (dr.Read())
                {
                    oBOTypeMaster = new TypeMaster();
                    oBOTypeMaster.TypeID = Convert.ToInt32(dr["TypeID"]);
                    oBOTypeMaster.TypeName = Convert.ToString(dr["TypeName"]);
                    oboTypeMasterList.Add(oBOTypeMaster);
                }
                dr.Close();
                return oboTypeMasterList;
            }
            catch (Exception)
            { throw; }
        }
        public CategoryMasterList GetCategoryMasterList(int type, int? UserTypeID, int companyid, int DepartID)
        {
            DataTableReader dr = null;
            CategoryMasterList oboCategoryMasterList = new CategoryMasterList();
           CategoryMaster oBOCategoryMaster;
            String SQL = String.Empty;
            ParameterList paramList = new ParameterList();
            try
            {
                SQL = "usp_ITissueLog_GetCategoryMaster";
                paramList.Add(new SQLParameter("@type", type));
                paramList.Add(new SQLParameter("@UserTypeID", UserTypeID));
                paramList.Add(new SQLParameter("@companyid", companyid));
                paramList.Add(new SQLParameter("@DepartID", DepartID));
                dr = _executequery.ExecuteReader(SQL, paramList);
                while (dr.Read())
                {
                    oBOCategoryMaster = new CategoryMaster();
                    oBOCategoryMaster.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                    oBOCategoryMaster.CategoryName = Convert.ToString(dr["CategoryName"]).Trim();
                    oboCategoryMasterList.Add(oBOCategoryMaster);
                }
                dr.Close();
                return oboCategoryMasterList;
            }
            catch (Exception)
            { throw; }
        }
        public SubCategoryMasterList GetSubCategoryMasterList(int CategoryId)
        {
            DataTableReader dr = null;
            SubCategoryMasterList oboSubCategoryMasterList = new SubCategoryMasterList();
            SubCategoryMaster oBOSubCategoryMaster;
            String SQL = String.Empty;
            ParameterList paramList = new ParameterList();
            try
            {
                SQL = "usp_ITissueLog_GetSubCategoryMaster";
                paramList.Add(new SQLParameter("@CategoryId", Convert.ToInt32(CategoryId)));
                dr = _executequery.ExecuteReader(SQL, paramList);
                while (dr.Read())
                {
                    oBOSubCategoryMaster = new SubCategoryMaster();
                    oBOSubCategoryMaster.SubCategoryId = Convert.ToInt32(dr["SubCategoryId"]);
                    oBOSubCategoryMaster.SubCategoryName = Convert.ToString(dr["SubCategoryName"]).Trim();
                    oboSubCategoryMasterList.Add(oBOSubCategoryMaster);
                }
                dr.Close();
                return oboSubCategoryMasterList;
            }
            catch (Exception)
            { throw; }
        }

        public ProblemMasterList GetProblemMasterList(int CategoryId, int SubCategoryId)
        {
            DataTableReader dr = null;
           ProblemMasterList oboProblemMasterList = new ProblemMasterList();
           ProblemMaster oBOProblemMaster;
            String SQL = String.Empty;
            ParameterList paramList = new ParameterList();
            try
            {
                SQL = "usp_ITissueLog_GetProblemMaster";
                paramList.Add(new SQLParameter("@CategoryId", CategoryId));
                paramList.Add(new SQLParameter("@SubCategoryId", SubCategoryId));
                dr = _executequery.ExecuteReader(SQL, paramList);
                while (dr.Read())
                {

                    oBOProblemMaster = new ProblemMaster();
                    oBOProblemMaster.ProblemID = Convert.ToInt32(dr["ProblemID"]);
                    oBOProblemMaster.ProblemName = Convert.ToString(dr["ProblemName"]).Trim();
                    oBOProblemMaster.SLAID = Convert.ToInt32(dr["SLAID"]);
                    oBOProblemMaster.PopupRequired = Convert.ToInt32(dr["PopupRequired"]);
                    oBOProblemMaster.RequiredMessage = Convert.ToString(dr["RequiredMessage"]);
                    oBOProblemMaster.Domain_Description = Convert.ToString(dr["Domain_Description"]);//OR-17832
                    oBOProblemMaster.PopupType = Convert.ToInt32(dr["PopupType"]);//OR-18251
                    oboProblemMasterList.Add(oBOProblemMaster);
                }
                dr.Close();
                return oboProblemMasterList;
            }
            catch (Exception)
            { throw; }
        }

        public CircleMasterList GetCircleMasterList()
        {
            DataTableReader dr = null;
           CircleMasterList oboCircleMasterList = new CircleMasterList();
            CircleMaster oBOCircleMaster;
            String SQL = String.Empty;
            ParameterList paramList = new ParameterList();
            try
            {
                SQL = "usp_ITissueLog_GetTMS_CircleMaster";
                dr = _executequery.ExecuteReader(SQL);
                while (dr.Read())
                {
                    oBOCircleMaster = new CircleMaster();
                    oBOCircleMaster.CircleID = Convert.ToInt32(dr["CircleID"]);
                    oBOCircleMaster.CircleName = Convert.ToString(dr["CircleName"]).Trim();
                    oboCircleMasterList.Add(oBOCircleMaster);
                }
                dr.Close();
                return oboCircleMasterList;
            }
            catch (Exception)
            { throw; }
        }

        public IssueOut SubmitIssue(IssueLog oBOIssue)
        {
            DataTableReader dr = null;
            IssueOut oboIssueOut = new IssueOut();

            String SQL = String.Empty;
            ParameterList paramList = new ParameterList();
            try
            {
                SQL = "usp_ItIssueLog_SubmitIssue";
                paramList.Add(new SQLParameter("@attachment", oBOIssue.attachment));
                paramList.Add(new SQLParameter("@ComplaintTranID", oBOIssue.ComplaintTranID));
                paramList.Add(new SQLParameter("@it_department", oBOIssue.it_department));
                paramList.Add(new SQLParameter("@problem_details", oBOIssue.problem_details));
                paramList.Add(new SQLParameter("@ReasonToLog", oBOIssue.ReasonToLog));
                paramList.Add(new SQLParameter("@severity_id", oBOIssue.severity_id));
                paramList.Add(new SQLParameter("@specific_id", oBOIssue.specific_id));
                paramList.Add(new SQLParameter("@status_id", oBOIssue.status_id));
                paramList.Add(new SQLParameter("@Sub_specific_id", oBOIssue.Sub_specific_id));
                paramList.Add(new SQLParameter("@user_id", oBOIssue.user_id));
                paramList.Add(new SQLParameter("@usersaffected", oBOIssue.usersaffected));
                paramList.Add(new SQLParameter("@VCNo", oBOIssue.VCNo));
                paramList.Add(new SQLParameter("@dept_type", oBOIssue.dept_type));
                //CR : 10971 Start 
                paramList.Add(new SQLParameter("@CompanyID", oBOIssue.CompanyID));
                paramList.Add(new SQLParameter("@TypID", oBOIssue.TypID));
                paramList.Add(new SQLParameter("@Location_Id", oBOIssue.Location_Id));
                paramList.Add(new SQLParameter("@Location_Name", oBOIssue.Location_Name));
                paramList.Add(new SQLParameter("@ChildComplaint_id", oBOIssue.ChildComplaint_id));

                paramList.Add(new SQLParameter("@CategoryID", oBOIssue.CategoryID));
                paramList.Add(new SQLParameter("@SubCategoryID", oBOIssue.SubCategoryID));
                paramList.Add(new SQLParameter("@ProblemID", oBOIssue.ProblemID));
                paramList.Add(new SQLParameter("@SLAID", oBOIssue.SLAID));
                paramList.Add(new SQLParameter("@URLPath", oBOIssue.UrlPath));
                paramList.Add(new SQLParameter("@ticketNo", oBOIssue.TicketID));
                paramList.Add(new SQLParameter("@MobileNo", oBOIssue.MobileNo));
                paramList.Add(new SQLParameter("@Emp_Code", oBOIssue.Emp_Code));
                paramList.Add(new SQLParameter("@CC", oBOIssue.CC));//CR : 10971 End
                paramList.Add(new SQLParameter("@EditRemarks", oBOIssue.EngineerRemarks));  // CR :15381
                paramList.Add(new SQLParameter("@DeptId", oBOIssue.DepartmentID));  // CR :15908
                paramList.Add(new SQLParameter("@DomainID", oBOIssue.DomainID));  // CR :17421 by Nazia
                paramList.Add(new SQLParameter("@problem_details_html", oBOIssue.problem_details_html));  // OR :18251 by Nazia
                paramList.Add(new SQLParameter("@IPAddress", oBOIssue.IPAddress));  // CR :44380
                paramList.Add(new SQLParameter("@Latitude", oBOIssue.Latitude));  // CR :44380
                paramList.Add(new SQLParameter("@Longitude", oBOIssue.Longitude));  // CR :44380

                dr = _executequery.ExecuteReader(SQL, paramList);
                while (dr.Read())
                {
                    oboIssueOut.actualTime = dr["actualTime"] == null ? string.Empty : Convert.ToString(dr["actualTime"]);
                    oboIssueOut.complaint_id = dr["complaint_id"] == null ? string.Empty : Convert.ToString(dr["complaint_id"]);
                    oboIssueOut.expected_date = dr["expected_date"] == null ? string.Empty : Convert.ToString(dr["expected_date"]);
                    oboIssueOut.loggedin_time = dr["loggedin_time"] == null ? string.Empty : Convert.ToString(dr["loggedin_time"]);
                    //  oboIssueOut.problem_details = dr["problem_details"] == null ? string.Empty : Convert.ToString(dr["problem_details"]);
                    oboIssueOut.resolution = dr["resolution"] == null ? string.Empty : Convert.ToString(dr["resolution"]);
                    oboIssueOut.response = dr["response"] == null ? string.Empty : Convert.ToString(dr["response"]);
                    oboIssueOut.severity_description = dr["severity_description"] == null ? string.Empty : Convert.ToString(dr["severity_description"]);
                    oboIssueOut.severity_ids = dr["severity_ids"] == null ? string.Empty : Convert.ToString(dr["severity_ids"]);
                    oboIssueOut.specific_emailid = dr["specific_emailid"] == null ? string.Empty : Convert.ToString(dr["specific_emailid"]);
                    oboIssueOut.specific_engineer = dr["specific_engineer"] == null ? string.Empty : Convert.ToString(dr["specific_engineer"]);
                }
                dr.Close();
                return oboIssueOut;
            }
            catch (Exception)
            { throw; }
        }
    }
}
