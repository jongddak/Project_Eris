using System.Collections;
using UnityEngine;

public class SideWall : MonoBehaviour
{
    [SerializeField] float poisionDamage; // 독 데미지의 크기
    [SerializeField] float poisionDebuffTime; // 독 데미지 디버프 주기
    // 충돌한 플레이어의 플레이어 컨트롤러를 가져오기위해서 작성
    // 미리 유니티에서 넣어 놓는 것도 합칠때 생각해보는 것이 좋을 것 같음
    // 이 경우 OnCollsionEnter2D에 samplePlayer = collision.gameObject.GetComponent<SamplePlayer>(); 삭제할 것
    [SerializeField] SamplePlayer samplePlayer;

    bool isDebuff = false; // 디버프의 활성화 여부 설정

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // 플레이어와 충돌하면
            isDebuff = true; // 디버프 활성화
            // 충돌한 플레이어의 플레이어 컨트롤러 불러오기
            samplePlayer = collision.gameObject.GetComponent<SamplePlayer>();
            // 플레이어의 체력이 일정시간 단위로 감소하는 코루틴 실행
            StartCoroutine(PoisonDebuff());
        }
    }

    /// <summary>
    /// 물체가 충돌에서 빠져나오는 경우 판단
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        // 플레이어가 빠져나오는 경우
        if (collision.gameObject.tag == "Player")
        {
            isDebuff = false; // 디버프 비활성화
            StopCoroutine(PoisonDebuff()); // 코루틴 멈춤
        }
    }

    /// <summary>
    /// 독 데미지가 들어가는 주기에 맞춰서 독 디버프를 실행하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator PoisonDebuff()
    {
        while (true)
        {
            samplePlayer.TakeDamage(poisionDamage); // 플레이어 컨트롤러의 TakeDamage로 플레이어 체력 감소
            Debug.Log("데미지");
            // 플레이어의 체력 감소
            yield return new WaitForSeconds(poisionDebuffTime); // 일정 시간 멈춤
            Debug.Log("주기 시간 지남");
        }
    }

    /* OnCollisionStay2D를 사용하여 충돌하는 동안
     * 코루틴을 실행하여 충돌한 플레이어에게 디버프를 부여하려 체력이 감소하고
     * OnCollisionExit2D를 사용하여 충돌에서 빠져나오면
     * 코루틴을 종료하도록 설정
     * - 제대로 실행되지 않아서 폐기 예정
     * - 데미지가 계속해서 업데이트되며 주기는 따로 실행됨
     
    [SerializeField] float poisionDamage;
    [SerializeField] float poisionDebuffTime;
    Coroutine PoisionDebuffR;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // 플레이어와 충돌하는 동안에
            // 플레이어의 체력이 일정하게 감소
            SamplePlayer samplePlayer = collision.gameObject.GetComponent<SamplePlayer>();
            PoisionDebuffR = StartCoroutine(PoisonDebuff(samplePlayer));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        StopCoroutine(PoisionDebuffR);
    }

    /// <summary>
    /// 독 데미지가 들어가는 주기에 맞춰서 독 디버프를 실행하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator PoisonDebuff(SamplePlayer samplePlayer)
    {
        while (true)
        {
        samplePlayer.TakeDamage(poisionDamage);
        Debug.Log("데미지");
        // 플레이어의 체력 감소
        yield return new WaitForSeconds(poisionDebuffTime);
        Debug.Log("주기 시간 지남");
        }
    }
    */
    /* bool 변수 isDebuff를 사용하여 디버프의 활성화 여부를 판단하고 
     * 디버프 주기를 코루틴으로 제어하는 소스코드
     * 단, 데미지를 무한정 입히는 에러가 발생하여 폐기 - 추후 삭제 예정
     *
    [Header("State")]
    [SerializeField] float poisionDebuffTime; // 독 데미지가 들어가는 주기
    float poisionDamage = 2f; // 독 데미지
    SamplePlayer samplePlayer;

    bool isDebuff = false; // 디버프가 작동하는지 여부를 판단 - true = 디버프 활성화


    private void Update()
    {
        if (isDebuff) // 디버프가 작동하고 있는 중이면
        {
            StartCoroutine(PoisonDebuff()); // 디버프 주기를 제한하는 코루틴 활성화
        }
        else if (!isDebuff) // 디버프가 종료되면
        {
            StopCoroutine(PoisonDebuff()); // 코루틴도 종료
        }
    }
    /// <summary>
    /// 지속적으로 충돌 중인동안 지속적으로 발생하는 행동
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("플레이어와 충돌");
            isDebuff = true; // 디버프를 활성화
            samplePlayer = collision.gameObject.GetComponent<SamplePlayer>();
            // 플레이어의 TakeDamage(); 함수를 가져오기위해 선언
            // StartCoroutine(PoisonDebuff());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isDebuff = false; // 디버프를 종료
            Debug.Log("플레이어 체력 디버프 종료");
        }
    }
    /// <summary>
    /// 독 데미지가 들어가는 주기에 맞춰서 독 디버프를 실행하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator PoisonDebuff()
    {
        samplePlayer.TakeDamage(poisionDamage);
        Debug.Log("데미지");
        // 플레이어의 체력 감소
        yield return new WaitForSeconds(poisionDebuffTime);
        Debug.Log("주기 시간 지남");
    }
    */
}
