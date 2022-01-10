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
      else if (vault.isPrivate == true)
      {
        throw new Exception("Nope, it's private");
      }
      return vault;
    }

    internal Vault CreateVault(Vault newVault)
    {
      return _repo.CreateVault(newVault);
    }

    internal Vault Edit(Vault update)
    {
      Vault og = GetById(update.id);
      og.name = update.name != null ? update.name : og.name;
      og.description = update.description != null ? update.description : og.description;
      og.isPrivate = update.isPrivate != null ? update.isPrivate : og.isPrivate;
      _repo.Edit(og);
      return og;
    }

    internal void Delete(int id, string creatorId)
    {
      Vault toDelete = GetById(id);
      if (toDelete.creatorId != creatorId)
      {
        throw new Exception("Try deleting your own vault for a change");
      }
      _repo.Delete(id);
    }
  }
}