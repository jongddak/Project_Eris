using UnityEngine;

public class Boss3Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed; // 탄환의 이동속도

    private void Update()
    {
        // 총알의 방향에 맞게 직진
        gameObject.transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime, Space.Self);
    }

    public void SetSpeed(float speed)
    {
        this.bulletSpeed = speed;
    }
}
