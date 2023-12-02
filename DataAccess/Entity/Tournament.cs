using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity
{

    public class Tournament
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int RelationId { get; set; }
    }

}