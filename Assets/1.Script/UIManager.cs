using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{


    public ballrumble ballrumble_PC;
    public Text pc_rot_text;
    public Text tGameCurrentScore;
    public Text tGameBestScore;
    public Text tPCgotScore;
    public Button Shot_Button;
    public Image Clear;


    // Start is called before the first frame update
    void Start()
    {
        ballrumble_PC = GameObject.FindObjectOfType<ballrumble>();
        Shot_Button = transform.Find("Shot_Button").gameObject.GetComponent<Button>();
        Clear.transform.position = new Vector3(580, 1080);
        Clear.gameObject.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
        pc_rot_text.text = ballrumble_PC.nHP.ToString();


    
        tGameCurrentScore.text = GameManager.current.nGameScore_current.ToString();
        tGameBestScore.text = GameManager.current.nGameScore_Best.ToString();

        if (ballrumble_PC.bJumpOK == true)
            Shot_Button.gameObject.GetComponent<Image>().color = Color.cyan;
        else
            Shot_Button.gameObject.GetComponent<Image>().color = Color.red;

    }


    public void GameClear()
    {

        Time.timeScale = 0.1f;
        Clear.gameObject.SetActive(true);
     
    }



}
