using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Entities;
using FMS_Framework.Helper;
using FMS_Framework.Object;
using RatingWorker = FMS_Entities.RatingWorker;
namespace FMS_Repository_EF
{
    class RatingWorkerRepo:BaseRepo
    {

        public Result<RatingWorker> Save(RatingWorker userinfo)
        {
            var result = new Result<RatingWorker>();
            try
            {
                var objtosave = DbContext.RatingWorkers.FirstOrDefault(u => u.UserId == userinfo.UserId);
                if (objtosave == null)
                {
                    objtosave = new RatingWorker();
                    DbContext.RatingWorkers.Add(objtosave);
                }
                objtosave.CommunicationSkill = userinfo.CommunicationSkill;
                objtosave.OnTime = userinfo.OnTime;
                objtosave.OnBudget = userinfo.OnBudget;
                objtosave.Behaviour = userinfo.Behaviour;
                objtosave.Completeness = userinfo.Completeness;


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

        private bool IsValid(RatingWorker obj, Result<RatingWorker> result)
        {
            if (!ValidationHelper.IsStringValid(obj.CommunicationSkill.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid CommunicationSkill";
                return false;
            }


            if (!ValidationHelper.IsStringValid(obj.OnTime.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid OnTime";
                return false;
            }
            if (!ValidationHelper.IsStringValid(obj.OnBudget.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid OnBudget";
                return false;
            }
            if (!ValidationHelper.IsStringValid(obj.Behaviour.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid Behaviour";
                return false;
            }
            if (!ValidationHelper.IsStringValid(obj.Completeness.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid Completeness";
                return false;
            }

            return true;
        }

        public Result<List<RatingWorker>> GetAll(string key = "")
        {
            var result = new Result<List<RatingWorker>>() { Data = new List<RatingWorker>() };

            try
            {
                IQueryable<RatingWorker> query = DbContext.RatingWorkers;

                if (ValidationHelper.IsIntValid(key))
                {
                    query = query.Where(q => q.UserId == Int32.Parse(key));
                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.CommunicationSkill == Int32.Parse(key));

                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.OnTime == Int32.Parse(key));

                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.OnBudget == Int32.Parse(key));

                }


                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.Behaviour == Int32.Parse(key));

                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.Completeness == Int32.Parse(key));

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

        public Result<RatingWorker> GetByID(int id)
        {
            var result = new Result<RatingWorker>();

            try
            {
                var obj = DbContext.RatingWorkers.FirstOrDefault(c => c.UserId == id);
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
                var objtodelete = DbContext.RatingWorkers.FirstOrDefault(c => c.UserId == id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid UserID";
                    return result;


                }

                DbContext.RatingWorkers.Remove(objtodelete);
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
