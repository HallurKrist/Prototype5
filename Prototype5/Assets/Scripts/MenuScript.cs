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
    public TextAsset dialogue;
    public TextMeshProUGUI dialogueTextBox;
    public List<string> DialogueLines;
    public int timesWon = 0;


    // Start is called before the first frame update
    void Start()
    {
        timesWon = PlayerPrefs.GetInt("timesWonLifetime");

        

        var content = dialogue.text;
        var AllWords = content.Split("/n");
        DialogueLines = new List<string>(AllWords);

        foreach (GameObject UIelement in GameObject.FindGameObjectsWithTag(UItag)) menuObjects.Add(UIelement);

        Debug.Log(menuObjects);

        

        if(timesWon >= 1) { 
        for(int i = 1; i < DialogueLines.Count - 1; i++)
        {
                Debug.Log(DialogueLines[i]);
                dialogueTextBox.text = DialogueLines[i];
        }
        }
        else dialogueTextBox.text = DialogueLines[0];
        Debug.Log(dialogueTextBox.text + "THIS BITCH EMPTY");



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
            timesWon = timesWon + 1;
            PlayerPrefs.SetInt("timesWonLifetime", timesWon);
        }
        

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
