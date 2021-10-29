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

    internal Vault Create(Vault vaultData)
    {
      return _vr.Create(vaultData);
    }

    internal Vault Edit(Vault vaultData, string userId)
    {
      Vault foundVault = Get(vaultData.Id);
      if (foundVault.CreatorId != userId)
      {
        throw new Exception("Not Allowed To Edit");
      }
      return _vr.Edit(vaultData);
    }

    internal void Remove(int vaultId, string userId)
    {
      Vault foundVault = Get(vaultId);
      if (foundVault.CreatorId != userId)
      {
        throw new Exception("Not Authorized to Remove");
      }
      _vr.Remove(vaultId);
    }
  }
}