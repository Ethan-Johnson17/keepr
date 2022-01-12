using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    // internal List<VaultKeepViewModel> GetByVault(int id)
    // {
    //   string sql = @"
    //   SELECT
    //   vk.*,
    //   a.*
    //   FROM vaultKeeps vk
    //   JOIN accounts a ON vk.creatorId = a.id
    //   WHERE vk.vaultId = @id;
    //   ";
    //   return _db.Query<VaultKeepViewModel, Account, VaultKeepViewModel>(sql, (vk, acct) =>
    //   {
    //     vk.Creator = acct;
    //     return vk;
    //   }, new { id }).ToList();
    // }

    internal VaultKeepViewModel GetById(int id)
    {
      string sql = "SELECT * FROM vaultKeeps WHERE id = @id;";
      return _db.QueryFirstOrDefault<VaultKeepViewModel>(sql, new { id });
    }

    internal VaultKeep CreateVaultKeep(VaultKeep newVk)
    {
      string sql = @"
      INSERT INTO vaultKeeps
      (creatorId, vaultId, keepId)
      VALUES
      (@creatorId, @vaultId, @keepId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, newVk);
      newVk.id = id;
      return newVk;
    }

    internal List<VaultKeepViewModel> GetKeepsByVault(int id)
    {
      string sql = @"
      SELECT
      k.*,
      vk.id AS vaultKeepId,
      @id AS vaultId,
      a.*
      FROM vaultKeeps vk
      JOIN accounts a ON vk.creatorId = a.id
      JOIN keeps k ON vk.keepId = k.id
      WHERE vk.vaultId = @id;
      ";
      return _db.Query<VaultKeepViewModel, Account, VaultKeepViewModel>(sql, (keep, acct) =>
      {
        keep.Creator = acct;
        return keep;
      }, new { id }).ToList();
    }

    internal void Delete(int id)
    {
      string sql = @"DELETE FROM vaultKeeps WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }
  }
}