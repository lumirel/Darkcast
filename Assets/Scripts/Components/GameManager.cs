using UnityEngine;
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

            foreach (var input in selectedRecipe.ingredients)
            {
                _pulverizer.input.Store(input.item, input.count * 3);
            }
        }

        private void FixedUpdate()
        {
            _pulverizer.Tick();
        }
    }
}