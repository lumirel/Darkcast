using System;

namespace Darkcast.Items
{
    /// <summary>
    /// Represents a stack of items.
    /// </summary>
    [Serializable]
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
        /// Combines this stack with another stack.
        /// </summary>
        /// <param name="other">The other stack to combine.</param>
        public void Combine(ref ItemStack other)
        {
            if (isEmpty && other.isEmpty)
            {
                return;
            }

            if (item != other.item)
            {
                (item, other.item) = (other.item, item);
                (count, other.count) = (other.count, count);
                return;
            }

            var newCount = count + other.count;

            if (newCount > item.stackSize)
            {
                // Partially combined the two stacks.
                count = item.stackSize;

                other.count = newCount - item.stackSize;
            }
            else
            {
                // Completely combined the two stacks.
                count = newCount;

                other.item = null;
                other.count = 0;
            }
        }

        /// <summary>
        /// Splits this stack into another stack.
        /// </summary>
        /// <param name="amount">The number of items in the new stack.</param>
        /// <returns>A new stack containing the number of requested items or an empty stack is not possible.</returns>
        public ItemStack Split(int amount)
        {
            if (isEmpty)
            {
                return new ItemStack();
            }

            var newCount = count - amount;

            if (newCount < 1)
            {
                var newStack = new ItemStack(item, count);

                item = null;
                count = 0;

                return newStack;
            }

            count = newCount;

            return new ItemStack(item, amount);
        }

        /// <summary>
        /// Gets a string representation of the item stack.
        /// </summary>
        /// <returns>A string representation of the item stack.</returns>
        public override string ToString()
        {
            if (item)
            {
                return $"({item.name}, {count})";
            }
            
            return "(Empty)";
        }
    }
}