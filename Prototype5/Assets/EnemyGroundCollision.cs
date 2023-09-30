using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundCollision : MonoBehaviour
{
    private bool isGrounded;
    public LayerMask groundLayer; // Assign the ground layer in the Unity Inspector

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = true;
    }

    public bool GetIsGrounded() {return isGrounded;}

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("Collision ground");
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = true;
        }
    }

      private void OnCollisionExit2D(Collision2D collision)
    {
        // Reset the grounded state when leaving the collision with an object on the ground layer
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false;
        }
    }


}
