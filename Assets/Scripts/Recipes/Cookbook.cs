using System.Collections.Generic;
using UnityEngine;

namespace Darkcast.Recipes
{
    [CreateAssetMenu(menuName = "Darkcast/Recipes/Cookbook", fileName = "New Cookbook", order = 0)]
    public sealed class Cookbook : ScriptableObject
    {
        [SerializeField] private List<Recipe> _recipes;

        public List<Recipe> recipes => _recipes;
    }
}