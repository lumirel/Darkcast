using UnityEngine;
using Darkcast.Items;
using Darkcast.Recipes;

namespace Darkcast.Machines
{
    public sealed class Pulverizer : MonoBehaviour
    {
        [SerializeField] private Cookbook _cookbook;

        private Recipe _currentRecipe;
        private Recipe _selectedRecipe;

        public int _energyNeededToCompleteCurrentRecipe;

        public Cookbook cookbook => _cookbook;

        public Inventory input { get; } = new(10);

        public void SelectRecipe(Recipe recipe)
        {
            _selectedRecipe = recipe;
        }

        public void Tick()
        {
            // If the machine does not have a recipe to work on then try and work on new recipe.
            if (!_currentRecipe)
            {
                // If we don't have a selected recipe then we have nothing to work on.
                if (!_selectedRecipe)
                {
                    return;
                }

                // Check if the selected recipe can be worked on.
                var ingredient = _selectedRecipe.input[0];

                var itemStack = new ItemStack(ingredient.item, ingredient.count);

                if (input.Contains(itemStack))
                {
                    // Our input inventory has everything we need to keep working on this recipe.
                    _currentRecipe = _selectedRecipe;
                    _energyNeededToCompleteCurrentRecipe = _currentRecipe.energy;
                    
                    // TODO Remove the items from the inventory.
                }
                else
                {
                    // Our input inventory doesn't have everything we need to keep working. We're done.
                    return;
                }
            }

            // Work on the current recipe.
            _energyNeededToCompleteCurrentRecipe -= 20;
            
            // Check if the current recipe was completed.
            if (_energyNeededToCompleteCurrentRecipe == 0)
            {
                // Mark the current recipe as completed and reset it for the next tick.
                _currentRecipe = null;
            }
        }
    }
}