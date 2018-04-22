using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Data;
using FMS_Entities;
using FMS_Framework.Object;
using RatingWorker = FMS_Entities.RatingWorker;

namespace FMS_Repository
{
    class RatingWorkerDAO
    {
        public Result<RatingWorker> Save(RatingWorker RatingWorker)
        {
            var result = new Result<RatingWorker>();
            try
            {
                string query = "select * from RatingWorker where UserId=" + RatingWorker.UserId;
                var dt = DataAccess.GetDataTable(query);

                if (dt == null || dt.Rows.Count == 0)
                {
                    // RatingWorker.UserId = GetID();
                    query = "insert into RatingWorker values(" + RatingWorker.UserId + "," + RatingWorker.CommunicationSkill + "," + RatingWorker.OnBudget + "," + RatingWorker.OnTime + "," + RatingWorker.Behaviour + "," + RatingWorker.Completeness + ")";
                }
                else
                {
                    query = "update RatingWorker set CommunicationSkill=" + RatingWorker.CommunicationSkill + ",OnBudget=" + RatingWorker.OnBudget + ",OnTime=" + RatingWorker.OnTime + ",Behaviour=" + RatingWorker.Behaviour + ",Completeness=" + RatingWorker.Completeness + " where UserId=" +
                      RatingWorker.UserId;
                }

                //if (!IsValid(RatingWorker, result))
                //{
                //    return result;
                //}

                result.HasError = DataAccess.ExecuteQuery(query) <= 0;

                if (result.HasError)
                    result.Message = "Something Went Wrong";
                else
                {
                    result.Data = RatingWorker;
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
        //    string query = "select * from RatingWorker order by UserId desc";
        //    var dt = DataAccess.GetDataTable(query);
        //    int id = 1;

        //    if (dt != null && dt.Rows.Count != 0)
        //        id = Int32.Parse(dt.Rows[0]["ID"].ToString()) + 1;

        //    return id;
        //}
        public List<RatingWorker> GetAll()
        {
            var result = new List<RatingWorker>();
            try
            {
                string query = "select * from RatingWorker";
                var dt = DataAccess.GetDataTable(query);

                if (dt != null && dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= dt.Rows.Count; i++)
                    {
                        RatingWorker u = ConvertToEntity(dt.Rows[i]);
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

        public Result<RatingWorker> GetByID(int id)
        {
            var result = new Result<RatingWorker>();
            try
            {
                string query = "select * from RatingWorker where UserId=" + id;
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
            var result = new Result<RatingWorker>();
            try
            {
                string query = "delete from RatingWorker where UserId=" + id;
                return DataAccess.ExecuteQuery(query) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private RatingWorker ConvertToEntity(DataRow row)
        {
            try
            {
                RatingWorker u = new RatingWorker();
                u.UserId = Int32.Parse(row["UserId"].ToString());
                u.CommunicationSkill = Int32.Parse(row["CommunicationSkill"].ToString());
                u.OnBudget = Int32.Parse(row["OnBudget"].ToString());
                u.OnTime = Int32.Parse(row["OnTime"].ToString());
                u.Behaviour = Int32.Parse(row["Behaviour"].ToString());
                u.Completeness = Int32.Parse(row["Completeness"].ToString());



                return u;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
