using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyKey : MonoBehaviour
{
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
    }
}