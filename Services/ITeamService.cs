using NbaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NbaAPI.Services
{
    public interface ITeamService
    {
        IEnumerable<TeamModel> GetTeams(string orderBy);
        TeamModel GetTeam(int teamId);
        TeamModel CreateTeam(TeamModel team);
        TeamModel UpdateTeam(int teamId,TeamModel team);
        void DeleteTeam(int teamId);
        public TeamModel MoveOnTeam(int teamId, string newCity);
        public IEnumerable<TeamModel> RecordUpdate(int teamId, int rivalId);
    }
}
