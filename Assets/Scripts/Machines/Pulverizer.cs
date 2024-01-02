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

        public Inventory input = new(10);

        public Inventory output = new(10);
        
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

                // Check if the inventory contains all the ingredients for the selected recipe.
                // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
                foreach (var ingredient in _selectedRecipe.ingredients)
                {
                    if (!input.Contains(ingredient.item, ingredient.count))
                    {
                        // Our input inventory doesn't have everything we need to keep working. We're done.
                        return;
                    }
                }

                // Remove all the ingredients from from the inventory for the selected recipe.
                foreach (var ingredient in _selectedRecipe.ingredients)
                {
                    // Remove the items from the inventory.
                    input.Remove(ingredient.item, ingredient.count);
                }

                // Our input inventory has everything we need to keep working on this recipe.
                _currentRecipe = _selectedRecipe;
                _energyNeededToCompleteCurrentRecipe = _currentRecipe.energy;
            }

            // Work on the current recipe.
            _energyNeededToCompleteCurrentRecipe -= 20;
            
            // Check if the current recipe was completed.
            if (_energyNeededToCompleteCurrentRecipe == 0)
            {
                // Store all the resulting items in the output inventory.
                foreach (var result in _currentRecipe.output)
                {
                    output.Store(result.item, result.count);
                }

                // Mark the current recipe as completed and reset it for the next tick.
                _currentRecipe = null;
            }
        }
    }
}