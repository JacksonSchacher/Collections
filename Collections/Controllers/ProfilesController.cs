using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Collections.Models;
using Collections.Services;
using Microsoft.AspNetCore.Mvc;

namespace Collections.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ps;

    public ProfilesController(ProfilesService ps)
    {
      _ps = ps;
    }

    [HttpGet("{profileId}")]
    public ActionResult<Profile> Get(string profileId)
    {
      try
      {
        Profile foundProfile = _ps.Get(profileId);
        return Ok(foundProfile);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keep>> GetKeeps(string profileId)
    {
      try
      {
        return Ok(_ps.GetKeeps(profileId));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{profileId}/vaults")]
    async public Task<ActionResult<List<Vault>>> GetVaults(string profileId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_ps.GetVaults(profileId, userInfo?.Id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}