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
using FMS_RepositoryOracle;
using PostAProject = FMS_Entities.PostAProject;

namespace FMS_Repository
{
   public class PostProjectDAO
    {
       public Result<PostAProject> Save(PostAProject PostAProject)
        {
            var result = new Result<PostAProject>();
            try
            {
                string query = "select * from PostAProject where PostID=" + PostAProject.PostId;
                var dt = DataAccess.GetDataTable(query);

                if (dt == null || dt.Rows.Count == 0)
                {
                     PostAProject.PostId = GetID();
                     var d = PostAProject.StartTime.ToString(string.Format("dd/MMM/yyyy"));
                     var b = PostAProject.EndTime.ToString(string.Format("dd/MMM/yyyy"));
                     query = "insert into PostAProject values(" + PostAProject.PostId + "," +PostAProject.WUserId + ",'" + PostAProject.ProjectName + "'," + PostAProject.Price + ",'" + d + "','" + b + "','" + PostAProject.Description + "'," + PostAProject.Members + ")";
                }
                else
                {
                    query = "update PostAProject set ProjectName='" + PostAProject.ProjectName + "',StartTime='" + PostAProject.StartTime + "',EndTime='" + PostAProject.EndTime + "',Description='" + PostAProject.Description + "',Price=" + PostAProject.Price + ",Members=" + PostAProject.Members + " where PostID=" + PostAProject.PostId;
                }

                if (!IsValid(PostAProject, result))
                {
                    return result;
                }

                result.HasError = DataAccess.ExecuteQuery(query) <= 0;

                if (result.HasError)
                    result.Message = "Something Went Wrong";
                else
                {
                    result.Data = PostAProject;
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
           string query = "select * from PostAProject order by PostID desc";
           var dt = DataAccess.GetDataTable(query);
           int id = 1;

           if (dt != null && dt.Rows.Count != 0)
               id = Int32.Parse(dt.Rows[0]["PostID"].ToString()) + 1;

           return id;
       }

       public List<PostAProject> GetAll()
        {
            var result = new List<PostAProject>();
            try
            {
                string query = "select * from PostAProject";
                var dt = DataAccess.GetDataTable(query);

                if (dt != null && dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= dt.Rows.Count; i++)
                    {
                        PostAProject u = ConvertToEntity(dt.Rows[i]);
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

       public List<PostAProject> GetAll(int id)
       {
           var result = new List<PostAProject>();
           try
           {
               string query = "select * from PostAProject where PostID=" + id;
               var dt = DataAccess.GetDataTable(query);

               if (dt != null && dt.Rows.Count != 0)
               {
                   for (int i = 0; i <= dt.Rows.Count; i++)
                   {
                       PostAProject u = ConvertToEntity(dt.Rows[i]);
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

       public List<PostAProject> GetAllUser(int id)
       {
           var result = new List<PostAProject>();
           try
           {
               string query = "select * from PostAProject where WUserId=" + id;
               var dt = DataAccess.GetDataTable(query);

               if (dt != null && dt.Rows.Count != 0)
               {
                   for (int i = 0; i <= dt.Rows.Count; i++)
                   {
                       PostAProject u = ConvertToEntity(dt.Rows[i]);
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

       public Result<List<PostAProject>> GetAll(string key = "")
       {
           var result = new Result<List<PostAProject>>() { Data = new List<PostAProject>() };
           try
           {
               string query = "select * from PostAProject";
               if (ValidationHelper.IsStringValid(key))
               {
                   query += "where ProjectName like '%" + key + "%'";
               }
               if (ValidationHelper.IsStringValid(key))
               {
                   query += "where StartTime like '%" + key + "%'";
               }
               if (ValidationHelper.IsStringValid(key))
               {
                   query += "where EndTime like '%" + key + "%'";
               }
               
               if (ValidationHelper.IsIntValid(key))
               {
                   int id = Int32.Parse(key);
                   query += "or Price=" + id;
               }
               if (ValidationHelper.IsIntValid(key))
               {
                   int id = Int32.Parse(key);
                   query += "or PostId=" + id;
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

       public Result<PostAProject> GetByID(int id)
        {
            var result = new Result<PostAProject>();
            try
            {
                string query = "select * from PostAProject where PostID=" + id;
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
            var result = new Result<PostAProject>();
            UserDAO usr = new UserDAO();
            var user = usr.GetById(id);
            try
            {
                string q1 = "declare ID trackuser.Userid%type; UName trackuser.username%type;  begin  ID:=" + id + "; UName:='" + user.Data.FristName + "'; ";
                string q2 = "track_user_pkg.P_DELETEPOST(ID,UName); end /";
                string query = q1 + "delete from PostAProject where PostID=" + id + ";" + q2;
                return DataAccess.ExecuteQuery(query) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool IsValid(PostAProject obj, Result<PostAProject> result)
        {
            if (!ValidationHelper.IsStringValid(obj.ProjectName))
            {
                result.HasError = true;
                result.Message = "Invalid School Name";
                return false;
            }
           
          

            return true;
        }

        private PostAProject ConvertToEntity(DataRow row)
        {
            try
            {
                PostAProject u = new PostAProject();
                u.PostId = Int32.Parse(row["PostID"].ToString());
                u.WUserId = Int32.Parse(row["WUserID"].ToString());
                u.Price = Int32.Parse(row["Price"].ToString());
                u.Members = Int32.Parse(row["Members"].ToString());
             
                u.ProjectName = row["ProjectName"].ToString();
               
                u.Description = row["Description"].ToString();
                u.StartTime = Convert.ToDateTime(row["StartTime"]);
                u.EndTime = Convert.ToDateTime(row["EndTime"]);


                return u;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
