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
using ResponseToaJob = FMS_Entities.ResponseToaJob;
namespace FMS_Repository
{
   public class ResponseToJobDAO
    {
        public Result<ResponseToaJob> Save(ResponseToaJob ResponseToaJob)
        {
            var result = new Result<ResponseToaJob>();
            try
            {
                string query = "select * from ResponseToaJob where PostID=" + ResponseToaJob.PostId;
                //var dt = DataAccess.GetDataTable(query);

                //if (dt == null || dt.Rows.Count == 0)
                //{
                    // ResponseToaJob.PostID = GetID();
                    query = "insert into ResponseToaJob values(" + ResponseToaJob.PostId + "," + ResponseToaJob.WUserId + "," + ResponseToaJob.FixedPrice + ",'" + ResponseToaJob.SubmissionTime + "')";
                //}
                //else
                //{
                //    query = "update ResponseToaJob set FixedPrice=" + ResponseToaJob.FixedPrice + ",SubmissionTime='" + ResponseToaJob.SubmissionTime + "' where PostID=" +
                //      ResponseToaJob.PostId;
                //}

               

                result.HasError = DataAccess.ExecuteQuery(query) <= 0;

                if (result.HasError)
                    result.Message = "Something Went Wrong";
                else
                {
                    result.Data = ResponseToaJob;
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
        //    string query = "select * from ResponseToaJob order by PostID desc";
        //    var dt = DataAccess.GetDataTable(query);
        //    int id = 1;

        //    if (dt != null && dt.Rows.Count != 0)
        //        id = Int32.Parse(dt.Rows[0]["ID"].ToString()) + 1;

        //    return id;
        //}
        public List<ResponseToaJob> GetAll(int id)
        {
            var result = new List<ResponseToaJob>();
            try
            {
                string query = "select * from ResponseToaJob where PostID=" + id;
                var dt = DataAccess.GetDataTable(query);

                if (dt != null && dt.Rows.Count != 0)
                {
                    for (int i = 0; i <dt.Rows.Count; i++)
                    {
                        ResponseToaJob u = ConvertToEntity(dt.Rows[i]);
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

        public Result<ResponseToaJob> GetByID(int id)
        {
            var result = new Result<ResponseToaJob>();
            try
            {
                string query = "select * from ResponseToaJob where PostID=" + id;
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
            var result = new Result<ResponseToaJob>();
            try
            {
                string query = "delete from ResponseToaJob where PostID=" + id;
                return DataAccess.ExecuteQuery(query) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private ResponseToaJob ConvertToEntity(DataRow row)
        {
            try
            {
                ResponseToaJob u = new ResponseToaJob();
                u.PostId = Int32.Parse(row["PostID"].ToString());
                u.WUserId = Int32.Parse(row["WUserID"].ToString());
                u.FixedPrice = Int32.Parse(row["FixedPrice"].ToString());
                u.SubmissionTime = row["SubmissionDate"].ToString();
              


                return u;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
