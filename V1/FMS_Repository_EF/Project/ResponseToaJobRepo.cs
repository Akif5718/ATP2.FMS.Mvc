using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Entities;
using FMS_Framework.Helper;
using FMS_Framework.Object;
using ResponseToaJob = FMS_Entities.ResponseToaJob;
namespace FMS_Repository_EF.Project
{
    class ResponseToaJobRepo:BaseRepo
    {

        public Result<ResponseToaJob> Save(ResponseToaJob userinfo)
        {
            var result = new Result<ResponseToaJob>();
            try
            {
                var objtosave = DbContext.Response.FirstOrDefault(u => u.PostId == userinfo.PostId);
                if (objtosave == null)
                {
                    objtosave = new ResponseToaJob();
                    DbContext.Response.Add(objtosave);
                }
                objtosave.WUserId = userinfo.WUserId;
                objtosave.FixedPrice = userinfo.FixedPrice;
                objtosave.SubmissionTime = userinfo.SubmissionTime;


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

        private bool IsValid(ResponseToaJob obj, Result<ResponseToaJob> result)
        {
            if (!ValidationHelper.IsStringValid(obj.WUserId.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid WUserId";
                return false;
            }


            if (!ValidationHelper.IsStringValid(obj.FixedPrice.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid FixedPrice";
                return false;
            }

            if (ValidationHelper.IsStringValid(obj.SubmissionTime))
            {
                result.HasError = true;
                result.Message = "Invalid SubmissionTime";
                return false;


            }

            return true;

        }

        public Result<List<ResponseToaJob>> GetAll(string key = "")
        {
            var result = new Result<List<ResponseToaJob>>() { Data = new List<ResponseToaJob>() };

            try
            {
                IQueryable<ResponseToaJob> query = DbContext.Response;

                if (ValidationHelper.IsIntValid(key))
                {
                    query = query.Where(q => q.PostId == Int32.Parse(key));
                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.WUserId.Equals(Int32.Parse(key)));

                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.FixedPrice.Equals(Int32.Parse(key)));

                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.SubmissionTime.Contains(key));

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

        public Result<ResponseToaJob> GetByID(int id)
        {
            var result = new Result<ResponseToaJob>();

            try
            {
                var obj = DbContext.Response.FirstOrDefault(c => c.PostId == id);
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
                var objtodelete = DbContext.Response.FirstOrDefault(c => c.PostId == id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid UserID";
                    return result;


                }

                DbContext.Response.Remove(objtodelete);
                DbContext.SaveChanges();

            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;


            }
            return result;
        }

        /*   private bool  IsValidToSave(RatingWorker obj, Result<RatingWorker> result)
           {
               if(!ValidationHelper.IsIntValid(obj.UserId))
               {
                   result.HasError = true;
                   result.Message = "Invalid UserID";
                   return false;

               }
               if (DbContext.RatingWorkers.Any(u =>
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
