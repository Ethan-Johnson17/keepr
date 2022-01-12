using System;
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

    // internal List<VaultKeepViewModel> GetByVault(int id, string userId)
    // {
    //   Vault vault = _vs.GetById(id, userId);
    //   return _repo.GetByVault(id);
    // }

    internal VaultKeepViewModel GetById(int id)
    {
      VaultKeepViewModel vault = _repo.GetById(id);
      if (vault == null)
      {
        throw new Exception("Nope, invalid Id");
      }
      return vault;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep newVk, string userId)
    {
      Vault vault = _vs.GetById(newVk.vaultId, userId);
      if (vault.creatorId != newVk.creatorId)
      {
        throw new Exception("Please sign in");
      }
      return _repo.CreateVaultKeep(newVk);
    }

    internal void Delete(int id, string creatorId)
    {
      VaultKeepViewModel toDelete = GetById(id);
      if (toDelete.creatorId != creatorId)
      {
        throw new Exception("Try deleting your own vault for a change");
      }
      _repo.Delete(id);
    }

    public List<VaultKeepViewModel> GetKeepsByVault(int id, string userId)
    {
      Vault vault = _vs.GetById(id, userId);
      return _repo.GetKeepsByVault(id);
    }
  }
}