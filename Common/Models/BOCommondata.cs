using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class BOCommondata
    {
        public BOCommondata()
        {
            //
            // TODO: Add constructor logic here
            //
        }

    }
    public class Commondata
    {
        public Commondata()
        {

        }
        public int ID { set; get; }
        public string Name { set; get; }
    }
    public class CommondataList : List<Commondata>
    { }
}
