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
  }
}