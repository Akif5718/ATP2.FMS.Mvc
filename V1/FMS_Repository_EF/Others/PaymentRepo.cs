using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Entities;
using FMS_Framework.Helper;
using FMS_Framework.Object;
using Payment = FMS_Entities.Payment;
namespace FMS_Repository_EF.OthersRepo
{
    class PaymentRepo:BaseRepo
    {


        public Result<Payment> Save(Payment Payment)
        {
            var result = new Result<Payment>();
            try
            {
                var objtosave = DbContext.Payments.FirstOrDefault(u => u.Id == Payment.Id);
                if (objtosave == null)
                {
                    objtosave = new Payment();
                    DbContext.Payments.Add(objtosave);
                }
                
                objtosave.OUserId = Payment.OUserId;
                objtosave.WUserId = Payment.WUserId;
                objtosave.Balance = Payment.Balance;



                if (!IsValid(objtosave, result))
                {
                    return result;
                }
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }

        private bool IsValid(Payment obj, Result<Payment> result)
        {
            
            if (!ValidationHelper.IsStringValid(obj.OUserId.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid OUserId";
                return false;
            }
            if (!ValidationHelper.IsStringValid(obj.WUserId.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid WUserId";
                return false;
            } 
           if (!ValidationHelper.IsStringValid(obj.Balance.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid Balance";
                return false;
            }


            return true;
        }

        public Result<List<Payment>> GetAll(string key = "")
        {
            var result = new Result<List<Payment>>() { Data = new List<Payment>() };

            try
            {
                IQueryable<Payment> query = DbContext.Payments;

                if (ValidationHelper.IsIntValid(key))
                {
                    query = query.Where(q => q.Id == Int32.Parse(key));
                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.OUserId == Int32.Parse(key));

                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.WUserId == Int32.Parse(key));

                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.Balance.Equals(Int32.Parse(key)));

                }
                result.Data = query.ToList();
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;


            }
            return result;
        }

        public Result<Payment> GetByID(int id)
        {
            var result = new Result<Payment>();

            try
            {
                var obj = DbContext.Payments.FirstOrDefault(c => c.Id == id);
                if (obj == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid UserID";
                    return result;


                }
                result.Data = obj;
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;


            }
            return result;
        }

        public Result<bool> Delete(int id)
        {
            var result = new Result<bool>();

            try
            {
                var objtodelete = DbContext.Payments.FirstOrDefault(c => c.Id == id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid UserID";
                    return result;


                }

                DbContext.Payments.Remove(objtodelete);
                DbContext.SaveChanges();

            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;


            }
            return result;
        }



       
    }
}
