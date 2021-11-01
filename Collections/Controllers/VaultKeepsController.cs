using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Collections.Models;
using Collections.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Collections.Controllers
{
  [ApiController]
  [Route("api/[controller]")]

  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vks;

    public VaultKeepsController(VaultKeepsService vks)
    {
      _vks = vks;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vaultKeepData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vaultKeepData.CreatorId = userInfo.Id;
        VaultKeep createdVault = _vks.Create(vaultKeepData);
        return createdVault;
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [Authorize]
    [HttpDelete("{vaultkeepId}")]
    public async Task<ActionResult> Remove(int vaultkeepId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _vks.Remove(vaultkeepId, userInfo.Id);
        return Ok("VaultKeep Deleted");
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}