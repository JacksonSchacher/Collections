using System;
using Collections.Models;
using Collections.Repositories;

namespace Collections.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vkr;

    public VaultKeepsService(VaultKeepsRepository vkr)
    {
      _vkr = vkr;
    }

    internal VaultKeep Create(VaultKeep vaultKeepData)
    {
      return _vkr.Create(vaultKeepData);
    }

    internal void Remove(int vaultkeepId, string id)
    {
      VaultKeep foundVK = _vkr.Get(vaultkeepId);
      if (foundVK == null)
      {
        throw new Exception("Vault Keep Does Not Exist");
      }
      if (foundVK.CreatorId != id)
      {
        throw new Exception("Unathorized To Delete");
      }
      _vkr.Remove(vaultkeepId);
    }
  }
}