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
using Payment = FMS_Entities.Payment;

namespace FMS_Repository
{
   public class PaymentDAO
    {
        public Result<Payment> Save(Payment Payment)
        {
            var result = new Result<Payment>();
            try
            {
                string query = "select * from Payment where UserId=" + Payment.Id;
                var dt = DataAccess.GetDataTable(query);

                if (dt == null || dt.Rows.Count == 0)
                {
                    // Payment.UserId = GetID();
                    query = "insert into Payment values(" + Payment.Id + "," + Payment.OUserId + "," + Payment.WUserId + "," + Payment.Balance + ")";
                }
                else
                {
                    query = "update Payment set Balance=" + Payment.Balance + " where UserId=" + Payment.Id;
                }

                //if (!IsValid(Payment, result))
                //{
                //    return result;
                //}

                result.HasError = DataAccess.ExecuteQuery(query) <= 0;

                if (result.HasError)
                    result.Message = "Something Went Wrong";
                else
                {
                    result.Data = Payment;
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
            string query = "select * from Payment order by Id desc";
            var dt = DataAccess.GetDataTable(query);
            int id = 1;

            if (dt != null && dt.Rows.Count != 0)
                id = Int32.Parse(dt.Rows[0]["ID"].ToString()) + 1;

            return id;
        }

        public List<Payment> GetAll()
        {
            var result = new List<Payment>();
            try
            {
                string query = "select * from Payment";
                var dt = DataAccess.GetDataTable(query);

                if (dt != null && dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Payment u = ConvertToEntity(dt.Rows[i]);
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

        public Result<Payment> GetByID(int id)
        {
            var result = new Result<Payment>();
            try
            {
                string query = "select * from Payment where Id=" + id;
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
            var result = new Result<Payment>();
            try
            {
                string query = "delete from Payment where Id=" + id;
                return DataAccess.ExecuteQuery(query) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private Payment ConvertToEntity(DataRow row)
        {
            try
            {
                Payment u = new Payment();
                u.Id = Int32.Parse(row["Id"].ToString());
                u.OUserId = Int32.Parse(row["OUserId"].ToString());
                u.WUserId = Int32.Parse(row["WUserId"].ToString());
                u.Balance = Int32.Parse(row["Balance"].ToString());
               
                return u;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
