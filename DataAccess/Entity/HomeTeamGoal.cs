using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class HomeTeamGoal
    {
        [Key]
        public int Id { get; set; }
        public HomeTeam HomeTeam { get; set; }
        public Goal Goal { get; set; }
    }
}
