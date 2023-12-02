using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class AwayTeamGoal
    {
        [Key]
        public int Id { get; set; }
        public AwayTeam AwayTeam { get; set; }
        public Goal Goal { get; set; }
    }
}
