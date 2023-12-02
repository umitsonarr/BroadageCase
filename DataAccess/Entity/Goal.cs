using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Goal
    {
        [Key]
        public int Id { get; set; }
        public int? Current { get; set; }
        public int? Regular { get; set; }
        public int? HalfTime { get; set; }
    }
}
