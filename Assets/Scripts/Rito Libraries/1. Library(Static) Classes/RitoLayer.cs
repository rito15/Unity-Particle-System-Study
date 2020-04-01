using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito
{
    // 2020. 01. 15. 작성

    /// <summary>
    /// <para/> 레이어 관리, 레이어 비교(Compare 메소드)
    /// <para/> 
    /// <para/> ---------------------------------
    /// <para/> 2020. 01. 15. 23:52
    /// </summary>
    public static class RitoLayer
    {
        /* 1. 숫자로만 이루어진 레이어 */
        /// <summary> 레이어 아무것도 선택 X </summary>
        public const int none = 0;
        /// <summary> 모든 레이어 선택 </summary>
        public const int all = -1;
        public const int Number_IgnoreRaycast = 2;

        /* 2. 레이어 마스크(shifted) */
        public const int IgnoreRaycast = 1 << 2;


        #region Public Methods

        /// <summary>
        /// 레이어 비교 : 레이어 번호 및 레이어 마스크 모두 비교 가능
        /// </summary>
        public static bool Compare(in int layerA, in int layerB)
        {
            if (layerA      == layerB) return true;
            if (1 << layerA == layerB) return true;
            if (layerA      == 1 << layerB) return true;
            if (1 << layerA == 1 << layerB) return true;

            return false;
        }

        #endregion // ==========================================================

        #region Comparations - GameObject, Transform Overloadings

        /// <summary>
        /// 레이어 비교 : 게임오브젝트 &lt;-&gt; 레이어(번호 및 마스크)
        /// </summary>
        public static bool Compare(in GameObject gobj, in int layerB)
        {
            if (gobj == null) return false;

            int layerA = gobj.layer;
            return Compare(layerA, layerB);
        }
        /// <summary>
        /// 레이어 비교 : 게임오브젝트
        /// </summary>
        public static bool Compare(in GameObject gobjA, in GameObject gobjB)
        {
            if (gobjA == null) return false;
            if (gobjB == null) return false;

            return (gobjA.layer == gobjB.layer);
        }

        /// <summary>
        /// 레이어 비교 : 트랜스폼 &lt;-&gt; 레이어(번호 및 마스크)
        /// </summary>
        public static bool Compare(in Transform transform, in int layerB)
        {
            if (transform == null) return false;

            int layerA = transform.gameObject.layer;
            return Compare(layerA, layerB);
        }
        /// <summary>
        /// 레이어 비교 : 트랜스폼
        /// </summary>
        public static bool Compare(in Transform trA, in Transform trB)
        {
            if (trA == null) return false;
            if (trB == null) return false;

            return (trA.gameObject.layer == trB.gameObject.layer);
        }

        /// <summary>
        /// 레이어 비교 : 게임오브젝트 &lt;-&gt; 트랜스폼
        /// </summary>
        public static bool Compare(in GameObject gobjA, in Transform trB)
        {
            if (gobjA == null) return false;
            if (trB == null) return false;

            return (gobjA.layer == trB.gameObject.layer);
        }

        /// <summary>
        /// 레이어 비교 : 트랜스폼 &lt;-&gt; 게임오브젝트
        /// </summary>
        public static bool Compare(in Transform trA, in GameObject gobjB)
        {
            if (trA == null) return false;
            if (gobjB == null) return false;

            return (trA.gameObject.layer == gobjB.layer);
        }

        #endregion // ==========================================================
    }
}