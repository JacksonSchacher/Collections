using System.Collections.Generic;
using Collections.Models;
using Collections.Services;
using Microsoft.AspNetCore.Mvc;

namespace Collections.Controllers
{
  [ApiController]
  [Route("[controller]")]

  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;

    public VaultsController(VaultsService vs)
    {
      _vs = vs;
    }

    [HttpGet]
    public ActionResult<List<Vault>> Get()
    {
      try
      {
        return Ok(_vs.Get());
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet]
    public ActionResult<List<Vault>> Get(int VaultId)
    {
      try
      {
        return Ok(_vs.Get(VaultId));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}