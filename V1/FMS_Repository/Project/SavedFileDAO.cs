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
using SavedFile = FMS_Entities.SavedFile;

namespace FMS_Repository.Project
{
  public  class SavedFileDAO
    {
        public Result<SavedFile> Save(SavedFile SavedFile)
        {
            var result = new Result<SavedFile>();
            try
            {
                string query = "select * from SavedFile where SavedFileId=" + SavedFile.SavedFileId;
                var dt = DataAccess.GetDataTable(query);

                if (dt == null || dt.Rows.Count == 0)
                {
                    SavedFile.SavedFileId = GetID();
                    query = "insert into SavedFile values(" + SavedFile.SavedFileId + "," + SavedFile.ProjectSection + "," + SavedFile.UserId + ",'" + SavedFile.FileLink + "')";
                }
                //else
                //{
                //    query = "update SavedFile set ProjectName='" + SavedFile.ProjectName + "',StartTime='" + SavedFile.StartTime + "',EndTime='" + SavedFile.EndTime + "',Description='" + SavedFile.Description + "',ProjectSection='" + SavedFile.ProjectSection + "',Price=" + SavedFile.Price + ",Members=" + SavedFile.Members + " where PostID=" + SavedFile.PostId;
                //}

                if (!IsValid(SavedFile, result))
                {
                    return result;
                }

                result.HasError = DataAccess.ExecuteQuery(query) <= 0;

                if (result.HasError)
                    result.Message = "Something Went Wrong";
                else
                {
                    result.Data = SavedFile;
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
            string query = "select * from SavedFile order by SavedFileId desc";
            var dt = DataAccess.GetDataTable(query);
            int id = 1;

            if (dt != null && dt.Rows.Count != 0)
                id = Int32.Parse(dt.Rows[0]["SavedFileId"].ToString()) + 1;

            return id;
        }
        public List<SavedFile> GetAll()
        {
            var result = new List<SavedFile>();
            try
            {
                string query = "select * from SavedFile";
                var dt = DataAccess.GetDataTable(query);

                if (dt != null && dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= dt.Rows.Count; i++)
                    {
                        SavedFile u = ConvertToEntity(dt.Rows[i]);
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

        public Result<SavedFile> GetByID(int id)
        {
            var result = new Result<SavedFile>();
            try
            {
                string query = "select * from SavedFile where SavedFileId=" + id;
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
        //    var result = new Result<SavedFile>();
        //    try
        //    {
        //        string query = "delete from SavedFile where SavedFileId=" + id;
        //        return DataAccess.ExecuteQuery(query) > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        private bool IsValid(SavedFile obj, Result<SavedFile> result)
        {
            if (!ValidationHelper.IsStringValid(obj.FileLink))
            {
                result.HasError = true;
                result.Message = "Invalid School Name";
                return false;
            }




            return true;
        }

        private SavedFile ConvertToEntity(DataRow row)
        {
            try
            {
                SavedFile u = new SavedFile();
                u.SavedFileId = Int32.Parse(row["CommunicationId"].ToString());
                u.UserId = Int32.Parse(row["UserID"].ToString());
                u.ProjectSectionId = Int32.Parse(row["ProjectSectionId"].ToString());
                u.FileLink = row["comment"].ToString();



                return u;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
