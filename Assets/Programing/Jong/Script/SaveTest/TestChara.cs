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


    // 데이터 저장 및 불러오기 예시 , 데이터를 저장 및 불러오기를 하려면 LoadGameData() , SaveGameData();  가 선행 되어야 함 
    // 저장이 필요한 타이밍에 아래와 비슷하게 작성해서 저장 및 불러오기를 해야함 
    private void Start()
    {
        SaveTest.Instance.LoadGameData(); // 불러오기
        Loading();
    }

    private void OnApplicationQuit()
    {
        Saving();
        SaveTest.Instance.SaveGameData(); // 저장하기 
    }

    public void Saving() // 씬에 있는 인스턴스로 데이터 전달 
    {
        SaveTest.Instance.data.isUnlock[0] = firstStage;
        SaveTest.Instance.data.isUnlock[1] = secondStage;
        SaveTest.Instance.data.isUnlock[2] = thridStage;


        SaveTest.Instance.data.vol = BgmVol;
        SaveTest.Instance.data.atk = Atk;
        SaveTest.Instance.data.speed = moveSpeed;

        SaveTest.Instance.data.dash = ondash;
        SaveTest.Instance.data.hover = onhover;
    }
    public void Loading() // json 데이터 파일에서 불러온 데이터를 뽑아옴 
    {
        firstStage = SaveTest.Instance.data.isUnlock[0];
        secondStage = SaveTest.Instance.data.isUnlock[1];
        thridStage = SaveTest.Instance.data.isUnlock[2];


        BgmVol = SaveTest.Instance.data.vol;
        Atk = SaveTest.Instance.data.atk;
        moveSpeed = SaveTest.Instance.data.speed;

        ondash = SaveTest.Instance.data.dash;
        onhover = SaveTest.Instance.data.hover;
    }

}
