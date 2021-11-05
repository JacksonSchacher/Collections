using System;
using System.Collections.Generic;
using Collections.Models;
using Collections.Repositories;

namespace Collections.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vkr;
    private readonly VaultsRepository _vr;
    private readonly KeepsRepository _kr;

    public VaultKeepsService(VaultKeepsRepository vkr, VaultsRepository vr, KeepsRepository kr)
    {
      _vkr = vkr;
      _vr = vr;
      _kr = kr;
    }

    internal VaultKeep Create(VaultKeep vaultKeepData)
    {
      VaultKeep createdVaultKeep = _vkr.Create(vaultKeepData);
      var foundVault = _vr.Get(vaultKeepData.VaultId);
      var foundKeep = _kr.Get(vaultKeepData.KeepId);
      if (vaultKeepData.CreatorId != foundVault.CreatorId)
      {
        throw new Exception("Cannot Add Post");
      }
      createdVaultKeep.Keep = foundKeep;
      createdVaultKeep.Vault = foundVault;
      return createdVaultKeep;
      
    }
    internal List<KeepModelView> GetVKM(int vaultKeepId)
    {
      return _vkr.GetVKM(vaultKeepId);
    }
    internal VaultKeep Get(int vaultkeepId, string userId)
    {
      VaultKeep foundVK = _vkr.Get(vaultkeepId);
      List<Keep> foundKeeps = _kr.Get();
      Keep foundKeep = foundKeeps.Find(k => k.Id == foundVK.KeepId);
      if (foundVK == null)
      {
        throw new Exception("Vault Keep Does Not Exist");
      }
      if (foundVK.CreatorId != userId)
      {
        if (foundVK.Vault.IsPrivate)
        {
          throw new Exception("Vault is Private");
        }
      }
      foundVK.Keep = foundKeep;
      return foundVK;
    }
    internal void Remove(int vaultkeepId, string userId)
    {
      VaultKeep foundVK = Get(vaultkeepId, userId);

      if (foundVK.CreatorId != userId)
      {
        throw new Exception("Unathorized To Delete");
      }
      _vkr.Remove(vaultkeepId);
    }
  }
}