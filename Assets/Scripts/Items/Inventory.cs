using System;
using System.Collections.Generic;

namespace Darkcast.Items
{
    /// <summary>
    /// Represents an inventory of items.
    /// </summary>
    [Serializable]
    public sealed class Inventory
    {
        [Serializable]
        private struct Slot
        {
            public Item item;

            public int count;

            public Slot(Item item, int count)
            {
                this.item = item;
                this.count = count;
            }
        }

        private List<Slot> _slots;

        /// <summary>
        /// Creates an inventory that can store the given number of items.
        /// </summary>
        /// <param name="capacity">The maximum number of items the inventory can contain.</param>
        public Inventory(int capacity)
        {
            if (capacity < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            _slots = new List<Slot>();

            this.capacity = capacity;
        }

        /// <summary>
        /// The maximum number of items the inventory can contain.
        /// </summary>
        public int capacity { get; }

        /// <summary>
        /// The current number of items the inventory contains.
        /// </summary>
        public int count { get; private set; }

        /// <summary>
        /// Stores a certain number of items in the inventory.
        /// </summary>
        /// <param name="item">The item to store.</param>
        /// <param name="amount">The number of items to store.</param>
        public void Store(Item item, int amount = 1)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (amount < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            if (count + amount > capacity)
            {
                return;
            }

            for (var i = 0; i < _slots.Count; i++)
            {
                var existingSlot = _slots[i];
                if (existingSlot.item != item)
                {
                    continue;
                }

                _slots[i] = new Slot(item, existingSlot.count + amount);
                count += amount;
                return;
            }

            var newSlot = new Slot(item, amount);
            _slots.Add(newSlot);
            count += amount;
        }

        /// <summary>
        /// Checks if the inventory contains a certain number of items.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <param name="amount">The number of items to check.</param>
        /// <returns>True if the inventory contains the number of items requested, false otherwise.</returns>
        public bool Contains(Item item, int amount = 1)
        {
            // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
            foreach (var existingSlot in _slots)
            {
                if (existingSlot.item == item && existingSlot.count >= amount)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes a certain number of items from the inventory.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <param name="amount">The number of items to remove.</param>
        public void Remove(Item item, int amount = 1)
        {
            for (var i = 0; i < _slots.Count; i++)
            {
                var existingSlot = _slots[i];
                if (existingSlot.item != item)
                {
                    continue;
                }

                if (existingSlot.count > amount)
                {
                    _slots[i] = new Slot(item, existingSlot.count - amount);
                    count -= amount;
                    return;

                }

                if (existingSlot.count == amount)
                {
                    _slots.RemoveAt(i);
                    count -= amount;
                    return;
                }

                return;
            }
        }
    }
}