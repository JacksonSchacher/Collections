using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Collections.Models;
using Dapper;

namespace Collections.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Vault> Get()
    {
      string sql = "SELECT * FROM vaults;";
      return _db.Query<Vault>(sql).ToList();
    }

    internal Vault Get(int vaultId)
    {
      string sql = @"
      SELECT 
      v.*,
      p.*
      FROM vaults v
      JOIN accounts p ON p.id = v.creatorId
      WHERE v.id = @vaultId;";
      return _db.Query<Vault, Profile, Vault>(sql, (v, p) => {
        v.Creator = p;
        return v;
      }, new {vaultId}, splitOn:"id").FirstOrDefault();
    }

    internal Vault Create(Vault vaultData)
    {
      string sql = @"
      INSERT INTO vaults(name, description, isPrivate, creatorId, img)
      VALUES(@Name, @Description, @IsPrivate, @CreatorId, @Img);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, vaultData);
      vaultData.Id = id;
      return vaultData;
    }

    internal Vault Edit(Vault vaultData)
    {
      string sql = @"
      UPDATE vaults
      SET
      name = @Name,
      description = @Description,
      isPrivate = @IsPrivate,
      img = @Img
      WHERE id = @Id LIMIT 1;
      ";
      var rowsAffected = _db.Execute(sql, vaultData);
      if (rowsAffected == 0)
      {
        throw new Exception("Edit Vault Failed");
      }
      return vaultData;
    }

    internal List<VaultKeep> GetVKs(int vaultId)
    {
      string sql = @"
      SELECT 
      vk.*,
      p.*,
      k.* 
      FROM vaultKeeps vk 
      JOIN accounts p ON p.id = vk.creatorId
      JOIN keeps k ON k.id = vk.keepId
      WHERE vk.vaultId = @vaultId;";
      return _db.Query<VaultKeep, Profile, Keep, VaultKeep>(sql, (vk, p, k) => {
        vk.Creator = p;
        vk.Keep = k;
        return vk;
      },new {vaultId}).ToList();
    }

    internal void Remove(int vaultId)
    {
      string sql = "DELETE FROM vaults WHERE id = @vaultId LIMIT 1;";
      var rowsAffected = _db.Execute(sql, new {vaultId});
      if (rowsAffected == 0)
      {
        throw new Exception("Delete Failed");
      }
    }
  }
}