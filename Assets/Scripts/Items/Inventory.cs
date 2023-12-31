namespace Darkcast.Items
{
    /// <summary>
    /// Represents an item inventory.
    /// </summary>
    public sealed class Inventory
    {
        private readonly ItemStack[] _itemStacks;

        /// <summary>
        /// Creates an inventory with the given number of item stacks.
        /// </summary>
        /// <param name="size">The number of item stacks the inventory contains.</param>
        public Inventory(int size)
        {
            _itemStacks = new ItemStack[size];

            this.size = size;
        }

        /// <summary>
        /// The number of item stacks the inventory contains.
        /// </summary>
        public int size { get; }

        /// <summary>
        /// Stores an item stack in the inventory.
        /// </summary>
        /// <param name="itemStack">The item stack to store.</param>
        public void Store(ref ItemStack itemStack)
        {
            // Keep track of the index of the first empty item stack found.
            var firstEmptyItemStackIndex = -1;
            
            // Attempt to combine this item stack with any existing items stacks.
            for (var index = 0; index < _itemStacks.Length; index++)
            {
                ref var existingItemStack = ref _itemStacks[index];

                if (existingItemStack.isEmpty)
                {
                    firstEmptyItemStackIndex = index;
                }

                if (existingItemStack.item != itemStack.item)
                {
                    continue;
                }

                existingItemStack.Combine(ref itemStack);

                // Check if the item stack has been completely emptied.
                if (itemStack.isEmpty)
                {
                    // The entire item stack has been stored.
                    return;
                }
            }

            // Where any empty item stack found?
            if (firstEmptyItemStackIndex == -1)
            {
                // No empty item stacks found.
                return;
            }

            // Combine this item stack with the first empty item stack found.
            ref var firstEmptyItemStack = ref _itemStacks[firstEmptyItemStackIndex];
            firstEmptyItemStack.Combine(ref itemStack);
        }

        /// <summary>
        /// Checks if the inventory contains an item stack.
        /// </summary>
        /// <param name="itemStack">The item stack to check.</param>
        /// <returns>True if the inventory contains the equivalent item stack even if spread out among multiple stacks.</returns>
        public bool Contains(ItemStack itemStack)
        {
            var totalCount = 0;
            
            foreach (var existingItemStack in _itemStacks)
            {
                if (existingItemStack.item != itemStack.item)
                {
                    continue;
                }

                totalCount += existingItemStack.count;

                if (totalCount >= itemStack.count)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes an item stack from the inventory.
        /// </summary>
        /// <param name="itemStack">The item stack to remove.</param>
        public void Remove(ref ItemStack itemStack)
        {
            
        }
    }
}