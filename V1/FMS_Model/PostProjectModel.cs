using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Entities;

namespace FMS_Model
{
    public class PostProjectModel
    {
        
        [Required]
        public string ProjectName { get; set; }

        [Required]
        public string UFirstName { get; set; }

        [Required]
        public string ULastName { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Members { get; set; }

        [Required]
        public List<int> SkillId { get; set; }

        public int WUserId { get; set; }

        public int PostId { get; set; }

        [Required]
        public List<string> SectionName { get; set; }
       
        [Required]
        public List<string> SkillName =new List<string>();


    }
}
