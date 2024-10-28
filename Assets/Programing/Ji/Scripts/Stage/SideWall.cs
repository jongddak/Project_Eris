using System.Collections;
using UnityEngine;

public class SideWall : MonoBehaviour
{
    [SerializeField] float poisionDamage; // 독 데미지의 크기
    [SerializeField] float poisionDebuffTime; // 독 데미지 디버프 주기
    // 충돌한 플레이어의 플레이어 컨트롤러를 가져오기위해서 작성
    // 미리 유니티에서 넣어 놓는 것도 합칠때 생각해보는 것이 좋을 것 같음
    // 이 경우 OnCollsionEnter2D에 samplePlayer = collision.gameObject.GetComponent<SamplePlayer>(); 삭제할 것
    [SerializeField] SamplePlayer samplePlayer; // 플레이어의 스크립트에 따라 다르게 변경할 것

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
        while (isDebuff)
        {
            samplePlayer.TakeDamage(poisionDamage); // 플레이어 컨트롤러의 TakeDamage로 플레이어 체력 감소
            // 플레이어의 체력 감소
            yield return new WaitForSeconds(poisionDebuffTime); // 일정 시간 멈춤
        }
    }
}
