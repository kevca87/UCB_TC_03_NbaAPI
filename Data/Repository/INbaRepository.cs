using NbaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NbaAPI.Data.Repository
{
    public interface INbaRepository
    {
        //Teams
        IEnumerable<TeamModel> GetTeams(string orderBy);//, bool showPlayers = false);
        TeamModel GetTeam(int teamId);//, bool showPlayers = false);
        TeamModel CreateTeam(TeamModel team);
        TeamModel UpdateTeam(int teamId, TeamModel team);
        void DeleteTeam(int teamId);

    }
}
