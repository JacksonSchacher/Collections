using System;
using System.Collections.Generic;
using Collections.Models;
using Collections.Repositories;

namespace Collections.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vr;

    public VaultsService(VaultsRepository vr)
    {
      _vr = vr;
    }

    internal List<Vault> Get()
    {
      return _vr.Get();
    }

    internal Vault Get(int vaultId)
    {
      Vault foundVault = _vr.Get(vaultId);
      if (foundVault == null)
      {
        throw new Exception("Vault Not Found");
      }
      return foundVault;
    }
  }
}