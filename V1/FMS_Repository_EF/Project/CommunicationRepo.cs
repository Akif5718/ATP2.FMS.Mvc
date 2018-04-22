using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Entities;
using FMS_Framework.Helper;
using FMS_Framework.Object;
using COMMENTSEC = FMS_Entities.COMMENTSEC;

namespace FMS_Repository_EF
{
    //class CommunicationRepo:BaseRepo
    //{
    //    public Result<COMMENTSEC> Save(COMMENTSEC userinfo)
    //    {
    //        var result = new Result<COMMENTSEC>();
    //        try
    //        {
    //            var objtosave = DbContext.COMMENTSECs.FirstOrDefault(u => u.UserId == userinfo.UserId);
    //            if (objtosave == null)
    //            {
    //                objtosave = new COMMENTSEC();
    //                DbContext.COMMENTSECs.Add(objtosave);
    //            }
    //            objtosave.CommunicationSkill = userinfo.CommunicationSkill;
    //            objtosave.OnTime = userinfo.OnTime;
    //            objtosave.OnBudget = userinfo.OnBudget;
    //            objtosave.Behaviour = userinfo.Behaviour;
    //            objtosave.Completeness = userinfo.Completeness;


    //            if (!IsValid(objtosave, result))
    //            {
    //                return result;
    //            }
    //            DbContext.SaveChanges();
    //        }
    //        catch (Exception ex)
    //        {
    //            result.HasError = true;
    //            result.Message = ex.Message;
    //        }
    //        return result;
    //    }

    //    private bool IsValid(COMMENTSEC obj, Result<COMMENTSEC> result)
    //    {
    //        if (!ValidationHelper.IsStringValid(obj.CommunicationSkill.ToString()))
    //        {
    //            result.HasError = true;
    //            result.Message = "Invalid CommunicationSkill";
    //            return false;
    //        }


    //        if (!ValidationHelper.IsStringValid(obj.OnTime.ToString()))
    //        {
    //            result.HasError = true;
    //            result.Message = "Invalid OnTime";
    //            return false;
    //        }
    //        if (!ValidationHelper.IsStringValid(obj.OnBudget.ToString()))
    //        {
    //            result.HasError = true;
    //            result.Message = "Invalid OnBudget";
    //            return false;
    //        }
    //        if (!ValidationHelper.IsStringValid(obj.Behaviour.ToString()))
    //        {
    //            result.HasError = true;
    //            result.Message = "Invalid Behaviour";
    //            return false;
    //        }
    //        if (!ValidationHelper.IsStringValid(obj.Completeness.ToString()))
    //        {
    //            result.HasError = true;
    //            result.Message = "Invalid Completeness";
    //            return false;
    //        }

    //        return true;
    //    }

    //    public Result<List<COMMENTSEC>> GetAll(string key = "")
    //    {
    //        var result = new Result<List<COMMENTSEC>>() { Data = new List<COMMENTSEC>() };

    //        try
    //        {
    //            IQueryable<COMMENTSEC> query = DbContext.COMMENTSECs;

    //            if (ValidationHelper.IsIntValid(key))
    //            {
    //                query = query.Where(q => q.UserId == Int32.Parse(key));
    //            }

    //            if (ValidationHelper.IsStringValid(key))
    //            {
    //                query = query.Where(q => q.CommunicationSkill == Int32.Parse(key));

    //            }

    //            if (ValidationHelper.IsStringValid(key))
    //            {
    //                query = query.Where(q => q.OnTime == Int32.Parse(key));

    //            }

    //            if (ValidationHelper.IsStringValid(key))
    //            {
    //                query = query.Where(q => q.OnBudget == Int32.Parse(key));

    //            }


    //            if (ValidationHelper.IsStringValid(key))
    //            {
    //                query = query.Where(q => q.Behaviour == Int32.Parse(key));

    //            }

    //            if (ValidationHelper.IsStringValid(key))
    //            {
    //                query = query.Where(q => q.Completeness == Int32.Parse(key));

    //            }
    //            result.Data = query.ToList();
    //        }
    //        catch (Exception e)
    //        {
    //            result.HasError = true;
    //            result.Message = e.Message;


    //        }
    //        return result;
    //    }

    //    public Result<COMMENTSEC> GetByID(int id)
    //    {
    //        var result = new Result<COMMENTSEC>();

    //        try
    //        {
    //            var obj = DbContext.COMMENTSECs.FirstOrDefault(c => c.UserId == id);
    //            if (obj == null)
    //            {
    //                result.HasError = true;
    //                result.Message = "Invalid UserID";
    //                return result;


    //            }
    //            result.Data = obj;
    //        }
    //        catch (Exception e)
    //        {
    //            result.HasError = true;
    //            result.Message = e.Message;


    //        }
    //        return result;
    //    }

    //    public Result<bool> Delete(int id)
    //    {
    //        var result = new Result<bool>();

    //        try
    //        {
    //            var objtodelete = DbContext.COMMENTSECs.FirstOrDefault(c => c.UserId == id);
    //            if (objtodelete == null)
    //            {
    //                result.HasError = true;
    //                result.Message = "Invalid UserID";
    //                return result;


    //            }

    //            DbContext.COMMENTSECs.Remove(objtodelete);
    //            DbContext.SaveChanges();

    //        }
    //        catch (Exception e)
    //        {
    //            result.HasError = true;
    //            result.Message = e.Message;


    //        }
    //        return result;
    //    }

    //    /*   private bool  IsValidToSave(COMMENTSEC obj, Result<COMMENTSEC> result)
    //       {
    //           if(!ValidationHelper.IsIntValid(obj.UserId))
    //           {
    //               result.HasError = true;
    //               result.Message = "Invalid UserID";
    //               return false;

    //           }
    //           if (DbContext.COMMENTSECs.Any(u =>
    //               u.UserId == obj.UserId && u.School != obj.School && u.Collage != obj.Collage &&
    //               u.UniversityPost != obj.UniversityPost && u.UniversityUnder != obj.UniversityUnder &&
    //               u.Others != obj.Others))
    //           {
                
            

    //               result.HasError= true;
    //               result.Message = "UserID Exists";
    //               return false;



    //           }
    //           return true;

    //       }*/

    //}
}
