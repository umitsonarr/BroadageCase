using DataAccess.Context;
using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;
using System.Text.Json.Serialization;
using WorkerService.DTO;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<DataContext>();
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                try
                {
                    string date = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-US"));
                    string url = configuration["ScoreApiInfo:Url"];
                    string token = configuration["ScoreApiInfo:Token"];
                    var client = new HttpClient();
                    var data = $"{url}?date={date}";
                    var request = new HttpRequestMessage(HttpMethod.Get, data);
                    request.Headers.Add("Ocp-Apim-Subscription-Key", token);
                    request.Headers.Add("languageId", "2");
                    var response = await client.SendAsync(request);


                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Servis sonucu : {response.StatusCode}");
                    }


                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();

                    var listResult = JsonConvert.DeserializeObject<List<LiveScoreDto>>(result);
                    var dbscores = db.Scores
                                       .Include(x => x.Minute)
                                       .Include(x => x.Status)
                                       .ToList();


                    foreach (var x in listResult)
                    {
                        var control = dbscores.Where(y => y.RelationId == x.id).OrderByDescending(x => x.Id).FirstOrDefault();

                        if (control != null && control.Status.ShortName == "FT")
                            continue;
                        if (control != null && control.Minute.CurrentMinute == x.times?.currentMinute)
                            continue;

                        if (control == null)
                        {
                            var dt = new DataAccess.Entity.Score
                            {
                                Date = x.date,
                                RelationId = x.id,
                                Status = new DataAccess.Entity.Status
                                {
                                    Name = x.status.name,
                                    ShortName = x.status.shortName
                                },
                                Stage = new DataAccess.Entity.Stage
                                {
                                    Name = x.stage.name,
                                    ShortName = x.stage.name
                                },
                                Tournament = new DataAccess.Entity.Tournament
                                {
                                    Name = x.tournament.name,
                                    ShortName = x.tournament.shortName
                                },
                                Round = new DataAccess.Entity.Round
                                {
                                    Name = x.round.name,
                                    ShortName = x.round.shortName
                                },
                                Minute = new Minute
                                {
                                    CurrentMinute = x.times?.currentMinute
                                },
                                AwayTeamGoal = new AwayTeamGoal
                                {
                                    AwayTeam = new DataAccess.Entity.AwayTeam
                                    {
                                        Name = x.awayTeam.name,
                                        MediumName = x.awayTeam.mediumName,
                                        ShortName = x.awayTeam.shortName,

                                    },
                                    Goal = new Goal
                                    {
                                        Current = x.awayTeam.score?.current,
                                        Regular = x.awayTeam.score?.Regular,
                                        HalfTime = x.awayTeam?.score?.HalfTime,
                                    }
                                },
                                HomeTeamGoal = new HomeTeamGoal
                                {
                                    HomeTeam = new DataAccess.Entity.HomeTeam
                                    {
                                        Name = x.homeTeam.name,
                                        MediumName = x.homeTeam.mediumName,
                                        ShortName = x.homeTeam.shortName,

                                    },
                                    Goal = new Goal
                                    {
                                        Current = x.homeTeam.score?.current,
                                        Regular = x.homeTeam.score?.Regular,
                                        HalfTime = x.homeTeam?.score?.HalfTime,
                                    }
                                }
                            };

                            await db.Scores.AddRangeAsync(dt);
                            int dbInsertResult = await db.SaveChangesAsync();

                            if (!(dbInsertResult > 0))
                            {
                                throw new Exception($"Kayýt insert edilirken hata oluþtu");
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    var log = new Log
                    {
                        Status = LogStatus.Error,
                        ErrorMessage = JsonConvert.SerializeObject(ex)
                    };

                    await db.Logs.AddAsync(log);
                    await db.SaveChangesAsync();
                    //log db insert et
                }


                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(60 * 1000, stoppingToken);

            }
        }
    }
}