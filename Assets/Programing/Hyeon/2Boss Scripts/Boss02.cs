using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02 : MonoBehaviour
{
    enum BossState
    {
        Idle, Move, Attack, Die, win
    }

    // 보스 게임 오브젝트
    [SerializeField] GameObject bossObject;

    // 플레이어 프리펩
    [SerializeField] GameObject player;
    // 보스의 Rigidbody
    [SerializeField] Rigidbody2D bossRigid;

    // 패턴 시작 판정 bool
    private bool skillStart = false;
    // 벽 충돌 판정
    private bool isWall = false;

    // 보스 스탯
    // 보스 HP
    [SerializeField] float bossHP = 10;
    // 보스 공격 사거리
    [SerializeField] float attackRange;
    // 보스 스피드
    [SerializeField] float bossSpeed;

    // 플레이어 위치
    Vector2 playerPosition;
    // 보스 몬스터 패턴 선택
    int bossPatternNum;
    // 보스 패턴 스택
    int bosscount;

    // 상태를 Idle로 셋팅
    BossState state = BossState.Idle;


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        bossRigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        switch (state)
        {
            case BossState.Idle:
                StartCoroutine(Idle());
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
            case BossState.win:
                Win();
                break;
        }
    }

    private IEnumerator Idle()
    {
        Debug.Log("대기");
        skillStart = false;

        // 대기 애니메이션
        // animator.Play("");

        // 플레이어 위치를 바라보게
        Mirrored();

        // 스킬 사이에 대기 모션 넣으려면 활성화
        yield return new WaitForSeconds(1f);

        // 플레이어와의 거리 playerDirection
        float playerDirection = Vector2.Distance(transform.position, player.transform.position);

        // 플레이어와 거리를 계산하여 상태 변경
        if (playerDirection >= attackRange)
        {
            //거리가 사거리보다 멀면 Move 상태로 변경
            state = BossState.Move;
        }
        // 스킬 패턴이 시작을 안했을때
        else if (!skillStart)
        {
            // 플레이어와의 거리가 사거리보다 작을때는 공격준비
            StartCoroutine(WaitSkill());
        }
    }


    private void Move()
    {
        Debug.Log("MOVE");
        // 달리기 애니메이션 재생
        // animator.Play("");

        // 앞으로 가는 코드
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
                StartCoroutine(WaitSkill());
            }
        }
    }
    private IEnumerator WaitSkill()
    {
        // 스킬 패턴 시작
        skillStart = true;
        // 대기 애니메이션
        // animator.Play("boss1 2 idel");

        // 공격 상태
        state = BossState.Attack;

        // 플레이어 방향 저장
        playerPosition = (player.transform.position - transform.position).normalized;

        yield return new WaitForSeconds(2f);

        // 플레이어 거리 계산
        float playerDirection = Vector2.Distance(transform.position, player.transform.position);

        // 거리에 따라 보스의 패턴 분화
        if (playerDirection <= attackRange / 3)
        {
            // Debug.Log($"짧은 거리");
            // 1,2 중 랜덤
            bossPatternNum = 1;
        }
        else
        {
            //Debug.Log($"긴 거리");
            // 3,4 중 랜덤
            bossPatternNum = 2;
        }

        StartCoroutine(ExecuteAttackPattern());
    }
    
    private IEnumerator ExecuteAttackPattern()
    {
        if (bosscount == 3)
        {
            bossPatternNum = Random.Range(2, 4);
            bosscount = 0;
        }
        skillStart = true;
        switch (bossPatternNum)
        {
            case 1:
                yield return StartCoroutine(Bash());
                Debug.Log("베기 패턴");
                break;
            case 2:
                yield return StartCoroutine(FootWork());
                Debug.Log("발도 패턴");
                break;
            case 3:
                yield return StartCoroutine(Bash());
                Debug.Log("공중 검기 날리기");
                break;
        }
        skillStart = false;
        state = BossState.Idle;
    }

    private IEnumerator Bash()
    {
        // 일반베기
        bosscount += 1;
        Debug.Log("베어가르기!");
        yield return new WaitForSeconds(2f);
    }
    private IEnumerator FootWork()
    {
        // 발도 
        isWall = false;

        // 돌진 시작 위치
        Vector2 startPosition = transform.position;
        // 좌측 우측 판정
        Vector2 dashDirection;
        if (bossObject.transform.localScale.x > 0)
        {
            dashDirection = Vector2.right;
        }
        else
        {
            dashDirection = Vector2.left;
        }
        // 순간이동 거리
        float targetDistance = 50f;
        // 칼 뽑는 애니메이션 재생
        //animator.Play("boss1 2 BodyTackle");
        // 애니메이션 재생 시간
        yield return new WaitForSeconds(0.7f);
        // 레이케스트 사용
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dashDirection, targetDistance, LayerMask.GetMask("Wall"));

        Vector2 targetPosition;
        // 레이케스트가 벽에 닿았을때
        if (hit.collider != null)
        {
            // 벽이 가까이 있으면 벽 앞까지만 이동
            targetPosition = (Vector2)transform.position + dashDirection * (hit.distance - 1f);
        }
        else
        {
            // 벽이 없으면 정해진 거리만큼 순간이동
            targetPosition = (Vector2)transform.position + dashDirection * targetDistance;
        }
        // 보스 위치
        transform.position = targetPosition;
        // 칼 넣는 애니메이션 재생
        //animator.Play("boss1 2 BodyTackle");

        // 잔상 프리펩 생성

        yield return new WaitForSeconds(0.7f);
    }
    // 데미지 계산 함수
    public void TakeDamage(float damage)
    {
        bossHP -= damage;

        // 보스의 체력이 0 이하가 되면 상태를 Die로 변경
        if (bossHP <= 0)
        {
            state = BossState.Die;
        }
    }
    private void Die()
    {
        // hp 전부 소모 시 사망 애니메이션 송출 후 프리펩 소멸

        // 사망 애니메이션 
        //animator.Play("");

        // 오브젝트 삭제 처리
        Destroy(gameObject, 4f);
    }
    private void Win()
    {
        // 플레이어의 HP가 0이되었거나 상태가 DIE가 되었을때

        // 승리 애니메이션
        //animator.Play("boss1 2 win");
    }
    // 좌우반전
    private void Mirrored()
    {
        // 플레이어가 보스의 왼쪽에 있으면 보스를 왼쪽으로, 오른쪽에 있으면 오른쪽을 바라보게 설정
        if (player.transform.position.x < bossObject.transform.position.x)
        {
            Debug.Log("보스 왼쪽");
            // 보스가 왼쪽을 바라보도록 함
            bossObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            Debug.Log("보스 오른쪽");
            // 보스가 오른쪽을 바라보도록 함
            bossObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
