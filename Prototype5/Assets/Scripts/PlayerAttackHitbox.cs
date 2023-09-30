using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MushroomGame;

public class PlayerAttackHitbox : MonoBehaviour
{
    private int damage;

    void Start()
    {
        damage = transform.parent.gameObject.GetComponent<Agent>().GetDamage();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            collision.gameObject.GetComponent<Agent>().TakeDamage(damage);
        }
    }
}
