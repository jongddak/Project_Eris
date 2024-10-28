using System.Collections;
using UnityEngine;

public class HealFlat : MonoBehaviour
{
    // 코루틴으로 시간 타이밍을 조절
    // 시간 조절 후 오브젝트 삭제
    [Header("State")]
    [SerializeField] float DeleteTime; // 삭제까지 걸리는 시간 조절
    [SerializeField] SpriteRenderer spriteRenderer; // 발판의 이미지

    /// <summary>
    /// HealFlat에 충돌체가 충돌할 때
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            /* tag가 Player인 경우
             * OnCollisionEnter가 발생하기 전의 collision의 relativeVelocity(충돌이전의 속도)를 가져와서
             * relativeVelocity.y <0인 경우에만 삭제하는 코루틴을 출력하도록 함
             * 
             * 위에서 밟았을 때에만 코루틴이 작동해야하므로
             */
            if (collision.relativeVelocity.y < 0)
            {
                // collision의 충돌 직전의 속도가 0보다 작다는 것은
                // 충돌체가 위에서 아래로 내려오고 있다는 뜻이므로
                // 충돌체가 위에서 밟았을 때에만 일정시간 후 삭제하는 코루틴 작동
                StartCoroutine(FlatDelete()); 
            }
        }
    }

    /// <summary>
    /// HealFlat의 깜빡임을 조절하는 코루팀
    /// </summary>
    /// <returns></returns>
    IEnumerator FlatDelete()
    {
        float timeTerm = 0.3f; // 깜빡이는 간격을 조정하는 타이머 - 원하는 시간으로 조정 가능
        while (DeleteTime > 0) // 지워지려는 시간이 0보다 큰 동안에 깜빡임 반복
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f); // 이미지의 투명도 50% 설정
            yield return new WaitForSeconds(timeTerm); // 일정시간 유지
            DeleteTime -= timeTerm; // timeTerm이 지나간 만큼 DeletTime 감소 
            spriteRenderer.color = new Color(1, 1, 1, 1f); // 이미지의 투명도 100% 설정
            yield return new WaitForSeconds(timeTerm); // 일정시간 유지
            DeleteTime -= timeTerm; // timeTerm이 지나간 만큼 DeletTime 감소 
        }
        // DeleteTime 이 0이되면 오브젝트를 삭제함
        Destroy(gameObject); // 삭제까지 시간이 지나면 HealFlat 오브젝트 삭제
    }
}
