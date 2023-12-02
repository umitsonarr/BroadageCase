using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity
{

    public class AwayTeam
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string MediumName { get; set; }
    }

}