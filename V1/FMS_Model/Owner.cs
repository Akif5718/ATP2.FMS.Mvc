using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS_Model
{
    public class Owner
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

      
        public string UserType { get; set; }

        public double Balance { get; set; }
        
        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }
    }
}
