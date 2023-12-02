using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace DataAccess.Context
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<HomeTeam> HomeTeams { get; set; }
        public DbSet<AwayTeam> AwayTeams { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Minute> Minutes { get; set; }
        public DbSet<Log> Logs { get; set; }

    }
}
