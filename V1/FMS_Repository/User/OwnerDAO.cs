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
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using OwnerInfo = FMS_Entities.OwnerInfo;
namespace FMS_RepositoryOracle
{
   public class OwnerDAO
    {
        public Result<OwnerInfo> Save(OwnerInfo OwnerInfo)
        {
            var result = new Result<OwnerInfo>();
            try
            {
                string query = "select * from OwnerInfo where UserId=" + OwnerInfo.UserId;
                var dt = DataAccess.GetDataTable(query);

                if (dt == null || dt.Rows.Count == 0)
                {
                    OwnerInfo.UserId = CurrentUser.User.UserId;
                    query = "insert into OwnerInfo values(" + OwnerInfo.UserId + ",'" + OwnerInfo.CompanyName + "','" + OwnerInfo.CompanyAddress + "','" + OwnerInfo.CompanyCode + "','" + OwnerInfo.Position + "')";
                }
                else
                {
                    query = "update OwnerInfo set CompanyName='" + OwnerInfo.CompanyName + "',CompanyCode='" + OwnerInfo.CompanyCode + "',CompanyAddress='" + OwnerInfo.CompanyAddress + "',Position='" + OwnerInfo.Position + "' where UserId=" +
                      OwnerInfo.UserId;
                }

                if (!IsValid(OwnerInfo, result))
                {
                    return result;
                }

                result.HasError = DataAccess.ExecuteQuery(query) <= 0;

                if (result.HasError)
                    result.Message = "Something Went Wrong";
                else
                {
                    result.Data = OwnerInfo;
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
        //    string query = "select * from OwnerInfo order by UserId desc";
        //    var dt = DataAccess.GetDataTable(query);
        //    int id = 1;

        //    if (dt != null && dt.Rows.Count != 0)
        //        id = Int32.Parse(dt.Rows[0]["ID"].ToString()) + 1;

        //    return id;
        //}
        public List<OwnerInfo> GetAll()
        {
            var result = new List<OwnerInfo>();
            try
            {
                string query = "select * from OwnerInfo";
                var dt = DataAccess.GetDataTable(query);

                if (dt != null && dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= dt.Rows.Count; i++)
                    {
                        OwnerInfo u = ConvertToEntity(dt.Rows[i]);
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

        public Result<OwnerInfo> GetByID(int id)
        {
            var result = new Result<OwnerInfo>();
            try
            {
                string query = "select * from OwnerInfo where UserId=" + id;
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
            var result = new Result<OwnerInfo>();
            try
            {
                string query = "delete from OwnerInfo where UserId=" + id;
                return DataAccess.ExecuteQuery(query) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool IsValid(OwnerInfo obj, Result<OwnerInfo> result)
        {
            if (!ValidationHelper.IsStringValid(obj.CompanyName))
            {
                result.HasError = true;
                result.Message = "Invalid Frist Name";
                return false;
            }
            if (!ValidationHelper.IsStringValid(obj.CompanyCode))
            {
                result.HasError = true;
                result.Message = "Invalid Last Name";
                return false;
            }
           

            return true;
        }

        private OwnerInfo ConvertToEntity(DataRow row)
        {
            try
            {
                OwnerInfo u = new OwnerInfo();
                u.UserId = Int32.Parse(row["UserId"].ToString());
                u.CompanyName = row["CompanyName"].ToString();
                u.CompanyAddress = row["CompanyAddress"].ToString();
                u.CompanyCode = row["CompanyCode"].ToString();
                u.Position = row["Position"].ToString();
                

                return u;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
