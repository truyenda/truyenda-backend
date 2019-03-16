using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Database.Schema
{
    public static class Connection
    {
        public static string stringConnection() {
            // return @"data source=scomic.database.windows.net;initial catalog=ReadComic;User Id=minhduc;Password=5@03mn0l4ch0n9;")
            //return @"data source=truyenda.database.windows.net;initial catalog=ReadComic;User Id=truyenda;Password=5@03mn0l4ch0n9;")
            return @"data source=ADMIN\SQLEXPRESS;initial catalog=REadComic;User Id=sa;Password=123456;";
        }

    }
}