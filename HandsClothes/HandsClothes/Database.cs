using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace HandsClothes
{
    class Database
    {
        public static HandsClothesNewEntities db = new HandsClothesNewEntities();
    }
    public partial class Material
    {
        public string StringImage
        {
            get
            {
                if (Image != "")
                {
                    string s = @"\Images" + Image;
                    return s;
                }
                else return @"\Images\picture.png";
            }
        }
        public string Color
        {
            get
            {
                if (CountInWarehouse < MinCount) return "#f19292";
                else if (CountInWarehouse >= MinCount * 3) return "#ffba01";
                else return "White";
            }
        }
        public int Remain
        {
            get
            {
                return CountInWarehouse % CountInPack;
            }
        }
        public string StringRemain
        {
            get
            {
                return "Остаток: " + Remain + " " + Unit;
            }
        }
        public string Suppliers
        {
            get
            {
                string s = "Поставщики: ";
                if (MaterialSuppliers.Count == 0) return s + "-";
                foreach (MaterialSupplier sup in MaterialSuppliers)
                {
                    s += sup.Supplier1.Name + ", ";
                }
                return s.Remove(s.Length - 2, 1);
            }
        }
        public static List<Material> OrderToWane(List<Material> list, int i)
        {
            List<Material> newList = new List<Material>();
            if (i == 0)
            {
                newList = list.OrderBy(u => u.Name).ToList();
            }
            else if (i == 1)
            {
                newList = list.OrderBy(u => u.Remain).ToList();
            }
            else if (i == 2)
            {
                newList = list.OrderBy(u => u.Price).ToList();
            }
            return newList;
        }
        public static List<Material> OrderToRise(List<Material> list, int i)
        {
            List<Material> newList = new List<Material>();
            if (i == 0)
            {
                newList = list.OrderByDescending(u => u.Name).ToList();
            }
            else if (i == 1)
            {
                newList = list.OrderByDescending(u => u.Remain).ToList();
            }
            else if (i == 2)
            {
                newList = list.OrderByDescending(u => u.Price).ToList();
            }
            return newList;
        }
        //public string SupsStack
        //{
        //    get
        //    {
        //        string ret = "";
        //        foreach (MaterialSupplier s in MaterialSuppliers)
        //        {
        //            ret += s.Supplier1.Name + "/n";
        //        }
        //        return ret;
        //    }
        //}
        public string Description
        {
            get
            {
                return "СРОЧНО МЕНЯЙ ДБ";
            }
            set
            {

            }
        }
    }
}
