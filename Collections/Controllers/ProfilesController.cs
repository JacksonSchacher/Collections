using System.Collections.Generic;
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
    public ActionResult<List<Vault>> GetVaults(string profileId)
    {
      try
      {
        return Ok(_ps.GetVaults(profileId));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}