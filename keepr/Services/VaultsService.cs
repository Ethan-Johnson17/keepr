using System;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;

    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }

    internal Vault GetById(int id)
    {
      Vault vault = _repo.GetById(id);
      if (vault == null)
      {
        throw new Exception("Nope, invalid Id");
      }
      return vault;
    }

    internal Vault CreateVault(Vault newVault)
    {
      return _repo.CreateVault(newVault);
    }
  }
}