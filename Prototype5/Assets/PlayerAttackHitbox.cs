using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MushroomGame;

public class PlayerAttackHitbox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            collision.gameObject.GetComponent<Agent>().TakeDamage(1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            collision.gameObject.GetComponent<Agent>().TakeDamage(1);
        }
    }
}
