using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace ReadComic.Common.Permission
{
    public class Permission
    {
        public string Name { get; set; }
        public decimal TongBit { get; set; }
    }
    public class GetPermission
    {
        public decimal GetQuyen(string key)
        {
            string[] lines = File.ReadAllLines(HostingEnvironment.MapPath("~/Permission.json"));
            string json = string.Join("", lines);
            var permissions = new JavaScriptSerializer().Deserialize<Permission[]>(json);
            foreach (Permission permission in permissions)
            {
                if (string.Compare(key,permission.Name) == 0)
                    return permission.TongBit;
            }
            return 0;
        }
    }
}