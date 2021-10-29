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
  } 
}