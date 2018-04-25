using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Data;
using FMS_Framework.Helper;
using FMS_Framework.Object;
using UserInfo = FMS_Entities.UserInfo;


namespace FMS_RepositoryOracle
{
    public class UserDAO
    {
            public Result<UserInfo> Save(UserInfo userinfo)
            {
                var result = new Result<UserInfo>();
                try
                {
                    string query = "select * from UserInfo where UserId=" + userinfo.UserId;
                    var dt = DataAccess.GetDataTable(query);
                   
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        userinfo.UserId = GetId();
                        var d = userinfo.JoinDate.ToString(string.Format("dd/MMM/yyyy"));
                        var b = userinfo.DateofBrith.ToString(string.Format("dd/MMM/yyyy"));
                        query = "insert into UserInfo values(" + userinfo.UserId + ",'" + userinfo.FristName + "','" + userinfo.LastName + "','" + userinfo.Email + "','" + userinfo.Password + "','" +b + "','" + d + "','','" + userinfo.City + "','" + userinfo.State + "','" + userinfo.Country + "',"+0+",'" + userinfo.UserType + "')";
                    }
                    else
                    {
                       // var b = userinfo.DateofBrith.ToString(string.Format("dd/MMM/yyyy"));

                        //string q1 = "declare ID trackuser.Userid%type; UName trackuser.username%type;  begin ID:=" + userinfo.UserId + ";  UName:='" + userinfo.FristName + "'; ";
                        //string q2 = "track_user_pkg.P_UPDATEUSER(ID, UName); end;";

                        //query = q1 + "update UserInfo set FirstName='" + userinfo.FristName + "',LastName='" +
                        //        userinfo.LastName + "',Password='" + userinfo.Password + "',City='" + userinfo.City +
                        //        "',State='" + userinfo.State + "',ProPic='" + userinfo.ProPic + "',Country='" +
                        //        userinfo.Country + "',Balance=" + userinfo.Balance + " where UserId=" +
                        //        userinfo.UserId + ";" + q2;
                        query = "update UserInfo set FirstName='" + userinfo.FristName + "',LastName='" +
                                       userinfo.LastName + "',Password='" + userinfo.Password + "',City='" +
                                       userinfo.City +
                                       "',State='" + userinfo.State + "',ProPic='" + userinfo.ProPic + "',Country='" +
                                       userinfo.Country + "',Balance=" + userinfo.Balance + " where UserId=" +
                                       userinfo.UserId;

                    }

                    //if (!IsValid(userinfo, result))
                    //{
                    //    return result;
                    //}

                    result.HasError = DataAccess.ExecuteQuery(query) <= 0;

                    if (result.HasError)
                        result.Message = "Something Went Wrong";
                    else
                    {
                        result.Data = userinfo;
                    }
                }
                catch (Exception ex)
                {
                    result.HasError = true;
                    result.Message = ex.Message;
                }
                return result;
            }

            private int GetId()
            {
                string query = "select * from UserInfo order by UserId desc";
                var dt = DataAccess.GetDataTable(query);
                int UserId = 1;

                if (dt != null && dt.Rows.Count != 0)
                    UserId = Int32.Parse(dt.Rows[0]["UserId"].ToString()) + 1;

                return UserId;
            }

            public Result<UserInfo> Login(string email, string password)
            {
                var result = new Result<UserInfo>();
                try
                {
                    string query = "select * from UserInfo where Email='" + email + "' and Password='" + password + "'";
                    var dt = DataAccess.GetDataTable(query);

                    if (dt == null || dt.Rows.Count == 0)
                    {
                        result.HasError = true;
                        result.Message = "InvalUserId Email or Pssword";
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

            public List<UserInfo> GetAll()
            {
                var result = new List<UserInfo>();
                try
                {
                    string query = "select * from UserInfo";
                    var dt = DataAccess.GetDataTable(query);

                    if (dt != null && dt.Rows.Count != 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            UserInfo u = ConvertToEntity(dt.Rows[i]);
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

            public Result<UserInfo> GetById(int UserId)
            {
                var result = new Result<UserInfo>();
                try
                {
                    string query = "select * from UserInfo where UserId=" + UserId;
                    var dt = DataAccess.GetDataTable(query);

                    if (dt == null || dt.Rows.Count == 0)
                    {
                        result.HasError = true;
                        result.Message = "Invalid UserId";
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

            public Result<UserInfo> GetByEmail(string Email)
            {
                var result = new Result<UserInfo>();
                try
                {
                    string query = "select * from UserInfo where Email='" + Email+"'";
                    var dt = DataAccess.GetDataTable(query);

                    if (dt == null || dt.Rows.Count == 0)
                    {
                        result.HasError = true;
                        result.Message = "Invalid UserId";
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

            public Result<List<UserInfo>> GetAll(string key = "")
            {
                var result = new Result<List<UserInfo>>() { Data = new List<UserInfo>() };
                try
                {
                    string query = "select * from UserInfo";
                    if (ValidationHelper.IsStringValid(key))
                    {
                        query += "where FristName like '%" + key + "%'";
                    }
                    if (ValidationHelper.IsStringValid(key))
                    {
                        query += "where LastName like '%" + key + "%'";
                    }
                    if (ValidationHelper.IsStringValid(key))
                    {
                        query += "where Email like '%" + key + "%'";
                    }
                    if (ValidationHelper.IsStringValid(key))
                    {
                        query += "where City like '%" + key + "%'";
                    }
                    if (ValidationHelper.IsStringValid(key))
                    {
                        query += "where State like '%" + key + "%'";
                    }
                    if (ValidationHelper.IsStringValid(key))
                    {
                        query += "where Country like '%" + key + "%'";
                    }
                    if (ValidationHelper.IsIntValid(key))
                    {
                        int id = Int32.Parse(key);
                        query += "or UserId=" + id;
                    }
                    var dt = DataAccess.GetDataTable(query);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Data.Add(ConvertToEntity(dt.Rows[i]));
                    }

                }
                catch (Exception e)
                {
                    result.HasError = true;
                    result.Message = e.Message;
                }
                return result;
            } 

            public bool Delete(int UserId)
            {
                var result = new Result<UserInfo>();
                try
                {
                    string query = "delete from UserInfo where UserId=" + UserId;
                    return DataAccess.ExecuteQuery(query) > 0;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            private bool IsValid(UserInfo obj, Result<UserInfo> result)
            {
                if (!ValidationHelper.IsStringValid(obj.FristName))
                {
                    result.HasError = true;
                    result.Message = "InvalUserId Frist Name";
                    return false;
                }
                if (!ValidationHelper.IsStringValid(obj.LastName))
                {
                    result.HasError = true;
                    result.Message = "InvalUserId Last Name";
                    return false;
                }
                if (!ValidationHelper.IsStringValid(obj.Email))
                {
                    result.HasError = true;
                    result.Message = "InvalUserId Email";
                    return false;
                }
                //if (!ValidationHelper.IsStringValid(obj.Password) || obj.Password.Length < 8)
                //{
                //    result.HasError = true;
                //    result.Message = "InvalUserId Password";
                //    return false;
                //}
              
                return true;
            }

            private UserInfo ConvertToEntity(DataRow row)
            {
                try
                {
                    UserInfo u = new UserInfo();
                    u.UserId = Int32.Parse(row["UserId"].ToString());
                    u.FristName = row["FirstName"].ToString();
                    u.LastName = row["LastName"].ToString();
                    u.Email = row["Email"].ToString();
                   // u.Email = row["Email"].ToString();
                    u.Password = row["Password"].ToString();
                    u.DateofBrith = Convert.ToDateTime(row["DateofBirth"]);
                    u.JoinDate = Convert.ToDateTime(row["JoinDate"]);
                    u.City = row["City"].ToString();
                    u.State = row["State"].ToString();
                    u.Country = row["Country"].ToString();
                    u.UserType = row["UserType"].ToString();
                    u.Balance = Int32.Parse(row["Balance"].ToString());
                    return u;
                }
                catch (Exception ex)
                {
                    return null;
                }

            }

         
        }
 }

