using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.Inventory
{
    class ItemDatabase
    {
        private Dictionary<int, Item> Items = new Dictionary<int, Item>();

        public void AddItem(int id, char character, int level, int ult = 0)
        {
            var Item = new Item
            {
                Id = id,
                Character = character,
                Level = level,
                Ult = ult
            };

            Items.Add(id, Item);
        }

        public Item GetItemById(int id)
        {
            if (Items.ContainsKey(id))
            {
                return Items[id];
            }
            else
            {
                Console.WriteLine($"Item with ID {id} not found.");
                return null;
            }
        }

        // You can add more methods for updating, deleting, or querying the database as needed.
    }
}
