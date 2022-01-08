using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultsService _vs;
    private readonly VaultKeepsRepository _repo;

    public VaultKeepsService(VaultsService vs, VaultKeepsRepository repo)
    {
      _vs = vs;
      _repo = repo;
    }

    internal List<VaultKeepViewModel> GetByVault(int id)
    {
      _vs.GetById(id);
      return _repo.GetByVault(id);
    }

    internal VaultKeepViewModel CreateVaultKeep(VaultKeepViewModel newVk)
    {
      return _repo.CreateVaultKeep(newVk);
    }
  }
}