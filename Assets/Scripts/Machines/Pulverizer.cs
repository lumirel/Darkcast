using UnityEngine;
using Darkcast.Items;
using Darkcast.Recipes;

namespace Darkcast.Machines
{
    public sealed class Pulverizer : MonoBehaviour
    {
        [SerializeField] private Cookbook _cookbook;

        private Recipe _activeRecipe;
        private Recipe _selectedRecipe;

        public int _energyNeededToCompleteCurrentRecipe;

        public Cookbook cookbook => _cookbook;

        public Inventory input = new();

        public Inventory output = new();

        private bool TrySetActiveRecipe()
        {
            // The machine already has an active recipe.
            if (_activeRecipe)
            {
                return true;
            }

            // If there's no active recipe and no selected recipe then there's nothing to work on.
            if (!_selectedRecipe)
            {
                return false;
            }

            // Check if the inventory contains all the ingredients for the selected recipe.
            // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
            foreach (var ingredient in _selectedRecipe.ingredients)
            {
                if (!input.Contains(ingredient.item, ingredient.count))
                {
                    // The input inventory doesn't have everything needed work on the selected active recipe.
                    return false;
                }
            }

            // Remove all the ingredients from the inventory for the selected recipe.
            foreach (var ingredient in _selectedRecipe.ingredients)
            {
                // Remove the items from the inventory.
                input.Remove(ingredient.item, ingredient.count);
            }

            // Our input inventory has everything we need to keep working on this recipe.
            _activeRecipe = _selectedRecipe;
            _energyNeededToCompleteCurrentRecipe = _activeRecipe.energy;

            return true;
        }

        private void WorkActiveRecipe()
        {
            // Work on the active recipe.
            _energyNeededToCompleteCurrentRecipe -= 20;

            // Check if the active recipe was completed.
            if (_energyNeededToCompleteCurrentRecipe > 0)
            {
                // Active recipe was not completed.
                return;
            }

            // Store all the resulting items in the output inventory.
            foreach (var result in _activeRecipe.results)
            {
                output.Store(result.item, result.count);
            }

            // Reset the active recipe for the next tick.
            _activeRecipe = null;
        }

        public void SelectRecipe(Recipe recipe)
        {
            _selectedRecipe = recipe;
        }

        public void Tick()
        {
            if (TrySetActiveRecipe())
            {
                WorkActiveRecipe();
            }
        }
    }
}