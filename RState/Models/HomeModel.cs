using System;
using System.Linq;
using System.Collections.Generic;

namespace RState.Models
{
    public class HomeViewModel
    {
        //public Pager Pager { get; set; }
        public IEnumerable<sp_getUserList_Result> Users { get; set; }
        public IEnumerable<sp_getPropList_Result> Properties { get; set; }

        public List<Tb_Pictures> GetPicturesByProId(int iProp)
        {
            var db = new DbCon();
            return db.Tb_Properties.Find(iProp).Tb_Pictures.ToList();
        }

        public static string GetRandomStr(int iLen)
        {
            int i, iVal;
            Random rnd = new Random();
            string str = string.Empty;
            char[] ch = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            for (i = 0; i < iLen; i++)
            {
                iVal = rnd.Next(1, ch.Length);
                if (!str.Contains(ch.GetValue(iVal).ToString()))
                    str += ch.GetValue(iVal);
                else i--;
            }
            return str;
        }
    }
    public class BookingViewModel
    {
        public int UserId { get; set; }
        public int PropId { get; set; }
    }

}