using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Entities;
using FMS_Framework.Helper;
using FMS_Framework.Object;
using OwnerInfo = FMS_Entities.OwnerInfo;
namespace FMS_Repository_EF
{
    class OwnerInfoRepo:BaseRepo
    {
        public Result<OwnerInfo> Save(OwnerInfo userinfo)
        {
            var result = new Result<OwnerInfo>();
            try
            {
                var objtosave = DbContext.OwnerInfos.FirstOrDefault(u => u.UserId == userinfo.UserId);
                if (objtosave == null)
                {
                    objtosave = new OwnerInfo();
                    DbContext.OwnerInfos.Add(objtosave);
                }
                objtosave.CompanyName = userinfo.CompanyName;
                objtosave.CompanyAddress = userinfo.CompanyAddress;
                objtosave.CompanyCode = userinfo.CompanyCode;

                objtosave.Position = userinfo.Position;


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

        private bool IsValid(OwnerInfo obj, Result<OwnerInfo> result)
        {
            if (!ValidationHelper.IsStringValid(obj.CompanyName))
            {
                result.HasError = true;
                result.Message = "Invalid CompanyName";
                return false;
            }
           

            if (!ValidationHelper.IsStringValid(obj.CompanyAddress))
            {
                result.HasError = true;
                result.Message = "Invalid CompanyAddress";
                return false;
            }
            if (!ValidationHelper.IsStringValid(obj.CompanyCode))
            {
                result.HasError = true;
                result.Message = "Invalid CompanyCode";
                return false;
            }
            if (!ValidationHelper.IsStringValid(obj.Position))
            {
                result.HasError = true;
                result.Message = "Invalid Position";
                return false;
            }

            return true;
        }

        public Result<List<OwnerInfo>> GetAll(string key = "")
        {
            var result = new Result<List<OwnerInfo>>() { Data = new List<OwnerInfo>() };

            try
            {
                IQueryable<OwnerInfo> query = DbContext.OwnerInfos;

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
                    query = query.Where(q => q.CompanyAddress.Contains(key));

                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.CompanyCode.Contains(key));

                }


                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.Position.Contains(key));

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

        public Result<OwnerInfo> GetByID(int id)
        {
            var result = new Result<OwnerInfo>();

            try
            {
                var obj = DbContext.OwnerInfos.FirstOrDefault(c => c.UserId == id);
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
                var objtodelete = DbContext.OwnerInfos.FirstOrDefault(c => c.UserId == id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid UserID";
                    return result;


                }

                DbContext.OwnerInfos.Remove(objtodelete);
                DbContext.SaveChanges();

            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;


            }
            return result;
        }

        /*   private bool  IsValidToSave(OwnerInfo obj, Result<OwnerInfo> result)
           {
               if(!ValidationHelper.IsIntValid(obj.UserId))
               {
                   result.HasError = true;
                   result.Message = "Invalid UserID";
                   return false;

               }
               if (DbContext.OwnerInfos.Any(u =>
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
