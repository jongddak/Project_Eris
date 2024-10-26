using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // Scene의 상태를 저장
    public enum SceneState { Start, Phase1, Phase2 };
    /* Start : Scene에 들어온 순간, 시작하자마자의 상태
     * Phase1 : 대사를 전부 출력한 후 Phase1 시작
     *          - Phase1이면서 보스의 상태가 Phase2로 변할 때 Phase2로 변동
     *          - Boss의 Phase를 씬으로 분리할지, 애니메이션과 상태로만 분리할지에 따라 달라져야함
     * Phase2 : Phase2로 변하자마자 보스의 대사 스크립트를 출력
     *          - Phase2일 때, 보스가 사망하면 사망 스크립트를 출력해야 함
     */
    public SceneState nowSceneState; // 현재 씬의 상태를 저장

    [SerializeField] DialogueSystem dialogueSystem; // DialogueSystem.cs를 할당

    private void Awake()
    {
        nowSceneState = SceneState.Start; // 씬이 시작하자마자 Start 상태로 시작
    }

    private void Update()
    {
        // 현재 씬의 상태에 따라서 진행하는 함수로 구현 - 상태패턴
        switch (nowSceneState)
        {
            case SceneState.Start:
                dialogueSystem.StartDialogue(); // DialogueSystem.cs에서 시작시 출력하는 대사
                break;
            case SceneState.Phase1:
                dialogueSystem.Phase1Dialogue(); // DialogueSystem.cs에서 Phase1 종료시 출력하는 대사
                break;
            case SceneState.Phase2:
                dialogueSystem.Phase2Dialogue(); // DialogueSystem.cs에서 Phase2 종료시 출력하는 대사
                break;
        }

    }
}
