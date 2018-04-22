using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Entities;
using FMS_Framework.Helper;
using FMS_Framework.Object;
using ProjectSection = FMS_Entities.ProjectSection;
namespace FMS_Repository_EF
{
    class ProjectSectionRepo:BaseRepo
    {
        public Result<ProjectSection> Save(ProjectSection userinfo)
        {
            var result = new Result<ProjectSection>();
            try
            {
                var objtosave = DbContext.ProjectSections.FirstOrDefault(u => u.ProjectSectionId == userinfo.ProjectSectionId);
                if (objtosave == null)
                {
                    objtosave = new ProjectSection();
                    DbContext.ProjectSections.Add(objtosave);
                }
                objtosave.PostId = userinfo.PostId;
                objtosave.SectionName = userinfo.SectionName;
                objtosave.Percentage = userinfo.Percentage;
                objtosave.Price = userinfo.Price;


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

        private bool IsValid(ProjectSection obj, Result<ProjectSection> result)
        {
            if (!ValidationHelper.IsStringValid(obj.PostId.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid PostId";
                return false;
            }


            if (!ValidationHelper.IsStringValid(obj.SectionName))
            {
                result.HasError = true;
                result.Message = "Invalid SectionName";
                return false;
            }
            if (!ValidationHelper.IsStringValid(obj.Percentage.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid Percentage";
                return false;
            }
            if (!ValidationHelper.IsStringValid(obj.Price.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid Price";
                return false;
            }

            return true;
        }

        public Result<List<ProjectSection>> GetAll(string key = "")
        {
            var result = new Result<List<ProjectSection>>() { Data = new List<ProjectSection>() };

            try
            {
                IQueryable<ProjectSection> query = DbContext.ProjectSections;

                if (ValidationHelper.IsIntValid(key))
                {
                    query = query.Where(q => q.ProjectSectionId == Int32.Parse(key));
                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.PostId.Equals(Int32.Parse(key)));

                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.SectionName.Contains(key));

                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.Percentage.Equals(Int32.Parse(key)));

                }


                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.Price.Equals(Int32.Parse(key)));

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

        public Result<ProjectSection> GetByID(int id)
        {
            var result = new Result<ProjectSection>();

            try
            {
                var obj = DbContext.ProjectSections.FirstOrDefault(c => c.ProjectSectionId == id);
                if (obj == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid ProjectSectionId";
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
                var objtodelete = DbContext.ProjectSections.FirstOrDefault(c => c.ProjectSectionId == id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid ProjectSectionId";
                    return result;


                }

                DbContext.ProjectSections.Remove(objtodelete);
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
