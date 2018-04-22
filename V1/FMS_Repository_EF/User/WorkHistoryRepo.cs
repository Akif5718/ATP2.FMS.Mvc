using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Entities;
using FMS_Framework.Helper;
using FMS_Framework.Object;
using WorkHistory = FMS_Entities.WorkHistory;

namespace FMS_Repository_EF
{
    class WorkHistoryRepo : BaseRepo
    {


        public Result<WorkHistory> Save(WorkHistory userinfo)
        {
            var result = new Result<WorkHistory>();
            try
            {

                DbContext.WorkHistories.FirstOrDefault(u => u.UserId == userinfo.UserId);
                var objtosave = DbContext.WorkHistories.FirstOrDefault(u => u.UserId == userinfo.UserId);
                if (objtosave == null)
                {
                    objtosave = new WorkHistory();
                    DbContext.WorkHistories.Add(objtosave);
                }
                objtosave.CompanyName = userinfo.CompanyName;
                objtosave.Position = userinfo.Position;
                objtosave.Experience = userinfo.Experience;



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

        private bool IsValid(WorkHistory obj, Result<WorkHistory> result)
        {
            if (!ValidationHelper.IsStringValid(obj.CompanyName))
            {
                result.HasError = true;
                result.Message = "Invalid CompanyName";
                return false;
            }
            if (!ValidationHelper.IsStringValid(obj.Position))
            {
                result.HasError = true;
                result.Message = "Invalid Position";
                return false;
            }
            if (!ValidationHelper.IsStringValid(obj.Experience))
            {
                result.HasError = true;
                result.Message = "Invalid Experience";
                return false;
            }


            return true;
        }

        public Result<List<WorkHistory>> GetAll(string key = "")
        {
            var result = new Result<List<WorkHistory>>() { Data = new List<WorkHistory>() };

            try
            {
                IQueryable<WorkHistory> query = DbContext.WorkHistories;

                if (ValidationHelper.IsIntValid(key))
                {
                    query = query.Where(q => q.UserId == Int32.Parse(key));
                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.CompanyName.Contains(key));

                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.Position.Contains(key));

                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.Experience.Contains(key));

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

        public Result<WorkHistory> GetByID(int id)
        {
            var result = new Result<WorkHistory>();

            try
            {
                var obj = DbContext.WorkHistories.FirstOrDefault(c => c.UserId == id);
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
                var objtodelete = DbContext.WorkHistories.FirstOrDefault(c => c.UserId == id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid UserID";
                    return result;


                }

                DbContext.WorkHistories.Remove(objtodelete);
                DbContext.SaveChanges();

            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;


            }
            return result;
        }

        /*   private bool  IsValidToSave(WorkHistory obj, Result<WorkHistory> result)
           {
               if(!ValidationHelper.IsIntValid(obj.UserId))
               {
                   result.HasError = true;
                   result.Message = "Invalid UserID";
                   return false;

               }
               if (DbContext.WorkHistorys.Any(u =>
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
