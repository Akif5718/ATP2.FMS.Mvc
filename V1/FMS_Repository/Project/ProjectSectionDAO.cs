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
using ProjectSection = FMS_Entities.ProjectSection;

namespace FMS_Repository
{
   public class ProjectSectionDAO
    {
        public Result<ProjectSection> Save(ProjectSection ProjectSection)
        {
            var result = new Result<ProjectSection>();
            try
            {
                string query = "select * from ProjectSection where ProjectSectionId=" + ProjectSection.ProjectSectionId;
                var dt = DataAccess.GetDataTable(query);

                if (dt == null || dt.Rows.Count == 0)
                {
                    ProjectSection.ProjectSectionId = GetID();
                    ProjectSection.PostId = GetID2();
                    query = "insert into ProjectSection values("+ ProjectSection.ProjectSectionId + "," +ProjectSection.PostId + ",'" + ProjectSection.SectionName + "','" + ProjectSection.Percentage + "'," + ProjectSection.Price + ")";
                }
                else
                {
                    query = "update ProjectSection set SectionName='" + ProjectSection.SectionName + "',Percentage='" + ProjectSection.Percentage + "',Price=" + ProjectSection.Price +  " where PostID=" + ProjectSection.PostId;
                }

                if (!IsValid(ProjectSection, result))
                {
                    return result;
                }

                result.HasError = DataAccess.ExecuteQuery(query) <= 0;

                if (result.HasError)
                    result.Message = "Something Went Wrong";
                else
                {
                    result.Data = ProjectSection;
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
            string query = "select * from ProjectSection order by ProjectSectionId desc";
            var dt = DataAccess.GetDataTable(query);
            int id = 1;

            if (dt != null && dt.Rows.Count != 0)
                id = Int32.Parse(dt.Rows[0]["ProjectSectionId"].ToString()) + 1;

            return id;
        }

        private int GetID2()
        {
            string query = "select * from PostAProject order by PostID desc";
            var dt = DataAccess.GetDataTable(query);
            int id = 1;

            if (dt != null && dt.Rows.Count != 0)
                id = Int32.Parse(dt.Rows[0]["PostID"].ToString());

            return id;
        }
        public List<ProjectSection> GetAll()
        {
            var result = new List<ProjectSection>();
            try
            {
                string query = "select * from ProjectSection";
                var dt = DataAccess.GetDataTable(query);

                if (dt != null && dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= dt.Rows.Count; i++)
                    {
                        ProjectSection u = ConvertToEntity(dt.Rows[i]);
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

        public Result<ProjectSection> GetByID(int id)
        {
            var result = new Result<ProjectSection>();
            try
            {
                string query = "select * from ProjectSection where PostID=" + id;
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
            var result = new Result<ProjectSection>();
            try
            {
                string query = "delete from ProjectSection where PostID=" + id;
                return DataAccess.ExecuteQuery(query) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool IsValid(ProjectSection obj, Result<ProjectSection> result)
        {
            if (!ValidationHelper.IsStringValid(obj.SectionName))
            {
                result.HasError = true;
                result.Message = "Invalid School Name";
                return false;
            }
           


            return true;
        }

        private ProjectSection ConvertToEntity(DataRow row)
        {
            try
            {
                ProjectSection u = new ProjectSection();
                u.PostId = Int32.Parse(row["PostID"].ToString());

                u.Price = Int32.Parse(row["Price"].ToString());
                u.Percentage = Int32.Parse(row["Percentage"].ToString());
                u.SectionName = row["SectionName"].ToString();
         


                return u;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
