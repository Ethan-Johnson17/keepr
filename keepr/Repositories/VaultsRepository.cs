using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Vault GetById(int id)
    {
      string sql = @"
      SELECT
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON v.creatorId = a.id
      WHERE v.id = @id;";
      return _db.Query<Vault, Account, Vault>(sql, (vault, acct) =>
      {
        vault.Creator = acct;
        return vault;
      }, new { id }).FirstOrDefault();
    }

    internal Vault CreateVault(Vault newVault)
    {
      string sql = @"
      INSERT INTO vaults
      (creatorId, name, description, img, isPrivate)
      VALUES
      (@creatorId, @name, @description, @img, @isPrivate);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newVault);
      newVault.id = id;
      return newVault;
    }

    internal void Edit(Vault og)
    {
      string sql = @"
      UPDATE vaults
      SET
      name = @name,
      description = @description,
      img = @img,
      isPrivate = @isPrivate
      WHERE id = id;";
      _db.Execute(sql, og);
    }

    internal void Delete(int id)
    {
      string sql = @"DELETE FROM vaults WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }
  }
}