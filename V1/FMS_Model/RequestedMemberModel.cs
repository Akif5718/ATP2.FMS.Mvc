using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Entities;

namespace FMS_Model
{
   public class RequestedMemberModel
    {
       public List<UserInfo> UserInfo =new List<UserInfo>();

       public int PostId { get; set; }

       public string ProjectName { get; set; }

       public string Description { get; set; }

    }
}
