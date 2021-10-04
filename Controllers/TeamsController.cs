using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NbaAPI.Exceptions;
using NbaAPI.Models;
using NbaAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NbaAPI.Controllers
{
    [Route("api/v1/teams")]
    public class TeamsController : Controller
    {
        private ITeamService _teamService;
        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<TeamModel>> GetTeams()
        {
            var teams = _teamService.GetTeams();
            return Ok(teams);
        }

        [HttpGet("{id:int}")]
        public ActionResult<TeamModel> GetTeam(int id)
        {
            ActionResult<TeamModel> response;
            try
            {
                var team = _teamService.GetTeam(id);
                response = Ok(team);
            }
            catch (NotFoundElementException ex)
            {
                response = BadRequest(ex.Message);
            }
            catch (Exception)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError, "General exception: Something happened");
            }
            return response;
        }

        [HttpPost]
        public ActionResult<TeamModel> PostTeam([FromBody] TeamModel team)
        {
            ActionResult<TeamModel> response;
            try
            {
                if (!ModelState.IsValid)
                    response = BadRequest(ModelState);
                else
                {
                    var newTeam = _teamService.CreateTeam(team);
                    response = Created($"/api/v1/teams/{newTeam.Id}", newTeam);
                }
            }
            catch (NotFoundElementException ex)
            {
                response = BadRequest(ex.Message);
            }
            catch (Exception)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError, "General exception: Something happened");
            }
            return response;
        }

        [HttpPut("{teamId:int}")]
        public ActionResult<TeamModel> PostTeam(int teamId,[FromBody] TeamModel team)
        {
            ActionResult<TeamModel> response;
            try
            {
                if (!ModelState.IsValid)
                    response = BadRequest(ModelState);
                else
                {
                    var updatedTeam = _teamService.UpdateTeam(teamId, team);
                    response = Created($"/api/v1/teams/{updatedTeam.Id}", updatedTeam);
                }
            }
            catch (NotFoundElementException ex)
            {
                response = BadRequest(ex.Message);
            }
            catch (Exception)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError, "General exception: Something happened");
            }
            return response;
        }

        [HttpDelete("{teamId:int}")]
        public ActionResult<TeamModel> DeleteTeam(int teamId)
        {
            ActionResult<TeamModel> response;
            try
            {
                _teamService.DeleteTeam(teamId);
                response = Ok();
            }
            catch (NotFoundElementException ex)
            {
                response = BadRequest(ex.Message);
            }
            catch (Exception)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError, "General exception: Something happened");
            }
            return response;
        }
    }
}
