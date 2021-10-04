using NbaAPI.Exceptions;
using NbaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NbaAPI.Services
{
    public class TeamService : ITeamService
    {
        private IList<TeamModel> _teams;
        public TeamService()
        {
            _teams = new List<TeamModel>();
            _teams.Add(new TeamModel()
            {
                Id = 1,
                Abb = "GSW",
                Arena = "Chase Center",
                City = "Golden State",
                Name = "Warriors",
                CoachName = "Steve Kerr",
                Founded = new DateTime(1946,1,1)
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
        public IEnumerable<TeamModel> GetTeams()
        {
            return _teams;
        }
        public TeamModel GetTeam(int teamId)
        {
            var team = _teams.SingleOrDefault(t => t.Id == teamId);
            if (team == null)
                throw new NotFoundElementException($"the team with id: {teamId} does not exists.");
            return team;
        }

        public TeamModel CreateTeam(TeamModel team)
        {
            var lastTeam = _teams.OrderByDescending(t => t.Id).FirstOrDefault();
            int nextId = lastTeam != null ? lastTeam.Id + 1 : 1;
            team.Id = nextId;
            _teams.Add(team);
            return team;
        }

        public TeamModel UpdateTeam(int teamId, TeamModel team)
        {
            var teamToUpdate = _teams.SingleOrDefault(t => t.Id == teamId);
            if (teamToUpdate == null)
                throw new NotFoundElementException($"the team with id: {teamId} does not exists.");
            teamToUpdate.Name = team.Name ?? teamToUpdate.Name;
            teamToUpdate.City = team.City ?? teamToUpdate.City;
            teamToUpdate.Founded = team.Founded ?? teamToUpdate.Founded;
            teamToUpdate.Abb = team.Abb ?? teamToUpdate.Abb;
            teamToUpdate.Arena = team.Arena ?? teamToUpdate.Arena;
            teamToUpdate.CoachName = team.CoachName ?? teamToUpdate.CoachName;
            return teamToUpdate;
        }

        public void DeleteTeam(int teamId)
        {
            var team = _teams.SingleOrDefault(t => t.Id == teamId);
            if (team == null)
                throw new NotFoundElementException($"the team with id: {teamId} does not exists.");
            _teams.Remove(team);
        }
    }
}
