using UnityEngine;
using Darkcast.Items;
using Darkcast.Machines;

namespace Darkcast.Components
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField] private Pulverizer _pulverizer;

        private void Awake()
        {
            var selectedRecipe = _pulverizer.cookbook.recipes[0];
            _pulverizer.SelectRecipe(selectedRecipe);

            foreach (var input in selectedRecipe.input)
            {
                var inputItems = new ItemStack(input.item, input.count);
                _pulverizer.inputInventory.Store(ref inputItems);
            }
        }

        private void FixedUpdate()
        {
            _pulverizer.Tick();
        }
    }
}