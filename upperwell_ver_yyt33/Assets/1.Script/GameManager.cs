using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Resources;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;


public enum GameState
{
    Ready,
    Play,
    Pause,
    Clear,
    Gameover,
    FinalResult,
    AskEnd,
    DieDeley

}
public class GameManager : Singleton<GameManager>
{


    public static GameManager current = null;
    public static GameManager me;
    public ballrumble ballrumble_pc;
    public UIManager UIManager;

    public float LeftLimit = -22;
    public float RightLimit = 21;
    public float TopLimit = 9.8f;
    public float BottomLimit = -9.5f; 
 
    public GameState GS;

    public static int nLevel = 1;
    public static int p1Score = 0;
    public  int nGameScore_current;
    public  int nGameScore_Best;

    //도전과제용 변수
    public float fGametime; //플레이타임
    public int UseBullet;   //총알사용개수
    public int KillEnemy;   //적 사망
    public int Die;         //나 사망
    public int HiddenItems; //히든아이템 찾은 개수


    // [HideInInspector] public InitializeBase[] initBaseObjs = null;
    //최적화 변수
    public GameObject camera;
    private GameObject[] ExitEffect; // 골이펙트

    //무적모드
    private Rect PowerFullWindowRet = new Rect(Screen.width * 0.5f, Screen.height * 0.5f, 200, 200);
    private GUIStyle g = new GUIStyle();
    private float powerTextTime = 1.5f; // 화면에 표시시간
    private float powerTime;
    public int WinPlayer;




    public int nClear = 100;// y : 100


    private void Awake()
    {

        if (null == current)
            current = this;
        else
        {
            Debug.Log("Not Single GameManager");
            Destroy(gameObject);
        }

        g.fontSize = 50;
        g.normal.textColor = Color.white;
        g.alignment = TextAnchor.MiddleCenter;

        //바운더리 사이즈 세팅
        /*
        LeftLimit = GameObject.Find("Boundary(Left)").transform.position.x;
        RightLimit = GameObject.Find("Boundary(Right)").transform.position.x;
        TopLimit = GameObject.Find("Boundary(Top)").transform.position.y;
        BottomLimit = GameObject.Find("Boundary(Bottom)").transform.position.y;
        */
        /*
        if (FindObjectsOfType<ResourceManager>().Length == 0)
        {
            GameObject TempResource = new GameObject("TempResource");
            TempResource.AddComponent<ResourceManager>().Init();
        }
        // Advertisemnet.Initialize("00000"); // Unity Ads Codes
        initBaseObjs = (InitializeBase[])FindObjectsOfType(typeof(InitializeBase));
        */
        camera = GameObject.FindGameObjectWithTag("MainCamera");

       
        me = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        nGameScore_current = 0;
        nGameScore_Best = 0;


        Die = 0;    //내가 죽은 회수는 계속 누적해야 하므로.
//        ExitEffect = GameObject.FindGameObjectsWithTag("ExitEffect");
   //     if (loadingScreen == null)
   //         GameObject.Find("Lodingpage");
   //     loadingScreen.SetActive(false);
        GameInit();

    }

    public void GameInit() // 게임 리셋시, 초기화 함수
    {
        Time.timeScale = 1f;
        fGametime = 0;
        UseBullet = 0;
        KillEnemy = 0;
        HiddenItems = 0;
       /* MsgDirector.nStage_count = 0;
        nLevel = SceneManager.GetActiveScene().buildIndex;
        
     

        if (camera && camera.GetComponent<BlurBehind>().radius > 0f)
            camera.GetComponent<BlurBehind>().radius = 0f;

        if (initBaseObjs.Length > 0)
        {
            for (int cnt = 0; cnt < initBaseObjs.Length; cnt++)
                initBaseObjs[cnt].InitializeStart();
        }
       */
        //살아있는 총알들은 모두 삭제한다.
//        GameObject[] Missiles = GameObject.FindGameObjectsWithTag("Missile");
//        GameObject[] EMissiles = GameObject.FindGameObjectsWithTag("EMissile");
 /*     
        if (EMissiles.Length > 0)
        {
            for (int cnt = 0; cnt < EMissiles.Length; cnt++)

                DestroyImmediate(EMissiles[cnt]);

        }
        if (Missiles.Length > 0)
        {
            for (int cnt = 0; cnt < Missiles.Length; cnt++)
                DestroyImmediate(Missiles[cnt]);

        }
        if (ExitEffect.Length > 0)
        {
            for (int i = 0; i < ExitEffect.Length; i++)
                ExitEffect[i].SetActive(false);
        }
 */
        GS = GameState.Ready;
    }

    public void AddScore()
    {
        nGameScore_current += 1;

    }

    public void AddScore(int Score)
    {
        nGameScore_current += Score;

    }
    // Update is called once per frame
    void Update()
    {

       // if(fbird.bIsDead)          
  
        //Show bullet count on GUI
      
            switch (GameManager.me.GS)
            {
                case GameState.Ready: //150929 현재 사용하지 않는 코드
                    break;

                case GameState.Play:
                    if (fGametime < 1000)
                        fGametime += Time.deltaTime;

        

                break;

                case GameState.Pause:
          //          if (camera.GetComponent<BlurBehind>().radius < 2.1f) camera.GetComponent<BlurBehind>().radius += Time.smoothDeltaTime * 15f;
                    Time.timeScale = 0f;

                    break;

                case GameState.Clear:
                    Time.timeScale = 0.7f;
                UIManager.GameClear();
                //     float fradiusvalue = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BlurBehind>().radius;

                /*  if (camera.GetComponent<BlurBehind>().radius < 2.1f)
                      camera.GetComponent<BlurBehind>().radius += Time.smoothDeltaTime * 15f;
                   GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BlurBehind>().radius += Time.deltaTime*10;
                */

                if (Input.GetKeyDown(KeyCode.Space) ) //|| Input.GetButtonDown(btnNum))
                        Clear();
                    break;



                case GameState.Gameover:
                    Time.timeScale = 0.05f;
                  /*  if (camera.GetComponent<BlurBehind>().radius < 2.1f)
                        camera.GetComponent<BlurBehind>().radius += Time.smoothDeltaTime * 15f; */
                    if (Input.GetKeyDown(KeyCode.Space) )// || Input.GetButtonDown(btnNum))
                    {
                        Die++;
                        GameInit();
                    }
                    break;


            }

        


    }

    public void Clear()
    {
        /*
            Debug.LogFormat("Level: {0} --> {1}", nLevel - 1, nLevel);
            //nLevel++;

            int nHighestLevel = PlayerPrefs.GetInt(SaveLoadManager.CLEAR_SINGLE_STAGE_PREF_KEY);

            Debug.LogFormat("Highest Level (OLD): {0}", nHighestLevel);

            if (nLevel - 1 > nHighestLevel) // && GM == GameMode.SINGLE)
            {
                nHighestLevel = nLevel - 1;
               // PlayerPrefs.SetInt(SaveLoadManager.CLEAR_SINGLE_STAGE_PREF_KEY, nHighestLevel);
               // PlayerPrefs.Save();
                Debug.LogFormat("Highest Level updated to {0}", nHighestLevel);
                Debug.LogFormat("Trying GPGSIds.leaderboard_who_is_best_runner = {0}...", nHighestLevel);
            
                if (Application.isEditor == false)
                {
                    Social.ReportScore(nHighestLevel, GPGSIds.leaderboard_who_is_best_runner, (bool _send) =>
                    {
                        Debug.LogFormat("Trying GPGSIds.leaderboard_who_is_best_runner = {0} sent result {1}", nHighestLevel, _send);
                    });
                }
                else
                {
                    Debug.LogFormat("GPGSIds.leaderboard_who_is_best_runner = {0} (Not logged in)", nHighestLevel);
                }
            
            }
            */
            Debug.LogFormat("Loading a new level {0}...", nLevel);
            LoadLevel(nLevel);
           
        

    }

    public void LoadLevel(int nlevel)
    {

        SceneManager.LoadScene(nlevel);
      //  SceneManager.LoadScene(level[ResourceManager.Instance.levelnum++]); //임의 레벨 하고싶을때

    }
    // 게임오버를 위한 함수.
    public IEnumerator GameOver()
    {

        GS = GameState.DieDeley;
        yield return new WaitForSeconds(1.2f);
        SoundManager.me.GameOver();
        GS = GameState.Gameover;

    }

    IEnumerable SpawnLoop()
    {


        while (true)
        {
            Debug.Log("출력 ");
            yield return new WaitForSeconds(1f);

        }
    }
    IEnumerable corutineEx()
    {

        for(; ; )
        {

            yield return new WaitForSeconds(1f);
        }

       // yield return new WaitForSeconds(1f);
    }
}



