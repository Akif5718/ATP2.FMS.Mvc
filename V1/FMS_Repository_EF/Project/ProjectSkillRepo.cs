using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Entities;
using FMS_Framework.Helper;
using FMS_Framework.Object;
using ProjectSkills = FMS_Entities.ProjectSkills;
namespace FMS_Repository_EF
{
    class ProjectSkillRepo:BaseRepo
    {
        public Result<ProjectSkills> Save(ProjectSkills userinfo)
        {
            var result = new Result<ProjectSkills>();
            try
            {
                var objtosave = DbContext.ProjectSkills.FirstOrDefault(u => u.PostID == userinfo.PostID);
                if (objtosave == null)
                {
                    objtosave = new ProjectSkills();
                    DbContext.ProjectSkills.Add(objtosave);
                }
                objtosave.SkillID = userinfo.SkillID;
                  

                if (!IsValid(objtosave, result))
                {
                    return result;
                }
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }

        private bool IsValid(ProjectSkills obj, Result<ProjectSkills> result)
        {
            if (!ValidationHelper.IsStringValid(obj.SkillID.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid SkillID";
                return false;
            }


           
            return true;
        }

        public Result<List<ProjectSkills>> GetAll(string key = "")
        {
            var result = new Result<List<ProjectSkills>>() { Data = new List<ProjectSkills>() };

            try
            {
                IQueryable<ProjectSkills> query = DbContext.ProjectSkills;

                if (ValidationHelper.IsIntValid(key))
                {
                    query = query.Where(q => q.PostID == Int32.Parse(key));
                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.SkillID.Equals(Int32.Parse(key)));

                }

               


                result.Data = query.ToList();
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;


            }
            return result;
        }

        public Result<ProjectSkills> GetByID(int id)
        {
            var result = new Result<ProjectSkills>();

            try
            {
                var obj = DbContext.ProjectSkills.FirstOrDefault(c => c.PostID == id);
                if (obj == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid PostID";
                    return result;


                }
                result.Data = obj;
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;


            }
            return result;
        }

        public Result<bool> Delete(int id)
        {
            var result = new Result<bool>();

            try
            {
                var objtodelete = DbContext.ProjectSkills.FirstOrDefault(c => c.PostID == id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid PostID";
                    return result;


                }

                DbContext.ProjectSkills.Remove(objtodelete);
                DbContext.SaveChanges();

            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;


            }
            return result;
        }

        /*   private bool  IsValidToSave(ProjectSection obj, Result<ProjectSection> result)
           {
               if(!ValidationHelper.IsIntValid(obj.UserId))
               {
                   result.HasError = true;
                   result.Message = "Invalid UserID";
                   return false;

               }
               if (DbContext.ProjectSections.Any(u =>
                   u.UserId == obj.UserId && u.School != obj.School && u.Collage != obj.Collage &&
                   u.UniversityPost != obj.UniversityPost && u.UniversityUnder != obj.UniversityUnder &&
                   u.Others != obj.Others))
               {
                
            

                   result.HasError= true;
                   result.Message = "UserID Exists";
                   return false;



               }
               return true;

           }*/

    }
}
