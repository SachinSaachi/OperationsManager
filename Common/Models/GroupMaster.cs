using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class GroupMaster
    {
        public GroupMaster()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public int CompanyId { get; set; }
    }
}
