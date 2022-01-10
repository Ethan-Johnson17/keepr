using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _repo;

    public ProfilesService(ProfilesRepository repo)
    {
      _repo = repo;
    }

    internal object GetById(string id)
    {
      Profile prof = _repo.GetById(id);
      if (prof == null)
      {
        throw new Exception("Nope, invalid Id");
      }
      return prof;
    }

    internal List<Keep> GetProfilesKeeps(string id)
    {
      GetById(id);
      return _repo.GetProfilesKeeps(id);
    }

    internal List<Vault> GetProfileVaults(string id)
    {
      GetById(id);
      return _repo.GetProfileVaults(id);
    }
  }
}