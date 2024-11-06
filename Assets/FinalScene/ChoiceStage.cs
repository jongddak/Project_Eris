using UnityEngine;

public class ChoiceStage : MonoBehaviour
{
    bool stage1;
    bool stage2;
    bool stage3;

    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] LayerMask layerMask;

    private void Awake()
    {
        DataManager.Instance.LoadGameData(); // �ҷ�����
        stage1 = DataManager.Instance.data.isUnlock[0];
        stage2 = DataManager.Instance.data.isUnlock[1];
        stage3 = DataManager.Instance.data.isUnlock[2];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Collider2D player = Physics2D.OverlapBox(boxCollider.offset + (Vector2)transform.position, boxCollider.size, 0, layerMask);
            
            if(player == null)
            {
                return;
            }
            switch (gameObject.name) // �÷��̾�� �浹�� ��� ���� ������Ʈ�� �̸����� �б�
            {
                case "Stage1":
                    GameManager.Instance.LoadSceneByName("Boss1DStart");
                    break;
                case "Stage2":
                    if (stage1)
                    {
                        GameManager.Instance.LoadSceneByName("Boss2DStart");
                    }
                    break;
                case "Stage3":

                    if (stage2)
                    {
                        GameManager.Instance.LoadSceneByName("Boss3DStart");
                    }
                    break;
            }
        }
    }
    /*
    /// <summary>
    /// Ʈ���Ű� �߻� ���� ���ȿ�
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (gameObject.name) // �÷��̾�� �浹�� ��� ���� ������Ʈ�� �̸����� �б�
            {
                case "Stage1":
                    if (Input.GetKey(KeyCode.F))
                    {
                        GameManager.Instance.LoadSceneByName("Boss1DStart");
                    }
                    break;
                case "Stage2":
                    if (Input.GetKey(KeyCode.F))
                    {
                        if (stage1)
                        {
                            GameManager.Instance.LoadSceneByName("Boss2DStart");
                        }
                    }
                    break;
                case "Stage3":
                    if (Input.GetKey(KeyCode.F))
                    {
                        if (stage2)
                        {
                            GameManager.Instance.LoadSceneByName("Boss3DStart");
                        }
                    }
                    break;
            }
        }
    }*/
}
