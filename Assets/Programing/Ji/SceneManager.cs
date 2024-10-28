using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // Scene의 상태를 저장
    public enum SceneState { Start, Phase1Attack, Phase1Talk, Phase2, MonsterDie };
    /* Start : Scene에 들어온 순간, 시작하자마자의 상태
     * Phase1 : 대사를 전부 출력한 후 Phase1 시작
     *          - Phase1이면서 보스의 상태가 Phase2로 변할 때 Phase2로 변동
     *          - Boss의 Phase를 씬으로 분리할지, 애니메이션과 상태로만 분리할지에 따라 달라져야함
     * Phase2 : Phase2로 변하자마자 보스의 대사 스크립트를 출력
     *          - Phase2일 때, 보스가 사망하면 사망 스크립트를 출력해야 함
     */
    public SceneState nowSceneState; // 현재 씬의 상태를 저장

    [SerializeField] DialogueManager dialogueManager; // DialogueSystem.cs를 할당

    [Header("UI")]
    [SerializeField] private GameObject imgBoss; // 보스 캐릭터의 대화 시 출력 이미지
    [SerializeField] private GameObject imgPlayer; // 플레이어 캐릭터의 대화 시 출력 이미지
    [SerializeField] private GameObject imgDialogue; // 대화창 이미지
    [SerializeField] private GameObject uiTextName; // 대화하는 캐릭터의 이름 출력 오브젝트
    [SerializeField] private GameObject uiTextDialogue; // 캐릭터가 대사 출력 오브젝트

    private void Awake()
    {
        imgBoss.SetActive(false); // 보스 캐릭터의 대화 시 출력 이미지
        imgPlayer.SetActive(false); // 플레이어 캐릭터의 대화 시 출력 이미지
        imgDialogue.SetActive(true); // 대화창 이미지
        uiTextName.SetActive(false); // 대화하는 캐릭터의 이름 출력 오브젝트
        uiTextDialogue.SetActive(false); // 캐릭터가 대사 출력 오브젝트
        /*
        imgBoss = dialogueManager.imgBoss; // 보스 캐릭터의 대화 시 출력 이미지
        imgPlayer = dialogueManager.imgPlayer; // 플레이어 캐릭터의 대화 시 출력 이미지
        imgDialogue = dialogueManager.imgDialogue; // 대화창 이미지
        uiTextName = dialogueManager.uiTextName; // 대화하는 캐릭터의 이름 출력 오브젝트
        uiTextDialogue = dialogueManager.uiTextDialogue; // 캐릭터가 대사 출력 오브젝트
        */
    }

    private void Start()
    {
        nowSceneState = SceneState.Start; // 씬이 시작하자마자 Start 상태로 시작
    }
 /*   private void Update()
    {
        switch (nowSceneState)
        {
            case SceneState.Start:
                imgBoss.SetActive(true); // 보스 캐릭터의 대화 시 출력 이미지
                uiTextName.SetActive(true); // 대화하는 캐릭터의 이름 출력 오브젝트
                imgPlayer.SetActive(true); // 플레이어 캐릭터의 대화 시 출력 이미지
                uiTextName.SetActive(true); // 대화하는 캐릭터의 이름 출력 오브젝트
                uiTextDialogue.SetActive(true);
                dialogueManager.NowDialogue(0, 5);
                imgBoss.SetActive(false); // 보스 캐릭터의 대화 시 출력 이미지
                uiTextName.SetActive(false); // 대화하는 캐릭터의 이름 출력 오브젝트
                imgPlayer.SetActive(false); // 플레이어 캐릭터의 대화 시 출력 이미지
                uiTextName.SetActive(false); // 대화하는 캐릭터의 이름 출력 오브젝트
                uiTextDialogue.SetActive(false);
                nowSceneState = SceneState.Phase1Attack;
                break;
            case SceneState.Phase1Attack:
                break;
            case SceneState.Phase1Talk:

                break;
            case SceneState.Phase2:
                break;
            case SceneState.MonsterDie:

                break;
        }

    }*/


}
