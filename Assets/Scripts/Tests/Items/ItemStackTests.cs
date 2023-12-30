using System;
using NUnit.Framework;
using UnityEngine;
using Darkcast.Items;

namespace Darkcast.Tests.Items
{
    public sealed class ItemStackTests : ScriptableObject
    {
        [SerializeField] private Item _dummyItem;
        [SerializeField] private Item _anotherDummyItem;

        [Test]
        public void Initializes_item_stack_without_item_or_count()
        {
            var itemStack = new ItemStack();

            Assert.IsNull(itemStack.item);
            Assert.AreEqual(0, itemStack.count);
            Assert.IsTrue(itemStack.isEmpty);
        }

        [Test]
        public void Cannot_initialize_item_stack_without_an_item()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new ItemStack(null, 8));
        }

        [Test]
        public void Cannot_initialize_item_stack_with_negative_count()
        {
            const int itemStackCount = -1;

            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new ItemStack(_dummyItem, itemStackCount));
        }

        [Test]
        public void Cannot_initialize_item_stack_with_count_greater_than_item_stack_size()
        {
            var itemStackCount = _dummyItem.stackSize + 1;

            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new ItemStack(_dummyItem, itemStackCount));
        }

        [Test]
        public void Initializes_item_stack_with_item_and_count_less_than_item_stack_size()
        {
            var itemStackCount = _dummyItem.stackSize / 2;
            var itemStack = new ItemStack(_dummyItem, itemStackCount);

            Assert.AreEqual(_dummyItem, itemStack.item);
            Assert.AreEqual(itemStackCount, itemStack.count);
            Assert.IsFalse(itemStack.isEmpty);
        }

        [Test]
        public void Combining_item_stacks_with_different_items_swaps_values()
        {
            const int itemStackCount = 10;
            var itemStack = new ItemStack(_dummyItem, itemStackCount);

            const int otherItemStackCount = 15;
            var otherItemStack = new ItemStack(_anotherDummyItem, otherItemStackCount);

            itemStack.Combine(ref otherItemStack);

            Assert.AreEqual(_anotherDummyItem, itemStack.item);
            Assert.AreEqual(otherItemStackCount, itemStack.count);

            Assert.AreEqual(_dummyItem, otherItemStack.item);
            Assert.AreEqual(itemStackCount, otherItemStack.count);
        }

        [Test]
        public void Combining_item_stacks_with_different_items_swaps_values_even_when_empty()
        {
            var itemStack = new ItemStack();

            const int otherItemStackCount = 15;
            var otherItemStack = new ItemStack(_anotherDummyItem, otherItemStackCount);

            itemStack.Combine(ref otherItemStack);

            Assert.AreEqual(_anotherDummyItem, itemStack.item);
            Assert.AreEqual(otherItemStackCount, itemStack.count);

            Assert.IsNull(otherItemStack.item);
            Assert.AreEqual(0, otherItemStack.count);
        }

        [Test]
        public void Combining_stacks_with_different_items_swaps_stacks_even_when_the_other_one_is_empty()
        {
            const int itemStackCount = 15;
            var itemStack = new ItemStack(_dummyItem, itemStackCount);

            var otherItemStack = new ItemStack();

            itemStack.Combine(ref otherItemStack);

            Assert.IsNull(itemStack.item);
            Assert.AreEqual(0, itemStack.count);

            Assert.AreEqual(_dummyItem, otherItemStack.item);
            Assert.AreEqual(itemStackCount, otherItemStack.count);
        }

        [Test]
        public void Partially_combines_one_stack_into_another_stack()
        {
            const int itemStackCount = 24;
            var itemStack = new ItemStack(_dummyItem, itemStackCount);

            const int otherItemStackCount = 16;
            var otherItemStack = new ItemStack(_dummyItem, otherItemStackCount);

            itemStack.Combine(ref otherItemStack);

            Assert.AreEqual(_dummyItem, itemStack.item);
            Assert.AreEqual(itemStack.item.stackSize, itemStack.count);

            Assert.AreEqual(_dummyItem, otherItemStack.item);
            Assert.AreEqual(itemStackCount + otherItemStackCount - _dummyItem.stackSize, otherItemStack.count);
        }

        [Test]
        public void Completely_combines_one_stack_into_another_stack()
        {
            const int itemStackCount = 10;
            var itemStack = new ItemStack(_dummyItem, itemStackCount);

            const int otherItemStackCount = 15;
            var otherItemStack = new ItemStack(_dummyItem, otherItemStackCount);

            itemStack.Combine(ref otherItemStack);

            Assert.AreEqual(_dummyItem, itemStack.item);
            Assert.AreEqual(itemStackCount + otherItemStackCount, itemStack.count);

            Assert.IsNull(otherItemStack.item);
            Assert.AreEqual(0, otherItemStack.count);
            Assert.IsTrue(otherItemStack.isEmpty);
        }

        [Test]
        public void Splits_item_stack_evenly_into_another_item_stack()
        {
            const int itemStackCount = 32;
            var itemStack = new ItemStack(_dummyItem, itemStackCount);

            const int itemStackSplitCount = 8;

            var otherItemStack = itemStack.Split(itemStackSplitCount);

            Assert.AreEqual(_dummyItem, itemStack.item);
            Assert.AreEqual(itemStackCount - itemStackSplitCount, itemStack.count);
            
            Assert.AreEqual(_dummyItem, otherItemStack.item);
            Assert.AreEqual(itemStackSplitCount, otherItemStack.count);
        }
        
        [Test]
        public void Splits_entire_item_stack_into_another_item_stack()
        {
            const int itemStackCount = 32;
            var itemStack = new ItemStack(_dummyItem, itemStackCount);

            var otherItemStack = itemStack.Split(itemStackCount);

            Assert.IsNull(itemStack.item);
            Assert.AreEqual(0, itemStack.count);

            Assert.AreEqual(_dummyItem, otherItemStack.item);
            Assert.AreEqual(itemStackCount, otherItemStack.count);
        }
    }
}