using System;
using System.Collections.Generic;
using Collections.Models;
using Collections.Repositories;

namespace Collections.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _pr;

    public ProfilesService(ProfilesRepository pr)
    {
      _pr = pr;
    }

    internal Profile Get(string profileId)
    {
      Profile foundProfile = _pr.Get(profileId);
      if (foundProfile == null)
      {
        throw new Exception("Profile Not Found");
      }
      return foundProfile;
    }

    internal List<Keep> GetKeeps(string profileId)
    {
      return _pr.GetKeeps(profileId);
    }

    internal List<Vault> GetVaults(string profileId)
    {
      return _pr.GetVaults(profileId);
    }
  }
}