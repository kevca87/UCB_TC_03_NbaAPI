using NbaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NbaAPI.Data.Repository
{
    public class NbaRepository : INbaRepository
    {
        private IList<TeamModel> _teams = new List<TeamModel>();
        //private IList<PlayerModel> _players = new List<PlayerModel>();
        public NbaRepository()
        {
            _teams.Add(new TeamModel()
            {
                Id = 1,
                Abb = "GSW",
                Arena = "Chase Center",
                City = "Golden State",
                Name = "Warriors",
                CoachName = "Steve Kerr",
                Founded = new DateTime(1946, 1, 1)
            });
            _teams.Add(new TeamModel()
            {
                Id = 2,
                Abb = "BOS",
                Arena = "TD Garden",
                City = "Boston",
                Name = "Celtics",
                CoachName = "Ime Udoka",
                Founded = new DateTime(1946, 1, 1)
            });
        }
        public TeamModel CreateTeam(TeamModel team)
        {
            var lastTeam = _teams.OrderByDescending(r => r.Id).FirstOrDefault();
            int nextId = lastTeam != null ? lastTeam.Id + 1 : 1;
            team.Id = nextId;
            _teams.Add(team);
            return team;
        }

        public void DeleteTeam(int teamId)
        {
            var teamToDelete = GetTeam(teamId);
            _teams.Remove(teamToDelete);
        }

        public TeamModel GetTeam(int teamId)
        {
            var team = _teams.FirstOrDefault(t => t.Id == teamId);
            return team;
        }

        public IEnumerable<TeamModel> GetTeams(string orderBy)
        {
            Dictionary<string, Func<TeamModel, object>> orders = new Dictionary<string, Func<TeamModel, object>>();
            orders.Add("id", t => t.Id);
            orders.Add("name", t => t.Name);
            orders.Add("abb", t => t.Abb);
            var teams = _teams.OrderBy(orders[orderBy.ToLower()]);
            return teams;
        }

        public TeamModel UpdateTeam(int teamId, TeamModel team)
        {
            var teamToUpdate = GetTeam(teamId);
            teamToUpdate.Name = team.Name ?? teamToUpdate.Name;
            teamToUpdate.City = team.City ?? teamToUpdate.City;
            teamToUpdate.Abb = team.Abb ?? teamToUpdate.Abb;
            teamToUpdate.Arena = team.Arena ?? teamToUpdate.Arena;
            teamToUpdate.Founded = team.Founded ?? teamToUpdate.Founded;
            teamToUpdate.CoachName = team.CoachName ?? teamToUpdate.CoachName;
            return teamToUpdate;
        }
    }
}
