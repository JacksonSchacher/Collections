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

    internal Vault GetOne(int vaultId, string userId)
    {
      Vault foundVault = _vr.Get(vaultId);
      if (foundVault == null)
      {
        throw new Exception("Vault Not Found");
      }
      if (foundVault.IsPrivate && foundVault.CreatorId != userId) {
        throw new Exception("Vault is Private");
      }
      return foundVault;
    }

    internal Vault Create(Vault vaultData)
    {
      return _vr.Create(vaultData);
    }

    internal Vault Edit(Vault vaultData, string userId)
    {
      Vault foundVault = GetOne(vaultData.Id, userId);
      if (foundVault.CreatorId != userId)
      {
        throw new Exception("Not Allowed To Edit");
      }
      return _vr.Edit(vaultData);
    }

    internal void Remove(int vaultId, string userId)
    {
      Vault foundVault = GetOne(vaultId, userId);
      if (foundVault.CreatorId != userId)
      {
        throw new Exception("Not Authorized to Remove");
      }
      _vr.Remove(vaultId);
    }

    internal List<VaultKeep> GetVKs(int vaultId, string userId)
    {
      List<VaultKeep> foundVKs = _vr.GetVKs(vaultId);
      List<VaultKeep> nonPrivateVks = foundVKs.FindAll(vk => vk.Vault.IsPrivate != true);
      if (foundVKs.Count == 0) 
      {
        return foundVKs;
      }
      if (userId == foundVKs[0].CreatorId) {
        return foundVKs;
      }
      if (nonPrivateVks.Count == 0)
      {
        throw new Exception("Unauthorized");
      }
      return nonPrivateVks;
    }
  }
}