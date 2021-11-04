using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Collections.Models;
using Dapper;

namespace Collections.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Keep> Get()
    {
      string sql = @"
      SELECT 
      k.*,
      p.* 
      FROM keeps k
      JOIN accounts p ON p.id = k.creatorId;
      ";
      return _db.Query<Keep, Profile, Keep>(sql, (k, p) => {
        k.Creator = p;
        return k;
      }).ToList();
    }

    internal Keep Get(int keepId)
    {
      string sql = @"
      SELECT 
      k.*,
      p.* 
      FROM keeps k
      JOIN accounts p ON p.id = k.creatorId 
      WHERE k.id = @keepId;
      UPDATE keeps
      SET views = views + 1
      WHERE keeps.id = @keepId;
      ";
      return _db.Query<Keep, Profile, Keep>(sql, (k, p) => {
        k.Creator = p;
        return k;
      }, new {keepId}).FirstOrDefault();
    }

    internal Keep Create(Keep keepData)
    {
      string sql = @"
      INSERT INTO keeps(creatorId, name, description, img)
      VALUES(@creatorId, @Name, @Description, @Img);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, keepData);
      keepData.Id = id;
      return keepData;
    }

    internal Keep Edit(Keep keepData)
    {
      string sql = @"
      UPDATE keeps
      SET
      name = @Name,
      description = @Description,
      img = @Img
      WHERE id = @Id LIMIT 1;
      ";
      var rowsAffected = _db.Execute(sql, keepData);
      if (rowsAffected == 0)
      {
        throw new Exception("Edit Keep Failed");
      }
      return keepData;
    }

    internal void Remove(int keepId)
    {
      string sql = "DELETE FROM keeps WHERE id = @keepId LIMIT 1;";
      var rowsAffected = _db.Execute(sql, new {keepId});
      if (rowsAffected == 0)
      {
        throw new Exception("Delete Failed");
      }
    }
  } 
}