using System;
using UnityEngine;
using Darkcast.Items;

namespace Darkcast.Recipes
{
    [Serializable]
    public struct Ingredient
    {
        [SerializeField] private Item _item;

        [SerializeField] private int _count;

        public Item item => _item;

        public int count => _count;
    }
}