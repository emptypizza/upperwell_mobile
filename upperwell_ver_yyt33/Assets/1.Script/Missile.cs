using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

enum MissileKind {  Pbullet = 0,Ebullet =1, Nbullet =2};

public class Missile : MonoBehaviour
{
    MissileKind missilekind;


   
    [SerializeField]
    float fBspeed = 2f;


    public Rigidbody2D rb; // 플레이어의 Rigidbody2D 참조
    
    public Transform missileSpawnPoint; // 미사일이 발사될 위치
    public float missileForce = 10f; // 미사일이 받을 힘
    public float fLifeTime;
    
    private void OnEnable()
    {
        fLifeTime = 0;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * fBspeed;
    }


    public void DestroyThis()
    {

        Destroy(this.gameObject);
       // ObjectPool.current.PoolObejct(gameObject);
        
    }


    // 다른 코드 생략...

  
    //위 코드는 미사일 프리팹을 아래 방향으로 발사합니다. 미사일 프리팹에는 Rigidbody2D 컴포넌트가 포함되어 있어야 하며, 발사 위치와 발사 힘은 적절하게 조정해야 할 수 있습니다.


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fLifeTime < 1.5f)
            fLifeTime += Time.deltaTime;
        else
            DestroyThis();

    }


}










