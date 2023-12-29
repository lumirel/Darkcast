using System;

namespace Darkcast
{
    public sealed class Inventory
    {
        private readonly ItemStack[] _stacks;

        public Inventory(int size)
        {
            if (size < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }

            _stacks = new ItemStack[size];
        }

        public void Insert(ref ItemStack other)
        {
            foreach (var stack in _stacks)
            {
                stack.Combine(ref other);

                if (stack.isEmpty)
                {
                    return;
                }
            }
        }

        public bool Contains(ItemStack other)
        {
            throw new NotImplementedException();
        }
    }
}