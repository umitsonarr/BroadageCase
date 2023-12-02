using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Log
    {
        public int Id { get; set; }
        public LogStatus Status { get; set; }
        public string ErrorMessage { get; set; }
    }
    public enum LogStatus
    {
        Success = 1,
        Error
    }
}
