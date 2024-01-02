using System;
using NUnit.Framework;
using UnityEngine;
using Darkcast.Items;

namespace Darkcast.Tests.Items
{
    public sealed class InventoryTests : ScriptableObject
    {
        [SerializeField] private Item _dummyItem;
        [SerializeField] private Item _anotherDummyItem;

        [Test]
        public void Cannot_initialize_inventory_with_non_positive_capacity()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Inventory(0));
        }

        [Test]
        public void Initializes_inventory_with_positive_capacity_and_no_count()
        {
            const int capacity = 10;
            var inventory = new Inventory(capacity);

            Assert.AreEqual(capacity, inventory.capacity);
            Assert.AreEqual(0, inventory.count);
        }

        [Test]
        public void Cannot_store_a_null_item()
        {
            var inventory = new Inventory(10);

            Assert.Throws<ArgumentNullException>(() => inventory.Store(null));
        }

        [Test]
        public void Cannot_store_a_non_positive_amount_of_item()
        {
            var inventory = new Inventory(10);

            Assert.Throws<ArgumentOutOfRangeException>(() => inventory.Store(_dummyItem, 0));
        }

        [Test]
        public void Stores_a_single_item_in_an_inventory()
        {
            var inventory = new Inventory(10);

            inventory.Store(_dummyItem);

            Assert.IsTrue(inventory.Contains(_dummyItem));
            Assert.AreEqual(1, inventory.count);
        }

        [Test]
        public void Stores_a_single_item_in_an_inventory_multiple_times()
        {
            var inventory = new Inventory(10);

            inventory.Store(_dummyItem);
            inventory.Store(_dummyItem);

            Assert.IsTrue(inventory.Contains(_dummyItem, 2));
            Assert.AreEqual(2, inventory.count);
        }

        [Test]
        public void Stores_a_single_item_of_different_types_in_an_inventory()
        {
            var inventory = new Inventory(10);

            inventory.Store(_dummyItem);

            inventory.Store(_anotherDummyItem);

            Assert.IsTrue(inventory.Contains(_dummyItem));
            Assert.IsTrue(inventory.Contains(_anotherDummyItem));
            Assert.AreEqual(2, inventory.count);
        }
        
        [Test]
        public void Stores_a_single_item_of_different_types_in_an_inventory_multiple_times()
        {
            var inventory = new Inventory(10);

            inventory.Store(_dummyItem);
            inventory.Store(_dummyItem);

            inventory.Store(_anotherDummyItem);
            inventory.Store(_anotherDummyItem);

            Assert.IsTrue(inventory.Contains(_dummyItem, 2));
            Assert.IsTrue(inventory.Contains(_anotherDummyItem, 2));
            Assert.AreEqual(4, inventory.count);
        }

        [Test]
        public void Stores_multiple_items_in_an_inventory()
        {
            var inventory = new Inventory(10);

            const int amount = 5;
            inventory.Store(_dummyItem, amount);

            Assert.IsTrue(inventory.Contains(_dummyItem, amount));
            Assert.AreEqual(5, inventory.count);
        }

        [Test]
        public void Stores_multiple_items_in_an_inventory_multiple_times()
        {
            var inventory = new Inventory(10);

            const int amount = 5;
            inventory.Store(_dummyItem, amount);
            inventory.Store(_dummyItem, amount);

            Assert.IsTrue(inventory.Contains(_dummyItem, 10));
            Assert.AreEqual(10, inventory.count);
        }

        [Test]
        public void Stores_multiple_items_of_different_types_in_an_inventory_multiple_times()
        {
            var inventory = new Inventory(10);
        
            const int amount = 5;
            inventory.Store(_dummyItem, amount);
            inventory.Store(_anotherDummyItem, amount);
        
            Assert.IsTrue(inventory.Contains(_dummyItem, amount));
            Assert.IsTrue(inventory.Contains(_anotherDummyItem, amount));
            Assert.AreEqual(10, inventory.count);
        }

        [Test]
        public void Does_not_stores_more_items_than_inventory_capacity()
        {
            const int capacity = 10;
            var inventory = new Inventory(capacity);

            inventory.Store(_dummyItem, capacity + 1);
        
            Assert.IsFalse(inventory.Contains(_dummyItem));
            Assert.AreEqual(0, inventory.count);
        }

        [Test]
        public void Removes_one_item_store_in_an_inventory()
        {
            var inventory = new Inventory(10);

            inventory.Store(_dummyItem);
            inventory.Remove(_dummyItem);
        
            Assert.IsFalse(inventory.Contains(_dummyItem));
            Assert.AreEqual(0, inventory.count);
        }

        [Test]
        public void Removes_one_of_several_items_stored_in_an_inventory()
        {
            var inventory = new Inventory(10);

            inventory.Store(_dummyItem, 8);
            inventory.Remove(_dummyItem, 4);
        
            Assert.IsTrue(inventory.Contains(_dummyItem));
            Assert.AreEqual(4, inventory.count);
        }

        [Test]
        public void Does_not_remove_an_item_that_was_never_stored_in_an_inventory()
        {
            var inventory = new Inventory(10);

            inventory.Store(_dummyItem);
            inventory.Remove(_anotherDummyItem);

            Assert.IsTrue(inventory.Contains(_dummyItem));
            Assert.IsFalse(inventory.Contains(_anotherDummyItem));
            Assert.AreEqual(1, inventory.count);
        }

        [Test]
        public void Does_not_remove_items_if_not_enough_were_stored_in_an_inventory()
        {
            var inventory = new Inventory(10);

            inventory.Store(_dummyItem, 4);
            inventory.Remove(_dummyItem, 8);

            Assert.IsTrue(inventory.Contains(_dummyItem));
            Assert.AreEqual(4, inventory.count);
        }
    }
}