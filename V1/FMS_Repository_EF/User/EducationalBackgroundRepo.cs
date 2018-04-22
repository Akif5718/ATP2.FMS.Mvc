using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Entities;
using FMS_Framework.Helper;
using FMS_Framework.Object;
using EducationalBackground = FMS_Entities.EducationalBackground;

namespace FMS_Repository_EF
{
    class EducationalBackgroundRepo:BaseRepo
    {
        public Result<EducationalBackground> Save(EducationalBackground userinfo)
        {
            var result = new Result<EducationalBackground>();
            try
            {
                var objtosave = DbContext.EducationalBackgrounds.FirstOrDefault(u => u.UserId == userinfo.UserId);
                if (objtosave == null)
                {
                    objtosave = new EducationalBackground();
                    DbContext.EducationalBackgrounds.Add(objtosave);
                }
                objtosave.School = userinfo.School;
                objtosave.Collage = userinfo.Collage;
                objtosave.UniversityPost = userinfo.UniversityPost;
                objtosave.UniversityUnder = userinfo.UniversityUnder;
                objtosave.Others = userinfo.Others;
            

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

        private bool IsValid(EducationalBackground obj, Result<EducationalBackground> result)
        {
            if (!ValidationHelper.IsStringValid(obj.School))
            {
                result.HasError = true;
                result.Message = "Invalid School";
                return false;
            }
            if (!ValidationHelper.IsStringValid(obj.Collage))
            {
                result.HasError = true;
                result.Message = "Invalid Collage";
                return false;
            }

            if (!ValidationHelper.IsStringValid(obj.UniversityPost))
            {
                result.HasError = true;
                result.Message = "Invalid UniversityPost";
                return false;
            }
            if (!ValidationHelper.IsStringValid(obj.UniversityUnder))
            {
                result.HasError = true;
                result.Message = "Invalid UniversityUnder";
                return false;
            }
         
          
            return true;
        }

        public Result<List<EducationalBackground>> GetAll(string key="")
        {
            var result = new Result<List<EducationalBackground>>(){Data = new List<EducationalBackground>()};

            try
            {
                IQueryable<EducationalBackground> query = DbContext.EducationalBackgrounds;

                if (ValidationHelper.IsIntValid(key))
                {
                    query = query.Where(q => q.UserId == Int32.Parse(key));
                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.School.Contains(key));

                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.Collage.Contains(key));

                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.UniversityPost.Contains(key));

                }


                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.Others.Contains(key));

                }
                result.Data = query.ToList();
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
               
               
            }
            return result ;
        }

        public Result<EducationalBackground> GetByID(int id )
        {
            var result = new Result<EducationalBackground>();

            try
            {
                var obj = DbContext.EducationalBackgrounds.FirstOrDefault(c => c.UserId == id);
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
                var objtodelete = DbContext.EducationalBackgrounds.FirstOrDefault(c => c.UserId == id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid UserID";
                    return result;


                }

                DbContext.EducationalBackgrounds.Remove(objtodelete);
                DbContext.SaveChanges();

            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;


            }
            return result;
        }

     /*   private bool  IsValidToSave(EducationalBackground obj, Result<EducationalBackground> result)
        {
            if(!ValidationHelper.IsIntValid(obj.UserId))
            {
                result.HasError = true;
                result.Message = "Invalid UserID";
                return false;

            }
            if (DbContext.EducationalBackgrounds.Any(u =>
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
