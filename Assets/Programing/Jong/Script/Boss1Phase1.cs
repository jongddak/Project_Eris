using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Phase1 : MonoBehaviour
{

    // 지상 상태와 공중상태 2가지 지상에서 2패턴+ 백스텝  공중에서 2패턴 한쪽패턴에서 3번쓰면 다른패턴으로 전환
    // 지상일때 거리체크랑 공중일때 거리체크 및 이동방식이 바뀌어야 함 

    //백스텝(쿨타임있음) , 전방으로 크게 휘두름, 돌진 , 하늘에서 화염구 발사 , 공중에서 돌진하면서 여러번 베기, 2페이즈 돌입
    // 보스는 계속 플레이어를 추적하니까 일단 아이들은 필요 없을 듯?
    enum BossState
    {
        Flying, Walk, Attack, Die
    }
    // 보스 애니메이션
    // [SerializeField] Animator animator;
    // 플레이어 프리펩
    [SerializeField] GameObject player;

    // 보스 스탯  : 체력 이동속도 
    [SerializeField] int stateCount = 0;  // fly 나 walk 상태에서 카운트가 3이 되면(3번 오르면) 상태를 변경할때 쓸것 , 패턴을 실행 할 때 마다 하나씩 증가

    public float bossHP = 10;
    [SerializeField] float toPlayerDistance; // 보스와 플레이어의 거리 
    [SerializeField] float walkAttackRange;  // 지상사거리 

    [SerializeField] float flyAttackRange;  // 공중사거리 

    [SerializeField] float walkbossSpeed;    // 이동속도 
    [SerializeField] float flybossSpeed;    // 공중 이동속도 
    private bool isPatternOn = false;
    private BossState preState;


    Coroutine curCoroutine;
    BossState state = BossState.Walk;
    [SerializeField] BossState curBossState; // 보스의 현재 상태 확인용 
    // 보스에게 데미지를 주려면 BossPattern bossPattern = boss.GetComponent<BossPattern>();
    // bossPattern.TakeDamage(데미지);로 데미지를 줄 수 있음 , 이벤트로 호출하면 될듯
    private void Start()
    {
        StartCoroutine("BossDo");
        curBossState = state;
    }
    IEnumerator BossDo()
    {

        WaitForSeconds time = new WaitForSeconds(0.0125f);  // 1초에 80번 호출
        curCoroutine = StartCoroutine(Walk());
        while (true)
        {

            toPlayerDistance = Vector2.Distance(player.transform.position, transform.position);
                
            if (curBossState != state)
            {
                curBossState = state;

                // 현재 실행 중인 코루틴이 있으면 정지
                if (curCoroutine != null)
                {
                    StopCoroutine(curCoroutine);
                }

                // 새로운 상태에 맞는 코루틴을 시작
                switch (state)
                {
                    case BossState.Walk:
                        curCoroutine = StartCoroutine(Walk());
                        break;
                    case BossState.Flying:
                        curCoroutine = StartCoroutine(Flying());
                        break;
                    case BossState.Attack:
                        curCoroutine = StartCoroutine(Attack());
                        break;
                    case BossState.Die:
                        // Die();
                        break;
                }
            }

            yield return time;
        }
    }


    IEnumerator Flying()
    {
        // 지금 코드는 너무 휙 하고 올라가서 좀 별로 올라갈때 애니메이션이나 이펙트가 있으면 좀 나을듯 
        if (stateCount >= 3)
        {
            stateCount = 0;
            state = BossState.Walk;
            Debug.Log("상태전환");
        }
        while (state == BossState.Flying)
        {
            preState = curBossState;
            Mirrored();
            Vector2 newPosition = new Vector2(
            Mathf.MoveTowards(transform.position.x, player.transform.position.x, flybossSpeed * Time.deltaTime),
            30f
            );
            transform.position = newPosition;
          

            if (toPlayerDistance <= flyAttackRange)
            {
                if (isPatternOn == false)
                {
                    preState = curBossState;
                    state = BossState.Attack;
                    stateCount++;
                }
            }


            yield return null;
        }


    }

    IEnumerator Walk()
    {
        if (stateCount >= 3)
        {
            stateCount = 0;
            state = BossState.Flying;
            Debug.Log("상태전환");
        }
        while (state == BossState.Walk)
        {

            preState = curBossState;
            Mirrored();
            Vector2 newPosition = new Vector2(
                Mathf.MoveTowards(transform.position.x, player.transform.position.x, walkbossSpeed * Time.deltaTime),
                transform.position.y
            );
            transform.position = newPosition;


            // 지상용 패턴 넣기   공중에서 지상 내려올때 패턴있으면 더 재밌을듯 ,공격 패턴이 실행되면 이동 루틴은 정지 해야할듯
            if (toPlayerDistance <= walkAttackRange)
            {
                if (isPatternOn == false)
                {
                    preState = state;
                    state = BossState.Attack;
                    stateCount++;
                }

            }


            yield return null;
        }

    }

    //백스텝(쿨타임있음) , 전방으로 크게 휘두름, 돌진 , 하늘에서 화염구 발사 , 공중에서 돌진하면서 여러번 베기, 2페이즈 돌입
    IEnumerator Attack()
    {
        isPatternOn = true;
        //int x = Random.Range(0, 2);
        //switch (x)
        //{
        //    case 0:
        //        StartCoroutine("BackStep");
        //        break;
        //    case 1:
        //        StartCoroutine("Slash");
        //        break;
        //    case 2:
        //        StartCoroutine("Bodytacle");
        //        break;
        //}
        //int y = Random.Range(0, 3);
        //switch (x)
        //{
        //    case 0:
        //        StartCoroutine("FireBall");
        //        break;
        //    case 1:
        //        StartCoroutine("RushSlash");
        //        break;
        //}

        Debug.Log($"{preState}{stateCount}공격 진입");
        yield return new WaitForSeconds(1f);

        state = preState;
        isPatternOn = false;
    }

    IEnumerator BackStep()
    {
        Debug.Log("백스텝");
        StopCoroutine("BossDo");
        yield return new WaitForSeconds(1f);
        StartCoroutine("BossDo");
    }
    IEnumerator Slash()
    {
        Debug.Log("베기");
        StopCoroutine("BossDo");
        yield return new WaitForSeconds(1f);
        StartCoroutine("BossDo");
    }
    IEnumerator Bodytacle()
    {
        Debug.Log("돌진");
        StopCoroutine("BossDo");
        yield return new WaitForSeconds(1f);
        StartCoroutine("BossDo");
    }
    IEnumerator FireBall()
    {
        Debug.Log("화염구");
        StopCoroutine("BossDo");
        yield return new WaitForSeconds(1f);
        StartCoroutine("BossDo");
    }
    IEnumerator RushSlash()
    {
        Debug.Log("공중돌진베기");
        StopCoroutine("BossDo");
        yield return new WaitForSeconds(1f);
        StartCoroutine("BossDo");
    }
    // 2페이즈?


    private void Die()
    {
        // hp 전부 소모 시 사망 애니메이션 송출 후 프리펩 소멸

        // 사망 애니메이션 
        // animator.Play();

        // 오브젝트 삭제 처리
        //Destroy(gameObject, 2f);
    }

    public void TakeDamage(float damage)
    {
        bossHP -= damage;

        // 보스의 체력이 0 이하가 되면 상태를 Die로 변경
        if (bossHP <= 0)
        {
            state = BossState.Die;
        }
    }

    private void Mirrored()
    {
        // 플레이어가 보스의 왼쪽에 있으면 보스를 왼쪽으로, 오른쪽에 있으면 오른쪽을 바라보게 설정
        if (player.transform.position.x < transform.position.x)
        {
            // 보스가 왼쪽을 바라보도록 함
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            // 보스가 오른쪽을 바라보도록 함
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
