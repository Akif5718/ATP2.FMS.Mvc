using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Entities;
using FMS_Framework.Helper;
using FMS_Framework.Object;
using Skill = FMS_Entities.Skill;
namespace FMS_Repository_EF
{
    
    
        class SkillRepo : BaseRepo
        {


            public Result<Skill> Save(Skill userinfo)
            {
                var result = new Result<Skill>();
                try
                {

                    DbContext.Skills.FirstOrDefault(u => u.SkillId== userinfo.SkillId);
                    var objtosave = DbContext.Skills.FirstOrDefault(u => u.SkillId == userinfo.SkillId);
                    if (objtosave == null)
                    {
                        objtosave = new Skill();
                        DbContext.Skills.Add(objtosave);
                    }
                    objtosave.SkillName = userinfo.SkillName;
                    objtosave.CategoryId = userinfo.CategoryId;
               



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

            private bool IsValid(Skill obj, Result<Skill> result)
            {
                if (!ValidationHelper.IsStringValid(obj.SkillName))
                {
                    result.HasError = true;
                    result.Message = "Invalid SkillName";
                    return false;
                }
                if (!ValidationHelper.IsStringValid(obj.CategoryId.ToString()))
                {
                    result.HasError = true;
                    result.Message = "Invalid CategoryId";
                    return false;
                }
                

                return true;
            }

            public Result<List<Skill>> GetAll(string key = "")
            {
                var result = new Result<List<Skill>>() { Data = new List<Skill>() };

                try
                {
                    IQueryable<Skill> query = DbContext.Skills;

                    if (ValidationHelper.IsIntValid(key))
                    {
                        query = query.Where(q => q.SkillId == Int32.Parse(key));
                    }

                    if (ValidationHelper.IsStringValid(key))
                    {
                        query = query.Where(q => q.SkillName.Contains(key));

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

            public Result<Skill> GetByID(int id)
            {
                var result = new Result<Skill>();

                try
                {
                    var obj = DbContext.Skills.FirstOrDefault(c => c.SkillId == id);
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
                    var objtodelete = DbContext.Skills.FirstOrDefault(c => c.SkillId == id);
                    if (objtodelete == null)
                    {
                        result.HasError = true;
                        result.Message = "Invalid UserID";
                        return result;


                    }

                    DbContext.Skills.Remove(objtodelete);
                    DbContext.SaveChanges();

                }
                catch (Exception e)
                {
                    result.HasError = true;
                    result.Message = e.Message;


                }
                return result;
            }

            /*   private bool  IsValidToSave(Skill obj, Result<Skill> result)
               {
                   if(!ValidationHelper.IsIntValid(obj.UserId))
                   {
                       result.HasError = true;
                       result.Message = "Invalid UserID";
                       return false;

                   }
                   if (DbContext.Skills.Any(u =>
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
