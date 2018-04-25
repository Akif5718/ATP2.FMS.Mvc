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
using RatingOwner = FMS_Entities.RatingOwner;

namespace FMS_Repository
{
   public class RatingOwnerDAO
    {
        public Result<RatingOwner> Save(RatingOwner RatingOwner)
        {
            var result = new Result<RatingOwner>();
            try
            {
                string query = "select * from RatingOwner where UserId=" + RatingOwner.UserId;
                var dt = DataAccess.GetDataTable(query);

                if (dt == null || dt.Rows.Count == 0)
                {
                    RatingOwner.UserId = CurrentUser.User.UserId;
                    query = "insert into RatingOwner values(" + RatingOwner.UserId + "," + RatingOwner.CommunicationSkill + "," + RatingOwner.Reliability + "," + RatingOwner.OnWord + "," + RatingOwner.Behaviour + ")";
                }
                else
                {
                    query = "update RatingOwner set CommunicationSkill=" + RatingOwner.CommunicationSkill + ",Reliability=" + RatingOwner.Reliability + ",OnWord=" + RatingOwner.OnWord + ",Behaviour=" + RatingOwner.Behaviour + " where UserId=" +
                      RatingOwner.UserId;
                }

                //if (!IsValid(RatingOwner, result))
                //{
                //    return result;
                //}

                result.HasError = DataAccess.ExecuteQuery(query) <= 0;

                if (result.HasError)
                    result.Message = "Something Went Wrong";
                else
                {
                    result.Data = RatingOwner;
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
        //    string query = "select * from RatingOwner order by UserId desc";
        //    var dt = DataAccess.GetDataTable(query);
        //    int id = 1;

        //    if (dt != null && dt.Rows.Count != 0)
        //        id = Int32.Parse(dt.Rows[0]["ID"].ToString()) + 1;

        //    return id;
        //}
        public List<RatingOwner> GetAll()
        {
            var result = new List<RatingOwner>();
            try
            {
                string query = "select * from RatingOwner";
                var dt = DataAccess.GetDataTable(query);

                if (dt != null && dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= dt.Rows.Count; i++)
                    {
                        RatingOwner u = ConvertToEntity(dt.Rows[i]);
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

        public Result<RatingOwner> GetByID(int id)
        {
            var result = new Result<RatingOwner>();
            try
            {
                string query = "select * from RatingOwner where UserId=" + id;
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
            var result = new Result<RatingOwner>();
            try
            {
                string query = "delete from RatingOwner where UserId=" + id;
                return DataAccess.ExecuteQuery(query) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

       
        private RatingOwner ConvertToEntity(DataRow row)
        {
            try
            {
                RatingOwner u = new RatingOwner();
                u.UserId = Int32.Parse(row["UserId"].ToString());
                u.CommunicationSkill = Int32.Parse(row["CommunicationSkill"].ToString());
                u.Reliability = Int32.Parse(row["Reliability"].ToString());
                u.OnWord = Int32.Parse(row["OnWord"].ToString());
                u.Behaviour = Int32.Parse(row["Behaviour"].ToString());
              


                return u;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
