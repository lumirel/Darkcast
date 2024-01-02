using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Darkcast.Recipes
{
    [CreateAssetMenu(menuName = "Darkcast/Recipes/Recipe", fileName = "New Recipe", order = 1)]
    public sealed class Recipe : ScriptableObject
    {
        [SerializeField] private int _energy;

        [SerializeField] private List<Ingredient> _ingredients;

        [SerializeField] private List<Ingredient> _results;

        public int energy => _energy;
        
        public List<Ingredient> ingredients => _ingredients;

        public List<Ingredient> results => _results;
    }
}