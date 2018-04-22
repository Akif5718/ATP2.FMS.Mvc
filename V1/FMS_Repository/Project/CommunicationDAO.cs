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
using COMMENTSEC = FMS_Entities.COMMENTSEC;

namespace FMS_Repository.Project
{
   public class CommunicationDAO
    {
        public Result<COMMENTSEC> Save(COMMENTSEC COMMENTSEC)
        {
            var result = new Result<COMMENTSEC>();
            try
            {
                string query = "select * from COMMENTSEC where CommunicationId=" + COMMENTSEC.CommunicationId;
                var dt = DataAccess.GetDataTable(query);

                if (dt == null || dt.Rows.Count == 0)
                {
                    COMMENTSEC.CommunicationId = GetID();
                    query = "insert into COMMENTSEC values(" + COMMENTSEC.CommunicationId + "," + COMMENTSEC.UserId + "," + COMMENTSEC.ProjectSection + ",'" + COMMENTSEC.Commt + "')";
                }
                //else
                //{
                //    query = "update COMMENTSEC set ProjectName='" + COMMENTSEC.ProjectName + "',StartTime='" + COMMENTSEC.StartTime + "',EndTime='" + COMMENTSEC.EndTime + "',Description='" + COMMENTSEC.Description + "',ProjectSection='" + COMMENTSEC.ProjectSection + "',Price=" + COMMENTSEC.Price + ",Members=" + COMMENTSEC.Members + " where PostID=" + COMMENTSEC.PostId;
                //}

                if (!IsValid(COMMENTSEC, result))
                {
                    return result;
                }

                result.HasError = DataAccess.ExecuteQuery(query) <= 0;

                if (result.HasError)
                    result.Message = "Something Went Wrong";
                else
                {
                    result.Data = COMMENTSEC;
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
            string query = "select * from COMMENTSEC order by CommunicationId desc";
            var dt = DataAccess.GetDataTable(query);
            int id = 1;

            if (dt != null && dt.Rows.Count != 0)
                id = Int32.Parse(dt.Rows[0]["CommunicationId"].ToString()) + 1;

            return id;
        }
        public List<COMMENTSEC> GetAll()
        {
            var result = new List<COMMENTSEC>();
            try
            {
                string query = "select * from COMMENTSEC";
                var dt = DataAccess.GetDataTable(query);

                if (dt != null && dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= dt.Rows.Count; i++)
                    {
                        COMMENTSEC u = ConvertToEntity(dt.Rows[i]);
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

        public Result<COMMENTSEC> GetByID(int id)
        {
            var result = new Result<COMMENTSEC>();
            try
            {
                string query = "select * from COMMENTSEC where CommunicationId=" + id;
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
        //    var result = new Result<COMMENTSEC>();
        //    try
        //    {
        //        string query = "delete from COMMENTSEC where CommunicationId=" + id;
        //        return DataAccess.ExecuteQuery(query) > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        private bool IsValid(COMMENTSEC obj, Result<COMMENTSEC> result)
        {
            if (!ValidationHelper.IsStringValid(obj.Commt))
            {
                result.HasError = true;
                result.Message = "Invalid School Name";
                return false;
            }
          
            


            return true;
        }

        private COMMENTSEC ConvertToEntity(DataRow row)
        {
            try
            {
                COMMENTSEC u = new COMMENTSEC();
                u.CommunicationId = Int32.Parse(row["CommunicationId"].ToString());
                u.UserId = Int32.Parse(row["UserID"].ToString());
                u.ProjectSectionId = Int32.Parse(row["ProjectSectionId"].ToString());
                u.Commt = row["Commt"].ToString();
               


                return u;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
