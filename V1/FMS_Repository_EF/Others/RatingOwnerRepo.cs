using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Entities;
using FMS_Framework.Helper;
using FMS_Framework.Object;
using RatingOwner = FMS_Entities.RatingOwner;
namespace FMS_Repository_EF
{
    class RatingOwnerRepo:BaseRepo
    {
        public Result<RatingOwner> Save(RatingOwner userinfo)
        {
            var result = new Result<RatingOwner>();
            try
            {
                var objtosave = DbContext.RatingOwners.FirstOrDefault(u => u.UserId == userinfo.UserId);
                if (objtosave == null)
                {
                    objtosave = new RatingOwner();
                    DbContext.RatingOwners.Add(objtosave);
                }
                objtosave.CommunicationSkill = userinfo.CommunicationSkill;
                objtosave.Reliability = userinfo.Reliability;
                objtosave.OnWord = userinfo.OnWord;
                objtosave.Behaviour = userinfo.Behaviour;


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

        private bool IsValid(RatingOwner obj, Result<RatingOwner> result)
        {
            if (!ValidationHelper.IsStringValid(obj.CommunicationSkill.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid CommunicationSkill";
                return false;
            }


            if (!ValidationHelper.IsStringValid(obj.Reliability.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid Reliability";
                return false;
            }
            if (!ValidationHelper.IsStringValid(obj.OnWord.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid OnWord";
                return false;
            }
            if (!ValidationHelper.IsStringValid(obj.Behaviour.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid Behaviour";
                return false;
            }

            return true;
        }

        public Result<List<RatingOwner>> GetAll(string key = "")
        {
            var result = new Result<List<RatingOwner>>() { Data = new List<RatingOwner>() };

            try
            {
                IQueryable<RatingOwner> query = DbContext.RatingOwners;

                if (ValidationHelper.IsIntValid(key))
                {
                    query = query.Where(q => q.UserId == Int32.Parse(key));
                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.CommunicationSkill. Equals(Int32.Parse(key)));

                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.Reliability.Equals(Int32.Parse(key)));

                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.OnWord.Equals(Int32.Parse(key)));

                }


                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.Behaviour .Equals(Int32.Parse(key)));

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

        public Result<RatingOwner> GetByID(int id)
        {
            var result = new Result<RatingOwner>();

            try
            {
                var obj = DbContext.RatingOwners.FirstOrDefault(c => c.UserId == id);
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
                var objtodelete = DbContext.RatingOwners.FirstOrDefault(c => c.UserId == id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid UserID";
                    return result;


                }

                DbContext.RatingOwners.Remove(objtodelete);
                DbContext.SaveChanges();

            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;


            }
            return result;
        }

        /*   private bool  IsValidToSave(RatingOwner obj, Result<RatingOwner> result)
           {
               if(!ValidationHelper.IsIntValid(obj.UserId))
               {
                   result.HasError = true;
                   result.Message = "Invalid UserID";
                   return false;

               }
               if (DbContext.RatingOwners.Any(u =>
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
