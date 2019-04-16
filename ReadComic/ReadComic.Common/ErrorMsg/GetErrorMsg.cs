using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace ReadComic.Common.ErrorMsg
{
    //1: Confirm (xác nhận hầu như ở đây không dùng đến)
    //2: Success (thành công ,dùng để báo cho người dùng về việc họ muốn)
    //3: Warning (cảnh báo cho người dùng, chắc cũng không dùng đến)
    //4: Error (báo người dùng việc họ muốn thực hiện bị thất bại)
    //5: Info
    //6: Alert (thông báo cho người dùng)
    public class KeyErrorMsg
    {
        public int Key { get; set; }
        public ErrorMsg ErrorMsg { get; set; }
    }

    public class GetErrorMsg
    {
        public ErrorMsg GetMsg(int key)
        {
            string[] lines = File.ReadAllLines(HostingEnvironment.MapPath("~/Json.json"));
            string json = string.Join("", lines);
            var ErrorMsgs = new JavaScriptSerializer().Deserialize<KeyErrorMsg[]>(json);
            foreach (KeyErrorMsg errorMsg in ErrorMsgs)
            {
                if (key == errorMsg.Key)
                    return errorMsg.ErrorMsg;
            }
            return null;
        }
        
    }
}