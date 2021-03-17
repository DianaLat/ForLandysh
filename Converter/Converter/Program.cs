using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    class Program
    {
        public static HandsClothesEntities2 db = new HandsClothesEntities2();
        static void Main(string[] args)
        {
            foreach (var item in db.Materials)
            {
                if (item.Image!="")
                {
                    byte[] str = Encoding.Default.GetBytes(@"C:\Users\201713\Desktop\SecretRepozitory\КОД 1.1._ВАРИАНТ_4\Сессия 1" + item.Image);
                    item.ByteImage = str;
                    db.Materials.AddOrUpdate<Material>();

                    //var bytes = File.ReadAllBytes(@"C:\Users\201713\Desktop\SecretRepozitory\КОД 1.1._ВАРИАНТ_4\Сессия 1" + item.Image);
                    //db.Materials.AddOrUpdate<Material>();
                }
            }
            db.SaveChanges();
        }
    }
}
