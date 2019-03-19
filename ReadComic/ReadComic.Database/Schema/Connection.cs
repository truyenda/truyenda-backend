using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Database.Schema
{
    public static class Connection
    {
        public static string stringConnection() {
            //return @"data source=truyenda.database.windows.net;Initial catalog=truyenda;Persist Security Info=False;User Id=truyenda;Password=5@03mn0l4ch0n9;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            return @"data source=ADMIN\SQLEXPRESS;initial catalog=REadComic;User Id=sa;Password=123456;";
        }

    }
}