using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballrumble : MonoBehaviour
{

    public Vector2 gotopositon;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    public float Dash_fDeltatime;
    public float fDelayDuration = 1.5f;
    public bool bJumpOK = false;
    public float fJumpPower = 30;


    private float fFillTimer = 0f; // Timer for tracking fill duration
    private float fillDuration = 1.5f; // Time in seconds for the alpha value to fill up
    private float alphaIncreaseRate = 1f; // Rate at which the alpha value increases per second
    private float currentAlpha = 0f; // Current alpha value



    public TextMesh Textjumpdelay;
    public TextMesh TextRbRot;

    public Animator animator;

   
    float moveInput_keyboard;
    private bool isGrounded;
    public bool bIsDead = false;


    private int touchId = -1;


    public GameObject Aim;

    public DynamicJoystick joystick; // 기존 PACSHOT에서 쓰던 Joystick 그대로 가져옴

    public bool bJoysitck_contorl;  // 가상조이스틱을 쓸건지 물어보는 불변
    public float fPCSpeed = 0.2f;

    public List<GameObject> list_ComboStack;
    public float fComboTime = 0;
    public const float fMaxComboTime = 10;
    public int nHP;



    public float comboGauge = 1.0f; // 콤보 게이지의 현재 값
    public int comboStack = 0; // 누적된 콤보 스택
    private float decreaseRate = 0.1f / 3600f; // 시간당 0.1 감소


  
    public GameObject missilePrefab; // 미사일 프리팹
    public Transform missileSpawnPoint; // 미사일이 발사될 위치
    public float missileForce = 10f; // 미사일이 받을


    public bool bCharInterpolationOn = false;

    public float rotationAmount = 1.0f;

    void Combostack_Update()
    {
        // 경과 시간을 기반으로 콤보 게이지 감소
        comboGauge -= decreaseRate * Time.deltaTime;

        // 값이 0과 1 사이가 되도록 조정
        comboGauge = Mathf.Clamp(comboGauge, 0, 1);

        // 콤보 게이지가 가득 찼는지 확인
        if (comboGauge >= 1)
        {
            // 콤보 스택 증가
            comboStack++;

            // 콤보 게이지 초기화
            comboGauge = 0;
        }

        // 콤보 게이지와 스택과 관련된 게임플레이 효과를 처리하기 위한 로직을 여기에 추가할 수 있습니다
    }


// Start is called before the first frame update
void Start()
    {

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        //   float fStartSpeed = 3;
        //  rb.velocity = new Vector2(fStartSpeed, 0);
        //  list_ComboStack = new List<GameObject>();
        Dash_fDeltatime = 0;
     

        //Aim = transform.GetComponentsInChildren<>
    }


void ResetGame()
{
        Dash_fDeltatime = 0;
}


    public void DamageCombo()
    {
        if (list_ComboStack.Count > 0)
        {
            list_ComboStack.RemoveAt(list_ComboStack.Count - 1);

            //animator.SetTrigger("ani_dead");
        }

        else if (list_ComboStack.Count <= 0)
        {
            Dead();
        }

    }

 public void Dead()
 {
        //animator.SetTrigger("ani_dead");
        bIsDead = true;

 }
    private void OnCollisionEnter2D(Collision2D collision) //
    {
        Wall wall = collision.gameObject.GetComponent<Wall>();


        if (wall != null)
        {
            if (collision.gameObject.CompareTag("wall_good"))
            {


                wall.n_hp -= 1;
                GameManager.current.AddScore();

                // anim.SetTrigger("ani_dash");

                rb.AddRelativeForce(Vector2.up * 10f); 
            }

            if (collision.gameObject.CompareTag("wall_no"))
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
                transform.Find("DIO").gameObject.GetComponent<SpriteRenderer>().color = Color.black;
                Dead();
                DamageCombo();
            }

        

        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Wall wall = collision.gameObject.GetComponent<Wall>();


        if (collision.gameObject.tag == "Finish")
        {
            GameManager.current.GS = GameState.Clear;
          //  GameManager.current.Clear();
        }

        if (collision.gameObject.tag == "wall_good")
        {
            //rb.AddRelativeForce(Vector2.up * 10f);
            rb.AddForce(Vector2.up * 1.3f, ForceMode2D.Impulse);

            wall.n_hp -= 1;
        }

    }


    public void OnShot() // CHATGPT4
    {
         if (bJumpOK == true)// && isGrounded)
         {
        // Calculate the direction aiming from the object to the Aim
        Vector2 dic_aim = Aim.transform.position - this.transform.position;

            // Reverse the direction of dic_aim
            dic_aim = -dic_aim;

            rb.AddForce(dic_aim.normalized * fJumpPower, ForceMode2D.Impulse); //this.rb.velocity = new Vector2(forwardSpeed, 0);
                                                                               // animator.SetTrigger("ani_fly");
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;

            Dash_fDeltatime = 0;
            fFillTimer = 0f;
            bJumpOK = false;
            FireMissile(); // 미사일 발사 함수 호출 call the missile launch function
        }
    }


    private void FireMissile()
    {
      
        // 미사일 프리팹 인스턴스화
        GameObject missile = Instantiate(missilePrefab, missileSpawnPoint.position, Quaternion.identity);
        // 미사일에 Rigidbody2D 컴포넌트 가져오기 (미사일 프리팹에 Rigidbody2D가 있어야 함)
        Rigidbody2D missileRb = missile.GetComponent<Rigidbody2D>();
        // 아래 방향으로 미사일에 힘 가하기
        // "DIO" 오브젝트의 회전각도에 따라 미사일을 발사하도록 수정
        Vector2 shootDirection = transform.Find("DIO").up;
        missileRb.AddForce(shootDirection * missileForce, ForceMode2D.Impulse);
    }


    public float interpolationSpeed = 15f;
    // 보간 속도를 조정합니다. (보간 속도를 높이면 더 빠르게 각도가 0으로 보간됩니다.)


    public void Rotationinterpolation()
    {
        // rigidbody의 위치에 땅을 검사할 레이캐스트를 발사합니다.
        Vector2 raycastOrigin = rb.position;
        //  RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.down, 1.0f, CompareTag("Wall");
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.down, 1.0f, LayerMask.GetMask("Ground"));

        // 땅에 닿아 있는지 여부를 판단합니다.
        isGrounded = (hit.collider != null);

        // 만약 땅에 닿아 있다면, rigidbody의 각도를 0으로 보간합니다.
        if (isGrounded)
        {   

        // 충돌한 오브젝트의 태그가 "wall"인지 확인합니다.
       
            // 보간 시 사용할 각도 (0으로 보간하려면 해당 값이 0이 되도록 설정합니다).
            float targetAngle = 0f;

            // 보간할 각도로 현재 각도와의 차이를 구합니다.
            float angleDifference = Mathf.DeltaAngle(rb.rotation, targetAngle);

            // 보간된 각도를 적용합니다.
            rb.rotation += angleDifference * interpolationSpeed * Time.fixedDeltaTime;
         }

    }
    public void totheleft()
    {

    
    }
    public void totheright()
    {


    }


    // Update is called once per frame
    void Update()
    {

        nHP = list_ComboStack.Count;

        switch(nHP)
        {
            case 2:
                transform.Find("combo3").gameObject.SetActive(false);
                break;


            case 1:
                transform.Find("combo2").gameObject.SetActive(false);

                break;


            case 0:
                transform.Find("combo1").gameObject.SetActive(false);
                break;


            default:
                transform.Find("combo_base").gameObject.SetActive(false);
               
                break;


        }
        //타임어택 시간10sec 지나면 피 한칸 씩 줄어드는 codes
        /*
                    if (bIsDead != true)
                    {
                        if (fComboTime < fMaxComboTime)
                            fComboTime += Time.deltaTime;

                        if (fComboTime >= fMaxComboTime && list_ComboStack.Count > 0)
                        {
                            list_ComboStack.RemoveAt(list_ComboStack.Count - 1);
                            fComboTime = 0;
                        }

                    }
        */

        if (bCharInterpolationOn)
            Rotationinterpolation();

        TextRbRot.text = this.rb.rotation.ToString();
        Textjumpdelay.text = Dash_fDeltatime.ToString("F2");
        moveInput_keyboard = Input.GetAxisRaw("Horizontal");
        Vector2 dir = new Vector2(moveInput_keyboard, 0);

        if (bIsDead == true) //만약 죽으면
            UnityEditor.SceneManagement.EditorSceneManager.LoadScene("level0");// 게임 오버 처리 구현 할것


        if (bJumpOK == false)
        {
            Dash_fDeltatime += Time.deltaTime;
            //  FillAlpha();
            if (Dash_fDeltatime >= fDelayDuration)
                bJumpOK = true;

            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }


      

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (bJumpOK == true)// && isGrounded)
            {
             
                Vector2 dic_aim = Aim.transform.position - this.transform.position;

                dic_aim = -dic_aim;

                rb.AddForce(dic_aim.normalized * fJumpPower, ForceMode2D.Impulse);
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;

                bJumpOK = false;

                GameObject missile = Instantiate(missilePrefab, missileSpawnPoint.position, Quaternion.identity);
                Rigidbody2D missileRb = missile.GetComponent<Rigidbody2D>();

                // "DIO" 오브젝트의 회전각도에 따라 미사일을 발사하도록 수정
                Vector2 shootDirection = transform.Find("DIO").up;
                missileRb.AddForce(shootDirection * missileForce, ForceMode2D.Impulse);
            }
        }
        if (bJoysitck_contorl == false)
        {        
            transform.position += (Vector3)dir* fPCSpeed * Time.deltaTime;
            rb.AddTorque(-moveInput_keyboard * fPCSpeed, ForceMode2D.Force);    // 객체를 굴리기 위해 토크 적용. 올바른 방향으로 구르기 위해 음수 값 사용

     
            float horizontalInput = Input.GetAxisRaw("Horizontal");

            // Rotate based on horizontal input
            if (horizontalInput != 0)
            {
                transform.Find("DIO").transform.Rotate(0, 0, rotationAmount * horizontalInput);
            }

        
        }

      else if (bJoysitck_contorl == true)
        { 
            rb.AddForce(new Vector2(joystick.Horizontal * fPCSpeed, 0), ForceMode2D.Force);
        }

        
        
       
    }


 
}
