using System.Linq;
using System.Collections.Generic;

namespace RState.Services
{
    public class PropertyService
    {
        public List<Tb_Prop_Pictures> GetPicturesByPackId(int iProp)
        {
            var db = new DbCon();
            return db.Tb_Properties.Find(iProp).Tb_Prop_Pictures.ToList();
        }
    }
}
