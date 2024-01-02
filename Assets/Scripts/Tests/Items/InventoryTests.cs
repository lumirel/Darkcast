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
        public void Cannot_store_a_null_item()
        {
            var inventory = new Inventory();

            Assert.Throws<ArgumentNullException>(() => inventory.Store(null));
        }

        [Test]
        public void Cannot_store_a_non_positive_amount_of_item()
        {
            var inventory = new Inventory();

            Assert.Throws<ArgumentOutOfRangeException>(() => inventory.Store(_dummyItem, 0));
        }

        [Test]
        public void Stores_a_single_item_in_an_inventory()
        {
            var inventory = new Inventory();

            inventory.Store(_dummyItem);

            Assert.IsTrue(inventory.Contains(_dummyItem));
        }

        [Test]
        public void Stores_a_single_item_in_an_inventory_multiple_times()
        {
            var inventory = new Inventory();

            inventory.Store(_dummyItem);
            inventory.Store(_dummyItem);

            Assert.IsTrue(inventory.Contains(_dummyItem, 2));
        }

        [Test]
        public void Stores_a_single_item_of_different_types_in_an_inventory()
        {
            var inventory = new Inventory();

            inventory.Store(_dummyItem);
            inventory.Store(_anotherDummyItem);

            Assert.IsTrue(inventory.Contains(_dummyItem));
            Assert.IsTrue(inventory.Contains(_anotherDummyItem));
        }
        
        [Test]
        public void Stores_a_single_item_of_different_types_in_an_inventory_multiple_times()
        {
            var inventory = new Inventory();

            inventory.Store(_dummyItem);
            inventory.Store(_dummyItem);

            inventory.Store(_anotherDummyItem);
            inventory.Store(_anotherDummyItem);

            Assert.IsTrue(inventory.Contains(_dummyItem, 2));
            Assert.IsTrue(inventory.Contains(_anotherDummyItem, 2));
        }

        [Test]
        public void Stores_multiple_items_in_an_inventory()
        {
            var inventory = new Inventory();

            const int amount = 5;
            inventory.Store(_dummyItem, amount);

            Assert.IsTrue(inventory.Contains(_dummyItem, amount));
        }

        [Test]
        public void Stores_multiple_items_in_an_inventory_multiple_times()
        {
            var inventory = new Inventory();

            const int amount = 5;
            inventory.Store(_dummyItem, amount);
            inventory.Store(_dummyItem, amount);

            Assert.IsTrue(inventory.Contains(_dummyItem, amount * 2));
        }

        [Test]
        public void Stores_multiple_items_of_different_types_in_an_inventory_multiple_times()
        {
            var inventory = new Inventory();
        
            const int amount = 5;
            inventory.Store(_dummyItem, amount);
            inventory.Store(_anotherDummyItem, amount);
        
            Assert.IsTrue(inventory.Contains(_dummyItem, amount));
            Assert.IsTrue(inventory.Contains(_anotherDummyItem, amount));
        }

        [Test]
        public void Removes_one_item_store_in_an_inventory()
        {
            var inventory = new Inventory();

            inventory.Store(_dummyItem);
            inventory.Remove(_dummyItem);
        
            Assert.IsFalse(inventory.Contains(_dummyItem));
        }

        [Test]
        public void Removes_one_of_several_items_stored_in_an_inventory()
        {
            var inventory = new Inventory();

            const int storeAmount = 8;
            inventory.Store(_dummyItem, storeAmount);

            const int removeAmount = 4;
            inventory.Remove(_dummyItem, removeAmount);
        
            Assert.IsTrue(inventory.Contains(_dummyItem, storeAmount - removeAmount));
        }

        [Test]
        public void Does_not_remove_an_item_that_was_never_stored_in_an_inventory()
        {
            var inventory = new Inventory();

            inventory.Store(_dummyItem);
            inventory.Remove(_anotherDummyItem);

            Assert.IsTrue(inventory.Contains(_dummyItem));
            Assert.IsFalse(inventory.Contains(_anotherDummyItem));
        }

        [Test]
        public void Does_not_remove_items_if_not_enough_were_stored_in_an_inventory()
        {
            var inventory = new Inventory();

            inventory.Store(_dummyItem, 4);
            inventory.Remove(_dummyItem, 8);

            Assert.IsTrue(inventory.Contains(_dummyItem));
        }
    }
}