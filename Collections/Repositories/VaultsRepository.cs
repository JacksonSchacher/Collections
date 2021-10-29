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
      string sql = "SELECT * FROM vaults WHERE id = @vaultId;";
      return _db.QueryFirstOrDefault<Vault>(sql, new {vaultId});
    }

    internal Vault Create(Vault vaultData)
    {
      string sql = @"
      INSERT INTO vaults(name, description, isPrivate)
      VALUES(@Name, @Description, @IsPrivate);
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
      isPrivate = @IsPrivate
      WHERE id = @Id LIMIT 1;
      ";
      var rowsAffected = _db.Execute(sql, vaultData);
      if (rowsAffected == 0)
      {
        throw new Exception("Edit Vault Failed");
      }
      return vaultData;
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