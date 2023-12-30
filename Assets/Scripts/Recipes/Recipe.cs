using System.Collections.Generic;
using UnityEngine;

namespace Darkcast.Recipes
{
    [CreateAssetMenu(menuName = "Darkcast/Recipes/Recipe", fileName = "New Recipe", order = 1)]
    public sealed class Recipe : ScriptableObject
    {
        [SerializeField] private int _energy;

        [SerializeField] private List<Ingredient> _input;

        [SerializeField] private List<Ingredient> _output;

        public int energy => _energy;
        
        public List<Ingredient> input => _input;

        public List<Ingredient> output => _output;
    }
}