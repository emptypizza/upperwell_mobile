using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

   // public Button[] buttons;
    public Image Creditimg;
    public AudioClip btnpress1;
    public AudioClip btn;
   

    public AudioClip buttonClickSound;

    public AudioSource audioSource;

    public int nSelect = -1; //버튼을 누를 경우 정수 값을 주는 필수  이 값으로 Update에서 계속 실행되며 Switch로 값이 바뀔 경우 버튼을 누른경우 Update-switch문으/ 실행




    // Start is called before the first frame update
    void Start()
    {

        audioSource = GetComponent<AudioSource>();

        //Creditimg.transform.position = Vector3.up*200;
        Creditimg.gameObject.SetActive(false);

        /*
        buttons = new Button[3];
        buttons[0] = GameObject.Find("Button_start").GetComponent<Button>();
        buttons[1] = GameObject.Find("Button_credit").GetComponent<Button>();
        buttons[2] = GameObject.Find("Button_exit").GetComponent<Button>();
        */
    }





    public void executeGame()
    {

        audioSource.PlayOneShot(btnpress1);


        SceneManager.LoadScene(1);
        nSelect = 1;



    }



    public void credit()
    {
        audioSource.PlayOneShot(buttonClickSound);

        Creditimg.gameObject.SetActive(true);
        nSelect = 2;
        

    }

    public void exitfromcredit()
    {


        audioSource.PlayOneShot(buttonClickSound);

        Creditimg.gameObject.SetActive(false);
        nSelect = 3;

    }

    public void exit()
    {

        audioSource.PlayOneShot(btn);

        nSelect = 4;
        Application.Quit();



    }

    /*
    
    private void OnEnable()
        {
            _root = uiDocument.rootVisualElement.Q<VisualElement>(ROOT_Q);
 
            _root.RegisterCallback<ClickEvent>(OnPress, TrickleDown.TrickleDown);
        }
 
        private void OnDisable()
        {
            _root.UnregisterCallback<ClickEvent>(OnPress, TrickleDown.TrickleDown);
        }
 
        private void OnPress(EventBase evt)
        {
            if (evt.target is Button)
            {
                pressSound.Play();
            }
        }
    private void Update()
    {

        switch(nSelect)
        {

           case 1:
                SceneManager.LoadScene(1);
           break;

           case 2:
                Creditimg.gameObject.SetActive(true);
                break;


          case 3:
                Creditimg.gameObject.SetActive(false);
                break;

          case 4:
                Application.Quit();
          break;
            

    }

}*/

}
