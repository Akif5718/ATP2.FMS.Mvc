using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Data;
using FMS_Entities;
using FMS_Framework;
using FMS_Framework.Helper;
using FMS_Framework.Object;
using WorkerSkill = FMS_Entities.WorkerSkill;

namespace FMS_Repository
{
  public  class WorkerSkillDAO
    {
        public Result<WorkerSkill> Save(WorkerSkill WorkerSkill)
        {
            var result = new Result<WorkerSkill>();
            try
            {
                string query = "select * from WorkerSkill where UserId=" + WorkerSkill.UserId;
                var dt = DataAccess.GetDataTable(query);

                if (dt == null || dt.Rows.Count == 0)
                {
                    WorkerSkill.UserId = CurrentUser.User.UserId;
                    query = "insert into WorkerSkill values(" + WorkerSkill.UserId + "," + WorkerSkill.SkillId + ")";
                }
                else
                {
                    query = "update WorkerSkill set SkillId=" + WorkerSkill.SkillId + " where UserId=" + WorkerSkill.UserId;
                }

                result.HasError = DataAccess.ExecuteQuery(query) <= 0;

                if (result.HasError)
                    result.Message = "Something Went Wrong";
                else
                {
                    result.Data = WorkerSkill;
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }

        public List<WorkerSkill> GetAll()
        {
            var result = new List<WorkerSkill>();
            try
            {
                string query = "select * from WorkerSkill";
                var dt = DataAccess.GetDataTable(query);

                if (dt != null && dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        WorkerSkill u = ConvertToEntity(dt.Rows[i]);
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

        public Result<WorkerSkill> GetById(int UserId)
        {
            var result = new Result<WorkerSkill>();
            try
            {
                string query = "select * from WorkerSkill where UserId=" + UserId;
                var dt = DataAccess.GetDataTable(query);

                if (dt == null || dt.Rows.Count == 0)
                {
                    result.HasError = true;
                    result.Message = "Invalid UserId";
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

        public bool Delete(int UserId)
        {
            var result = new Result<WorkerSkill>();
            try
            {
                string query = "delete from WorkerSkill where UserId=" + UserId;
                return DataAccess.ExecuteQuery(query) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private WorkerSkill ConvertToEntity(DataRow row)
        {
            try
            {
                WorkerSkill u = new WorkerSkill();
                u.UserId = Int32.Parse(row["UserId"].ToString());
                u.SkillId = Int32.Parse(row["SkillId"].ToString());
               
                return u;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
