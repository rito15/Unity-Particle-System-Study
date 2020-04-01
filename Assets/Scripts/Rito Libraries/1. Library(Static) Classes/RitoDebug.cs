using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito
{
    // 2020. 01. 15. 깔끔히 정리, in 한정자 추가

    /// <summary>
    /// <para/> [사용자 정의 디버거 클래스]
    /// <para/> - 유니티 엔진에서 디버그로 사용할 기능을 선택적으로 적용
    /// <para/> - 특정 클래스의 로그 출력에 대해, 일괄 적용 여부 선택 가능(GUI 연동)
    /// <para/> - 호출자의 정보를 로그로 즉각 출력 가능
    /// <para/> - GUI 연동 : DebugController
    /// </summary>
    public static class RitoDebug
    {

        /// <summary> 모든 디버그 기능 사용 여부 </summary>
        public static bool DEBUG = true;

        #region Log

        /// <summary>
        /// 디버그 변수, 로그 기록 내용 정의하여 해당변수가 true일 때만 로그 출력
        /// <para/> 추가 기능 : GUI와 연동하여 각 클래스마다 로그 출력 여부 선택 가능
        /// </summary>
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Log<T>(in T log,
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "")
        {
            if (!DEBUG) return;
            //Debug.Log(className);

            // 2020. 01. 11. 추가 : 오토 디버깅(GUI 연결) - 클래스 이름을 인식하여 PlayerPrefs 참조,
            // 얻은 값에 따라 디버그 출력 여부 결정
            var onOff = PlayerPrefs.GetInt(GetClassName(sourceFilePath), 0);
            if (onOff != 1) return;

            Debug.Log(log);
        }

        #endregion // ==========================================================

        #region Warn, Error

        /// <summary>
        /// 강조해서 경고해주기
        /// </summary>
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Warn<T>(in T log,
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "")
        {
            if (!DEBUG) return;
            if (PlayerPrefs.GetInt(GetClassName(sourceFilePath), 0) != 1) return;

            Debug.Log("[WARNNING !] - " + log);
        }

        /// <summary>
        /// 무언가가 (꼭 필요한데) 없다고 경고해주기
        /// </summary>
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void WarnMissing<T>(in T target,
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "")
        {
            if (!DEBUG) return;
            if (PlayerPrefs.GetInt(GetClassName(sourceFilePath), 0) != 1) return;

            Debug.Log("[MISSING !] - " + target + "이 없습니다");
        }

        /// <summary>
        /// 에러 메시지 출력(에러일 경우 씬정지)
        /// </summary>
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Error<T>(T log,
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "")
        {
            if (!DEBUG) return;
            if (PlayerPrefs.GetInt(GetClassName(sourceFilePath), 0) != 1) return;

            Debug.LogError("[ERROR !] - " + log);
        }

        #endregion // ==========================================================

        #region Reflection : Mark

        /// <summary>
        /// 메소드 호출 전파 추적용 메소드
        /// </summary>
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Mark(
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath]   string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0
        )
        {
            if (!DEBUG) return;

            string className = GetClassName(sourceFilePath);

            if (PlayerPrefs.GetInt(className, 0) == 1)
            {
                Debug.Log($"[Mark] " +
                $"{className}.{memberName}, " +
                $"{sourceLineNumber}");
            }
        }

        #endregion // ==========================================================

        #region Raycast Check

        // 레이캐스트 타겟 이름 디버그
        public static void RaycastFromCamera(in int layer = -1, in string msg = "",
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "")
        {
            if (!DEBUG) return;
            if (PlayerPrefs.GetInt(GetClassName(sourceFilePath), 0) != 1) return;

            if (RitoRaycaster.CamToMouse(out RaycastHit hit, layer))
                Debug.Log("[레이캐스트 " + msg + "] : " + hit.collider.name);
        }

        #endregion // ==========================================================

        #region Null Check

        /// <summary>
        /// 해당 객체가 Null이면 디버그 로그로 출력 + true 리턴
        /// </summary>
        /// <returns></returns>
        public static bool NullCheck(in Object obj,
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath]   string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            if (obj != null) return false;

            string className = GetClassName(sourceFilePath);

            if (PlayerPrefs.GetInt(className, 0) == 1 && DEBUG)
            {
                Debug.Log($"[Null Object Found] - " +
                $"호출자 : {className}.{memberName}, " +
                $"줄 : {sourceLineNumber}");
            }
            return true;
        }

        #endregion // ==========================================================

        #region Static Methods

        /// <summary>
        /// 호출자 클래스명, 메소드명, 라인번호 리턴
        /// </summary>
        public static string GetFileInfo(
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath]   string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0
        )
        {
            string fileName = sourceFilePath.Substring(sourceFilePath.LastIndexOf('\\') + 1);
            string className = fileName.Substring(0, fileName.IndexOf('.'));

            return "[" + className + "." + memberName + "/" + sourceLineNumber + "]";
        }

        /// <summary>
        /// <para/> [Private]
        /// <para/> sourceFilePath 스트링에서 클래스명 찾아 리턴
        /// </summary>
        private static string GetClassName(in string sourceFilePath)
        {
            int begin = sourceFilePath.LastIndexOf(@"\");
            int end = sourceFilePath.LastIndexOf(@".cs");
            return sourceFilePath.Substring(begin + 1, end - begin - 1);
        }

        #endregion // ==========================================================

    }
}