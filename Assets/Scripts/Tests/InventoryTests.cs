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

            var itemStack = new ItemStack(_dummyItem, 4);
            inventory.Insert(ref itemStack);

            Assert.True(inventory.Contains(itemStack));
        }
    }
}