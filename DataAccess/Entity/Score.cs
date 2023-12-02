using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity
{

    public class Score
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public HomeTeamGoal HomeTeamGoal { get; set; }
        public AwayTeamGoal AwayTeamGoal { get; set; }
        public Tournament Tournament { get; set; }
        public Status Status { get; set; }
        public Stage Stage { get; set; }
        public Round Round { get; set; }
        public Minute Minute { get; set; }
        public int RelationId { get; set; }
    }
}