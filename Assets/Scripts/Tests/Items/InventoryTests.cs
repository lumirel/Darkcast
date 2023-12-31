using NUnit.Framework;
using UnityEngine;
using Darkcast.Items;

namespace Darkcast.Tests.Items
{
    public sealed class InventoryTests : ScriptableObject
    {
        [SerializeField] private Item _dummyItem;

        [Test]
        public void Initializes_inventory_size()
        {
            const int size = 10;
            var inventory = new Inventory(size);

            Assert.AreEqual(size, inventory.size);
        }

        [Test]
        public void Stores_items()
        {
            const int size = 10;
            var inventory = new Inventory(size);

            var itemStack = new ItemStack(_dummyItem, _dummyItem.stackSize);
            inventory.Store(ref itemStack);

            Assert.IsTrue(inventory.Contains(itemStack));
        }
    }
}