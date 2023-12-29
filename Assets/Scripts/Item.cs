using UnityEngine;

namespace Darkcast
{
    [CreateAssetMenu(menuName = "Darkcast/Item", fileName = "New Item", order = 0)]
    public sealed class Item : ScriptableObject
    {
        [SerializeField] private int _stackSize;

        public Item(string name, int stackSize)
        {
            this.name = name; 
            _stackSize = stackSize;
        }

        public int stackSize => _stackSize;
    }
}