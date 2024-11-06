using UnityEngine;

public class Boss3Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed; // źȯ�� �̵��ӵ�
    AudioSource bulletSound; // �Ѿ� �߻���

    private void Awake()
    {
        bulletSound = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        bulletSound.Play();
    }
    private void OnDisable()
    {
        bulletSound.Stop();
    }
    private void Update()
    {
        // �Ѿ��� ���⿡ �°� ����
        gameObject.transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime, Space.Self);

    }

    public void SetSpeed(float speed)
    {
        this.bulletSpeed = speed;
    }
}
