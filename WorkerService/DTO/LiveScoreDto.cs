using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService.DTO
{
    public class LiveScoreDto
    {
        public HomeTeam homeTeam { get; set; }
        public AwayTeam awayTeam { get; set; }
        public Status status { get; set; }
        public Times times { get; set; }
        public string date { get; set; }
        public int id { get; set; }
        public Tournament tournament { get; set; }
        public Stage stage { get; set; }
        public Round round { get; set; }
    }
    public class AwayTeam
    {
        public Score score { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string mediumName { get; set; }
        public int id { get; set; }
    }

    public class HomeTeam
    {
        public Score score { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string mediumName { get; set; }
        public int id { get; set; }
    }

    public class Round
    {
        public string name { get; set; }
        public string shortName { get; set; }
        public int id { get; set; }
    }

    public class Score
    {
        public int current { get; set; }
        public int? Regular { get; set; }
        public int? HalfTime { get; set; }
    }

    public class Stage
    {
        public string name { get; set; }
        public string shortName { get; set; }
        public int id { get; set; }
    }

    public class Status
    {
        public string name { get; set; }
        public string shortName { get; set; }
        public int id { get; set; }
    }

    public class Times
    {
        public int currentMinute { get; set; }
    }

    public class Tournament
    {
        public string name { get; set; }
        public string shortName { get; set; }
        public int id { get; set; }
    }
}
