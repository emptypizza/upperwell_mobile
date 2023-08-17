using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{

   // public Button[] buttons;
    public Image Creditimg;


    // Start is called before the first frame update
    void Start()
    {

        Creditimg.gameObject.SetActive(false);
        Creditimg.transform.position = new Vector3(540, 850);
        /*
        buttons = new Button[3];
        buttons[0] = GameObject.Find("Button_start").GetComponent<Button>();
        buttons[1] = GameObject.Find("Button_credit").GetComponent<Button>();
        buttons[2] = GameObject.Find("Button_exit").GetComponent<Button>();
        */
    }




    public int nSelect = -1; //버튼을 누를 경우 정수 값을 주는 필수  이 값으로 Update에서 계속 실행되며 Switch로 값이 바뀔 경우 버튼을 누른경우 Update-switch문으/ 실행



    private void Update()
    {

        switch(nSelect)
        {

           case 1:
            UnityEditor.SceneManagement.EditorSceneManager.LoadScene(1);
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

}

    public void executeGame()
    {

       // UnityEditor.SceneManagement.EditorSceneManager.LoadScene("u_level0");

        nSelect = 1;
 
       

    }



    public void credit()
    {
        //Creditimg.gameObject.SetActive(true);
        nSelect = 2;
        

    }

    public void exitfromcredit()
    {
        //Creditimg.gameObject.SetActive(false);
        nSelect = 3;

    }

    public void exit()
    {


        nSelect = 4;
      //  Application.Quit();



    }

}
