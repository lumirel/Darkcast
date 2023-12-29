using System;
using UnityEngine;
using NUnit.Framework;

namespace Darkcast.Tests
{
    public sealed class StackTests : ScriptableObject
    {
        [SerializeField] private Item _dummyItem;
        [SerializeField] private Item _anotherDummyItem;

        [Test]
        public void Cannot_initialize_stack_without_an_item()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new Stack(null, 8));
        }

        [Test]
        public void Cannot_initialize_stack_with_negative_count()
        {
            const int count = -1;

            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Stack(_dummyItem, count));
        }

        [Test]
        public void Initializes_stack_with_default_item_and_count()
        {
            var stack = new Stack();

            Assert.IsNull(stack.item);
            Assert.AreEqual(0, stack.count);
            Assert.IsTrue(stack.isEmpty);
        }

        [Test]
        public void Initializes_stack_with_item_and_count_that_is_less_than_item_stack_size()
        {
            var count = _dummyItem.stackSize / 2;
            var stack = new Stack(_dummyItem, count);

            Assert.AreEqual(_dummyItem, stack.item);
            Assert.AreEqual(count, stack.count);
            Assert.IsFalse(stack.isEmpty);
        }

        [Test]
        public void Initialize_stack_with_count_that_is_greater_than_item_stack_size()
        {
            var count = _dummyItem.stackSize + 1;
            var stack = new Stack(_dummyItem, count);

            Assert.AreEqual(_dummyItem, stack.item);
            Assert.AreEqual(_dummyItem.stackSize, stack.count);
            Assert.IsFalse(stack.isEmpty);
        }

        [Test]
        public void Combining_stacks_with_different_items_swaps_stacks()
        {
            const int firstStackCount = 10;
            var firstStack = new Stack(_dummyItem, firstStackCount);

            const int secondStackCount = 15;
            var secondStack = new Stack(_anotherDummyItem, secondStackCount);

            firstStack.Combine(secondStack);

            Assert.AreEqual(_anotherDummyItem, firstStack.item);
            Assert.AreEqual(secondStackCount, firstStack.count);

            Assert.AreEqual(_dummyItem, secondStack.item);
            Assert.AreEqual(firstStackCount, secondStack.count);
        }

        [Test]
        public void Combining_stacks_with_different_items_swaps_stacks_even_when_it_is_empty()
        {
            var firstStack = new Stack();

            const int secondStackCount = 15;
            var secondStack = new Stack(_anotherDummyItem, secondStackCount);

            firstStack.Combine(secondStack);

            Assert.AreEqual(_anotherDummyItem, firstStack.item);
            Assert.AreEqual(secondStackCount, firstStack.count);

            Assert.IsNull(secondStack.item);
            Assert.AreEqual(0, secondStack.count);
        }

        [Test]
        public void Combining_stacks_with_different_items_swaps_stacks_even_when_the_other_one_is_empty()
        {
            const int firstStackCount = 15;
            var firstStack = new Stack(_dummyItem, firstStackCount);

            var secondStack = new Stack();

            firstStack.Combine(secondStack);

            Assert.IsNull(firstStack.item);
            Assert.AreEqual(0, firstStack.count);
            
            Assert.AreEqual(_dummyItem, secondStack.item);
            Assert.AreEqual(firstStackCount, secondStack.count);
        }
        
        [Test]
        public void Partially_combines_one_stacks_into_another_stack()
        {
            const int firstStackCount = 24;
            var firstStack = new Stack(_dummyItem, firstStackCount);
        
            const int secondStackCount = 16;
            var secondStack = new Stack(_dummyItem, secondStackCount);

            firstStack.Combine(secondStack);
        
            Assert.AreEqual(_dummyItem, firstStack.item);
            Assert.AreEqual(firstStack.item.stackSize, firstStack.count);
        
            Assert.AreEqual(_dummyItem, secondStack.item);
            Assert.AreEqual(firstStackCount + secondStackCount - firstStack.item.stackSize, secondStack.count);
        }

        [Test]
        public void Completely_combines_one_stack_into_another_stack()
        {
            const int firstStackCount = 10;
            var firstStack = new Stack(_dummyItem, firstStackCount);
        
            const int secondStackCount = 15;
            var secondStack = new Stack(_dummyItem, secondStackCount);

            firstStack.Combine(secondStack);

            Assert.AreEqual(_dummyItem, firstStack.item);
            Assert.AreEqual(firstStackCount + secondStackCount, firstStack.count);
        
            Assert.IsNull(secondStack.item);
            Assert.AreEqual(0, secondStack.count);
        }
    }
}
