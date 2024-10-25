using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;

public class BossPattern : MonoBehaviour
{
    enum BossState
    {
        Idle, Move, Attack, Die
    }
    // 보스 게임 오브젝트
    [SerializeField] GameObject bossObject;
    // 보스 애니메이션
    [SerializeField] Animator animator;
    // 플레이어 프리펩
    [SerializeField] GameObject player;
    [SerializeField] Rigidbody2D bossRigid;

    // 패턴 시작 판정 bool
    private bool skillStart = false;

    // 스킬 이펙트 프리펩


    // 공격 사거리
    [SerializeField] float attackRange;

    // 보스 스탯
    // 보스 HP
    public float bossHP = 10;
    // 보스 스피드
    [SerializeField] float bossSpeed;

    // 보스 몬스터 패턴 선택
    int bossPatternNum;
    // 상태를 Idle로 셋팅
    BossState state = BossState.Idle;

    // 등장시 대기상태, 패턴 중간 중간에 대기상태
    // 움직임은 기본적으로 플레이어를 따라감 조건 만족시 Attack 상황으로 변경

    // 스킬패턴 사용시 스킬 이펙트 프리펩 생성 

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        switch (state)
        {
            case BossState.Idle:
                Idle();
                break;
            case BossState.Move:
                Move();
                break;
            case BossState.Attack:
                // 패턴 중에는 다른 동작을 하지 않도록 함
                break;
            case BossState.Die:
                Die();
                break;
        }
    }

    private void Idle()
    {
        // Idle 애니메이션
        animator.Play("Testidel");

        // 플레이어 위치를 바라보게
        Mirrored();

        // 플레이어와의 거리 distanceToPlayer
        float playerDirection = Vector2.Distance(transform.position, player.transform.position);

        // 플레이어와 거리를 계산하여 상태 변경
        if (playerDirection >= attackRange)
        {
            Debug.Log("Move 상태로 변경");
            state = BossState.Move;
        }
        // 스킬 패턴이 시작을 안했을때
        else if (skillStart == false)
        {
            StartCoroutine(ExecuteAttackPattern());
        }
    }
    private void Move()
    {
        // 달리기 애니메이션 재생
        animator.Play("TestRun");

        Vector2 newPosition = new Vector2(
            Mathf.MoveTowards(transform.position.x, player.transform.position.x, bossSpeed * Time.deltaTime),
            transform.position.y
        );
        transform.position = newPosition;
        // 이동 중에도 스프라이트가 플레이어를 바라보게 처리
        Mirrored();

        // 사거리 판정
        float playerDirection = Vector2.Distance(transform.position, player.transform.position);

        // 사거리 내로 들어오면 공격 전환
        if (playerDirection <= attackRange)
        {
            if (skillStart == false)
            {
                StartCoroutine(ExecuteAttackPattern());
            }           
        }
    }
    private IEnumerator ExecuteAttackPattern()
    {
        // 스킬 패턴 시작
        skillStart = true;

        // 랜덤으로 보스 패턴 실행

        // 패턴 넘버 랜덤 생성
        bossPatternNum = Random.Range(1, 5);

        //패턴 test 코드
        bossPatternNum = 1;

        state = BossState.Attack;

        switch (bossPatternNum)
        {
            case 1:
                yield return StartCoroutine(BodyTackle());
                Debug.Log("BodyTackle 패턴");
                break;
            case 2:
                yield return StartCoroutine(Patton05());
                Debug.Log("Patton05");
                break;
            case 3:
                yield return StartCoroutine(Patton06());
                Debug.Log("Patton06");
                break;
            case 4:
                yield return StartCoroutine(Patton07());
                Debug.Log("Patton07");
                break;
        }
        Debug.Log("Idle 상태로 변경");

    }
    private void Die()
    {
        // hp 전부 소모 시 사망 애니메이션 송출 후 프리펩 소멸

        // 사망 애니메이션 
        // animator.Play();
        
        // 오브젝트 삭제 처리
        //Destroy(gameObject, 2f);
    }

    private IEnumerator BodyTackle()
    {
        // 몸통 박치기 패턴

        // 플레이어 위치 탐색
        Vector2 playerDirection = (player.transform.position - transform.position).normalized;
        // 돌진 시작 위치
        Vector2 startPosition = transform.position;

        // 애니메이션 재생
        // 대기시간
        Debug.Log("돌진 시작---!");

        // 돌진 목표 거리 설정
        float targetDistance = 30f; // 보스가 이동할 거리 (원하는 값으로 설정)
        float currentDistance = 0f;

        // 돌진 시작
        Debug.Log("돌진 시작---!");
        bossRigid.velocity = playerDirection * 70f; // 원하는 돌진 속도 설정

        while (currentDistance < targetDistance)
        {
            currentDistance = Vector2.Distance(transform.position, startPosition);

            if (currentDistance >= targetDistance)
            {
                // 목표 거리에 도달했으므로 멈춤
                bossRigid.velocity = Vector2.zero;
                bossRigid.AddForce(Vector2.zero, ForceMode2D.Impulse);
                break;
            }

            yield return null; // 다음 프레임까지 대기
        }

        yield return new WaitForSeconds(5f);
        Debug.Log("돌진 끝---!");
        skillStart = false;
        state = BossState.Idle;
    }
    private IEnumerator Patton05()
    {
        // 전방에 화염구 발사 패턴

        // 화염구 발사 애니메이션
        // 대기시간
        // 화염구 프리펩 생성 
        // 화염구는 직선으로 이동

        Debug.Log("불쏜다---!");

        // 불 발사 패턴 동안 대기 (1.5초 후 Idle 상태로 변경)
        yield return new WaitForSeconds(1.5f);

        Debug.Log("불 발사 끝---!");
        state = BossState.Idle;
    }
    private IEnumerator Patton06()
    {
        // 공격 전방 후방으로 화염 기둥 프리펩 생성

        // 차징하는 애니메이션
        // 대기시간

        // 랜덤한 장소에 화염기둥 프리펩 다수 생성

        Debug.Log("불기둥 만든다---!");

        // 불기둥 생성 패턴 동안 대기 (2초 후 Idle 상태로 변경)
        yield return new WaitForSeconds(2f);

        Debug.Log("불기둥 생성 끝---!");
        state = BossState.Idle;
    }
    private IEnumerator Patton07()
    {
        //넓은 범위에 점프 공격

        // 점프하는 애니메이션
        animator.Play("TestJump");
        // 보스 팔쪽 콜라이더만 피격판정

        Debug.Log("점프공격 시작---!");

        // 점프 공격 패턴 동안 대기 (3초 후 Idle 상태로 변경)
        yield return new WaitForSeconds(3f);

        Debug.Log("점프공격 끝---!");
        state = BossState.Idle;
    }

    // 보스에게 데미지를 주려면 BossPattern bossPattern = boss.GetComponent<BossPattern>();
    // bossPattern.TakeDamage(데미지);로 데미지를 줄 수 있음
    public void TakeDamage(float damage)
    {
        bossHP -= damage;

        // 보스의 체력이 0 이하가 되면 상태를 Die로 변경
        if (bossHP <= 0)
        {
            state = BossState.Die;
        }
    }

    // 좌우반전
    private void Mirrored()
    {
        // 플레이어가 보스의 왼쪽에 있으면 보스를 왼쪽으로, 오른쪽에 있으면 오른쪽을 바라보게 설정
        if (player.transform.position.x < bossObject.transform.position.x)
        {
            // 보스가 왼쪽을 바라보도록 함
            bossObject.transform.localScale = new Vector3(-1, bossObject.transform.localScale.y, bossObject.transform.localScale.z);
        }
        else
        {
            // 보스가 오른쪽을 바라보도록 함
            bossObject.transform.localScale = new Vector3(1, bossObject.transform.localScale.y, bossObject.transform.localScale.z);
        }
    }
}
