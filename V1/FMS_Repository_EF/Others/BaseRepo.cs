using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Data;

namespace FMS_Repository_EF
{
   public class BaseRepo
    {
        private static FMSDbContext _context;
        public static FMSDbContext DbContext
        {
            get
            {
                if (_context == null)
                    _context = new FMSDbContext();
                return _context;
            }
        }
    }
}
