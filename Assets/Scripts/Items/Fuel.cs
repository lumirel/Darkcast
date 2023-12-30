using UnityEngine;

namespace Darkcast.Items
{
    [CreateAssetMenu(menuName = "Darkcast/Fuel", fileName = "New Fuel", order = 1)]
    public class Fuel : Item
    {
        [SerializeField] private int _burnTime;

        public float burnTime => _burnTime;
    }
}