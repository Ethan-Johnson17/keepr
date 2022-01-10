using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;

    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }

    internal List<Keep> GetAll()
    {
      return _repo.GetAll();
    }

    internal Keep GetById(int id)
    {
      Keep keep = _repo.GetById(id);
      if (keep == null)
      {
        throw new Exception("Nope, invalid Id");
      }
      return keep;
    }

    internal Keep CreateKeep(Keep newKeep)
    {
      return _repo.CreateKeep(newKeep);
    }

    internal Keep Edit(Keep update)
    {
      Keep og = GetById(update.id);
      if (og.creatorId != update.creatorId)
      {
        throw new Exception("Not allowed");
      }
      og.name = update.name != null ? update.name : og.name;
      og.description = update.description != null ? update.description : og.description;
      og.img = update.img != null ? update.img : og.img;
      og.views = update.views;
      og.shares = update.shares;
      og.keeps = update.keeps;
      _repo.Edit(og);
      return og;
    }

    internal void Delete(int id, string creatorId)
    {
      Keep toDelete = GetById(id);
      if (toDelete.creatorId != creatorId)
      {
        throw new Exception("Try deleting your own keep for a change");
      }
      _repo.Delete(id);
    }
  }
}