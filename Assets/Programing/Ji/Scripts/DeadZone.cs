using UnityEngine;

public class DeadZone : MonoBehaviour
{
    /// <summary>
    /// DeadZone에 trigger로 판정
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            /* tag가 DeadZone인 경우 
             * 플레이어의 사망
             * DieUpdate()
             */
            Debug.Log("플레이어 사망");
        }
    }
}
