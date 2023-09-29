using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class MenuScript : MonoBehaviour
{

    public string sceneName;
    public List<GameObject> menuObjects;
    public string UItag = "UI Element";
    public bool startFadeButtons = false;
    public bool startFadeScreen = false;
    public float fadeSpeed;
    public GameObject Player;
    public bool playerMove = false;
    public float moveSpeed = 1;
    public float playerPos;
    public int fadestartPoint = -6;
    public GameObject fadePanel;

    // Start is called before the first frame update
    void Start()
    {


        
        foreach (GameObject UIelement in GameObject.FindGameObjectsWithTag(UItag)) menuObjects.Add(UIelement);

        Debug.Log(menuObjects);
        
        


    }

    // Update is called once per frame
    void Update()
    {
        if(startFadeButtons == true) { 
        foreach(GameObject UIelement in menuObjects)
        {
                
                Color newColor = UIelement.GetComponent<Image>().color;  
                float fadeAmount = newColor.a - (fadeSpeed * Time.deltaTime);
                newColor = new Color(newColor.r, newColor.g, newColor.b, fadeAmount);
                UIelement.GetComponent<Image>().color = newColor;
                UIelement.GetComponentInChildren<TextMeshProUGUI>().color = newColor;
        }
        }
        if (playerMove)
        {
            playerPos = Player.transform.position.x;
            playerPos -= moveSpeed * Time.deltaTime;
            Player.transform.position = new Vector3(playerPos,Player.transform.position.y);
        }
        if (startFadeScreen)
        {
            
            Color fadeColor = fadePanel.GetComponent<Image>().color;
            float fadeAmount = fadeColor.a + (fadeSpeed * Time.deltaTime);
            fadeColor = new Color(fadeColor.r, fadeColor.g, fadeColor.b, fadeAmount);
            fadePanel.GetComponent<Image>().color = fadeColor;
        }
        float fadePanelAlpha = fadePanel.GetComponent<Image>().color.a;
        if (fadePanelAlpha >= 1)
        {
            Debug.Log("fade away wowowowoow");
            SceneManager.LoadScene(sceneName);
        }
        Debug.Log(fadePanelAlpha);
    }
    public void StartGamePress() {

        startFadeButtons = true;
        playerMove = true;

        if(Player.transform.position.x <= fadestartPoint)
        {
            Debug.Log("we out here");
            fadePanel.SetActive(true);
            startFadeScreen = true;
            

        }
    }
}
