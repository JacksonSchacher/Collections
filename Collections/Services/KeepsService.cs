using System;
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

    internal Keep Get(int keepId)
    {
      Keep foundKeep = _kr.Get(keepId);
      if (foundKeep == null)
      {
        throw new Exception("Keep Not Found");
      }
      return foundKeep;
    }

    internal Keep Create(Keep keepData)
    {
      return _kr.Create(keepData);
    }

    internal Keep Edit(Keep keepData, string userId)
    {
      Keep foundKeep = Get(keepData.Id);
      if (foundKeep.CreatorId != userId)
      {
        throw new Exception("Not Allowed To Edit");
      }
      return _kr.Edit(keepData);
    }

    internal void Remove(int keepId, string userId)
    {
      Keep foundKeep = Get(keepId);
      if (foundKeep.CreatorId != userId)
      {
        throw new Exception("Not Authorized to Remove");
      }
      _kr.Remove(keepId);
    }
  }
}