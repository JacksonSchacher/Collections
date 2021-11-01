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
      string sql = "SELECT * FROM accounts WHERE id = @profileId;";
      return _db.QueryFirstOrDefault<Profile>(sql, new {profileId});
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