#define USE_AWAKE
#define USE_START
#define USE_UPDATE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito
{
    /// <summary>
    /// <para/> 
    /// <para/> -------------
    /// <para/> * 싱글톤
    /// <para/> 생성일 : 2019-11-20 AM 12:43:39
    /// </summary>
    public class FrameCheck : RitoSingleton<FrameCheck>
    {
        #region Fields

        GUIStyle style;
        Rect     rect;
        string   text;

        float msec;
        float fps;
        float worstFps = 100f;
        float deltaTime = 0.0f;

        #endregion //--------------------------------------------------------------

        #region Unity Events

#if USE_AWAKE

        protected override void Awake()
        {
            base.Awake();

            int w = Screen.width, h = Screen.height;

            // GUI 위치, 크기 설정
            rect = new Rect(0, 0, w, h * 4 / 100);

            style = new GUIStyle
            {
                alignment = TextAnchor.UpperLeft,
                fontSize = h * 4 / 100
            };
            style.normal.textColor = Color.cyan;

            StartCoroutine("WorstReset");
        }

#endif

#if USE_START_

        private void Start()
        {

        }

#endif

#if USE_UPDATE

        private void Update()
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        }

#endif

        //소스로 GUI 표시
        private void OnGUI()
        {
            msec = deltaTime * 1000.0f;
            fps = 1.0f / deltaTime;    //초당 프레임

            // 새로운 최저 fps가 나왔다면 worstFps 바꿔줌
            if (fps < worstFps)
                worstFps = fps;

            text = msec.ToString("F1") + "ms (" + fps.ToString("F1") + ") //worst : " + worstFps.ToString("F1");
            GUI.Label(rect, text, style);
        }

        #endregion //--------------------------------------------------------------

        #region CoRoutines

        // 코루틴으로 15초 간격으로 최저 프레임 리셋해줌
        IEnumerator WorstReset()
        {
            while (true)
            {
                yield return new WaitForSeconds(15f);
                worstFps = 100f;
            }
        }

        #endregion //--------------------------------------------------------------
    }
}