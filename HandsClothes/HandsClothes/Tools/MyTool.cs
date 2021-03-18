using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsClothes.Tools
{
    class MyTool
    {
        public static List<T> CreateNewItem<T> (List<T> source, T item)
        {
            var list = source.ToList();
            list.Insert(0, item);
            return list;
            //source=MyTool.Create(from db<>, new class)
        }
    }
}
