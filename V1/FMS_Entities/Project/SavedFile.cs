using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FMS_Entities
{
   public class SavedFile
    {
       [Key]
       public int SavedFileId { get; set; }

       [Required]
       public int ProjectSectionId { get; set; }

       [ForeignKey("ProjectSectionId")]
       public ProjectSection ProjectSection;

       public int UserId { get; set; }

       [ForeignKey("UserId")]
       public UserInfo UserInfo;

       [Required]
       public string FileLink { get; set; }
    }
}
