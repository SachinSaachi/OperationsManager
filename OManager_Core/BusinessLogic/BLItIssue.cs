using Common.Models;
using OManager_Core.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Models.BOIssue;
using static Common.Models.BOLogin;

namespace OManager_Core.businessLogic
{
	public interface IBLItissue 
	{
		UserTypeMasterList GetUserTypeMasterList();
		UserDetail GetUserDetailByLogin(LoginDetail oBOLoginDetail, int UsertypeID);
		bool CheckLoginStatus(int? userrid);
		bool InsertLoginCheck(int? userrid, string guiuniqueid, string LoginIP, string MacId);
		LoginDetail GetLoginStatus(int? Userid, string GUIIniqueid);
		PendingUser GetDashboardDetails(int? UserId);
		DepartmentMasterList GetDepartmentMasterList(string userTypeID = "");
		CompanyMasterList GetCompanyMasterList();
		LocationMasterList GetLocationMasterList(int intDepartmentid);
		TypeMasterList GetTypeMasterList(string userTypeID = "");
		CategoryMasterList GetCategoryMasterList(int type, int? UserTypeID, int companyid, int DepartID);
		SubCategoryMasterList GetSubCategoryMasterList(int CategoryId);
		ProblemMasterList GetProblemMasterList(int CategoryId, int SubCategoryId);
        CircleMasterList GetCircleMasterList();
        IssueOut SubmitIssue(IssueLog oBOIssue);
    }
	public class BLItIssue: IBLItissue
	{
		private IDLItIssue _dLItIssue;
		public BLItIssue(IDLItIssue dLItIssue)
		{
			_dLItIssue = dLItIssue;
		}

		public UserTypeMasterList GetUserTypeMasterList()
		{
			try
			{
				return _dLItIssue.GetUserTypeMasterList();
			}
			catch (Exception)
			{

				throw;
			}
		}

		public UserDetail GetUserDetailByLogin(LoginDetail oBOLoginDetail, int UsertypeID)
		{
			try
			{
				return _dLItIssue.GetUserDetailByLogin(oBOLoginDetail, UsertypeID);
			}
			catch (Exception)
			{

				throw;
			}

		}

		public bool CheckLoginStatus(int? userrid)
		{
			try
			{
				return _dLItIssue.CheckLoginStatus(userrid);
			}
			catch (Exception)
			{

				throw;
			}
		}

		public bool InsertLoginCheck(int? userrid, string guiuniqueid, string LoginIP, string MacId)
		{
			try
			{
				return _dLItIssue.InsertLoginCheck(userrid, guiuniqueid, LoginIP, MacId);
			}
			catch (Exception)
			{

				throw;
			}
		}

		public LoginDetail GetLoginStatus(int? Userid, string GUIIniqueid)
		{
			try
			{
				return _dLItIssue.GetLoginStatus(Userid, GUIIniqueid);
			}
			catch (Exception)
			{

				throw;
			}
		}


		public PendingUser GetDashboardDetails(int? UserId)
		{
			try
			{
				return _dLItIssue.GetDashboardDetails(UserId);
			}
			catch (Exception)
			{

				throw;
			}
		}

        public DepartmentMasterList GetDepartmentMasterList(string userTypeID = "")
        {
            try
            {
                return _dLItIssue.GetDepartmentMasterList(userTypeID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CompanyMasterList GetCompanyMasterList()
        {
            try
            {
                return _dLItIssue.GetCompanyMasterList();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public LocationMasterList GetLocationMasterList(int intDepartmentid)
        {
            try
            {
                return _dLItIssue.GetLocationMasterList(intDepartmentid);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TypeMasterList GetTypeMasterList(string userTypeID = "")
        {
            try
            {
                return _dLItIssue.GetTypeMasterList(userTypeID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CategoryMasterList GetCategoryMasterList(int type, int? UserTypeID, int companyid, int DepartID)
        {
            try
            {
                return _dLItIssue.GetCategoryMasterList(type, UserTypeID, companyid, DepartID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public SubCategoryMasterList GetSubCategoryMasterList(int CategoryId)
        {
            try
            {
                return _dLItIssue.GetSubCategoryMasterList(CategoryId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ProblemMasterList GetProblemMasterList(int CategoryId, int SubCategoryId)
        {
            try
            {
                return _dLItIssue.GetProblemMasterList(CategoryId, SubCategoryId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public CircleMasterList GetCircleMasterList()
        {
            try
            {
                return _dLItIssue.GetCircleMasterList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IssueOut SubmitIssue(IssueLog oBOIssue)
        {
            try
            {
                return _dLItIssue.SubmitIssue(oBOIssue);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
