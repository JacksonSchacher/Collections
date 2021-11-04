using System;
using System.Data;
using System.Linq;
using Collections.Models;
using Dapper;

namespace Collections.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal VaultKeep Create(VaultKeep vaultKeepData)
    {
      string sql = @"
      INSERT INTO vaultKeeps(creatorId, vaultId, keepId)
      VALUES(@CreatorId, @VaultId, @KeepId);
      SELECT LAST_INSERT_ID();
      UPDATE keeps
      SET keeps = keeps + 1
      WHERE keeps.id = @KeepId;
      ";
      int id = _db.ExecuteScalar<int>(sql, vaultKeepData);
      vaultKeepData.Id = id;
      return vaultKeepData;
    }

    internal VaultKeep Get(int vaultkeepId)
    {
      string sql = @"
      SELECT
      vk.*,
      p.*,
      k.*,
      v.*
      FROM vaultKeeps vk
      JOIN accounts p ON p.id = vk.creatorId
      JOIN keeps k ON k.id = vk.keepId
      JOIN vaults v ON v.id = vk.vaultId
      WHERE vk.id = @vaultkeepId
      LIMIT 1;
      ";
      return _db.Query<VaultKeep, Profile, Keep, Vault, VaultKeep>(sql, (vk, p, k, v) => {
        vk.Creator = p;
        vk.Keep = k;
        vk.Vault = v;
        return vk;
      }, new {vaultkeepId}).FirstOrDefault();
    }

    internal void Remove(int vaultkeepId)
    {
      string sql = "DELETE FROM vaultKeeps WHERE vaultKeeps.id = @vaultkeepId;";
      int rowsAffected = _db.Execute(sql, new {vaultkeepId});
      if (rowsAffected == 0)
      {
        throw new Exception("Delete Failed");
      }
    }
  }  
}    