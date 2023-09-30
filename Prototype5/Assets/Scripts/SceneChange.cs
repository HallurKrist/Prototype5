using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void GoToGameScene()
    {
        SceneManager.LoadScene (sceneName:"Test");
    }

    public void GoToStartScene()
    {
        SceneManager.LoadScene (sceneName:"StartScreen");
    }

    public void GoToEndScene()
    {
        SceneManager.LoadScene (sceneName:"EndScreen");
    }
}
