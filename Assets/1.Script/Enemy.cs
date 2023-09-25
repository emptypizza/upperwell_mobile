using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyKind { footman, wingman, foot_guided, fly_guided, foot_amored, BOSS0 }

public class Enemy : MonoBehaviour
{
    public int nHp = 1;
    public EnemyKind enemykind;
    public float moveDistance = 4f;
    public float flyDistance = 2f;
    public float fMoveSpeed = 2.0f;
    private Vector3 originPosition;
    public bool bMovingRight = true;
    public bool bMovingUp = true;
    public Rigidbody2D rb;
    private Vector3 boundaryPosition;  // Boundary의 위치를 저장할 변수

    public float forceMagnitude = 5f; // 이 값은 날아가는 힘의 크기를 결정합니다. Inspector에서 조절할 수 있습니다.

    public float fCheckdisfordic;


    private float boundaryWidth;
    private float boundaryHeight;
    GameObject boundary;

   void Start()
    {
        boundary = GameObject.FindGameObjectWithTag("Boundary");
        if (boundary != null)
        {
            boundaryPosition = boundary.transform.position;
            boundaryWidth = boundary.transform.localScale.x;
            boundaryHeight = boundary.transform.localScale.y;
        }
        else
        {
            Debug.LogError("Boundary를 찾을 수 없습니다.");
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        originPosition = transform.position;
        Vector2 gameCenter = new Vector2(-3.1f, 15); // 게임의 중앙을 가정합니다. 필요하다면 실제 게임의 중앙 좌표로 변경해 주세요.


        switch (enemykind)
        {
            case EnemyKind.footman:

                nHp = 1;

                break;
            case EnemyKind.wingman:
                nHp = 1;
                if (rb != null)
                {
                    Vector2 directionToCenter = (gameCenter - (Vector2)transform.position).normalized; // 게임 중앙을 향하는 방향 벡터
                    float forceMagnitude = 1f; // 적절한 값으로 조절합니다.
                    rb.AddForce(directionToCenter * forceMagnitude, ForceMode2D.Impulse);
                }
                break;

                if (rb != null)
                {
                    Vector2 forceDirection = new Vector2(0, 1); // 이 벡터는 날아가는 방향을 나타냅니다. 원하는 방향으로 변경할 수 있습니다.
                    rb.AddForce(forceDirection * forceMagnitude, ForceMode2D.Impulse);
                    /*이 코드를 wingman 게임 오브젝트에 연결된 스크립트에 추가하거나 수정하면, wingman이 생성될 때마다 특정 방향으로 힘이 가해져 날아갈 것입니다. 중력이 작용하여 아래로 떨어지지만, 추가된 힘에 의해 위로도 날아갑니다.

forceMagnitude 값을 조절하면 날아가는 힘의 크기를 조절할 수 있고, forceDirection 벡터를 변경하면 날아가는 방향을 변경할 수 있습니다.
                    */

                }
                break;
            // Add other movement cases here based on other enemy kinds

            default:
                break;
        }
      
    }

    void CheckBoundaryDistance()
    {
        if (Mathf.Abs(transform.position.x - boundaryPosition.x) > 10f ||
            Mathf.Abs(transform.position.y - boundaryPosition.y) > 40f)
        {
            Invoke("Dead", 3.14f); // Dead 함수를 4초 뒤에 호출합니다.
        }
    }

    void Update()
    {
        switch (enemykind)
        {
            case EnemyKind.footman:
                // 움직임의 방향을 기반으로 목표 위치 결정
                Vector3 targetPosition = bMovingRight ? originPosition + new Vector3(moveDistance, 0, 0) : originPosition - new Vector3(moveDistance, 0, 0);

                // 목표 위치로 적 움직임
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, fMoveSpeed * Time.deltaTime);
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
            case EnemyKind.wingman:

                FlyHorizontally();
                break;
                // Add other movement cases here based on other enemy kinds
        }
    }

    void MoveHorizontally()
    {
        float xDirection = bMovingRight ? 1 : -1;
        Vector3 targetPosition = originPosition + new Vector3(moveDistance * xDirection, 0, 0);

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, fMoveSpeed * Time.deltaTime);
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

        // Boundary 체크 로직 추가
        if (Mathf.Abs(targetPosition.x - boundaryPosition.x) > boundaryWidth ||
            Mathf.Abs(targetPosition.y - boundaryPosition.y) > boundaryHeight)
        {
            bMovingRight = !bMovingRight;
            bMovingUp = !bMovingUp;
            return; // Boundary를 넘어갈 경우 방향을 전환하고 메서드 종료
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, fMoveSpeed * Time.deltaTime);
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
    
    void FlyHorizontally1()
    {
        float xDirection = bMovingRight ? 1 : -1;
        float yDirection = bMovingUp ? 1 : -1;
        Vector3 targetPosition = originPosition + new Vector3(moveDistance * xDirection, flyDistance * yDirection, 0);

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, fMoveSpeed * Time.deltaTime);
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


        switch (enemykind)
        {
            case EnemyKind.footman:
                SoundManager.Instance.E1Dead();

                break;
            case EnemyKind.wingman:

                SoundManager.Instance.E1Dead();
                break;


            default:
                SoundManager.Instance.E1Dead();
                break;


        }
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        Destroy(this.gameObject, 0.45f);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "PMissile")
        {
            Debug.LogWarning("(Enemy) death sound.GameManager.Instance.AddScore();");
            GameManager.Instance.AddScore();
            Dead();
        }
    }
    public void OnCollisionExit2D(Collision2D collision){ }
}

