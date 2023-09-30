using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MushroomGame
{
    public class Agent : MonoBehaviour
    {
        [SerializeField]
        protected int health;
        [SerializeField]
        protected int damage;

        virtual public void TakeDamage(int damage)
        {
            health -= damage;
            Debug.Log("Agent taking damage. Health remaining: " + health);
        }

        public int GetHealth()
        {
            return health;
        }

        public int GetDamage()
        {
            return damage;
        }
    }
}
