using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Data;
using FMS_Entities;
using FMS_Framework.Helper;
using FMS_Framework.Object;
using EducationalBackground = FMS_Entities.EducationalBackground;

namespace FMS_Repository
{
   public class EducationalDAO
    {
       public Result<EducationalBackground> Save(EducationalBackground EducationalBackground)
        {
            var result = new Result<EducationalBackground>();
            try
            {
                string query = "select * from EducationalBackground where UserId=" + EducationalBackground.UserId;
                var dt = DataAccess.GetDataTable(query);

                if (dt == null || dt.Rows.Count == 0)
                {
                   // EducationalBackground.UserId = GetID();
                    query = "insert into EducationalBackground values(" + EducationalBackground.UserId + ",'" + EducationalBackground.School + "','" + EducationalBackground.Collage + "','" + EducationalBackground.UniversityPost + "','" + EducationalBackground.UniversityUnder + "','" + EducationalBackground.Others + "')";
                }
                else
                {
                    query = "update EducationalBackground set School='" + EducationalBackground.School + "',Collage='" + EducationalBackground.Collage + "',UniversityPost='" + EducationalBackground.UniversityPost + "',UniversityUnder='" + EducationalBackground.UniversityUnder + "',Others='" + EducationalBackground.Others + "' where UserId=" + EducationalBackground.UserId;
                }

                //if (!IsValid(EducationalBackground, result))
                //{
                //    return result;
                //}

                result.HasError = DataAccess.ExecuteQuery(query) <= 0;

                if (result.HasError)
                    result.Message = "Something Went Wrong";
                else
                {
                    result.Data = EducationalBackground;
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }

        //private int GetID()
        //{
        //    string query = "select * from EducationalBackground order by UserId desc";
        //    var dt = DataAccess.GetDataTable(query);
        //    int id = 1;

        //    if (dt != null && dt.Rows.Count != 0)
        //        id = Int32.Parse(dt.Rows[0]["ID"].ToString()) + 1;

        //    return id;
        //}
        public List<EducationalBackground> GetAll()
        {
            var result = new List<EducationalBackground>();
            try
            {
                string query = "select * from EducationalBackground";
                var dt = DataAccess.GetDataTable(query);

                if (dt != null && dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= dt.Rows.Count; i++)
                    {
                        EducationalBackground u = ConvertToEntity(dt.Rows[i]);
                        result.Add(u);
                    }
                }
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }

        public Result<EducationalBackground> GetByID(int id)
        {
            var result = new Result<EducationalBackground>();
            try
            {
                string query = "select * from EducationalBackground where UserId=" + id;
                var dt = DataAccess.GetDataTable(query);

                if (dt == null || dt.Rows.Count == 0)
                {
                    result.HasError = true;
                    result.Message = "Invalid ID";
                    return result;
                }

                result.Data = ConvertToEntity(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }

        public bool Delete(int id)
        {
            var result = new Result<EducationalBackground>();
            try
            {
                string query = "delete from EducationalBackground where UserId=" + id;
                return DataAccess.ExecuteQuery(query) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //private bool IsValid(EducationalBackground obj, Result<EducationalBackground> result)
        //{
        //    if (!ValidationHelper.IsStringValid(obj.School))
        //    {
        //        result.HasError = true;
        //        result.Message = "Invalid School Name";
        //        return false;
        //    }
        //    if (!ValidationHelper.IsStringValid(obj.Collage))
        //    {
        //        result.HasError = true;
        //        result.Message = "Invalid Collage Name";
        //        return false;

        //    }
        //    if (!ValidationHelper.IsStringValid(obj.UniversityPost))
        //    {
        //        result.HasError = true;
        //        result.Message = "Invalid University Name";
        //        return false;
        //    }
        //    if (!ValidationHelper.IsStringValid(obj.UniversityUnder))
        //    {
        //        result.HasError = true;
        //        result.Message = "Invalid University Name";
        //        return false;
        //    }


        //    return true;
        //}

        private EducationalBackground ConvertToEntity(DataRow row)
        {
            try
            {
                EducationalBackground u = new EducationalBackground();
                u.UserId = Int32.Parse(row["UserId"].ToString());
                u.School = row["School"].ToString();
                u.Collage = row["Collage"].ToString();
                u.UniversityPost = row["UniversityPost"].ToString();
                u.UniversityUnder = row["UniversityUnder"].ToString();
                u.Others = row["Others"].ToString();


                return u;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
