using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Entities;
using FMS_Framework.Helper;
using FMS_Framework.Object;
using SkillCategory = FMS_Entities.SkillCategory;
namespace FMS_Repository_EF
{
    class SkillCategoryRepo:BaseRepo
    {


        public Result<SkillCategory> Save(SkillCategory userinfo)
        {
            var result = new Result<SkillCategory>();
            try
            {

                DbContext.SkillCategories.FirstOrDefault(u => u.CategoryId == userinfo.CategoryId);
                var objtosave = DbContext.SkillCategories.FirstOrDefault(u => u.CategoryId == userinfo.CategoryId);
                if (objtosave == null)
                {
                    objtosave = new SkillCategory();
                    DbContext.SkillCategories.Add(objtosave);
                }
             
                objtosave.CategoryId = userinfo.CategoryId;
                objtosave.CategoryName = userinfo.CategoryName;




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

        private bool IsValid(SkillCategory obj, Result<SkillCategory> result)
        {
            if (!ValidationHelper.IsStringValid(obj.CategoryId.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid CategoryId";
                return false;
            }

            if (!ValidationHelper.IsStringValid(obj.CategoryName))
            {
                result.HasError = true;
                result.Message = "Invalid CategoryName";
                return false;
            }
          

            return true;
        }

        public Result<List<SkillCategory>> GetAll(string key = "")
        {
            var result = new Result<List<SkillCategory>>() { Data = new List<SkillCategory>() };

            try
            {
                IQueryable<SkillCategory> query = DbContext.SkillCategories;

                if (ValidationHelper.IsIntValid(key))
                {
                    query = query.Where(q => q.CategoryId == Int32.Parse(key));
                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.CategoryName.Contains(key));

                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.CategoryId == Int32.Parse(key));

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

        public Result<SkillCategory> GetByID(int id)
        {
            var result = new Result<SkillCategory>();

            try
            {
                var obj = DbContext.SkillCategories.FirstOrDefault(c => c.CategoryId == id);
                if (obj == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid UserID";
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
                var objtodelete = DbContext.SkillCategories.FirstOrDefault(c => c.CategoryId == id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid UserID";
                    return result;


                }

                DbContext.SkillCategories.Remove(objtodelete);
                DbContext.SaveChanges();

            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;


            }
            return result;
        }

        /*   private bool  IsValidToSave(SkillCategory obj, Result<SkillCategory> result)
           {
               if(!ValidationHelper.IsIntValid(obj.UserId))
               {
                   result.HasError = true;
                   result.Message = "Invalid UserID";
                   return false;

               }
               if (DbContextSkillCategories.Any(u =>
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
