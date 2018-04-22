using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Entities;
using FMS_Framework.Helper;
using FMS_Framework.Object;
using SelectedWorker = FMS_Entities.SelectedWorker;
namespace FMS_Repository_EF
{
    class SelectedWorkerRepo:BaseRepo
    {
        public Result<SelectedWorker> Save(SelectedWorker userinfo)
        {
            var result = new Result<SelectedWorker>();
            try
            {
                var objtosave = DbContext.SelectedWorkers.FirstOrDefault(u => u.PostId == userinfo.PostId);
                if (objtosave == null)
                {
                    objtosave = new SelectedWorker();
                    DbContext.SelectedWorkers.Add(objtosave);
                }
                objtosave.UserId = userinfo.UserId;
                objtosave.Price = userinfo.Price;
                objtosave.SubmissionDate = userinfo.SubmissionDate;
                


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

        private bool IsValid(SelectedWorker obj, Result<SelectedWorker> result)
        {
            if (!ValidationHelper.IsStringValid(obj.UserId.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid UserId";
                return false;
            }


            if (!ValidationHelper.IsStringValid(obj.Price.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid Price";
                return false;
            }
            if (!ValidationHelper.IsStringValid(obj.SubmissionDate.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid SubmissionDate";
                return false;
            }
            
            return true;
        }

        public Result<List<SelectedWorker>> GetAll(string key = "")
        {
            var result = new Result<List<SelectedWorker>>() { Data = new List<SelectedWorker>() };

            try
            {
                IQueryable<SelectedWorker> query = DbContext.SelectedWorkers;

                if (ValidationHelper.IsIntValid(key))
                {
                    query = query.Where(q => q.PostId == Int32.Parse(key));
                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.UserId.Equals(Int32.Parse(key)));

                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.Price.Equals(Int32.Parse(key)));

                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.SubmissionDate.ToString().Contains(key));

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

        public Result<SelectedWorker> GetByID(int id)
        {
            var result = new Result<SelectedWorker>();

            try
            {
                var obj = DbContext.SelectedWorkers.FirstOrDefault(c => c.PostId == id);
                if (obj == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid PostId";
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
                var objtodelete = DbContext.SelectedWorkers.FirstOrDefault(c => c.PostId == id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid PostId";
                    return result;


                }

                DbContext.SelectedWorkers.Remove(objtodelete);
                DbContext.SaveChanges();

            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;


            }
            return result;
        }

        /*   private bool  IsValidToSave(SelectedWorker obj, Result<SelectedWorker> result)
           {
               if(!ValidationHelper.IsIntValid(obj.UserId))
               {
                   result.HasError = true;
                   result.Message = "Invalid UserID";
                   return false;

               }
               if (DbContext.SelectedWorkers.Any(u =>
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
