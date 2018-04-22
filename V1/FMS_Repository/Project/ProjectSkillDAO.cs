using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Data;
using FMS_Entities;
using FMS_Framework.Object;
using ProjectSkills = FMS_Entities.ProjectSkills;

namespace FMS_Repository
{
  public  class ProjectSkillDAO
    {

        public Result<ProjectSkills> Save(ProjectSkills ProjectSkills)
        {
            var result = new Result<ProjectSkills>();
            try
            {
                string query = "select * from ProjectSkill where PostID=" + ProjectSkills.PostID;
                var dt = DataAccess.GetDataTable(query);

                if (dt == null || dt.Rows.Count == 0)
                {
                    ProjectSkills.PostID = GetID();
                    query = "insert into ProjectSkill values(" + ProjectSkills.PostID + "," + ProjectSkills.SkillID + ")";
                }
                else
                {
                    query = "update ProjectSkill set SkillID=" + ProjectSkills.SkillID + " where PostID=" +
                      ProjectSkills.PostID;
                }



                result.HasError = DataAccess.ExecuteQuery(query) <= 0;

                if (result.HasError)
                    result.Message = "Something Went Wrong";
                else
                {
                    result.Data = ProjectSkills;
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }

        private int GetID()
        {
            string query = "select * from PostAProject order by PostID desc";
            var dt = DataAccess.GetDataTable(query);
            int id = 1;

            if (dt != null && dt.Rows.Count != 0)
                id = Int32.Parse(dt.Rows[0]["PostID"].ToString());

            return id;
        }

        public List<ProjectSkills> GetAll(int id)
        {
            var result = new List<ProjectSkills>();
            try
            {
                string query = "select * from ProjectSkill where PostID=" + id;
                var dt = DataAccess.GetDataTable(query);

                if (dt != null && dt.Rows.Count != 0)
                {
                    for (int i = 0; i <dt.Rows.Count; i++)
                    {
                        ProjectSkills u = ConvertToEntity(dt.Rows[i]);
                        result.Add(u);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        //public Result<ProjectSkills> GetByID(int id)
        //{
        //    var result = new Result<ProjectSkills>();
        //    try
        //    {
        //        string query = "select * from ProjectSkill where PostID=" + id;
        //        var dt = DataAccess.GetDataTable(query);

        //        if (dt == null || dt.Rows.Count == 0)
        //        {
        //            result.HasError = true;
        //            result.Message = "Invalid ID";
        //            return result;
        //        }

        //        result.Data = ConvertToEntity(dt.Rows[0]);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.HasError = true;
        //        result.Message = ex.Message;
        //    }
        //    return result;
        //}

        public bool Delete(int id)
        {
            var result = new Result<ProjectSkills>();
            try
            {
                string query = "delete from ProjectSkill where PostID=" + id;
                return DataAccess.ExecuteQuery(query) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        private ProjectSkills ConvertToEntity(DataRow row)
        {
            try
            {
                ProjectSkills u = new ProjectSkills();
                u.PostID = Int32.Parse(row["PostId"].ToString());
                u.SkillID = Int32.Parse(row["SkillId"].ToString());
               



                return u;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
