using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 2020. 01. 25. <para/>
/// UI 패널을 상하/좌우로 미끄러지듯 이동시켜주는 컴포넌트(버튼 등록 필요)
/// </summary>
public class UISlider : MonoBehaviour
{
    #region Fields

    [Header("UI가 움직일 방향")]
    public Direction _nextDirection = Direction.Left;

    [Header("슬라이드 트리거 버튼 등록")]
    public Button _triggerButton;

    [Header("(추가 옵션)슬라이드 트리거 버튼 추가 등록")]
    public List<Button> _triggerButtonList = new List<Button>();

    [Header("슬라이드에 걸리는 총 시간")]
    public float _duration = 0.5f;


    [Header("슬라이드 거리 - 가로 (0 입력 시 - 기본 : transform.width)")]
    public float _slideWidth;

    [Header("슬라이드 거리 - 세로 (0 입력 시 - 기본 : transform.height)")]
    public float _slideHeight;


    [Header("UI Rect Transform")]
    private RectTransform _rectTransform;

    [Header("현재 UI가 미끄러지는 중")]
    private bool _isMoving = false;


    private WaitForSeconds _WFS_01f = new WaitForSeconds(0.01f);

    #endregion // ==========================================================

    #region Unity Events

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();

        if (_slideWidth  <= 0) _slideWidth  = _rectTransform.rect.width;
        if (_slideHeight <= 0) _slideHeight = _rectTransform.rect.height;

        if (_triggerButton)
            _triggerButton.onClick.AddListener(UISlide);

        // 추가기능 - 버튼 여러개 추가
        if (_triggerButtonList?.Count > 0)
            foreach (var item in _triggerButtonList)
            {
                if(item != null)
                    item.onClick.AddListener(UISlide);
            }
    }

    #endregion // ==========================================================

    #region Methods

    /// <summary>
    /// UI가 미끄러지며 이동(방향은 필드 확인)
    /// </summary>
    public void UISlide()
    {
        if (_isMoving) return;

        // _nextDirection에 설정된 방향에 따라 이동
        StartCoroutine(SlideRoutine(_nextDirection));
        _nextDirection = (Direction)(-(int)_nextDirection);
    }

    #endregion // ==========================================================

    #region Coroutines

    /// <summary>
    /// <para/> 코루틴 : 상하 또는 좌우로 UI 미끄러지기
    /// </summary>
    private IEnumerator SlideRoutine(Direction dir)
    {
        // 버튼 조작 비활성화
        if (_triggerButton) _triggerButton.interactable = false;
        _isMoving = true;


        // ====================================================================
        // [Clamp]duration은 0.1 이상 10 이하
        if (_duration < 0.1f) _duration = 0.1f;
        if (_duration > 10f) _duration = 10f;

        // 이동 방향 계산
        Vector3 move = Vector3.zero;
        switch(dir)
        {
            case Direction.Left:  move = Vector3.left  * _slideWidth; break;
            case Direction.right: move = Vector3.right * _slideWidth; break;
            case Direction.Up:    move = Vector3.up    * _slideHeight; break;
            case Direction.Down:  move = Vector3.down  * _slideHeight; break;
        }
        move *= (0.01f / _duration);

        /* 이동 */
        for (int i = 0; i < (int)(_duration * 100); i++)
        {
            yield return _WFS_01f;
            transform.Translate(move);
        }
        // ====================================================================


        // 버튼 조작 활성화
        if (_triggerButton) _triggerButton.interactable = true;
        _isMoving = false;
    }

    #endregion // ==========================================================

    #region Enums

    public enum Direction
    {
        Left  = -1,
        right = 1,

        Up    = -2,
        Down  = 2,
    }

    #endregion // ==========================================================
}