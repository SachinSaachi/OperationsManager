using Common.Models;
using HRMS_Core.DataAccessLayer.DatabaseHelper.Datablase.Common;
using HRMS_Core.DataAccessLayer.DatabaseHelper.Datablase.DatabaseHelper;
using OManager_Core.businessLogic;
using System.ComponentModel.Design;
using System.Data;
using System.DirectoryServices;
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
        CategoryMasterList GetCategoryMasterList(int type, int UserTypeID, int companyid, int DepartID);
        SubCategoryMasterList GetSubCategoryMasterList(int CategoryId);
        ProblemMasterList GetProblemMasterList(int CategoryId, int SubCategoryId);
        CommondataList GetStatusList();
        List<GroupMaster> GetGroupListRoleWise(string Role, int UserID, string Brand);

        PendingUserList GetLogSheet(string status_id,
         string employee_type, int CenterID, int loginid, string FromDate, string ToDate, string ticketID, string GroupID, int Group_CompanyID);

    }
    public class DLItIssue : IDLItIssue
    {
        private IExecuteQuery _executequery;
        public DLItIssue(IExecuteQuery executequery)
        {
            _executequery = executequery;
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
        public CategoryMasterList GetCategoryMasterList(int type, int UserTypeID, int companyid, int DepartID)
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

        public CommondataList GetStatusList()
        {
            String SQL = string.Empty;
            DataTableReader dr;
            // ParameterList paramList = new ParameterList();
            Commondata oBOCommondata = null;
            CommondataList oBOCommondataList = new CommondataList();
            try
            {
                SQL = "Usp_GetStatusList";
                dr = _executequery.ExecuteReader(SQL);
                while (dr.Read())
                {
                    oBOCommondata = new Commondata();

                    oBOCommondata.ID = Convert.ToInt32(dr["status_id"]);

                    oBOCommondata.Name = dr["status_name"] == null ? string.Empty : Convert.ToString(dr["status_name"]);
                    oBOCommondataList.Add(oBOCommondata);
                }
                dr.Close();
                return oBOCommondataList;
            }
            catch (Exception)
            { throw; }
        }

        public List<GroupMaster> GetGroupListRoleWise(string Role, int UserID, string CompanyId)
        {
            DataTableReader dr = null;
            List<GroupMaster> oboGroupMasterList = new List<GroupMaster>();
            GroupMaster oBOGroupMaster;
            String SQL = String.Empty;
            ParameterList paramList = new ParameterList();
            try
            {
                SQL = "Usp_GetGroupListRoleWise";
                paramList.Add(new SQLParameter("@Role", Role));
                paramList.Add(new SQLParameter("@UserID", UserID));
                paramList.Add(new SQLParameter("@CompanyId", CompanyId));
                dr = _executequery.ExecuteReader(SQL, paramList);
                while (dr.Read())
                {
                    oBOGroupMaster = new GroupMaster();
                    oBOGroupMaster.GroupID = Convert.ToInt32(dr["GroupID"]);
                    oBOGroupMaster.GroupName = Convert.ToString(dr["GroupName"]).Trim();
                    oboGroupMasterList.Add(oBOGroupMaster);
                }
                dr.Close();
                return oboGroupMasterList;
            }
            catch (Exception)
            { throw; }
        }

        public PendingUserList GetLogSheet(string status_id,  string employee_type, int CenterID, int loginid,  string FromDate, string ToDate, string ticketID, string GroupID, int Group_CompanyID)
        {
            DataTableReader dr = null;
            PendingUserList oboPendingUserList = new PendingUserList();
           PendingUser oBOPendingUser;
            String SQL = String.Empty;
            ParameterList paramList = new ParameterList();
            try
            {
                SQL = "Usp_GetLogSheet";
                //Start changes by nazia Cr-16442
                //paramList.Add(new SQLParameter("@ccedepartment", ccedepartment));
                //paramList.Add(new SQLParameter("@spproblem", spproblem));
                //paramList.Add(new SQLParameter("@itdepartment", itdepartment));
                //End changes by nazia Cr-16442
                paramList.Add(new SQLParameter("@status_id", status_id));
                paramList.Add(new SQLParameter("@employee_type", employee_type));
                paramList.Add(new SQLParameter("@CenterID", CenterID));
                paramList.Add(new SQLParameter("@loginid", loginid));
                paramList.Add(new SQLParameter("@monitoringId", "0"));
                paramList.Add(new SQLParameter("@FromDate", FromDate));
                paramList.Add(new SQLParameter("@ToDate",ToDate));
                paramList.Add(new SQLParameter("@complaint_id", ticketID));
                paramList.Add(new SQLParameter("@Group_id", GroupID));
                paramList.Add(new SQLParameter("@Group_CompanyID", Group_CompanyID));
                dr = _executequery.ExecuteReader(SQL, paramList);
                while (dr.Read())
                {
                    oBOPendingUser = new Common.Models.BOLogin.PendingUser();
                    oBOPendingUser.complaint_id = dr["complaint_id"].ToString();
                    //oBOPendingUser.resolved_engineer = dr["resolved_engineer"] == null ? string.Empty : dr["resolved_engineer"].ToString();
                    //oBOPendingUser.response_engineer = dr["response_engineer"] == null ? string.Empty : dr["response_engineer"].ToString();
                    //oBOPendingUser.remarks_engineer = dr["remarks_engineer"] == null ? string.Empty : dr["remarks_engineer"].ToString();
                    oBOPendingUser.severity_id = dr["severity_id"] == null ? string.Empty : dr["severity_id"].ToString();
                    oBOPendingUser.status = dr["status"] == null ? string.Empty : dr["status"].ToString();
                    oBOPendingUser.problem_details = dr["ProblemName"] == null ? string.Empty : dr["ProblemName"].ToString();
                    oBOPendingUser.shortproblem = dr["shortproblem"] == null ? string.Empty : dr["shortproblem"].ToString();
                    //oBOPendingUser.spproblem = dr["spproblem"] == null ? string.Empty : dr["spproblem"].ToString();
                    oBOPendingUser.loggedin_time = dr["loggedin_time"] == null ? string.Empty : dr["loggedin_time"].ToString();
                    //oBOPendingUser.employee_department = dr["employee_department"] == null ? string.Empty : dr["employee_department"].ToString();
                    //oBOPendingUser.it_department = dr["it_department"] == null ? string.Empty : dr["it_department"].ToString();
                    oBOPendingUser.engineer_name = dr["engineer_name"] == null ? string.Empty : dr["engineer_name"].ToString();
                    //oBOPendingUser.response_time = dr["response_time"] == null ? string.Empty : dr["response_time"].ToString();
                    oBOPendingUser.resolution_time = dr["resolution_time"] == null ? string.Empty : dr["resolution_time"].ToString();
                    oBOPendingUser.cce_name = dr["cce_name"] == null ? string.Empty : dr["cce_name"].ToString();
                    //oBOPendingUser.contact = dr["contact"] == null ? string.Empty : dr["contact"].ToString();
                    //oBOPendingUser.extension = dr["extension"] == null ? string.Empty : dr["extension"].ToString();
                    //oBOPendingUser.cce_remarks = dr["cce_remarks"] == null ? string.Empty : dr["cce_remarks"].ToString();
                    //oBOPendingUser.engineer_remarks = dr["engineer_remarks"] == null ? string.Empty : dr["engineer_remarks"].ToString();
                    //oBOPendingUser.ExpctdclsrDatetime = dr["ExpctdclsrDatetime"] == null ? string.Empty : Convert.ToString(dr["ExpctdclsrDatetime"]);
                    oBOPendingUser.CompanyName = dr["CompanyName"] == null ? string.Empty : Convert.ToString(dr["CompanyName"]);
                    oBOPendingUser.ChildComplaint_id = dr["ChildComplaint_id"] == null ? string.Empty : Convert.ToString(dr["ChildComplaint_id"]);
                    //  oBOPendingUser.TicketAssignTO = dr["ChildAssignTo"] == null ? string.Empty : Convert.ToString(dr["ChildAssignTo"]);
                    oBOPendingUser.BreachMinute = dr["BreachMinute"] == null ? string.Empty : Convert.ToString(dr["BreachMinute"]);
                    //oBOPendingUser.SLAMin = dr["SLAmin"] == null ? 0 : Convert.ToInt32(dr["SLAmin"]);
                    oBOPendingUser.UserType = dr["UserType"] == null ? string.Empty : Convert.ToString(dr["UserType"]);
                    oBOPendingUser.ParentComplaint_Id = dr["ParentComplaint_Id"] == null ? string.Empty : Convert.ToString(dr["ParentComplaint_Id"]); // CR: 15793
                    oBOPendingUser.DepartmentName = Convert.ToString(dr["DepartmentName"]); // CR: 15908
                    oBOPendingUser.Engineer_Remarks = Convert.ToString(dr["Engineer_Remarks"]);
                    oBOPendingUser.ResolvedBy = Convert.ToString(dr["ResolvedBy"]);
                    oBOPendingUser.LocationName = Convert.ToString(dr["LocationName"]);

                    oboPendingUserList.Add(oBOPendingUser);
                }
                dr.Close();
                return oboPendingUserList;
            }
            catch (Exception)
            { throw; }
        }


    }
}
