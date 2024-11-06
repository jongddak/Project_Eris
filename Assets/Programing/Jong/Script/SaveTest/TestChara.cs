using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChara : MonoBehaviour
{
    [SerializeField] float moveSpeed;
   
    [SerializeField] float Atk;

    [SerializeField] bool firstStage;
    [SerializeField] bool secondStage;
    [SerializeField] bool thridStage;

    [SerializeField] bool ondash;
    [SerializeField] bool onhover;



    [Range(0, 1)]
    [SerializeField] float BgmVol;


    // ������ ���� �� �ҷ����� ���� , �����͸� ���� �� �ҷ����⸦ �Ϸ��� LoadGameData() , SaveGameData();  �� ���� �Ǿ�� �� 
    // ������ �ʿ��� Ÿ�ֿ̹� �Ʒ��� ����ϰ� �ۼ��ؼ� ���� �� �ҷ����⸦ �ؾ��� 
    private void Start()
    {
        DataManager.Instance.LoadGameData(); // �ҷ�����
        DataManager.Instance.data.isUnlock[0] = firstStage;
        DataManager.Instance.data.isUnlock[1] = secondStage;
        DataManager.Instance.data.isUnlock[2] = thridStage;
        DataManager.Instance.SaveGameData();
    }

    private void OnApplicationQuit()
    {
      //  Saving();
      //  DataManager.Instance.SaveGameData(); // �����ϱ� 
    }

    public void Saving() // ���� �ִ� �ν��Ͻ��� ������ ���� 
    {
        DataManager.Instance.data.isUnlock[0] = firstStage;
        DataManager.Instance.data.isUnlock[1] = secondStage;
        DataManager.Instance.data.isUnlock[2] = thridStage;


        DataManager.Instance.data.vol = BgmVol;
   

        DataManager.Instance.data.dash = ondash;
        DataManager.Instance.data.hover = onhover;
    }
    public void Loading() // json ������ ���Ͽ��� �ҷ��� �����͸� �̾ƿ� 
    {
        firstStage = DataManager.Instance.data.isUnlock[0];
        secondStage = DataManager.Instance.data.isUnlock[1];
        thridStage = DataManager.Instance.data.isUnlock[2];


        BgmVol = DataManager.Instance.data.vol;
     

        ondash = DataManager.Instance.data.dash;
        onhover = DataManager.Instance.data.hover;
    }

}
