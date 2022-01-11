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

    internal Vault GetById(int id, string userId)
    {
      Vault vault = _repo.GetById(id);
      if (vault == null)
      {
        throw new Exception("Nope, invalid Id");
      }
      if (vault.isPrivate == true & vault.creatorId != userId)
      {
        throw new Exception("This is a private vault");
      }
      return vault;
    }

    internal Vault CreateVault(Vault newVault)
    {
      return _repo.CreateVault(newVault);
    }

    internal Vault Edit(Vault update, string userId)
    {
      Vault og = GetById(update.id, userId);
      og.name = update.name != null ? update.name : og.name;
      og.description = update.description != null ? update.description : og.description;
      og.img = update.img != null ? update.img : og.img;
      og.isPrivate = update.isPrivate != null ? update.isPrivate : og.isPrivate;
      _repo.Edit(og);
      return og;
    }

    internal void Delete(int id, string creatorId)
    {
      Vault toDelete = GetById(id, creatorId);
      if (toDelete.creatorId != creatorId)
      {
        throw new Exception("Try deleting your own vault for a change");
      }
      _repo.Delete(id);
    }
  }
}