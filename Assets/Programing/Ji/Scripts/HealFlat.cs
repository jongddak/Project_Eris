using System.Collections;
using UnityEngine;

public class HealFlat : MonoBehaviour
{
    // 코루틴으로 시간 타이밍을 조절
    // 시간 조절 후 오브젝트 삭제
    [Header("State")]
    [SerializeField] float DeleteTime; // 삭제까지 걸리는 시간 조절
    [SerializeField] SpriteRenderer spriteRenderer; // 발판의 이미지
    /*
        /// <summary>
        /// HealFlat에 충돌체가 충돌하는 순간 판단
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Finsh") // 추후 PlayerFoot 태그 추가 하여 변경 필요
            {
                /* tag가 PlayerFoot인 경우
                 * 플레이어가 밟은 순간 카운팅 활성화
                 * 특정 카운트가 지나면 오브젝트 삭제하도록
                 * HealFlat.cs에서 함수 작성해 불러오기
                 *
                StartCoroutine(FlatDelete());
                // DeleteFlat(); // 삭제하는 함수 실행
            }
        }
    */

    /// <summary>
    /// HealFlat에 Trigger 충돌체가 충돌할 때
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish") // 추후 PlayerFoot 태그 추가 하여 변경 필요
        {
            /* tag가 PlayerFoot인 경우
             * 플레이어가 밟은 순간 카운팅 활성화
             * 특정 카운트가 지나면 오브젝트 삭제하도록
             * HealFlat.cs에서 함수 작성해 불러오기
             */
            Debug.Log("충돌");
            StartCoroutine(FlatDelete());
            // DeleteFlat(); // 삭제하는 함수 실행
        }
    }
    /// <summary>
    /// 코루틴으로 시간을 조절한 후 현재 오브젝트 삭제
    /// </summary>
    public void DeleteFlat()
    {
        StartCoroutine(FlatDelete()); // 코루틴으로 시간 조절
    }

    /// <summary>
    /// HealFlat의 깜빡임을 조절하는 코루팀
    /// </summary>
    /// <returns></returns>
    IEnumerator FlatDelete()
    {
        float timeTerm = 0.5f; // 깜빡이는 간격을 조정하는 타이머
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
