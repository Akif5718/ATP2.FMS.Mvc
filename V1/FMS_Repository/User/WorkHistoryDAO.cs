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
using WorkHistory = FMS_Entities.WorkHistory;

namespace FMS_Repository
{
  public  class WorkHistoryDAO
    {
        public Result<WorkHistory> Save(WorkHistory WorkHistory)
        {
            var result = new Result<WorkHistory>();
            try
            {
                string query = "select * from WorkHistory where UserId=" + WorkHistory.UserId;
                var dt = DataAccess.GetDataTable(query);

                if (dt == null || dt.Rows.Count == 0)
                {
                    // WorkHistory.UserId = GetID();
                    query = "insert into WorkHistory values(" + WorkHistory.UserId + ",'" + WorkHistory.CompanyName + "','" + WorkHistory.Position + "','" + WorkHistory.Experience + "')";
                }
                else
                {
                    query = "update WorkHistory set CompanyName='" + WorkHistory.CompanyName + "',Experience='" + WorkHistory.Experience + "',Position='" + WorkHistory.Position + "' where UserId=" +
                      WorkHistory.UserId;
                }

                if (!IsValid(WorkHistory, result))
                {
                    return result;
                }

                result.HasError = DataAccess.ExecuteQuery(query) <= 0;

                if (result.HasError)
                    result.Message = "Something Went Wrong";
                else
                {
                    result.Data = WorkHistory;
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
        //    string query = "select * from WorkHistory order by UserId desc";
        //    var dt = DataAccess.GetDataTable(query);
        //    int id = 1;

        //    if (dt != null && dt.Rows.Count != 0)
        //        id = Int32.Parse(dt.Rows[0]["ID"].ToString()) + 1;

        //    return id;
        //}
        public List<WorkHistory> GetAll()
        {
            var result = new List<WorkHistory>();
            try
            {
                string query = "select * from WorkHistory";
                var dt = DataAccess.GetDataTable(query);

                if (dt != null && dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= dt.Rows.Count; i++)
                    {
                        WorkHistory u = ConvertToEntity(dt.Rows[i]);
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

        public Result<WorkHistory> GetByID(int id)
        {
            var result = new Result<WorkHistory>();
            try
            {
                string query = "select * from WorkHistory where UserId=" + id;
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
            var result = new Result<WorkHistory>();
            try
            {
                string query = "delete from WorkHistory where UserId=" + id;
                return DataAccess.ExecuteQuery(query) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool IsValid(WorkHistory obj, Result<WorkHistory> result)
        {
            if (!ValidationHelper.IsStringValid(obj.CompanyName))
            {
                result.HasError = true;
                result.Message = "Invalid Frist Name";
                return false;
            }
           

            return true;
        }

        private WorkHistory ConvertToEntity(DataRow row)
        {
            try
            {
                WorkHistory u = new WorkHistory();
                u.UserId = Int32.Parse(row["UserId"].ToString());
                u.CompanyName = row["CompanyName"].ToString();
                u.Position = row["Position"].ToString();
                u.Experience = row["Experience"].ToString();
               


                return u;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
