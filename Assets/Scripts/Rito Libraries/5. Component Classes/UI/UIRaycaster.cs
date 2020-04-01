using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using NaughtyAttributes;
using Rito;
using RitoExtension;

// 2020. 01. 21. 최초 작성
// 2020. 02. 02. Update코루틴(0.01f) -> 다시 Update로 변경

/// <summary>
/// <para/> 2020. 01. 21.
/// <para/> 간편히 사용 가능한 UI 레이캐스트 체크 클래스
/// <para/> UI 최상위 오브젝트에 컴포넌트로 넣어 사용(캔버스 단위로 동작)
/// <para/> GraphicRacaster, Canvas 컴포넌트 필요
/// <para/> -------------------------------------------------------------------------------
/// <para/> * 하위 오브젝트에 Canvas 또는 또다른 GraphicRaycaster가 존재하는 경우,
/// <para/> 상위 오브젝트에 있는 GraphicRaycaster의 영향을 받지 않는다.
/// </summary>
[RequireComponent(typeof(GraphicRaycaster))]
public sealed class UIRaycaster : MonoBehaviour
{
    #region Public Fields

    /// <summary>
    /// UI 레이캐스트 타겟들
    /// </summary>
    [BoxGroup("UI Raycaster"), Label("레이캐스트 결과 UI 목록")]
    public List<Transform> TargetList;// { get; private set; }

    [BoxGroup("UI Raycaster"), Header("마우스 부착 타겟")]
    public Transform attachedTarget;

    [BoxGroup("UI Raycaster"), Header("마우스 부착 타겟 초기 위치")]
    public Vector3 attachedTargetOrginalPos;

    #endregion // ==========================================================

    #region Private Fields

    private GraphicRaycaster gr;
    private List<RaycastResult> ResultList;
    private PointerEventData Ped;

    // 더블클릭 체크용
    private bool isLeftClicked = false;
    private float clickThreashold = 0.3f;   // 더블클릭 허용 시간
    private Vector3 leftClickPoint = Vector3.zero;  // 클릭 지점

    #endregion // ==========================================================

    #region Unity Events

    private void Awake()
    {
        gr         = this.Ex_GetComponent<GraphicRaycaster>();
        ResultList = new List<RaycastResult>();
        TargetList = new List<Transform>();
        Ped        = new PointerEventData(null);
    }

    private void Start()
    {
        StartCoroutine("UpdateRoutine");
    }

    private void Update()
    {
        // attachTarget이 존재할 경우, 마우스 따라다니기
        if (attachedTarget != null && attachedTarget.gameObject.activeInHierarchy)
            attachedTarget.position = Input.mousePosition;

        // 좌측 더블클릭 체크용
        if (GetMouseUpOnUI(0))
        {
            StartCoroutine("ClickCheckRoutine");
        }
        if (GetLeftMouseDoubleClickOnUI())
        {
            isLeftClicked = false;

            Debug.Log($"Left Double Clicked - {leftClickPoint}");
        }
    }

    #endregion // ==========================================================

    #region Checker Methods

    /// <summary>
    /// <para/> [Public]
    /// <para/> 마우스가 UI 위에 올라와 있는지 검사
    /// </summary>
    public bool IsMouseOverUI()
    {
        // 이벤트 시스템이 없는 경우 생성
        if (EventSystem.current == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }

        return EventSystem.current.IsPointerOverGameObject();
    }

    /// <summary>
    /// <para/> [Public]
    /// <para/> UI에 마우스 클릭을 유지하고 있는지 여부 검사
    /// </summary>
    public bool GetMouseOnUI(in int button = 0)
    {
        return IsMouseOverUI() &&
            Input.GetMouseButton(button);
    }

    /// <summary>
    /// <para/> [Public]
    /// <para/> UI에 마우스를 클릭했는지 여부 검사
    /// </summary>
    public bool GetMouseDownOnUI(in int button = 0)
    {
        return IsMouseOverUI() &&
            Input.GetMouseButtonDown(button);
    }

    /// <summary>
    /// <para/> [Public]
    /// <para/> UI 위에서 마우스를 뗐는지 여부 검사
    /// </summary>
    public bool GetMouseUpOnUI(in int button = 0)
    {
        return IsMouseOverUI() &&
            Input.GetMouseButtonUp(button);
    }

    /// <summary>
    /// <para/> [Public]
    /// <para/> UI에 마우스 좌측 버튼을 더블클릭했는지 여부 검사
    /// </summary>
    public bool GetLeftMouseDoubleClickOnUI()
    {
        return IsMouseOverUI() && 
            isLeftClicked &&
            Input.mousePosition == leftClickPoint &&
            Input.GetMouseButtonDown(0);
    }

    #endregion // ==========================================================

    #region Private Methods

    #endregion // ==========================================================

    #region Public Methods

    /// <summary>
    /// <para/> 마우스 위치에 레이캐스트하여 결과 UI들을 리스트에 넣기
    /// </summary>
    public void RaycastToMousePoint()
    {
        ResultList.Clear();
        Ped.position = Input.mousePosition;
        gr.Raycast(Ped, ResultList);

        TargetList.Clear();
        foreach (var item in ResultList)
        {
            TargetList.Add(item.gameObject.transform);
        }
    }

    /// <summary>
    /// <para/> [Public]
    /// <para/> 현재 마우스 위치에 targetUI가 포함되어 있는지 검사
    /// </summary>
    public bool IsTargetOnMousePoint(in Transform targetUI)
    {
        if (targetUI == null)
        {
            Debug.Log($"targetUI가 null입니다아아아아아아아아아ㅏ");
            return false;
        }

        RaycastToMousePoint();
        return TargetList.Contains(targetUI);
    }

    /// <summary>
    /// <para/> [Public]
    /// <para/> 특정 UI가 마우스 위치에 따라다니도록 부착
    /// <para/> + 부착하는 순간의 위치 기억
    /// </summary>
    public void AttachUIToMouse(in Transform targetUI)
    {
        if (targetUI == null)
        {
            Debug.Log($"targetUI가 null입니다아아아아아아아아아ㅏ");
            return;
        }

        attachedTargetOrginalPos = targetUI.position;
        attachedTarget = targetUI;
    }

    /// <summary>
    /// <para/> [Public]
    /// <para/> 마우스에 부착되었던 타겟 부착 해제
    /// </summary>
    public void DetachUI()
    {
        attachedTarget = null;
        attachedTargetOrginalPos = Vector3.zero;
    }

    /// <summary>
    /// <para/> [Public]
    /// <para/> 새로운 타겟의 위치와 부착 타겟의 원래 위치 서로 변경
    /// </summary>
    public void SwitchAttachedTargetPos(in Transform newTarget)
    {
        if (attachedTargetOrginalPos.Equals(Vector3.zero))
        {
            Debug.Log("부착 타겟이 등록되지 않아 위치를 서로 바꿀 수 없습니다.");
            return;
        }

        if (newTarget == null)
            return;

        Vector3 newPos = newTarget.position;

        newTarget.position = attachedTargetOrginalPos;
        attachedTarget.position = newPos;
    }

    #endregion // ==========================================================

    #region CoRoutines

    /// <summary>
    /// <para/> 코루틴 : 더블클릭 체크
    /// <para/> 
    /// </summary>
    private IEnumerator ClickCheckRoutine()
    {
        isLeftClicked = true;
        leftClickPoint = Input.mousePosition;

        yield return new WaitForSecondsRealtime(clickThreashold);
        isLeftClicked = false;
    }

    #endregion // ==========================================================
}
