using System.Collections.Generic;
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

  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;
    private readonly VaultKeepsService _vks;

    public VaultsController(VaultsService vs, VaultKeepsService vks)
    {
      _vs = vs;
      _vks = vks;
    }

    [HttpGet]
    async public Task<ActionResult<List<Vault>>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_vs.Get());
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{vaultId}")]
    async public Task<ActionResult<Vault>> Get(int vaultId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_vs.GetOne(vaultId, userInfo?.Id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    
    
    [HttpGet("{vaultId}/keeps")]
    async public Task<ActionResult<List<KeepModelView>>> GetVKM(int vaultId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
           return Ok(_vks.GetVKM(vaultId, userInfo?.Id));
      }
      catch (System.Exception error)
      {
          return BadRequest(error.Message);
      }
    }

    [HttpGet("{vaultId}/vaultKeeps")]
    async public Task<ActionResult<List<VaultKeep>>> GetVKs(int vaultId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_vs.GetVKs(vaultId, userInfo?.Id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault vaultData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vaultData.CreatorId = userInfo.Id;
        Vault createdVault = _vs.Create(vaultData);
        createdVault.Creator = userInfo;
        return createdVault;
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [Authorize]
    [HttpPut("{vaultId}")]
    public async Task<ActionResult<Vault>> Edit([FromBody] Vault vaultData, int vaultId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vaultData.CreatorId = userInfo.Id;
        vaultData.Id = vaultId;
        Vault editedVault = _vs.Edit(vaultData, userInfo.Id);
        return editedVault;
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [Authorize]
    [HttpDelete("{vaultId}")]
    public async Task<ActionResult<Vault>> Remove(int vaultId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _vs.Remove(vaultId, userInfo.Id);
        return Ok("Vault Removed");
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}