using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BornCollider : MonoBehaviour
{
    public GameObject[] bones; // ���̷��� ĳ������ �� ����Ʈ�� �迭�� ����
    private BoxCollider2D boxCollider;

    private PlayerController playerController;
    private Vector3 originalSize;
    private Vector2 originalOffset;

    private void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider2D>(); // ���ٸ� �߰�
        }

        originalSize = boxCollider.size;
        originalOffset = boxCollider.offset;
        playerController = gameObject.GetComponent<PlayerController>();
    }

    private void Update()
    {
        //�÷��̾� ���°� Idle or Run�� �ƴ� �� �ݶ��̴� ������Ʈ
        if (playerController.curState != PlayerController.PlayerState.Idle &&
            playerController.curState != PlayerController.PlayerState.Run)
        {
            UpdateColliderBounds();
        }
        else
        {
            boxCollider.offset = originalOffset;
            boxCollider.size = originalSize;
        }
        
    }

    private void UpdateColliderBounds()
    {
        if (bones == null || bones.Length == 0)
        {
            return;
        }

        /*float minX = bones[0].transform.position.x;
        float maxX = bones[0].transform.position.x;*/
        float minY = bones[0].transform.position.y;
        float maxY = bones[0].transform.position.y;

        // ��� ���� x, y ��ǥ�� ���Ͽ� ������ ã��
        foreach (var bone in bones)
        {
            Vector2 bonePos = bone.transform.position;

            /*if (bonePos.x < minX) minX = bonePos.x;
            if (bonePos.x > maxX) maxX = bonePos.x;*/
            if (bonePos.y < minY) minY = bonePos.y; 
            if (bonePos.y > maxY) maxY = bonePos.y + 1.5f;
        }

        // �ݶ��̴��� �߽� ��ġ�� ũ�� ���
        Vector2 center = new Vector2(transform.position.x, (minY + maxY) / 2);
        Vector2 size = new Vector2(boxCollider.size.x, maxY - minY);

        boxCollider.offset = transform.InverseTransformPoint(center); // ���� ��ǥ��� ��ȯ
        boxCollider.size = size;
    }
}
