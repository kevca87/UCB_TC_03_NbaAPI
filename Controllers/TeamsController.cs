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
        public ActionResult<IEnumerable<TeamModel>> GetTeams(string orderBy = "id")
        {
            ActionResult<IEnumerable<TeamModel>> response;
            try
            {
                var teams = _teamService.GetTeams(orderBy);
                response = Ok(teams);
            }
            catch (InvalidElementOperationException ex)
            {
                response = BadRequest(ex.Message);
            }
            catch (Exception)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError, "General exception: Something happened");
            }
            return response;
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
        public ActionResult<TeamModel> UpdateTeam(int teamId,[FromBody] TeamModel team)
        {
            ActionResult<TeamModel> response;
            try
            {
                bool ExistInvalidFields = (team.Name != null && ModelState.ContainsKey("name") && ModelState["name"].Errors.Count > 0)
                                       || (team.City != null && ModelState.ContainsKey("city") && ModelState["city"].Errors.Count > 0)
                                       || (team.Abb != null && ModelState.ContainsKey("abb") && ModelState["abb"].Errors.Count > 0);
                if (!ModelState.IsValid && ExistInvalidFields)
                {
                    response = BadRequest(ModelState);
                }
                else
                {
                    var updatedTeam = _teamService.UpdateTeam(teamId, team);
                    response = Ok(updatedTeam);
                }
            }
            catch (NotFoundElementException ex)
            {
                response = BadRequest(ex.Message);
            }
            catch (InvalidElementOperationException ex)
            {
                response = BadRequest(ex.Message);
            }
            catch (Exception)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError, "General exception: Something happened");
            }
            return response;
        }

        [HttpPut("{teamId:int}/moveon")]
        public ActionResult<TeamModel> MoveOnTeam(int teamId,string newCity)
        {
            ActionResult<TeamModel> response;
            try
            {
                var movedTeam = _teamService.MoveOnTeam(teamId, newCity);
                response = Ok(movedTeam);
            }
            catch (NotFoundElementException ex)
            {
                response = BadRequest(ex.Message);
            }
            catch (InvalidElementOperationException ex)
            {
                response = BadRequest(ex.Message);
            }
            catch (Exception)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError, "General exception: Something happened");
            }
            return response;
        }

        [HttpPut("{teamId:int}/winagainst")]
        public ActionResult<TeamModel> RecordUpdate(int teamId, int rivalId)
        {
            ActionResult<TeamModel> response;
            try
            {
                var updatedTeams = _teamService.RecordUpdate(teamId, rivalId);
                response = Ok(updatedTeams);
            }
            catch (NotFoundElementException ex)
            {
                response = BadRequest(ex.Message);
            }
            catch (InvalidElementOperationException ex)
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
        public ActionResult DeleteTeam(int teamId)
        {
            ActionResult response;
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
