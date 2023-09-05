using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public ballrumble ballrumble_PC; // Modified to BallRumble
    public Text pcHPText; // Modified to pcRotText
    public Text gameCurrentScore;
    public Text gameBestScore;
    public Button shotButton; // Modified to shotButton
    public Button clearButton;
    public Button gameoverButton;
    public Image clearImage; // Modified to clearImage
    public Image gameoverImage; 

    void Start()
    {
       // GameManager.Instance.UIManager = this;
        ballrumble_PC = GameObject.FindObjectOfType<ballrumble>();

        shotButton = transform.Find("Shot_Button").gameObject.GetComponent<Button>();
        clearButton = transform.Find("clearimg").gameObject.GetComponentInChildren<Button>();
        gameoverButton = transform.Find("gameoverimg").gameObject.GetComponentInChildren<Button>();


        shotButton.onClick.AddListener(ballrumble_PC.OnShot);
        clearButton.onClick.AddListener(GameManager.Instance.ClearGame);
        gameoverButton.onClick.AddListener(GameManager.Instance.GameOver);

        clearImage.transform.position = new Vector3(580, 1180);
        clearImage.gameObject.SetActive(false);

        gameoverImage.transform.position = new Vector3(580, 1180);
        gameoverImage.gameObject.SetActive(false);
    }

    void Update()
    {

        pcHPText.text = ballrumble_PC.nHP.ToString();//pcRotText.text = ballrumble_PC.GetNHP().ToString();

        gameCurrentScore.text = GameManager.Instance.nGameScore_current.ToString(); // Used a method to get current score
        gameBestScore.text = GameManager.Instance.nGameScore_Best.ToString();
       // gameBestScore.text = GameManager.Instance.GetGameScoreBest().ToString(); // Used a method to get best score

      
        if(ballrumble_PC.bJumpOK)    // if (ballrumble_PC.IsJumpOK()) 
        {
            shotButton.gameObject.GetComponent<Image>().color = Color.cyan;
        }
        else
        {
            shotButton.gameObject.GetComponent<Image>().color = Color.red;
        }
    }

    public void GameClear()
    {
        Time.timeScale = 0f;
        clearImage.gameObject.SetActive(true);
        
    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        gameoverImage.gameObject.SetActive(true);
       
    }
}

