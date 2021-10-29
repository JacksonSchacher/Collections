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
      string sql = "SELECT * FROM keeps;";
      return _db.Query<Keep>(sql).ToList();
    }

    internal Keep Get(int keepId)
    {
      string sql = "SELECT * FROM keeps WHERE id == @keepId;";
      return _db.QueryFirstOrDefault<Keep>(sql, new {keepId});
    }

    internal Keep Create(Keep keepData)
    {
      string sql = @"
      INSERT INTO keeps(creatorId, name, description, img, tags)
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
      name = @Name
      description = @Description
      img = @Img
      tags = @Tags
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
      string sql = "DELETE FROM keeps WHERE id == @keepId LIMIT 1;";
      var rowsAffected = _db.Execute(sql, new {keepId});
      if (rowsAffected == 0)
      {
        throw new Exception("Delete Failed");
      }
    }
  } 
}