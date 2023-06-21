namespace Common.Models
{
	public class BOLogin
	{
		public class LoginDetail
		{
			public LoginDetail()
			{
				//
				// TODO: Add constructor logic here
				//
			}
			private string _Type;
			private string _Username;
			private string _Password;
			private string _name;
			private string _value;
			private Int16 _CompanyId;
			public Int16 CompanyId
			{
				get { return _CompanyId; }
				set { _CompanyId = value; }
			}
			// CR : 10971 Start
			private string _Domain;
			public string Domain
			{
				get { return _Domain; }
				set { _Domain = value; }
			}
			// CR : 10971 End 
			public string name
			{
				get
				{
					return _name;
				}
				set
				{
					_name = value;
				}
			}
			public string value
			{
				get
				{
					return _value;
				}
				set
				{
					_value = value;
				}
			}
			public string Type
			{
				get
				{
					return _Type;
				}
				set
				{
					_Type = value;
				}
			}
			public string Username
			{
				get
				{
					return _Username;
				}
				set
				{
					_Username = value;
				}
			}
			public string Password
			{
				get
				{
					return _Password;
				}
				set
				{
					_Password = value;
				}
			}
		}
		public class UserDetail
		{
			public UserDetail()
			{
				//
				// TODO: Add constructor logic here
				//
			}
			private int _user_id;
			private string _employee_name;
			private string _login_name;
			private string _login_password;
			private string _email_id;
			private string _department;
			private string _contact;
			private string _extension;
			private string _last_visited;
			private string _designation;
			private string _direct_contact;
			private string _Address;
			private string _city;
			private string _state;
			private string _admin;
			private string _engineer;
			private string _cce;
			private string _employee_type;
			private int _UGID;
			private int _centerid;
			private string _Center;
			// CR : 10971 Start
			private string _Lan_ID;
			private string _User_type;
			private string _IsAvailable;
			private string _Domain;
			private string _Empcode;

			public string Empcode
			{
				get { return _Empcode; }
				set { _Empcode = value; }
			}
			public string Domain
			{
				get { return _Domain; }
				set { _Domain = value; }
			}
			public string IsAvailable
			{
				get { return _IsAvailable; }
				set { _IsAvailable = value; }
			}

			public string Lan_ID
			{
				get
				{
					return _Lan_ID;
				}
				set
				{
					_Lan_ID = value;
				}
			}
			public string User_type
			{
				get
				{
					return _User_type;
				}
				set
				{
					_User_type = value;
				}
			}
			// End

			public string Center
			{
				get
				{
					return _Center;
				}
				set
				{
					_Center = value;
				}
			}
			public int centerid
			{
				get
				{
					return _centerid;
				}
				set
				{
					_centerid = value;
				}
			}
			public int user_id
			{
				get
				{
					return _user_id;
				}
				set
				{
					_user_id = value;
				}
			}
			public string employee_name
			{
				get
				{
					return _employee_name;
				}
				set
				{
					_employee_name = value;
				}
			}
			public string login_name
			{
				get
				{
					return _login_name;
				}
				set
				{
					_login_name = value;
				}
			}
			public string login_password
			{
				get
				{
					return _login_password;
				}
				set
				{
					_login_password = value;
				}
			}
			public string email_id
			{
				get
				{
					return _email_id;
				}
				set
				{
					_email_id = value;
				}
			}
			public string department
			{
				get
				{
					return _department;
				}
				set
				{
					_department = value;
				}
			}
			public string contact
			{
				get
				{
					return _contact;
				}
				set
				{
					_contact = value;
				}
			}
			public string extension
			{
				get
				{
					return _extension;
				}
				set
				{
					_extension = value;
				}
			}
			public string last_visited
			{
				get
				{
					return _last_visited;
				}
				set
				{
					_last_visited = value;
				}
			}
			public string designation
			{
				get
				{
					return _designation;
				}
				set
				{
					_designation = value;
				}
			}
			public string direct_contact
			{
				get
				{
					return _direct_contact;
				}
				set
				{
					_direct_contact = value;
				}
			}
			public string Address
			{
				get
				{
					return _Address;
				}
				set
				{
					_Address = value;
				}
			}
			public string city
			{
				get
				{
					return _city;
				}
				set
				{
					_city = value;
				}
			}
			public string state
			{
				get
				{
					return _state;
				}
				set
				{
					_state = value;
				}
			}
			public string admin
			{
				get
				{
					return _admin;
				}
				set
				{
					_admin = value;
				}
			}
			public string engineer
			{
				get
				{
					return _engineer;
				}
				set
				{
					_engineer = value;
				}
			}
			public string cce
			{
				get
				{
					return _cce;
				}
				set
				{
					_cce = value;
				}
			}
			public string employee_type
			{
				get
				{
					return _employee_type;
				}
				set
				{
					_employee_type = value;
				}
			}
			public int UGID
			{
				get
				{
					return _UGID;
				}
				set
				{
					_UGID = value;
				}
			}
		}

		[Serializable]                  // CR : 15793
		public class PendingUser
		{
			public PendingUser()
			{
				//
				// TODO: Add constructor logic here
				//
			}
			private string _complaint_id;
			private string _resolved_engineer;
			private string _response_engineer;
			private string _status;
			private string _problem_details;
			private string _severity_id;
			private string _problem_category;
			private string _loggedin_time;
			private string _response_time;
			private string _resolution_time;
			private string _cce_department;
			private string _it_department;
			private string _cce_name;
			private string _engineer_name;
			private string _cce_remarks;
			private string _engineer_remarks;
			private string _remarks_engineer;
			private string _shortproblem;
			private string _spproblem;
			private string _employee_department;
			private string _extension;
			private string _emailid;
			private string _employee_time;
			private string _specific_emailid;
			private string _engineer_time;

			// added By ramesh start 
			private string _UsersAffected;
			private string _contact;
			private string _issuevalidity;
			private string _reasontolog;
			private string _subcategory;
			private string _expected_date;
			private string _pending_reason;
			private string _history_id;
			private string _specific_id;
			private string _employee_name;
			private string _logindepartment;
			private string _reopen_time;
			private string _user_id;
			private string _resolution_time_actual;
			private string _logintime_actual;

			// CR 10971
			private string _ChildComplaint_id;
			private int _status_id;
			private string _attachment;
			private string _vcno;
			private int _ComplaintTranID;
			private int _Company_ID;
			private int _Typ_ID;
			private int _Location_Id;
			private string _Location_Name;
			private string _Circle_ID;
			private string _SpecificProblem;
			private string _UrlPath;
			private string _UrlFullPath;
			private string _UrlPathTech;
			private string _UrlFullPathTech;

			private int _TotalTicket;
			private int _UnresolvedTicket;
			private int _WipTicket;
			private int _ResolvedTicket;
			private int _PendingTicket;
			private int _ClosedTicket;
			private int _OpenIssuesWithinSLA;
			private int _AbouttoBreachSLA;
			private int _BreachedSLA;
			private string _ExpctdclsrDatetime;
			private string _BreachMinute;
			private string _CompanyName;
			private string _TypeName;
			private string _UserType;
			private int _UserTypeId;
			private int _CategoryID;
			private int _SubCategoryID;
			private int _ProblemID;
			private int _SLAMin;
			private string _Engineer_Remarks;
			private string _ResolvedBy;
			private string _LocationName;

			public string Engineer_Remarks
			{
				get { return _Engineer_Remarks; }
				set { _Engineer_Remarks = value; }
			}
			public string ResolvedBy
			{
				get { return _ResolvedBy; }
				set { _ResolvedBy = value; }
			}
			public string LocationName
			{
				get { return _LocationName; }
				set { _LocationName = value; }
			}

			public int OpenIssuesWithinSLA
			{
				get { return _OpenIssuesWithinSLA; }
				set { _OpenIssuesWithinSLA = value; }
			}
			public int AbouttoBreachSLA
			{
				get { return _AbouttoBreachSLA; }
				set { _AbouttoBreachSLA = value; }
			}
			public int BreachedSLA
			{
				get { return _BreachedSLA; }
				set { _BreachedSLA = value; }
			}
			public int SubCategoryID
			{
				get { return _SubCategoryID; }
				set { _SubCategoryID = value; }
			}
			public int CategoryID
			{
				get { return _CategoryID; }
				set { _CategoryID = value; }
			}
			public int ProblemID
			{
				get { return _ProblemID; }
				set { _ProblemID = value; }
			}


			public int SLAMin
			{
				get { return _SLAMin; }
				set { _SLAMin = value; }
			}

			public int UserTypeID
			{
				get { return _UserTypeId; }
				set { _UserTypeId = value; }
			}
			public string UserType
			{
				get { return _UserType; }
				set { _UserType = value; }
			}


			public string CompanyName
			{
				get { return _CompanyName; }
				set { _CompanyName = value; }
			}

			public string BreachMinute
			{
				get
				{
					return _BreachMinute;
				}
				set
				{
					_BreachMinute = value;
				}
			}
			public string ExpctdclsrDatetime
			{
				get
				{
					return _ExpctdclsrDatetime;
				}
				set
				{
					_ExpctdclsrDatetime = value;
				}
			}

			public string ChildComplaint_id
			{
				get
				{
					return _ChildComplaint_id;
				}
				set
				{
					_ChildComplaint_id = value;
				}
			}
			public int status_id
			{
				get
				{
					return _status_id;
				}
				set
				{
					_status_id = value;
				}
			}
			public string attachment
			{
				get
				{
					return _attachment;
				}
				set
				{
					_attachment = value;
				}
			}
			public string Vcno
			{
				get
				{
					return _vcno;
				}
				set
				{
					_vcno = value;
				}
			}
			public int ComplaintTranID
			{
				get
				{
					return _ComplaintTranID;
				}
				set
				{
					_ComplaintTranID = value;
				}
			}
			public int Company_ID
			{
				get
				{
					return _Company_ID;
				}
				set
				{
					_Company_ID = value;
				}
			}
			public int Typ_ID
			{
				get
				{
					return _Typ_ID;
				}
				set
				{
					_Typ_ID = value;
				}
			}
			public int Location_Id
			{
				get
				{
					return _Location_Id;
				}
				set
				{
					_Location_Id = value;
				}
			}
			public string Location_Name
			{
				get
				{
					return _Location_Name;
				}
				set
				{
					_Location_Name = value;
				}
			}
			public string Circle_ID
			{
				get
				{
					return _Circle_ID;
				}
				set
				{
					_Circle_ID = value;
				}
			}
			public string TypeName
			{
				get
				{
					return _TypeName;
				}
				set
				{
					_TypeName = value;
				}
			}

			public string SpecificProblem
			{
				get
				{
					return _SpecificProblem;
				}
				set
				{
					_SpecificProblem = value;
				}
			}

			public string UrlPath
			{
				get
				{
					return _UrlPath;
				}
				set
				{
					_UrlPath = value;
				}
			}
			public string UrlFullPath
			{
				get
				{
					return _UrlFullPath;
				}
				set
				{
					_UrlFullPath = value;
				}
			}

			public string UrlPathTech
			{
				get
				{
					return _UrlPathTech;
				}
				set
				{
					_UrlPathTech = value;
				}
			}
			public string UrlFullPathTech
			{
				get
				{
					return _UrlFullPathTech;
				}
				set
				{
					_UrlFullPathTech = value;
				}
			}


			public int TotalTicket
			{
				get
				{
					return _TotalTicket;
				}
				set
				{
					_TotalTicket = value;
				}
			}
			public int UnresolvedTicket
			{
				get
				{
					return _UnresolvedTicket;
				}
				set
				{
					_UnresolvedTicket = value;
				}
			}
			public int WipTicket
			{
				get
				{
					return _WipTicket;
				}
				set
				{
					_WipTicket = value;
				}
			}
			public int ResolvedTicket
			{
				get
				{
					return _ResolvedTicket;
				}
				set
				{
					_ResolvedTicket = value;
				}
			}
			public int PendingTicket
			{
				get
				{
					return _PendingTicket;
				}
				set
				{
					_PendingTicket = value;
				}
			}

			public int ClosedTicket
			{
				get
				{
					return _ClosedTicket;
				}
				set
				{
					_ClosedTicket = value;
				}
			}

			public int ReOpenTicket { get; set; }

			// End 

			public string UsersAffected
			{
				get
				{
					return _UsersAffected;
				}
				set
				{
					_UsersAffected = value;
				}
			}
			public string issuevalidity
			{
				get
				{
					return _issuevalidity;
				}
				set
				{
					_issuevalidity = value;
				}
			}
			public string reasontolog
			{
				get
				{
					return _reasontolog;
				}
				set
				{
					_reasontolog = value;
				}
			}
			public string subcategory
			{
				get
				{
					return _subcategory;
				}
				set
				{
					_subcategory = value;
				}
			}
			public string expected_date
			{
				get
				{
					return _expected_date;
				}
				set
				{
					_expected_date = value;
				}
			}
			public string pending_reason
			{
				get
				{
					return _pending_reason;
				}
				set
				{
					_pending_reason = value;
				}
			}
			public string history_id
			{
				get
				{
					return _history_id;
				}
				set
				{
					_history_id = value;
				}
			}
			public string specific_id
			{
				get
				{
					return _specific_id;
				}
				set
				{
					_specific_id = value;
				}
			}
			public string employee_name
			{
				get
				{
					return _employee_name;
				}
				set
				{
					_employee_name = value;
				}
			}
			public string logindepartment
			{
				get
				{
					return _logindepartment;
				}
				set
				{
					_logindepartment = value;
				}
			}
			public string reopen_time
			{
				get
				{
					return _reopen_time;
				}
				set
				{
					_reopen_time = value;
				}
			}
			public string user_id
			{
				get
				{
					return _user_id;
				}
				set
				{
					_user_id = value;
				}
			}
			public string resolution_time_actual
			{
				get
				{
					return _resolution_time_actual;
				}
				set
				{
					_resolution_time_actual = value;
				}
			}
			public string logintime_actual
			{
				get
				{
					return _logintime_actual;
				}
				set
				{
					_logintime_actual = value;
				}
			}
			// added By ramesh END
			public string engineer_time
			{
				get
				{
					return _engineer_time;
				}
				set
				{
					_engineer_time = value;
				}
			}
			public string specific_emailid
			{
				get
				{
					return _specific_emailid;
				}
				set
				{
					_specific_emailid = value;
				}
			}
			public string employee_time
			{
				get
				{
					return _employee_time;
				}
				set
				{
					_employee_time = value;
				}
			}
			public string emailid
			{
				get
				{
					return _emailid;
				}
				set
				{
					_emailid = value;
				}
			}
			public string extension
			{
				get
				{
					return _extension;
				}
				set
				{
					_extension = value;
				}
			}
			public string contact
			{
				get
				{
					return _contact;
				}
				set
				{
					_contact = value;
				}
			}
			public string employee_department
			{
				get
				{
					return _employee_department;
				}
				set
				{
					_employee_department = value;
				}
			}
			public string spproblem
			{
				get
				{
					return _spproblem;
				}
				set
				{
					_spproblem = value;
				}
			}
			public string shortproblem
			{
				get
				{
					return _shortproblem;
				}
				set
				{
					_shortproblem = value;
				}
			}
			public string remarks_engineer
			{
				get
				{
					return _remarks_engineer;
				}
				set
				{
					_remarks_engineer = value;
				}
			}
			public string complaint_id
			{
				get
				{
					return _complaint_id;
				}
				set
				{
					_complaint_id = value;
				}
			}
			public string resolved_engineer
			{
				get
				{
					return _resolved_engineer;
				}
				set
				{
					_resolved_engineer = value;
				}
			}
			public string response_engineer
			{
				get
				{
					return _response_engineer;
				}
				set
				{
					_response_engineer = value;
				}
			}
			public string status
			{
				get
				{
					return _status;
				}
				set
				{
					_status = value;
				}
			}
			public string problem_details
			{
				get
				{
					return _problem_details;
				}
				set
				{
					_problem_details = value;
				}
			}
			public string severity_id
			{
				get
				{
					return _severity_id;
				}
				set
				{
					_severity_id = value;
				}
			}
			public string problem_category
			{
				get
				{
					return _problem_category;
				}
				set
				{
					_problem_category = value;
				}
			}
			public string loggedin_time
			{
				get
				{
					return _loggedin_time;
				}
				set
				{
					_loggedin_time = value;
				}
			}
			public string response_time
			{
				get
				{
					return _response_time;
				}
				set
				{
					_response_time = value;
				}
			}
			public string resolution_time
			{
				get
				{
					return _resolution_time;
				}
				set
				{
					_resolution_time = value;
				}
			}
			public string cce_department
			{
				get
				{
					return _cce_department;
				}
				set
				{
					_cce_department = value;
				}
			}
			public string it_department
			{
				get
				{
					return _it_department;
				}
				set
				{
					_it_department = value;
				}
			}
			public string cce_name
			{
				get
				{
					return _cce_name;
				}
				set
				{
					_cce_name = value;
				}
			}
			public string engineer_name
			{
				get
				{
					return _engineer_name;
				}
				set
				{
					_engineer_name = value;
				}
			}
			public string cce_remarks
			{
				get
				{
					return _cce_remarks;
				}
				set
				{
					_cce_remarks = value;
				}
			}
			public string engineer_remarks
			{
				get
				{
					return _engineer_remarks;
				}
				set
				{
					_engineer_remarks = value;
				}
			}

			public string RCAID
			{
				get; set;
			}
			public string TicketID
			{
				get; set;
			}
			public string TicketAssignTO
			{
				get; set;
			}

			public string CRNumber
			{
				get; set;
			}

			public string TypeOfTicket
			{
				get; set;
			}

			public string Remarks
			{
				get; set;
			}
			public string RCADescription
			{
				get; set;
			}
			public string ModifiedBy
			{
				get; set;
			}

			public string ITEngineerName { get; set; }
			public string CreatedOn { get; set; }
			public string ModifiedOn { get; set; }
			public string CreatedBy { get; set; }

			public string ParentComplaint_Id { get; set; }  // CR: 15793
			public int DepartmentID { get; set; }  // CR: 15908
			public string DepartmentName { get; set; }  // CR: 15908


			public string Emp_Code { get; set; }
			public string DomainID { get; set; } //CR: 17421 by nazia

			public string problem_details_html { get; set; } //OR: 18251 by nazia
			public int DevelopmentTaskCount { get; set; }  //Added by Rajesh
			public int IssueLogCount { get; set; } //Added by Rajesh

		}
		public class PendingUserList : List<PendingUser>
		{
			public PendingUserList()
			{
			}

		}

        public class LocationMaster
        {
            public LocationMaster()
            {
                //
                // TODO: Add constructor logic here
                //
            }
            private int _ID;
            private string _LocationName;
            public int LocationID
            {
                get
                {
                    return _ID;
                }
                set
                {
                    _ID = value;
                }
            }
            public string LocationName
            {
                get
                {
                    return _LocationName;
                }
                set
                {
                    _LocationName = value;
                }
            }
        }
        public class LocationMasterList : List<LocationMaster>
        {
            public LocationMasterList()
            {
            }
        }

        public class TypeMaster
        {
            public TypeMaster()
            {
                //
                // TODO: Add constructor logic here
                //
            }
            private int _ID;
            private string _TypeName;
            public int TypeID
            {
                get
                {
                    return _ID;
                }
                set
                {
                    _ID = value;
                }
            }
            public string TypeName
            {
                get
                {
                    return _TypeName;
                }
                set
                {
                    _TypeName = value;
                }
            }
        }
        public class TypeMasterList : List<TypeMaster>
        {
            public TypeMasterList()
            {
            }
        }

        public class CircleMaster
        {
            public CircleMaster()
            {
                //
                // TODO: Add constructor logic here
                //
            }
            private int _Circle_ID;
            private string _Circle_Name;
            private int _CircleMap_ID;
            public int CircleID
            {
                get
                {
                    return _Circle_ID;
                }
                set
                {
                    _Circle_ID = value;
                }
            }
            public string CircleName
            {
                get
                {
                    return _Circle_Name;
                }
                set
                {
                    _Circle_Name = value;
                }
            }

            public int CircleMapID
            {
                get
                {
                    return _CircleMap_ID;
                }
                set
                {
                    _CircleMap_ID = value;
                }
            }
        }
        public class CircleMasterList : List<CircleMaster>
        {
            public CircleMasterList()
            {
            }
        }

    }

}
