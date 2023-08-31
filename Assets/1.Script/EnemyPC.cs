using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum Direction { UP, DOWN, LEFT, RIGHT }
public enum AIState { Idle, Move, Moving, Attack }

public class EnemyPC : MonoBehaviour
{
  

    [Header("AI Settings")]
    public bool isAIModeOn;
    public AIState enemyState = AIState.Idle;
    public Transform playerCollectorTransform;

    [Header("Movement Settings")]
    public float speed = 5f;
    public Vector2 minMoveLimits = new Vector2(-12.3f, -3.5f);
    public Vector2 maxMoveLimits = new Vector2(-7.8f, 3f);
    public Vector2 moveLengthLimits = new Vector2(-5f, 5f);
    private Vector2 currentTurnMovePosition;

    [Header("Attack Settings")]
    public GameObject enemyBulletPrefab;
    public float attackDelay = 2f;
    public float playerDetectionRange = 5f;
    private bool isAttacking = false;
    private Transform firePosition;
    private float attackTimer;

    [Header("Health Settings")]
    public int maxHP;
    private int currentHP;
    public GameObject healthBar;
    [SerializeField] private GameObject[] healthSegments;

    private void Awake()
    {
        InitializeComponents();
        InitializeHealth();
    }

    private void Start()
    {
   //     SetBulletDirectionBasedOnPlayerNumber();
    }

    private void Update()
    {
        if (GameManager.current.GS == GameState.Play)
        {
            UpdateAIState();
        }
    }

    private void InitializeComponents()
    {
        firePosition = transform.Find("gunner/FirePosition");
    }

    private void InitializeHealth()
    {
        currentHP = maxHP;

        if (healthBar)
        {
            healthSegments = new GameObject[maxHP];
            for (int i = 0; i < maxHP; i++)
            {
                healthSegments[i] = healthBar.transform.GetChild(i).gameObject;
            }
            //UpdateHealthBar();
        }
    }

    private void SetBulletDirectionBasedOnPlayerNumber(int playerNumber)
    {
        Direction bulletDirection = (playerNumber % 2 == 1) ? Direction.UP : Direction.DOWN;
      //  enemyBulletPrefab.GetComponent<Bullet>().direction = bulletDirection;
    }

    private void UpdateAIState()
    {
        if (!isAIModeOn) return;

        attackTimer += Time.deltaTime;

        switch (enemyState)
        {
            case AIState.Idle:
                DetermineNextMove();
                enemyState = AIState.Move;
                break;
            case AIState.Move:
                StartMovingToTarget();
                break;
            case AIState.Moving:
                CheckForAttackOpportunity();
                break;
            case AIState.Attack:
                AttemptAttack();
                break;
        }
    }

    private void DetermineNextMove()
    {
        float xMove = Random.Range(moveLengthLimits.x, moveLengthLimits.y);
        float yMove = Random.Range(moveLengthLimits.x, moveLengthLimits.y);

        currentTurnMovePosition = transform.position + new Vector3(xMove, yMove);
        currentTurnMovePosition = new Vector2(
            Mathf.Clamp(currentTurnMovePosition.x, minMoveLimits.x, maxMoveLimits.x),
            Mathf.Clamp(currentTurnMovePosition.y, minMoveLimits.y, maxMoveLimits.y)
        );
    }

    private void StartMovingToTarget()
    {
    //    iTween.MoveTo(gameObject, iTween.Hash("easetype", iTween.EaseType.linear, "speed", speed, "position", currentTurnMovePosition, "oncomplete", "OnMovementComplete"));
        enemyState = AIState.Moving;
    }

    private void CheckForAttackOpportunity()
    {
        if (attackTimer >= attackDelay || IsPlayerWithinAttackRange())
        {
       //     iTween.Stop(gameObject);
            enemyState = AIState.Attack;
        }
    }

    private bool IsPlayerWithinAttackRange()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Enemy");
        layerMask = ~layerMask;

        RaycastHit2D hit = Physics2D.Raycast(firePosition.position, (playerCollectorTransform.position - firePosition.position).normalized, playerDetectionRange, layerMask);
        return hit.collider?.tag == "Player";
    }

    private void AttemptAttack()
    {
        if (attackTimer >= attackDelay)
        {
            FireBullet();
            attackTimer = 0f;
        }
        else
        {
            enemyState = AIState.Idle;
        }
    }

    private void FireBullet()
    {
        Instantiate(enemyBulletPrefab, firePosition.position, Quaternion.identity);
    }

    private void OnMovementComplete()
    {
        enemyState = AIState.Idle;
    }
}
   
