using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyKind { foot, fly, foot_guided, fly_guided, foot_amored, BOSS0 }

public class Enemy : MonoBehaviour
{
    public int nHp = 1;
    public EnemyKind enemykind;
    public float moveDistance = 4f;
    public float flyDistance = 2f;
    public float moveSpeed = 2.0f;
    private Vector3 originPosition;
    public bool bMovingRight = true;
    public bool bMovingUp = true;
    public Rigidbody2D rb;


    public float fCheckdisfordic;
    void Start()
    {
        originPosition = transform.position;
        switch (enemykind)
        {
            case EnemyKind.foot:

                nHp = 1;
           
                break;
            case EnemyKind.fly:
                nHp = 1;
                
                break;
                // Add other movement cases here based on other enemy kinds
        }
    }



    void Update()
    {
        switch (enemykind)
        {
            case EnemyKind.foot:
                // 움직임의 방향을 기반으로 목표 위치 결정
                Vector3 targetPosition = bMovingRight ? originPosition + new Vector3(moveDistance, 0, 0) : originPosition - new Vector3(moveDistance, 0, 0);

                // 목표 위치로 적 움직임
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                fCheckdisfordic = Vector2.Distance(transform.position, targetPosition);
                // 적이 목표 위치에 도달했는지 확인
                if (fCheckdisfordic < 2.3f)
                {
                    // 움직임의 방향 반전
                    bMovingRight = !bMovingRight;
                    gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
                }
                //MoveHorizontally();
                break;
            case EnemyKind.fly:

                FlyHorizontally();
                break;
                // Add other movement cases here based on other enemy kinds
        }
    }

    void MoveHorizontally()
    {
        float xDirection = bMovingRight ? 1 : -1;
        Vector3 targetPosition = originPosition + new Vector3(moveDistance * xDirection, 0, 0);

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        fCheckdisfordic = Vector2.Distance(transform.position, targetPosition);

        if (Mathf.Abs(transform.position.x - targetPosition.x) < 0.1f)
        {
            bMovingRight = !bMovingRight;
            gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
        }
    }

    void FlyHorizontally()
    {
        float xDirection = bMovingRight ? 1 : -1;
        float yDirection = bMovingUp ? 1 : -1;
        Vector3 targetPosition = originPosition + new Vector3(moveDistance * xDirection, flyDistance * yDirection, 0);

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        fCheckdisfordic = Vector2.Distance(transform.position, targetPosition);

        if (Mathf.Abs(transform.position.x - targetPosition.x) < 0.1f)
        {
            bMovingRight = !bMovingRight;
            gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
        }

        if (Mathf.Abs(transform.position.y - targetPosition.y) < 0.1f)
        {
            bMovingUp = !bMovingUp;
        }
    }

    public void Dead()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        Destroy(this.gameObject, 0.45f);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "PMissile")
        {
            Debug.LogWarning("(Enemy) death sound.GameManager.current.AddScore();");
            GameManager.current.AddScore();
            Dead();
        }
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
    }
}

/*public class Enemy : MonoBehaviour
{

    public int nHp = 1;
    public EnemyKind enemykind;
    public float moveDistance = 4f; // 왼쪽과 오른쪽으로 움직일 거리
    public float moveSpeed = 2.0f; // 움직임의 속도
    private Vector3 originPosition; // 적의 원래 위치
    public bool bMovingRight = true; // Flag to determine the movement direction
    public Rigidbody2D rb;
public float fCheckdisfordic;
    void Start()
    {
        originPosition = transform.position; // Store the original position
    }


    

    void Update()
    {
    // 움직임의 방향을 기반으로 목표 위치 결정
                Vector3 targetPosition = bMovingRight ? originPosition + new Vector3(moveDistance, 0, 0) : originPosition - new Vector3(moveDistance, 0, 0);

                // 목표 위치로 적 움직임
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                fCheckdisfordic = Vector2.Distance(transform.position, targetPosition);
                // 적이 목표 위치에 도달했는지 확인
                if (fCheckdisfordic < 2.3f)
                {
                    // 움직임의 방향 반전
                    bMovingRight = !bMovingRight;
                    gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
                }
                
    
   
    }
    public void Dead()
    {

        this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        Destroy(this.gameObject, 0.5f);
       // Destroy(this.gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "PMissile")
        {

            Debug.LogWarning("(적) 죽음음.GameManager.current.AddScore();");
            GameManager.current.AddScore();
            Dead();
            
        }  
    }
    public void OnCollisionExit2D(Collision2D collision){}
            
    
}
*/
