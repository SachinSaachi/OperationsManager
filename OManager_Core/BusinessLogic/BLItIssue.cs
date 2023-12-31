﻿using Common.Models;
using OManager_Core.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Models.BOIssue;
using static Common.Models.BOLogin;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
		CategoryMasterList GetCategoryMasterList(int type, int UserTypeID, int companyid, int DepartID);
		SubCategoryMasterList GetSubCategoryMasterList(int CategoryId);
		ProblemMasterList GetProblemMasterList(int CategoryId, int SubCategoryId);
        CommondataList GetStatusList();
        List<GroupMaster> GetGroupListRoleWise(string Role, int UserID, string CompanyId);
        PendingUserList GetLogSheet(string status_id,string employee_type, int CenterID, int loginid, string FromDate, string ToDate, string ticketID , string GroupID, int Group_CompanyID);
      


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

        public CategoryMasterList GetCategoryMasterList(int type, int UserTypeID, int companyid, int DepartID)
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

        public CommondataList GetStatusList()
        {
            try
            {
                return _dLItIssue.GetStatusList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<GroupMaster> GetGroupListRoleWise(string Role, int UserID, string CompanyId)
        {
            try
            {
                return _dLItIssue.GetGroupListRoleWise(Role,UserID, CompanyId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public PendingUserList GetLogSheet(string status_id, string employee_type, int CenterID, int loginid, string FromDate, string ToDate, string ticketID, string GroupID, int Group_CompanyID)
        {
            try
            {
                return _dLItIssue.GetLogSheet(status_id, employee_type, CenterID, loginid, FromDate, ToDate, ticketID, GroupID, Group_CompanyID);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
