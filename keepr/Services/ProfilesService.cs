using System;
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
  }
}