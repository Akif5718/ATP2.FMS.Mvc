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
using SelectedWorker = FMS_Entities.SelectedWorker;

namespace FMS_Repository.Project
{
   public class SelectedWorkerDAO
    {
        public Result<SelectedWorker> Save(SelectedWorker SelectedWorker)
        {
            var result = new Result<SelectedWorker>();
            try
            {
                string query = "select * from SelectedWorker where PostId=" + SelectedWorker.PostId;
                var dt = DataAccess.GetDataTable(query);

                if (dt == null || dt.Rows.Count == 0)
                {
                   // SelectedWorker.SavedFileId = GetID();
                    var d = SelectedWorker.SubmissionDate.ToString(string.Format("dd/MMM/yyyy"));

                    query = "insert into SelectedWorker values(" + SelectedWorker.PostId + "," + SelectedWorker.UserId + "," + SelectedWorker.Price + ",'" + d + "')";
                }
                //else
                //{
                //    query = "update SelectedWorker set ProjectName='" + SelectedWorker.ProjectName + "',StartTime='" + SelectedWorker.StartTime + "',EndTime='" + SelectedWorker.EndTime + "',Description='" + SelectedWorker.Description + "',ProjectSection='" + SelectedWorker.ProjectSection + "',Price=" + SelectedWorker.Price + ",Members=" + SelectedWorker.Members + " where PostID=" + SelectedWorker.PostId;
                //}

              

                result.HasError = DataAccess.ExecuteQuery(query) <= 0;

                if (result.HasError)
                    result.Message = "Something Went Wrong";
                else
                {
                    result.Data = SelectedWorker;
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
        //    string query = "select * from SelectedWorker order by PostId desc";
        //    var dt = DataAccess.GetDataTable(query);
        //    int id = 1;

        //    if (dt != null && dt.Rows.Count != 0)
        //        id = Int32.Parse(dt.Rows[0]["SavedFileId"].ToString()) + 1;

        //    return id;
        //}
        public List<SelectedWorker> GetAll()
        {
            var result = new List<SelectedWorker>();
            try
            {
                string query = "select * from SelectedWorker";
                var dt = DataAccess.GetDataTable(query);

                if (dt != null && dt.Rows.Count != 0)
                {
                    for (int i = 0; i <dt.Rows.Count; i++)
                    {
                        SelectedWorker u = ConvertToEntity(dt.Rows[i]);
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

        public List<SelectedWorker> GetAll(int id)
        {
            var result = new List<SelectedWorker>();
            try
            {
                string query = "select * from SelectedWorker where PostId=" + id;
                var dt = DataAccess.GetDataTable(query);

                if (dt != null && dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        SelectedWorker u = ConvertToEntity(dt.Rows[i]);
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

        public List<SelectedWorker> GetAllUser(int id)
        {
            var result = new List<SelectedWorker>();
            try
            {
                string query = "select * from SelectedWorker where UserId=" + id;
                var dt = DataAccess.GetDataTable(query);

                if (dt != null && dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        SelectedWorker u = ConvertToEntity(dt.Rows[i]);
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

        public Result<SelectedWorker> GetByID(int id)
        {
            var result = new Result<SelectedWorker>();
            try
            {
                string query = "select * from SelectedWorker where PostId=" + id;
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

        //public bool Delete(int id)
        //{
        //    var result = new Result<SelectedWorker>();
        //    try
        //    {
        //        string query = "delete from SelectedWorker where SavedFileId=" + id;
        //        return DataAccess.ExecuteQuery(query) > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

       

        private SelectedWorker ConvertToEntity(DataRow row)
        {
            try
            {
                SelectedWorker u = new SelectedWorker();
                u.PostId = Int32.Parse(row["PostId"].ToString());
                u.UserId = Int32.Parse(row["UserID"].ToString());
                u.Price = Int32.Parse(row["Price"].ToString());
                u.SubmissionDate = Convert.ToDateTime(row["SubmissionDate"]);



                return u;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
