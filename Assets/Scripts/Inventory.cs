using System;

namespace Darkcast
{
    public sealed class Inventory
    {
        private readonly Stack[] _stacks;

        public Inventory(int size)
        {
            if (size < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }

            _stacks = new Stack[size];

            for (var i = 0; i < size; i++)
            {
                _stacks[i] = new Stack();
            }
        }

        public void Insert(Stack other)
        {
            foreach (var stack in _stacks)
            {
                stack.Combine(other);

                if (stack.isEmpty)
                {
                    return;
                }
            }
        }

        public bool Contains(Stack other)
        {
            throw new NotImplementedException();
        }
    }
}