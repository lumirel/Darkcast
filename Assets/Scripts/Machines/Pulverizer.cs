using UnityEngine;
using Darkcast.Recipes;

namespace Darkcast.Machines
{
    public sealed class Pulverizer : MonoBehaviour
    {
        [SerializeField] private Cookbook _cookbook;

        public Cookbook cookbook => _cookbook;
    }
}