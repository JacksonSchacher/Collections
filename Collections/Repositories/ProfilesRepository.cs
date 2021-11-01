using System.Collections.Generic;
using System.Data;
using System.Linq;
using Collections.Models;
using Dapper;

namespace Collections.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Profile Get(string profileId)
    {
      string sql = "SELECT * FROM accounts p WHERE p.id = @profileId LIMIT 1;";
      return _db.Query(sql, new {profileId}).FirstOrDefault();
    }

    internal List<Keep> GetKeeps(string profileId)
    {
      string sql = @"
      SELECT 
      *
      FROM keeps k
      WHERE k.creatorId = @profileId;
      ";
      return _db.Query<Keep>(sql, new {profileId}).ToList();
    }

    internal List<Vault> GetVaults(string profileId)
    {
      string sql = "SELECT * FROM vaults v WHERE v.creatorId = @profileId;";
      return _db.Query<Vault>(sql, new {profileId}).ToList();
    }
  }
}