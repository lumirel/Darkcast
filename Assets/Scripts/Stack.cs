using System;

namespace Darkcast
{
    /// <summary>
    /// Represents a stack of items.
    /// </summary>
    public sealed class Stack
    {
        /// <summary>
        /// Creates an empty stack.
        /// </summary>
        public Stack()
        {
        }

        /// <summary>
        /// Creates a new stack of items.
        /// </summary>
        /// <param name="item">The item the stack holds.</param>
        /// <param name="count">The number of items the stack holds.</param>
        public Stack(Item item, int count = 1)
        {
            if (!item)
            {
                throw new ArgumentNullException(nameof(item));
            }
            
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            this.item = item;
            
            if (count > item.stackSize)
            {
                this.count = item.stackSize;
            }
            else
            {
                this.count = count;
            }
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
        /// Attempts to combine the other stack into this stack.
        /// </summary>
        /// <param name="other">The other stack to combine.</param>
        public void Combine(Stack other)
        {
            // Different items can't be combined. Maybe swap later?
            if (item != other.item)
            {
                (item, other.item) = (other.item, item);
                (count, other.count) = (other.count, count);
                return;
            }

            var totalCount = count + other.count;

            if (totalCount > item.stackSize)
            {
                // Partially combined the two stacks.
                count = item.stackSize;
                other.count = totalCount - item.stackSize;
            }
            else
            {
                // Completely combined the two stacks.
                count = totalCount;

                other.item = null;
                other.count = 0;
            }
        }
    }
}