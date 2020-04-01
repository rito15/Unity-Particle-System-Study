#define USE_AWAKE
#define USE_START
#define USE_UPDATE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito
{
    /// <summary>
    /// <para/> 타이머 
    /// <para/> [기능]
    /// <para/> - 타이머 인스턴스 생성 시각으로부터 경과시간 체크
    /// <para/> - Check(float변수) : 현재 시각을 변수에 기억(초단위)
    /// <para/> - GUI로 게임 화면에 경과 시간을 실시간 표시 가능
    /// <para/> -------------
    /// <para/> * 싱글톤
    /// <para/> 생성일 : 2019-11-20 AM 12:27:37
    /// </summary>
    public class Timer : RitoSingleton<Timer>
    {
        #region Fields

        // 게임 시작 이후 경과 시간 저장
        public int Hour { get; private set; } = 0;   // 시
        public int Min { get; private set; } = 0;    // 분
        public int Sec { get; private set; } = 0;    // 초

        public float CurrentTime { get; private set; } = 0.0f; // 현재 경과시간

        #endregion //--------------------------------------------------------------

        #region Inspector Option Fields

        [Header("GUI 설정")]
        public bool useGui = true;                  // GUI 시간 표시 여부
        public Color guiColor = Color.white;        // GUI 글자 색상
        public int fontSize = 50;                   // GUI 글자 크기 (0이면 화면 상대 값으로 설정)

        [Header("GUI 배치")]
        public int xPos = 0;
        public int yPos = 0;
        public TextAnchor anchor = TextAnchor.UpperCenter;

        #endregion //--------------------------------------------------------------

        #region Unity Events

#if USE_AWAKE

        protected override void Awake()
        {
            base.Awake();
        }

#endif

#if USE_START_

        private void Start()
        {

        }

#endif

#if USE_UPDATE_

        private void Update()
        {

        }

#endif
        private void FixedUpdate()
        {
            CurrentTime += Time.fixedDeltaTime;

            Sec = (int)(CurrentTime % 60f);
            Min = (int)(CurrentTime / 60) % 60;
            Hour = (int)(CurrentTime / 3600);
        }

        #endregion //--------------------------------------------------------------

        #region GUI

        private GUIStyle style;
        private Rect rect;
        private string text;

        private void SetGUI()
        {
            int w = Screen.width, h = Screen.height;

            // GUI 위치, 크기 설정
            rect = new Rect(xPos, yPos, w, h);

            style = new GUIStyle
            {
                alignment = anchor,
                fontSize = (fontSize > 0 ? fontSize : h * 4 / 100)
            };

            style.normal.textColor = guiColor;
        }

        // 화면에 GUI 표시
        private void OnGUI()
        {
            if (useGui == false) return;

            SetGUI();

            text =
                (Hour < 10 ? "0" : "") + Hour + " : " +
                (Min < 10 ? "0" : "") + Min + " : " +
                (Sec < 10 ? "0" : "") + Sec;

            GUI.Label(rect, text, style);
        }

        #endregion //--------------------------------------------------------------

        #region Public Methods

        // 경과 시간 출력
        public void PrintElapsedTime()
        {
            Debug.Log("GAME TIME : [" +
                (Hour < 10 ? "0" : "") + Hour + " : " +
                (Min < 10 ? "0" : "") + Min + " : " +
                (Sec < 10 ? "0" : "") + Sec + "]");
        }

        // 변수에 시간 지점 체크
        public float Check(out float variable)
        {
            variable = CurrentTime;
            return CurrentTime;
        }
        public float Check()
        {
            return CurrentTime;
        }

        // [체크된 시간 변수]로부터 현재까지의 경과 시간이 targetTime 이상인지 검사
        public bool CheckTimeElapsed(ref float checkedTimeVariable, float targetTime)
        {
            return (CurrentTime - checkedTimeVariable >= targetTime);
        }

        #endregion //--------------------------------------------------------------

    }
}