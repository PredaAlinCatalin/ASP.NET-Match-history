using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;


namespace DAWProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Attribute> Attributes { get; set; }
        public ICollection<Division> Divisions { get; set; }
        public ICollection<Game> Games { get; set; }
        public ICollection<GameResult> GameResults { get; set; }
        public ICollection<Player> Players { get; set; }
        public ICollection<PlayerStat> PlayersStat { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            //Database.SetInitializer<ApplicationDbContext>(new Initp());
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameResult> GamesResults { get; set; }
        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerStat> PlayersStat { get; set; }
        public DbSet<Division> Divisions { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class Initp : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext ctx)
        {
            var attributes = new Attribute[]
           {
                new Attribute{AttributeId = 1, Name="Ranked"},
                new Attribute{AttributeId = 2, Name="Recorded"},
                new Attribute{AttributeId = 3, Name="Summoners Rift"},
                new Attribute{AttributeId = 4, Name="5v5"},
           };
            ctx.Attributes.AddRange(attributes);


            var games = new Game[]
            {
                new Game
                {
                    GameId = 1,
                    Duration = 31,
                    Day = "01",
                    Month = "01",
                    Year = 2021,
                    Attributes = new List<Attribute>
                    {
                        attributes[0],
                        attributes[1],
                        attributes[2]
                    }
                },

                new Game
                {
                    GameId = 2,
                    Duration = 25,
                    Day = "02",
                    Month = "01",
                    Year = 2021,
                    Attributes = new List<Attribute>
                    {
                        attributes[1],
                        attributes[2],
                        attributes[3]
                    }
                },

                new Game
                {
                    GameId = 3,
                    Duration = 29,
                    Day = "04",
                    Month = "01",
                    Year = 2021,
                    Attributes = new List<Attribute>
                    {
                        attributes[0],
                        attributes[2],
                        attributes[3]
                    }
                }
            };
            ctx.Games.AddRange(games);

            var divisions = new Division[]
            {
                new Division{DivisionId = 1, Name = "Iron 4"},
                new Division{DivisionId = 2, Name = "Iron 3"},
                new Division{DivisionId = 3, Name = "Iron 2"},
                new Division{DivisionId = 4, Name = "Iron 1"},
                new Division{DivisionId = 5, Name = "Bronze 4"},
                new Division{DivisionId = 6, Name = "Bronze 3"},
                new Division{DivisionId = 7, Name = "Bronze 2"},
                new Division{DivisionId = 8, Name = "Bronze 1"},
                new Division{DivisionId = 9, Name = "Silver 4"},
                new Division{DivisionId = 10, Name = "Silver 3"},
                new Division{DivisionId = 11, Name = "Silver 2"},
                new Division{DivisionId = 12, Name = "Silver 1"},
                new Division{DivisionId = 13, Name = "Gold 4"},
                new Division{DivisionId = 14, Name = "Gold 3"},
                new Division{DivisionId = 15, Name = "Gold 2"},
                new Division{DivisionId = 16, Name = "Gold 1"},
                new Division{DivisionId = 17, Name = "Platinum 4"},
                new Division{DivisionId = 18, Name = "Platinum 3"},
                new Division{DivisionId = 19, Name = "Platinum 2"},
                new Division{DivisionId = 20, Name = "Platinum 1"},
                new Division{DivisionId = 21, Name = "Diamond 4"},
                new Division{DivisionId = 22, Name = "Diamond 3"},
                new Division{DivisionId = 23, Name = "Diamond 2"},
                new Division{DivisionId = 24, Name = "Diamond 1"},
                new Division{DivisionId = 18, Name = "Master"},
                new Division{DivisionId = 18, Name = "Grandmaster"},
                new Division{DivisionId = 18, Name = "Challenger"},
            };
            ctx.Divisions.AddRange(divisions);

            var players = new Player[]
            {
                new Player{PlayerId=1, Name="Nume1", Division=divisions[0]},
                new Player{PlayerId=2, Name="Nume2", Division=divisions[1]},
                new Player{PlayerId=3, Name="Nume3", Division=divisions[2]},
                new Player{PlayerId=4, Name="Nume4", Division=divisions[3]},
                new Player{PlayerId=5, Name="Nume5", Division=divisions[4]},
                new Player{PlayerId=6, Name="Nume6", Division=divisions[5]},
                new Player{PlayerId=7, Name="Nume7", Division=divisions[6]},
                new Player{PlayerId=8, Name="Nume8", Division=divisions[7]},
                new Player{PlayerId=9, Name="Nume9", Division=divisions[8]},
                new Player{PlayerId=10, Name="Nume10", Division=divisions[9]},
                new Player{PlayerId=11, Name="Nume11", Division=divisions[10]},
                new Player{PlayerId=12, Name="Nume12", Division=divisions[11]},
                new Player{PlayerId=13, Name="Nume13", Division=divisions[12]},
                new Player{PlayerId=14, Name="Nume14", Division=divisions[13]},
                new Player{PlayerId=15, Name="Nume15", Division=divisions[14]},
            };
            ctx.Players.AddRange(players);

            var playersStat = new PlayerStat[]
            {
                new PlayerStat{PlayerStatId=1, NrWins=50, NrLosses=45, Player=players[0]},
                new PlayerStat{PlayerStatId=2, NrWins=51, NrLosses=45, Player=players[1]},
                new PlayerStat{PlayerStatId=3, NrWins=52, NrLosses=45, Player=players[2]},
                new PlayerStat{PlayerStatId=4, NrWins=53, NrLosses=45, Player=players[3]},
                new PlayerStat{PlayerStatId=5, NrWins=54, NrLosses=45, Player=players[4]},
                new PlayerStat{PlayerStatId=6, NrWins=55, NrLosses=45, Player=players[5]},
                new PlayerStat{PlayerStatId=7, NrWins=56, NrLosses=45, Player=players[6]},
                new PlayerStat{PlayerStatId=8, NrWins=57, NrLosses=45, Player=players[7]},
                new PlayerStat{PlayerStatId=9, NrWins=58, NrLosses=45, Player=players[8]},
                new PlayerStat{PlayerStatId=10, NrWins=59, NrLosses=45, Player=players[9]},
                new PlayerStat{PlayerStatId=11, NrWins=50, NrLosses=45, Player=players[10]},
                new PlayerStat{PlayerStatId=12, NrWins=51, NrLosses=45, Player=players[11]},
                new PlayerStat{PlayerStatId=13, NrWins=52, NrLosses=45, Player=players[12]},
                new PlayerStat{PlayerStatId=14, NrWins=53, NrLosses=45, Player=players[13]},
                new PlayerStat{PlayerStatId=15, NrWins=54, NrLosses=45, Player=players[14]},
            };
            ctx.PlayersStat.AddRange(playersStat);

            var gameResults = new GameResult[]
            {
                new GameResult{NrKills = 10, NrDeaths = 2, NrAssists = 12, DamageDealt = 25000, CreepScore = 270, Game = games[0], Player = players[0]},
                new GameResult{NrKills = 11, NrDeaths = 3, NrAssists = 13, DamageDealt = 25001, CreepScore = 271, Game = games[0], Player = players[1]},
                new GameResult{NrKills = 12, NrDeaths = 4, NrAssists = 14, DamageDealt = 25002, CreepScore = 272, Game = games[0], Player = players[2]},
                new GameResult{NrKills = 13, NrDeaths = 5, NrAssists = 15, DamageDealt = 25003, CreepScore = 273, Game = games[0], Player = players[3]},
                new GameResult{NrKills = 14, NrDeaths = 6, NrAssists = 16, DamageDealt = 25004, CreepScore = 274, Game = games[0], Player = players[4]},
                new GameResult{NrKills = 15, NrDeaths = 7, NrAssists = 17, DamageDealt = 25005, CreepScore = 275, Game = games[0], Player = players[5]},
                new GameResult{NrKills = 16, NrDeaths = 8, NrAssists = 18, DamageDealt = 25006, CreepScore = 270, Game = games[0], Player = players[6]},
                new GameResult{NrKills = 17, NrDeaths = 9, NrAssists = 19, DamageDealt = 25007, CreepScore = 271, Game = games[0], Player = players[7]},
                new GameResult{NrKills = 18, NrDeaths = 10, NrAssists = 12, DamageDealt = 25008, CreepScore = 272, Game = games[0], Player = players[8]},
                new GameResult{NrKills = 19, NrDeaths = 11, NrAssists = 13, DamageDealt = 25009, CreepScore = 273, Game = games[0], Player = players[9]},
                new GameResult{NrKills = 10, NrDeaths = 2, NrAssists = 14, DamageDealt = 25000, CreepScore = 274, Game = games[1], Player = players[10]},
                new GameResult{NrKills = 10, NrDeaths = 2, NrAssists = 15, DamageDealt = 25001, CreepScore = 275, Game = games[1], Player = players[11]},
                new GameResult{NrKills = 10, NrDeaths = 2, NrAssists = 16, DamageDealt = 25002, CreepScore = 276, Game = games[1], Player = players[12]},
                new GameResult{NrKills = 10, NrDeaths = 2, NrAssists = 17, DamageDealt = 25003, CreepScore = 277, Game = games[1], Player = players[13]},
                new GameResult{NrKills = 10, NrDeaths = 2, NrAssists = 18, DamageDealt = 25004, CreepScore = 278, Game = games[1], Player = players[14]},
                new GameResult{NrKills = 10, NrDeaths = 2, NrAssists = 19, DamageDealt = 25005, CreepScore = 279, Game = games[1], Player = players[0]},
                new GameResult{NrKills = 10, NrDeaths = 2, NrAssists = 20, DamageDealt = 25006, CreepScore = 270, Game = games[1], Player = players[1]},
                new GameResult{NrKills = 10, NrDeaths = 2, NrAssists = 21, DamageDealt = 25007, CreepScore = 271, Game = games[1], Player = players[2]},
                new GameResult{NrKills = 10, NrDeaths = 2, NrAssists = 22, DamageDealt = 25008, CreepScore = 272, Game = games[1], Player = players[3]},
                new GameResult{NrKills = 10, NrDeaths = 2, NrAssists = 23, DamageDealt = 25009, CreepScore = 273, Game = games[1], Player = players[4]},
            };
            ctx.GamesResults.AddRange(gameResults);

            ctx.SaveChanges();
            base.Seed(ctx);
        }
    }
}