using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Profile GetById(string id)
    {
      string sql = @"
      SELECT * FROM accounts WHERE id = @id
      ";
      return _db.QueryFirstOrDefault<Profile>(sql, new { id });
    }

    internal List<Keep> GetProfilesKeeps(string id)
    {
      string sql = @"
      SELECT
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id
      WHERE k.creatorId = @id
      ";
      return _db.Query<Keep, Account, Keep>(sql, (keep, acct) =>
      {
        keep.Creator = acct;
        return keep;
      }, new { id }).ToList();
    }
    internal List<Vault> GetProfileVaults(string id)
    {
      string sql = @"
      SELECT
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON v.creatorId = a.id
      WHERE v.creatorId = @id AND v.isPrivate != 1;
      ";
      return _db.Query<Vault, Account, Vault>(sql, (vault, acct) =>
      {
        vault.Creator = acct;
        return vault;
      }, new { id }).ToList();
    }
  }
}