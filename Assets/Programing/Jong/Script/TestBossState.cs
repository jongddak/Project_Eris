using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBossState : MonoBehaviour
{
    enum BossState
    {
        Idle, Move, Attack, Die
    }
    // 보스 게임 오브젝트
    [SerializeField] GameObject bossObject;


    [SerializeField] Rigidbody2D bossRigid;
    [SerializeField] PolygonCollider2D bossPolygonCollider;
    // 보스 애니메이션
   // [SerializeField] Animator animator;
    // 플레이어 프리펩
    [SerializeField] GameObject player;
    // 스킬 이펙트 프리펩



    // 보스 스탯
    public float bossHP = 10;

    // 보스 몬스터 패턴 선택
    int bossPatternNum;
    // 상태를 Idle로 셋팅
    BossState state = BossState.Idle;
    // 등장시 대기상태, 패턴 중간 중간에 대기상태
    // 움직임은 기본적으로 플레이어를 따라감 조건 만족시 Attack 상황으로 

    // 스킬 패턴 사용시 스킬 이펙트 프리펩 생성 

    [SerializeField] BossState curBossState;
    private void Awake()
    {
      
    }


    private void Update()
    {   
        curBossState = state;
        switch (state)
        {
            case BossState.Idle:
                Idle();
                break;
            case BossState.Move:
                Move();
                break;
            case BossState.Attack:
                Attack();
                break;
            case BossState.Die:
                Die();
                break;
        }

        if (bossHP <= 0f)
        {
            state = BossState.Die;
        }
    }

    private void Idle()
    {
        // Idle 애니메이션
        // animator.Play();

        /* if (플레이어와의 거리가 일정거리 이상이면)
         * 몬스터의 상태 Move로 변경
         * state = BossState.Move;
         * 
         * else 아니라면
         * 몬스터의 상태 Attack으로 변경
         * state = BossState.Attack;
        */

       // Debug.Log(Vector2.Distance(player.transform.position, transform.position));
        if (Vector2.Distance(player.transform.position, transform.position) < 7f) 
        {
            Debug.Log("어택 상태로 변경 ");
            state = BossState.Attack;
        }
    }
    private void Move()
    {
        // 플레이어 위치로 이동

        // 플레이어와의 위치가 가깝다면
        // state = BossState.Attack;
    }
    private void Attack()
    {
        // 랜덤으로 보스 패턴 실행
        // 패턴 넘버 랜덤 생성

        Patton02();
        //bossPatternNum = Random.Range(1, 5);

        //switch (bossPatternNum)
        //{
        //    case 1:
        //        Patton02();
        //        break;
        //    case 2:
        //        Patton05();
        //        break;
        //    case 3:
        //        Patton06();
        //        break;
        //    case 4:
        //        Patton07();
        //        break;
        //}
    }
    private void Die()
    {
        // hp 전부 소모 시 사망 애니메이션 송출 후 프리펩 소멸

        // 사망 애니메이션 
        // animator.Play();

        // 오브젝트 삭제 처리
        //Destroy(gameObject, 2f);
    }
    // 백스텝(쿨타임있음) , 전방으로 크게 휘두름, 돌진 , 하늘에서 화염구 발사 , 공중에서 돌진하면서 여러번 베기 , 2페이즈 돌입 
    public void Patton02()
    {
        // 몸통 박치기 패턴
        // 애니메이션 재생
        // 대기시간
        // 전방으로 돌진
        bossRigid.AddForce(Vector2.left * 100f * Time.deltaTime, ForceMode2D.Impulse);
        // Idle() 상태로 변경
        state = BossState.Idle;
    }
    public void Patton05()
    {
        // 전방에 화염구 발사 패턴

        // 화염구 발사 애니메이션
        // 대기시간
        // 화염구 프리펩 생성 
        // 화염구는 직선으로 이동

        // Idle() 상태로 변경
        state = BossState.Idle;
    }
    public void Patton06()
    {
        // 공격 전방 후방으로 화염 기둥 프리펩 생성

        // 차징하는 애니메이션 
        // 대기시간

        // 랜덤한 장소에 화염기둥 프리펩 다수 생성

        // Idle() 상태로 변경
        state = BossState.Idle;
    }
    public void Patton07()
    {
        //넓은 범위에 점프 공격

        // 점프하는 애니메이션
        // 보스 팔쪽 콜라이더만 피격판정

        // Idle() 상태로 변경
        state = BossState.Idle;
    }
}
