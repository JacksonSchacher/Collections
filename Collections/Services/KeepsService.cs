using System.Collections.Generic;
using Collections.Models;
using Collections.Repositories;

namespace Collections.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _kr;

    public KeepsService(KeepsRepository kr)
    {
      _kr = kr;
    }

    internal List<Keep> Get()
    {
      return _kr.Get();
    }
  }
}