using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Data;
using FMS_Entities;
using FMS_Framework.Object;
using Skill = FMS_Entities.Skill;

namespace FMS_Repository
{
    public  class SkillsDAO
    {
        public List<Skill> GetAll()
        {
            var result = new List<Skill>();
            try
            {
                string query = "select * from Skill";
                var dt = DataAccess.GetDataTable(query);

                if (dt != null && dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= dt.Rows.Count; i++)
                    {
                        Skill u = ConvertToEntity(dt.Rows[i]);
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

        public Result<Skill> GetByID(int id)
        {
            var result = new Result<Skill>();
            try
            {
                string query = "select * from Skill where SkillID=" + id;
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

        private Skill ConvertToEntity(DataRow row)
        {
            try
            {
                Skill u = new Skill();
                u.SkillId = Int32.Parse(row["SkillID"].ToString());
                u.CategoryId = Int32.Parse(row["CategoryID"].ToString());

                u.SkillName = row["SkillName"].ToString();



                return u;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
