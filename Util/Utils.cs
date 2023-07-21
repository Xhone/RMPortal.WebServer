using Newtonsoft.Json.Linq;
using RMPortal.WebServer.Models.PackList;

namespace RMPortal.WebServer.Util
{
    public static class Utils
    {

        public static Array getListByArray(JArray jsonArray)
        {
            List<Summary> nlist = new List<Summary>();
            foreach (JToken jToken in jsonArray)
            {
                Summary data = jToken.ToObject<Summary>();
                nlist.Add(data);
            }
            return nlist.ToArray();
        }
        public static Array getListByArrayExcel(JArray jsonArray)
        {
            List<Object> nlist = new List<Object>();
            foreach (JToken jToken in jsonArray)
            {
                Object data = jToken.ToObject<Object>();
                nlist.Add(data);
            }
            return nlist.ToArray();
        }



        public static string getNumber(string str)
        {
            str = str.Trim();
            str = str.Substring(0, str.LastIndexOf("."));
            if (str.Contains("PO"))
            {
                str = str.Substring(2);
                str = str.Substring(0, str.LastIndexOf("_"));
            }
            else if (str.Contains("PackList"))
            {
                str = str.Substring(str.LastIndexOf("t") + 1);
            }
            str = "PackListExcel" + str;
            return str;
        }
        public static string getRightStr(string str)
        {
            str = str.Trim();
            str = str.Replace("\"", "");
            //str = str.Replace(",", "");
            str=str.Substring(str.LastIndexOf("P"));
            str = str.Substring(0,str.LastIndexOf("x")+1);
            return str;
        }
    }
}
