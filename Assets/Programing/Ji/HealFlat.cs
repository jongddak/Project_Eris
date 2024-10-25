using System.Collections;
using UnityEngine;

public class HealFlat : MonoBehaviour
{
    // 코루틴으로 시간 타이밍을 조절
    // 시간 조절 후 오브젝트 삭제
    [Header("State")]
    [SerializeField] float DeleteTime; // 삭제까지 걸리는 시간 조절

    /// <summary>
    /// HealFlat에 충돌체가 충돌하는 순간 판단
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            /* tag가 HealFlat인 경우
             * 플레이어가 밟은 순간 카운팅 활성화
             * 특정 카운트가 지나면 오브젝트 삭제하도록
             * HealFlat.cs에서 함수 작성해 불러오기
             */
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

    IEnumerator FlatDelete()
    {
        yield return new WaitForSeconds(DeleteTime);
        Destroy(gameObject); // 삭제까지 시간이 지나면 HealFlat 오브젝트 삭제
    }
}
