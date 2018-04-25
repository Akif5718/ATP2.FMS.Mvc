using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Entities;

namespace FMS_Model
{
   public class WorkProgressModel
   {
       public RequestedMemberModel RequestedMemberModel;

       public List<ProjectSection> ProjectSection= new List<ProjectSection>();

       public List<SelectedWorker> SelectedWorkers=new List<SelectedWorker>(); 

       public List<string> Name=new List<string>();


       public List<Comment>Comments=new List<Comment>(); 

       

   }

    public class Comment
    {

        public List<COMMENTSEC> Commentsec = new List<COMMENTSEC>();

    }
}
