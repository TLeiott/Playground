using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.Inventory
{
    internal class Item
    {
        public int Id { get; set; }
        public char Character { get; set; }
        public int Level { get; set; }
        public int Ult { get; set; }
    }
}
