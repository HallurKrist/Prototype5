using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PressAnyKey : MonoBehaviour
{
    // Setting fading variables
    bool fading = true;
    public float MinFade = 0.5f;
    public float FadeSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Detect if the player presses any button
        if(Input.anyKey) {
            // For now, "test" is the scene to load
            SceneManager.LoadScene("test");
        }

        // Fetch current color property of the Sprite Renderer component
        Color clr = GetComponent<SpriteRenderer>().color;
        if(fading) {
            clr.a -= FadeSpeed * Time.deltaTime;
            if(clr.a <= MinFade) {
                fading = false;
            } else {
                // Apply the update to alpha
                GetComponent<SpriteRenderer>().color = clr;
            }
        } else {
            clr.a += FadeSpeed * Time.deltaTime;
            if(clr.a >= 1f) {
                fading = true;
            } else {
                // Apply the update to alpha
                GetComponent<SpriteRenderer>().color = clr;
            }
        }
    }
}