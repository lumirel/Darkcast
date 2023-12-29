using UnityEngine;
using NUnit.Framework;

namespace Darkcast.Tests
{
    public class InventoryTests : ScriptableObject
    {
        [SerializeField] private Item _dummyItem;

        [Test]
        public void Inventory_contains_inserted_stack()
        {
            var inventory = new Inventory(1);
        
            var stack = new Stack(_dummyItem, 4);
            inventory.Insert(stack);
            
            Assert.True(inventory.Contains(stack));
        }
    }
}