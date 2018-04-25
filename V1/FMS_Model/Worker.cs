using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Entities;

namespace FMS_Model
{
    public class Worker
    {
    
        public int UserId { get; set; }


        public string FristName { get; set; }

  
        public string LastName { get; set; }

      
        public string Email { get; set; }

       
        public DateTime DateofBrith { get; set; }

       
        public DateTime JoinDate { get; set; }

        public string ProPic { get; set; }

        public string City { get; set; }
      
        public string State { get; set; }
       
        public string Country { get; set; }

        public string ConfirmPassword { get; set; }

      
        public string UserType { get; set; }

        public double Balance { get; set; }


        public List<SelectedWorker> SelectedWorkers=new List<SelectedWorker>();

        public List<PostAProject> PostAProjects=new List<PostAProject>();

        public UserInfo UserInfo;


        public string RatePerHour { get; set; }

       
        
    }
}
