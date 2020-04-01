using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Debug와 연결되어 동작하는 디버그 제어(On/Off) UI 컴포넌트
/// </summary>
public sealed class DebugControllerUI : MonoBehaviour
{
    /// <summary>
    /// 디버그 대상으로 등록할 클래스들
    /// </summary>
    [Header("디버그 대상으로 등록할 클래스 파일을 드래그하여 추가하세요")]
    public List<Object> debugTargetList = new List<Object>();

    /// <summary>
    /// 버튼 UI 프리팹
    /// </summary>
    [Header("버튼 UI프리팹 : 등록하지 않으면 스크립트가 작동하지 않습니다.")]
    public GameObject buttonUIPrefab;

    /// <summary>
    /// debugTargetList에 따라 생성한 버튼 오브젝트들
    /// </summary>
    private List<GameObject> debugButtonObjectList = new List<GameObject>();

    /// <summary>
    /// debugButtonObjectList에서 가져온 버튼 컴포넌트들
    /// </summary>
    private List<Button> debugButtonList = new List<Button>();

    /// <summary>
    /// 각 버튼의 자식 텍스트들
    /// </summary>
    private List<Text> debugButtonTextList = new List<Text>();

    private void Awake()
    {
        if (buttonUIPrefab == null) return;

        InitVerticalLayoutGroup();

        InstantiateButtonObjects();
        LoadDebugPrefs();
        AssignButtonEvent();
    }

    #region Init Methods

    /// <summary>
    /// <para/> [Private]
    /// <para/> Vertical Layout Group 컴포넌트가 없을 경우 생성 및 초기 설정
    /// </summary>
    private void InitVerticalLayoutGroup()
    {
        var vlg = GetComponent<VerticalLayoutGroup>();

        if (vlg == null)
        {
            vlg = gameObject.AddComponent<VerticalLayoutGroup>();
            vlg.childForceExpandWidth = false;
            vlg.childForceExpandHeight = false;

            vlg.childControlWidth = false;
            vlg.childControlHeight = false;

            vlg.childScaleWidth = false;
            vlg.childScaleHeight = false;

            vlg.childAlignment = TextAnchor.UpperCenter;
            vlg.padding = new RectOffset(10, 10, 10, 10);
            vlg.spacing = 10f;
        }

        if (vlg != null && vlg.enabled == false)
            vlg.enabled = true;
    }

    /// <summary>
    /// <para/> [Private]
    /// <para/> 등록된 클래스마다 버튼 프리팹을 인스턴스화하여 버튼 생성
    /// </summary>
    private void InstantiateButtonObjects()
    {
        foreach (var item in debugTargetList)
        {
            // 버튼 프리팹을 인스턴스화 + 부모 설정 + 게임오브젝트 이름 설정
            var buttonObj = Instantiate(buttonUIPrefab, transform);

            if (buttonObj)
            {
                buttonObj.gameObject.name = (item != null) ? item.name : "null";

                // 빈 버튼들은 비활성화
                buttonObj.SetActive(item != null);
            }

            // 오브젝트 리스트에 저장
            debugButtonObjectList.Add(buttonObj ?? null);

            // 버튼을 찾아 버튼 컴포넌트 리스트에 저장
            var button = buttonObj.GetComponent<Button>();
            debugButtonList.Add(button ?? null);

            // 버튼의 각 자식 텍스트 가져오기
            var text = button?.GetComponentInChildren<Text>();
            debugButtonTextList.Add(text ?? null);

            // 화면에 표시될 텍스트 변경
            if (text) text.text = (item != null) ? item.name : "null";
        }
    }

    /// <summary>
    /// <para/> [Private]
    /// <para/> 등록된 클래스들 이름으로 각각 PlayerPrefs 검색하여 버튼 상태 결정(On/Off)
    /// <para/> On : 흰 배경 + 검은 글씨 / Off : 검은 배경 + 흰 글씨
    /// </summary>
    private void LoadDebugPrefs()
    {
        for (int i = 0; i < debugTargetList.Count; i++)
        {
            if (debugTargetList[i] == null)
            {
                ChangeButtonColor(debugButtonList[i] ?? null, Color.black);
                ChangeTextColor(debugButtonTextList[i] ?? null, Color.white);
                continue;
            }

            int onOff = PlayerPrefs.GetInt(debugTargetList[i].name);

            // 디버그 true인 경우 : 버튼을 흰 배경 + 검은 글씨로 설정
            if (onOff == 1)
            {
                ChangeButtonColor(debugButtonList[i] ?? null, Color.white);
                ChangeTextColor(debugButtonTextList[i] ?? null, Color.black);
            }
            // 디버그 false인 경우 : 버튼을 검은 배경 + 흰 글씨로 설정
            else
            {
                ChangeButtonColor(debugButtonList[i] ?? null, Color.black);
                ChangeTextColor(debugButtonTextList[i] ?? null, Color.white);
            }
        }
    }

    /// <summary>
    /// <para/> [Private]
    /// <para/> 각 버튼에 클릭 이벤트 할당
    /// </summary>
    private void AssignButtonEvent()
    {
        foreach (var button in debugButtonList)
        {
            if (button.gameObject.name == "null") continue;

            button.onClick.AddListener(() =>
            {
                // 1. true일 때 클릭 시
                //  [1] PlayerPref 값을 0으로 설정
                //  [2] 검은 배경 + 흰 글씨로 변경
                if (PlayerPrefs.GetInt(button.gameObject.name) == 1)
                {
                    PlayerPrefs.SetInt(button.gameObject.name, 0);
                    ChangeButtonColor(button ?? null, Color.black);

                    var text = button.GetComponentInChildren<Text>();
                    if (text) text.color = Color.white;
                }
                // 2. false일 때 클릭 시
                //  [1] PlayerPref 값을 1로 설정
                //  [2] 흰 배경 + 검은 글씨로 변경
                else
                {
                    PlayerPrefs.SetInt(button.gameObject.name, 1);
                    ChangeButtonColor(button ?? null, Color.white);

                    var text = button.GetComponentInChildren<Text>();
                    if (text) text.color = Color.black;
                }
            });
        }
    }

    #endregion // ==========================================================

    #region Tiny Methods

    /// <summary>
    /// <para/> [Private]
    /// <para/> 버튼의 색상 변경
    /// </summary>
    private void ChangeButtonColor(in Button button, Color newColor)
    {
        if (button == null) return;

        var cb = button.colors;
        cb.normalColor = newColor;
        cb.highlightedColor = newColor;
        cb.selectedColor = newColor;
        cb.pressedColor = newColor;

        button.colors = cb;
    }

    /// <summary>
    /// <para/> [Private]
    /// <para/> 버튼에 달린 텍스트 색상 변경
    /// </summary>
    private void ChangeTextColor(in Text text, Color newColor)
    {
        if (text == null) return;

        text.color = newColor;
    }

    #endregion // ==========================================================

}
