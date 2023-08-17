using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigPress : MonoBehaviour // : InitializeBase
{
  

    public bool PressUpon;
    public float dtime;
    public float fStartTime = 2f;
    public float fSpeed = 1f;

    Vector3 StartPosion;
    // Use this for initialization

/*   public override void InitializeStart()
   {
       transform.position = StartPosion;
       dtime = 0f;        // GS가 플레이 3초 지나면 움직인가  
   }
*/

    void Awake()
    {
        StartPosion = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
      //  if (GameManager.me.GS == GameState.Play)
        {
            if (dtime < fStartTime)
            {
                dtime += Time.deltaTime;
                PressUpon = false;
            }
            else if (dtime > fStartTime)
                PressUpon = true;


            if (PressUpon)
            {
               if (transform.position.y < 3.0f)
                 transform.Translate(Vector2.up * fSpeed * Time.deltaTime);
              
                

            }
        }

    }
}



