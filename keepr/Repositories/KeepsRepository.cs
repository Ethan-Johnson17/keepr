using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Keep> GetAll()
    {
      string sql = @"
      SELECT
       k.*,
       a.* 
       FROM keeps k
       JOIN accounts a ON k.creatorId = a.id
      ;";
      return _db.Query<Keep, Account, Keep>(sql, (keep, acct) =>
      {
        keep.Creator = acct;
        return keep;
      }).ToList();
    }

    internal Keep GetById(int id)
    {
      string sql = @"
      SELECT
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id
      WHERE k.id = @id;";
      return _db.Query<Keep, Account, Keep>(sql, (keep, acct) =>
      {
        keep.Creator = acct;
        return keep;
      }, new { id }).FirstOrDefault();
    }

    internal Keep CreateKeep(Keep newKeep)
    {
      string sql = @"
      INSERT INTO keeps
      (creatorId, name, description, img, views, shares, keeps)
      VALUES
      (@creatorId, @name, @description, @img, @views, @shares, @keeps);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newKeep);
      newKeep.id = id;
      return newKeep;
    }
    internal void Edit(Keep og)
    {
      string sql = @"
      UPDATE keeps
      SET
      name = @name,
      description = @description,
      img = @img,
      views = @views,
      shares = @shares,
      keeps = @keeps
      WHERE id = id;";
      _db.Execute(sql, og);
    }

    internal void Delete(int id)
    {
      string sql = @"DELETE FROM keeps WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }

  }
}