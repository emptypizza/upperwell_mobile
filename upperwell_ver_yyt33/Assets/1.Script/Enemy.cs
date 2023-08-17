using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{



    public int nHp = 1;

    public float moveDistance = 4f; // 왼쪽과 오른쪽으로 움직일 거리
    public float moveSpeed = 2.0f; // 움직임의 속도
    private Vector3 originPosition; // 적의 원래 위치
    public bool bMovingRight = true; // Flag to determine the movement direction
    public Rigidbody2D rb;

    void Start()
    {
        originPosition = transform.position; // Store the original position
    }


    public float fCheckdisfordic;

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
        Destroy(this.gameObject, 1.2f);
       // Destroy(this.gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "PBullet")
            Dead();

        
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        
    }

}



