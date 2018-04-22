using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Data;
using FMS_Entities;
using FMS_Framework.Object;
using SkillCategory = FMS_Entities.SkillCategory;

namespace FMS_Repository
{
    class CategoryDAO
    {
        public List<SkillCategory> GetAll()
        {
            var result = new List<SkillCategory>();
            try
            {
                string query = "select * from SkillCategory";
                var dt = DataAccess.GetDataTable(query);

                if (dt != null && dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= dt.Rows.Count; i++)
                    {
                        SkillCategory u = ConvertToEntity(dt.Rows[i]);
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
        public Result<SkillCategory> GetByID(int id)
        {
            var result = new Result<SkillCategory>();
            try
            {
                string query = "select * from SkillCategory where CategoryID=" + id;
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
        private SkillCategory ConvertToEntity(DataRow row)
        {
            try
            {
                SkillCategory u = new SkillCategory();
                u.CategoryId = Int32.Parse(row["CategoryID"].ToString());            
                u.CategoryName = row["CategoryName"].ToString();

                return u;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
