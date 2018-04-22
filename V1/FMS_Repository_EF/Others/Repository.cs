using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Data;

namespace FMS_Repository_EF
{
   public class Repository<T>:IRepository<T> where T: class
   {
       protected FMSDbContext context = new FMSDbContext();
       public bool Insert(T entities)
       {

           try
           {


               context.Set<T>().Add(entities);
               context.SaveChanges();
               return true;
           }
           catch (Exception)
           {

               return false;
           }
       }

       public bool Update(T entities)
       {
           try
           {
               context.Entry(entities).State = EntityState.Modified;
               context.SaveChanges();
               return true;
           }
           catch (Exception)
           {

               return false;
           }
       }

       public bool Delete(int id)
       {
           try
           {
               var entities = context.Set<T>().Find(id);
               context.Entry(entities).State = EntityState.Modified;
               context.SaveChanges();
               return true;
           }
           catch (Exception)
           {

               return false;
           }
       }

       public ICollection<T> GetAll()
       {
           return context.Set<T>().ToList();
       }

       public T GetbyId(int id)
       {
           return context.Set<T>().Find(id);
       }
   }
}
