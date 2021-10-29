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
      string sql = "SELECT * FROM vaults WHERE id == @vaultId;";
      return _db.QueryFirstOrDefault<Vault>(sql, new {vaultId});
    }
  }
}