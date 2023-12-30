using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Darkcast.Recipes
{
    [Serializable]
    public struct Recipe
    {
        [SerializeField] private int _energy;

        [ReorderableList]
        [SerializeField] private List<Ingredient> _input;

        [SerializeField] private List<Ingredient> _output;

        public int energy => _energy;
        
        public List<Ingredient> input => _input;

        public List<Ingredient> output => _output;
    }
}