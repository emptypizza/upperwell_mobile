using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



public class ballrumble : MonoBehaviour
{
    // Public Variables
    public Vector2 gotopositon;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public float Dash_fDeltatime;
    public float fDelayDuration = 0.5f;
    public bool bJumpOK = false;
    public float fJumpPower = 30;
    public TextMesh Textjumpdelay;
    public TextMesh TextRbRot;
    public TextMesh TextnHP;
    public Animator animator;
    public GameObject Aim;
    public DynamicJoystick joystick;
    public bool bJoysitck_contorl;
    public float fPCSpeed = 0.2f;
    public List<GameObject> list_ComboStack;
    public float fComboTime = 0;
    public const float fMaxComboTime = 10;
    public int nHP;
    public float comboGauge = 1.0f;
    public int comboStack = 0;
    private float decreaseRate = 0.1f / 3600f;
    public GameObject missilePrefab;
    public Transform missileSpawnPoint;
    public float missileForce = 10f;
    public bool bCharInterpolationOn = false;
    public float rotationAmount = 1.0f;
    public float interpolationSpeed = 15f;

    // Private Variables
    private float fFillTimer = 0f;
    private float fillDuration = 1.5f;
    private float alphaIncreaseRate = 1f;
    private float currentAlpha = 0f;
    private int touchId = -1;
    float moveInput_keyboard;
    private bool isGrounded;
    public bool bIsDead = false;

    void Start()
    {
        //GameManager.Instance.ballrumble_pc = this;
        joystick = GameObject.FindObjectOfType<DynamicJoystick>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Dash_fDeltatime = 0;
        nHP = 4;
    }

    void ResetGame()
    {
        Dash_fDeltatime = 0;
    }

    public void DamageCombo()
    {
        if (list_ComboStack.Count > 0)
            list_ComboStack.RemoveAt(list_ComboStack.Count - 1);
        else if (list_ComboStack.Count <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        bIsDead = true;
        GameManager.nLevel = 1;
        SceneManager.LoadScene(1);// Level1 돌아간. 게임 오버 처리 구현 할것
    }

    public void OnShot()
    {
        if (bJumpOK)
        {

            SoundManager.Instance.Fire();
            Vector2 dic_aim = Aim.transform.position - this.transform.position;
           // dic_aim = -dic_aim;
         

            GameObject missile = Instantiate(missilePrefab, missileSpawnPoint.position, Quaternion.identity);
            Rigidbody2D missileRb = missile.GetComponent<Rigidbody2D>();
          //  Vector2 shootDirection = transform.Find("DIO").up;
            rb.AddForce(-dic_aim.normalized * fJumpPower, ForceMode2D.Impulse);
            missileRb.AddForce(dic_aim.normalized * missileForce, ForceMode2D.Impulse);

            Dash_fDeltatime = 0;
            fFillTimer = 0f;
            bJumpOK = false;
        }
    }



    public void Rotationinterpolation()
    {
        Vector2 raycastOrigin = rb.position;
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.down, 1.0f, LayerMask.GetMask("Ground"));
        isGrounded = (hit.collider != null);

        if (isGrounded)
        {
            float targetAngle = 0f;
            float angleDifference = Mathf.DeltaAngle(rb.rotation, targetAngle);
            rb.rotation += angleDifference * interpolationSpeed * Time.fixedDeltaTime;
        }
    }



  
    void FixedUpdate()// Upate()
    {

 
        //nHP = list_ComboStack.Count;
        // HandleComboDisplay();
        if (bCharInterpolationOn)
            Rotationinterpolation();

        TextRbRot.text = this.rb.rotation.ToString("F1");
        Textjumpdelay.text = Dash_fDeltatime.ToString("F2");
        TextnHP.text = this.nHP.ToString();


   

        if (nHP <= 0)
        {
            bIsDead = true;
            SoundManager.Instance.Playerdie();
        }
           
     

        if (bIsDead)
            Dead();

        // 키보드 입력 처리
        moveInput_keyboard = Input.GetAxisRaw("Horizontal");
        Vector2 dir = new Vector2(moveInput_keyboard, 0);
        transform.position += (Vector3)dir * fPCSpeed * Time.deltaTime;
        rb.AddTorque(-moveInput_keyboard * fPCSpeed, ForceMode2D.Force);
        if (moveInput_keyboard != 0)
            transform.Find("DIO").transform.Rotate(0, 0, -3 * moveInput_keyboard);

        // 조이스틱 입력 처리
        float horizontalInput = joystick.Horizontal;
        if (horizontalInput != 0)
            transform.Find("DIO").transform.Rotate(0, 0, rotationAmount * horizontalInput);


        /*
        moveInput_keyboard = Input.GetAxisRaw("Horizontal");
        Vector2 dir = new Vector2(moveInput_keyboard, 0);

     

        if (bJoysitck_contorl == false)
        {
            bJumpOK = true;
            fDelayDuration = 0f;
            transform.position += (Vector3)dir * fPCSpeed * Time.deltaTime;
            rb.AddTorque(-moveInput_keyboard * fPCSpeed, ForceMode2D.Force);
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            if (horizontalInput != 0)
                transform.Find("DIO").transform.Rotate(0, 0, -3 * horizontalInput);
        }
        else if (bJoysitck_contorl == true)
        {

          
            float horizontalInput = joystick.Horizontal;
            if (horizontalInput != 0)
                transform.Find("DIO").transform.Rotate(0, 0, rotationAmount * horizontalInput);
            
        }
        */




        if (bJumpOK == false)
        {
            Dash_fDeltatime += Time.deltaTime;
            if (Dash_fDeltatime >= fDelayDuration)
                bJumpOK = true;

        }
        //HandleMovement();
    }

    void Update()// Upate()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // (bJumpOK == true) &&
        {
            /*
            Vector2 dic_aim = Aim.transform.position - this.transform.position;
            // dic_aim = -dic_aim;


            GameObject missile = Instantiate(missilePrefab, missileSpawnPoint.position, Quaternion.identity);
            Rigidbody2D missileRb = missile.GetComponent<Rigidbody2D>();    //  Vector2 shootDirection = transform.Find("DIO").up;
            rb.AddForce(-dic_aim.normalized * fJumpPower, ForceMode2D.Impulse);
            missileRb.AddForce(dic_aim.normalized * missileForce, ForceMode2D.Impulse);

            Dash_fDeltatime = 0;
            fFillTimer = 0f;
            bJumpOK = false;
            */
            OnShot();

        }
    }

        private void HandleComboDisplay()
    {
        switch (nHP)
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
    }

    private void HandleMovement()
    {
        moveInput_keyboard = Input.GetAxisRaw("Horizontal");
        Vector2 dir = new Vector2(moveInput_keyboard, 0);

        if (bIsDead)
            SceneManager.LoadScene(1);// 게임 오버 처리 구현 할것

        if (bJumpOK == false)
        {
            Dash_fDeltatime += Time.deltaTime;
            if (Dash_fDeltatime >= fDelayDuration)
                bJumpOK = true;

            
        }

        if (bJoysitck_contorl == false)
        {
            transform.position += (Vector3)dir * fPCSpeed * Time.deltaTime;
            rb.AddTorque(-moveInput_keyboard * fPCSpeed, ForceMode2D.Force);
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            if (horizontalInput != 0)
                transform.Find("DIO").transform.Rotate(0, 0, rotationAmount * horizontalInput);
        }
        else
        {
            float horizontalInput = joystick.Horizontal;
            if (horizontalInput != 0)
                transform.Find("DIO").transform.Rotate(0, 0, rotationAmount * horizontalInput);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //HandleCollision(collision);
        Wall wall = collision.gameObject.GetComponent<Wall>();
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (wall != null)
        {
            if (collision.gameObject.CompareTag("wall_good"))
            {
                wall.n_hp -= 1;
                GameManager.Instance.AddScore();

                // anim.SetTrigger("ani_dash");

                rb.AddRelativeForce(Vector2.up * 10f);
            }

            if (collision.gameObject.CompareTag("wall_no"))
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
                transform.Find("DIO").gameObject.GetComponent<SpriteRenderer>().color = Color.black;
                nHP -= 1;
               // DamageCombo();
            }

        }
        if(enemy != null)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                nHP -= 1;
            }

        }
    }


    [SerializeField] float BlueBoomValue = 8.7f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Wall wall = collision.gameObject.GetComponent<Wall>();

        if (collision.gameObject.tag == "Finish")
        {
            GameManager.Instance.GS = GameState.Clear;
            

            SoundManager.Instance.Goalin();//  GameManager.Instance.Clear();
        }

        if (collision.gameObject.tag == "wall_good")        //    HandleTrigger(collision);
        {
            Quaternion rotation = Quaternion.Euler(260, 0, 0);
            // "BlueBoom" 이펙트 프리팹을 Instantiate합니다.
            GameObject blueBoom = Instantiate(Resources.Load("BlueBoom") as GameObject, collision.transform.position, rotation);

            // 여기서는 이펙트가 자동으로 사라지지 않는다면 몇 초 후에 파괴하는 코드를 추가할 수 있습니다.
            // 예: Destroy(blueBoom, 3.0f); // 3초 후에 파괴

            rb.AddForce(Vector2.up * BlueBoomValue, ForceMode2D.Impulse);
            SoundManager.Instance.WarpUp();
            wall.n_hp -= 1;

            //rb.AddRelativeForce(Vector2.up * 10f);
     
        }
       


        
    }

    private void HandleCollision(Collision2D collision)
    {
        Wall wall = collision.gameObject.GetComponent<Wall>();

        if (wall != null)
        {
            if (collision.gameObject.CompareTag("wall_good"))
            {
                comboStack++;
                comboGauge += decreaseRate * 3600f * comboStack;
                comboGauge = Mathf.Clamp(comboGauge, 0f, 1f);
                //  Debug.LogWarning("아wall_goodwall_goodwall_good ");
                GameObject combo = Instantiate(Resources.Load("combo") as GameObject);

                if (combo == null)
                    Debug.LogError("이상하다다다다다다");


                combo.transform.SetParent(this.transform, false);
                list_ComboStack.Add(combo);

               // wall.GetComponent<Animator>().SetTrigger("isHit");
            }
        }
    }

    private void HandleTrigger(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("wall_no"))
        {
            bIsDead = true;
        }
    }
}
