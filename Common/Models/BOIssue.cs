using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
	public class BOIssue
	{
		public class UserTypeMaster
		{
			public UserTypeMaster()
			{
				//
				// TODO: Add constructor logic here
				//
			}
			private int _UserTypeID;
			private string _UserType;
			private string _UserShortName;
			public int UserTypeID
			{
				get
				{
					return _UserTypeID;
				}
				set
				{
					_UserTypeID = value;
				}
			}
			public string UserType
			{
				get
				{
					return _UserType;
				}
				set
				{
					_UserType = value;
				}
			}

			public string UserShortName
			{
				get
				{
					return _UserShortName;
				}
				set
				{
					_UserShortName = value;
				}
			}
		}
		public class UserTypeMasterList : List<UserTypeMaster>
		{
			public UserTypeMasterList()
			{
			}

		}
        public class DepartmentMasterList : List<DepartmentMaster>
        {
            public DepartmentMasterList()
            {
            }
        }

        public class DepartmentMaster
        {
            public DepartmentMaster()
            {

            }
            public int DepartmentID { get; set; }
            public string DepartmentName { get; set; }
        }

        public class CompanyMasterList : List<CompanyMaster>
        {
            public CompanyMasterList()
            {
            }
        }
        public class CompanyMaster
        {
            public CompanyMaster()
            {
                //
                // TODO: Add constructor logic here
                //
            }
            private int _Company_ID;
            private string _CompanyName;
            public int CompanyID
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
            public string CompanyName
            {
                get
                {
                    return _CompanyName;
                }
                set
                {
                    _CompanyName = value;
                }
            }
        }

        public class CategoryMaster
        {
            public CategoryMaster()
            {
                //
                // TODO: Add constructor logic here
                //
            }
            private int _CategoryId;
            private string _CategoryName;
            private int _TypeID;
            private int _CompanyId;
            private int _DeptId;
            private int _CategoryID;
            public int CategoryId
            {
                get
                {
                    return _CategoryId;
                }
                set
                {
                    _CategoryId = value;
                }
            }
            public string CategoryName
            {
                get
                {
                    return _CategoryName;
                }
                set
                {
                    _CategoryName = value;
                }
            }

            public int TypeID
            {
                get
                {
                    return _TypeID;
                }
                set
                {
                    _TypeID = value;
                }
            }
            public int CompanyId
            {
                get
                {
                    return _CompanyId;
                }
                set
                {
                    _CompanyId = value;
                }
            }
            public int DeptId
            {
                get
                {
                    return _DeptId;
                }
                set
                {
                    _DeptId = value;
                }
            }
            public int CategoryID
            {
                get
                {
                    return _CategoryID;
                }
                set
                {
                    _CategoryID = value;
                }
            }
        }
        public class CategoryMasterList : List<CategoryMaster>
        {
            public CategoryMasterList()
            {
            }
        }

        public class SubCategoryMaster
        {
            public SubCategoryMaster()
            {
                //
                // TODO: Add constructor logic here
                //
            }
            private int _SubCategoryId;
            private string _SubCategoryName;
            private int _CategoryId;
            public int SubCategoryId
            {
                get
                {
                    return _SubCategoryId;
                }
                set
                {
                    _SubCategoryId = value;
                }
            }
            public string SubCategoryName
            {
                get
                {
                    return _SubCategoryName;
                }
                set
                {
                    _SubCategoryName = value;
                }
            }

            public int CategoryId
            {
                get
                {
                    return _CategoryId;
                }
                set
                {
                    _CategoryId = value;
                }
            }


        }
        public class SubCategoryMasterList : List<SubCategoryMaster>
        {
            public SubCategoryMasterList()
            {
            }
        }

        public class ProblemMaster
        {
            public ProblemMaster()
            {
                //
                // TODO: Add constructor logic here
                //
            }
            private int _ProblemID;
            private string _ProblemName;
            private int _CategoryId;
            private int _SubCategoryId;
            private int _SLAID;
            private int _PopupRequired;
            private string _RequiredMessage;
            private int _GroupID;
            private string _Domain_Description;//OR-17832
            private int _PopupType;//OR-18251
            public int ProblemID
            {
                get
                {
                    return _ProblemID;
                }
                set
                {
                    _ProblemID = value;
                }
            }
            public string ProblemName
            {
                get
                {
                    return _ProblemName;
                }
                set
                {
                    _ProblemName = value;
                }
            }
            public int CategoryId
            {
                get
                {
                    return _CategoryId;
                }
                set
                {
                    _CategoryId = value;
                }
            }
            public int SubCategoryId
            {
                get
                {
                    return _SubCategoryId;
                }
                set
                {
                    _SubCategoryId = value;
                }
            }
            public int SLAID
            {
                get
                {
                    return _SLAID;
                }
                set
                {
                    _SLAID = value;
                }
            }

            public int PopupRequired
            {
                get
                {
                    return _PopupRequired;
                }
                set
                {
                    _PopupRequired = value;
                }
            }

            public string RequiredMessage
            {
                get
                {
                    return _RequiredMessage;
                }
                set
                {
                    _RequiredMessage = value;
                }
            }
            public int GroupID
            {
                get
                {
                    return _GroupID;
                }
                set
                {
                    _GroupID = value;
                }
            }
            //OR-17832
            public string Domain_Description
            {
                get
                {
                    return _Domain_Description;
                }
                set
                {
                    _Domain_Description = value;
                }
            }
            //OR-18251
            public int PopupType
            {
                get
                {
                    return _PopupType;
                }
                set
                {
                    _PopupType = value;
                }
            }
        }
        public class ProblemMasterList : List<ProblemMaster>
        {
            public ProblemMasterList()
            {
            }
        }
    }
}
