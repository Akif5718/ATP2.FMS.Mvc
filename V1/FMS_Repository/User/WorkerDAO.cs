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
using WorkerInfo = FMS_Entities.WorkerInfo;

namespace FMS_RepositoryOracle
{
  public  class WorkerDAO
    {


        public Result<WorkerInfo> Save(WorkerInfo WorkerInfo)
        {
            var result = new Result<WorkerInfo>();
            try
            {
                string query = "select * from WorkerInfo where UserId=" + WorkerInfo.UserId;
                var dt = DataAccess.GetDataTable(query);

                if (dt == null || dt.Rows.Count == 0)
                {
                    WorkerInfo.UserId = CurrentUser.User.UserId;
                    query = "insert into WorkerInfo values(" + WorkerInfo.UserId + ",'" + WorkerInfo.EarnedMoney + "','" + WorkerInfo.RatePerHour + "')";
                }
                else
                {
                    query = "update WorkerInfo set EarnedMoney=" + WorkerInfo.EarnedMoney + ",RatePerHour='" + WorkerInfo.RatePerHour + "' where UserId=" +
                      WorkerInfo.UserId;
                }

              

                result.HasError = DataAccess.ExecuteQuery(query) <= 0;

                if (result.HasError)
                    result.Message = "Something Went Wrong";
                else
                {
                    result.Data = WorkerInfo;
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
            string query = "select * from WorkerInfo order by UserId desc";
            var dt = DataAccess.GetDataTable(query);
            int id = 1;

            if (dt != null && dt.Rows.Count != 0)
                id = Int32.Parse(dt.Rows[0]["ID"].ToString()) + 1;

            return id;
        }
        public List<WorkerInfo> GetAll()
        {
            var result = new List<WorkerInfo>();
            try
            {
                string query = "select * from WorkerInfo";
                var dt = DataAccess.GetDataTable(query);

                if (dt != null && dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= dt.Rows.Count; i++)
                    {
                        WorkerInfo u = ConvertToEntity(dt.Rows[i]);
                        result.Add(u);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public Result<WorkerInfo> GetByID(int id)
        {
            var result = new Result<WorkerInfo>();
            try
            {
                string query = "select * from WorkerInfo where UserId=" + id;
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
            var result = new Result<WorkerInfo>();
            try
            {
                string query = "delete from WorkerInfo where UserId=" + id;
                return DataAccess.ExecuteQuery(query) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    

        private WorkerInfo ConvertToEntity(DataRow row)
        {
            try
            {
                WorkerInfo u = new WorkerInfo();
                u.UserId = Int32.Parse(row["UserID"].ToString());
                u.EarnedMoney = Int32.Parse(row["EarnedMoney"].ToString());
                u.RatePerHour = row["RatePerHour"].ToString();



                return u;
            }
            catch (Exception)
            {
                return null;
            }

        }
   
    }
}
