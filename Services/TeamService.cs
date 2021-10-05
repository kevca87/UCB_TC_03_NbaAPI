using NbaAPI.Data.Repository;
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
        private INbaRepository _nbaRepository;
        private HashSet<string> _allowedSortValues = new HashSet<string> { "id", "name", "abb" };

        public TeamService(INbaRepository nbaRepository)
        {
            _nbaRepository = nbaRepository;
        }
        public IEnumerable<TeamModel> GetTeams(string orderBy)
        {
            if (!_allowedSortValues.Contains(orderBy.ToLower()))
                throw new InvalidElementOperationException($"Invalid orderBy value : {orderBy}. The allowed values for param are: {string.Join(',', _allowedSortValues)}");
            var teams = _nbaRepository.GetTeams(orderBy);
            return teams;
        }
        public TeamModel GetTeam(int teamId)
        {
            var team = _nbaRepository.GetTeam(teamId);
            if (team == null)
                throw new NotFoundElementException($"The team with id: {teamId} does not exists.");
            return team;
        }

        public TeamModel CreateTeam(TeamModel team)
        {
            var createdTeam = _nbaRepository.CreateTeam(team);
            return createdTeam;
        }

        public TeamModel UpdateTeam(int teamId, TeamModel team)
        {
            ExistTeam(teamId);
            var updatedTeam = _nbaRepository.UpdateTeam(teamId,team);
            return updatedTeam;
        }

        public void DeleteTeam(int teamId)
        {
            ExistTeam(teamId);
            _nbaRepository.DeleteTeam(teamId);
        }

        public void ExistTeam(int teamId)
        {
            var team = _nbaRepository.GetTeam(teamId);
            if (team == null)
                throw new NotFoundElementException($"the team with id: {teamId} does not exists.");
        }
    }
}
