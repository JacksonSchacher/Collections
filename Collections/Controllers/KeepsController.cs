using System.Collections.Generic;
using Collections.Models;
using Collections.Services;
using Microsoft.AspNetCore.Mvc;

namespace Collections.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _ks;

    public KeepsController(KeepsService ks)
    {
      _ks = ks;
    }

    [HttpGet]
    public ActionResult<List<Keep>> Get()
    {
      try
      {
        return Ok(_ks.Get());
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message)
      }
    }
  }
}