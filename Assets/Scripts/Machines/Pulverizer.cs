using UnityEngine;
using Darkcast.Items;

namespace Darkcast.Machines
{
    public sealed class Pulverizer : MonoBehaviour
    {
        private float _fuelTime;

        public bool isOn { get; private set; }

        public bool isRunning { get; private set; }

        public ItemStack fuelSlot { get; private set; }

        public float runTime { get; private set; }

        public void TurnOn()
        {
            isOn = true;
        }

        public void TurnOff()
        {
            isOn = false;
        }

        public void Tick(float deltaTime)
        {
            if (fuelSlot.isEmpty)
            {
                isRunning = false;
                return;
            }

            if (fuelSlot.item is not Fuel)
            {
                isRunning = false;
                return;
            }

            fuelSlot.Split(1);
            isRunning = true;
        }
    }
}