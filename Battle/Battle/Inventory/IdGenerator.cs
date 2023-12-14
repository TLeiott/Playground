using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.General
{
    /// <summary>
    /// Provides a Unique ID for items for the inventory
    /// </summary>
    public class IdGenerator
    {
        private HashSet<int> usedIds;
        private int counter;

        public IdGenerator()
        {
            usedIds = new HashSet<int>();
            counter = 1; // Start generating IDs from 1
        }

        public int GenerateId()
        {
            while (usedIds.Contains(counter))
            {
                counter++;
            }

            int newId = counter;
            usedIds.Add(newId);
            counter++;

            return newId;
        }

        public void ReleaseId(int id)
        {
            usedIds.Remove(id);
        }
    }
}
