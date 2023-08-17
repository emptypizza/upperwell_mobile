using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public static SoundManager me;
    public AudioSource speaker_A;
    public AudioSource speaker_B;
    public AudioSource speaker_C;
    public AudioSource speaker_D;
    public AudioSource speaker_E;


    public AudioClip Tooltipsound;
    public AudioClip clickSound;
    public AudioClip getBulletSound;
    public AudioClip EFireSound;
    public AudioClip TH2BossFireSound;
    public AudioClip TH2BossAtk1Sound;
    public AudioClip TH2CubeDropSound;
    public AudioClip TH2CubeDrop2Sound;
    public AudioClip damagedBoxSound;
    public AudioClip GoalinSound;
    public AudioClip WarpUpSound;
    public AudioClip gameoverSound;
    public AudioClip stageclearSound;
    public AudioClip roundresultSound;
    public AudioClip ShotSound; public AudioClip ShotSound1;
    public AudioClip HitBlockSound;
    public AudioClip Bulletblocksound;
    public AudioClip PlayerdieSound;
    public AudioClip PlayerwalkSound;
    public AudioClip ShieldSound;
    public AudioClip startani;
    public AudioClip[] MT_RocketCompl = new AudioClip[4];


    //---   언에이블 일렉홀 . 전기 함정 무효화 
    public AudioClip UnenableElesound;
    public AudioClip BGM; // 160319 경환 이거 써야 하는 변수인지


    void Awake()
    {
        me = this;
    }




    public void Click()
    {
        speaker_A.clip = clickSound;
        speaker_A.Play();


    }
    public void Getbullet()
    {
        speaker_B.clip = getBulletSound;
        speaker_B.Play();
    }
    public void playTooltipsound()
    {
        speaker_C.clip = Tooltipsound;
        speaker_C.Play();
    }


    public void Goalin()
    {
        speaker_D.clip = GoalinSound;
        speaker_D.Play();
    }
    public void WarpUp()
    {
        speaker_E.clip = WarpUpSound;
        speaker_E.Play();
    }


    public void Fire()
    {
        speaker_A.clip = ShotSound;
        speaker_A.Play();
    }
    public void Fire1()
    {
        speaker_D.clip = ShotSound1;
        speaker_D.Play();
    }


    public void StageClear()
    {
        speaker_B.clip = stageclearSound;
        speaker_B.Play();
    }
    public void GameOver()
    {
        speaker_C.clip = gameoverSound;
        speaker_C.Play();
    }


    public void Playerdie()
    {
        speaker_D.clip = PlayerdieSound;
        speaker_D.Play();
    }


    public void DamagedBox()
    {
        speaker_A.clip = damagedBoxSound;
        speaker_A.Play();
    }


    public void Bulletblock()
    {
        speaker_B.clip = Bulletblocksound;
        speaker_B.Play();
    }


    public void EnemyFire()
    {
        speaker_C.clip = EFireSound;
        speaker_C.Play();
    }


    public void Playerwalks()
    {
        speaker_D.clip = PlayerwalkSound;
        speaker_D.Play();
    }


    public void RoundResult()
    {
        speaker_A.clip = roundresultSound;
        speaker_A.Play();
    }


    public void UnenableElecHole()
    {
        speaker_B.clip = UnenableElesound;
        speaker_B.Play();
    }


    public void Shiled()
    {
        speaker_C.clip = ShieldSound;
        speaker_C.Play();
    }
    public void TH2Bossfire()
    {
        speaker_D.clip = TH2BossFireSound;
        speaker_D.Play();
    }


    public void TH2CubeDrop1()
    {
        speaker_E.clip = TH2CubeDropSound;
        speaker_E.Play();
    }
    public void P2RocketReload()
    {
        speaker_A.clip = MT_RocketCompl[0];
        speaker_A.Play();
    }
    public void P2RocketRefill()
    {
        speaker_B.clip = MT_RocketCompl[2];
        speaker_B.Play();
    }
    public void TH2CubeDrop2()
    {
        speaker_D.clip = TH2CubeDrop2Sound;

        speaker_D.Play();
    }
    public void P1RocketReload()
    {
        speaker_E.clip = MT_RocketCompl[1];
        speaker_E.volume = 0.2f;
        speaker_E.Play();
    }
    public void P1RocketRefill()
    {
        speaker_A.clip = MT_RocketCompl[3];
        speaker_A.volume = 0.2f;
        speaker_A.Play();
    }


    /*
    public void HitBlock()
    {
        speaker_A.clip = HitBlockSound;
        speaker_A.Play();
    }
    */




}
