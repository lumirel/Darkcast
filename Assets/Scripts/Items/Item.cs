using UnityEngine;

namespace Darkcast.Items
{
    [CreateAssetMenu(menuName = "Darkcast/Item", fileName = "New Item", order = 0)]
    public class Item : ScriptableObject
    {
        [SerializeField] private int _stackSize;

        public int stackSize => _stackSize;
    }
}