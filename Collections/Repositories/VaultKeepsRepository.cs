using System;
using System.Data;
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
      VALUES(@creatorId, @vaultId, @KeepId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, new {vaultKeepData});
      vaultKeepData.Id = id;
      return vaultKeepData;
    }

    internal VaultKeep Get(int vaultkeepId)
    {
      string sql = @"
      SELECT
      vk.*,
      FROM vaultkeeps vk
      WHERE vk.id = @vaultkeepId;
      ";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new {vaultkeepId});
    }

    internal void Remove(int vaultkeepId)
    {
      string sql = "DELETE FROM vaultKeeps vk WHERE vk.id = @vaultkeepId;";
      int rowsAffected = _db.Execute(sql, new {vaultkeepId});
      if (rowsAffected == 0)
      {
        throw new Exception("Delete Failed");
      }
    }
  }  
}    