using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MushroomGame {
public class Agent: MonoBehaviour
{
    [SerializeField]
    protected int health;
    [SerializeField]
    protected int damage;

    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(this.gameObject);
        }
    }

    public int GetHealth() {
        return health;
    }

    public int GetDamage() {
        return damage;
    }
}

}
