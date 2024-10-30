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
    // SwordAura 검기 프리펩
    [SerializeField] GameObject swordAura;
    // 패턴 시작 판정 bool
    private bool skillStart = false;
    // SwordAura 검기 생성 좌표
    [SerializeField] Transform swordAuraPoint;
    // 보스 스탯
    // 보스 HP
    [SerializeField] float bossHP = 10;
    // 보스 현재 HP
    float bossNowHP;
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
        bossNowHP = bossHP;
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

        yield return new WaitForSeconds(1.5f);

        // 플레이어 거리 계산
        float playerDirection = Vector2.Distance(transform.position, player.transform.position);

        // 거리에 따라 보스의 패턴 분화
        if (playerDirection <= attackRange / 3)
        {
            // Debug.Log($"짧은 거리");
            // 1,2 중 랜덤
            bossPatternNum = 1;
        }
        else if (bossNowHP <= bossHP / 2)
        {
            //Debug.Log($"긴 거리");
            // 3,4 중 랜덤
            bossPatternNum = Random.Range(2, 4); ;
        }
        else
        {
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
        bossPatternNum = 3;
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
                yield return StartCoroutine(SkySwordAura());
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
        // 베는 애니메이션
        // 이펙트 프리펩 생성
        yield return new WaitForSeconds(2f);
    }
    private IEnumerator FootWork()
    {
        // 발도
        // 현재 보스의 Y 좌표 저장
        float y = transform.position.y;
        
        // 순간이동 거리
        float targetDistance = 100f;
        // 칼 뽑는 애니메이션 재생
        //animator.Play("boss1 2 BodyTackle");
        // 애니메이션 재생 시간
        yield return new WaitForSeconds(0.7f);
        // 레이케스트 사용
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerPosition, targetDistance, LayerMask.GetMask("Wall"));

        Vector2 targetPosition;
        // 레이케스트가 벽에 닿았을때
        if (hit.collider != null)
        {
            // 벽에 닿았을 때, 벽 앞까지 이동
            targetPosition = new Vector2(hit.point.x - playerPosition.x * 1f, y);
        }
        else
        {
            // 벽이 없으면 설정된 거리만큼 순간이동
            targetPosition = new Vector2(transform.position.x + playerPosition.x * targetDistance, y);
        }
        // 보스 위치
        transform.position = targetPosition;
        // 칼 넣는 애니메이션 재생
        //animator.Play("boss1 2 BodyTackle");

        // 잔상 프리펩 생성

        yield return new WaitForSeconds(0.7f);
    }
    // 데미지 계산 함수
    private IEnumerator SkySwordAura()
    {
        float bossJumpPower = 110f;

        // animator.Play("공중 점프 애니메이션");     

        bossRigid.bodyType = RigidbodyType2D.Dynamic;
        bossRigid.velocity = Vector2.zero;
        // 보스가 위로 올라감
        bossRigid.AddForce(Vector2.up * bossJumpPower, ForceMode2D.Impulse);

        // 올라가고 차징하는 애니메이션
        //animator.Play("boss1 2 FireBarrier");

        // 애니메이션 및 올라가는 시간을 위한 대기시간
        yield return new WaitForSeconds(0.2f);
        // 보스의 위치 고정
        bossRigid.velocity = Vector2.zero;
        bossRigid.bodyType = RigidbodyType2D.Kinematic;

        // 보스가 뿌리는 검기 생성
        for (int i = 0; i < 5; i++)
        {
            Mirrored();
            SwordAuraSpon();
            yield return new WaitForSeconds(1f);
        }

        bossRigid.bodyType = RigidbodyType2D.Dynamic;
        bossRigid.gravityScale = 10;
        yield return new WaitForSeconds(2f);
        bossRigid.gravityScale = 1;
    }
    public void TakeDamage(float damage)
    {
        bossNowHP -= damage;

        // 보스의 체력이 0 이하가 되면 상태를 Die로 변경
        if (bossNowHP <= 0)
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
            bossObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            bossObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public void SwordAuraSpon()
    {     
        GameObject swordSopn = Instantiate(swordAura, swordAuraPoint.position, Quaternion.identity);
        swordSopn.transform.LookAt(player.transform);
        SwordAura type = swordSopn.GetComponentInChildren<SwordAura>();

        // 플레이어가 보스의 좌에 있는지 우에 있는지 판단
        if (player.transform.position.x <= bossObject.transform.position.x)
        {
            type.direction = -1;
        }
        else
        {
            type.direction = 1;
        }
    }
}
