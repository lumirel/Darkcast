using System;

namespace Darkcast
{
    /// <summary>
    /// Represents a stack of items.
    /// </summary>
    public struct ItemStack
    {
        /// <summary>
        /// Creates a new stack of items.
        /// </summary>
        /// <param name="item">The item the stack holds.</param>
        /// <param name="count">The number of items the stack holds.</param>
        public ItemStack(Item item, int count = 1)
        {
            if (!item)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (count < 0 || count > item.stackSize)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            this.item = item;
            this.count = count;
        }

        /// <summary>
        /// The item the stack holds.
        /// </summary>
        public Item item { get; private set; }

        /// <summary>
        /// The number of items the stack holds.
        /// </summary>
        public int count { get; private set; }

        /// <summary>
        /// Checks if the stack is empty.
        /// </summary>
        public bool isEmpty => count == 0;

        /// <summary>
        /// Combine this stack with another stack.
        /// </summary>
        /// <param name="other">The other stack to combine.</param>
        public void Combine(ref ItemStack other)
        {
            if (item != other.item)
            {
                (item, other.item) = (other.item, item);
                (count, other.count) = (other.count, count);
                return;
            }

            var combinedCount = count + other.count;

            if (combinedCount > item.stackSize)
            {
                // Partially combined the two stacks.
                count = item.stackSize;

                other.count = combinedCount - item.stackSize;
            }
            else
            {
                // Completely combined the two stacks.
                count = combinedCount;

                other.item = null;
                other.count = 0;
            }
        }

        /// <summary>
        /// Split with stack into another stack.
        /// </summary>
        /// <param name="amount">The number of items in the new stack.</param>
        /// <returns>A new stack containing the number of requested items or an empty stack is not possible.</returns>
        public ItemStack Split(int amount)
        {
            if (isEmpty)
            {
                return new ItemStack();
            }

            var newStackCount = count - amount;

            if (newStackCount <= 0)
            {
                var newStack = new ItemStack(item, count);

                item = null;
                count = 0;

                return newStack;
            }

            count = newStackCount;

            return new ItemStack(item, amount);
        }
    }
}