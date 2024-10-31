using UnityEngine;

public class MoveLenPlatform : MonoBehaviour
{
    // 일정한 속도로
    [SerializeField] public float moveSpeed;

    // 방향별로 이동
    // PlatformAttackPattern에서 라인별로 받아오도록 구현하기
    // [SerializeField] PatternController patternController;
    [SerializeField] public bool isUpMove;

    private void Start()
    {
        int num = Random.Range(0, 2);
        switch (num)
        {
            case 0:
                isUpMove = false;
                Debug.Log("다운");
                break;
            case 1:
                isUpMove = true;
                Debug.Log("업");
                break;
        }

    }

    private void Update()
    {
        if (isUpMove)
        {
            MoveUp();
        }
        else if (!isUpMove)
        {
            MoveDown();
        }
    }

    /// <summary>
    /// 발판을 일정한 속도로 위로 계속 이동
    /// </summary>
    public void MoveUp()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }

    public void MoveDown()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }

}
