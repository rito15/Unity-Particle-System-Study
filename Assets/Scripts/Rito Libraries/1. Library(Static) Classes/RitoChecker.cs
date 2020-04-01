using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito
{
    // 2020. 01. 22. IsNull, NotNull 메소드 추가

    public static class RitoChecker// : MonoBehaviour
    {
        #region NullCheck

        /// <summary>
        /// 파라미터들이 하나라도 Null이면 true 리턴
        /// <para/> * 제네릭 공통 타입
        /// <para/> * 왼쪽 파라미터부터 순차적 검사
        /// <para/> --------------------------------
        /// <para/> [주의사항]
        /// <para/> - 객체.멤버 꼴로 파라미터를 입력할 경우,
        /// <para/> 객체?.멤버 형태로 사용해야 함
        /// </summary>
        public static bool IsNull<T>(params T[] targets)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i] == null) return true;
            }
            return false;
        }

        /// <summary>
        /// 파라미터들이 하나라도 Null이면 true 리턴
        /// <para/> * object 타입 - 박싱 발생
        /// <para/> * 왼쪽 파라미터부터 순차적 검사
        /// <para/> --------------------------------
        /// <para/> [주의사항]
        /// <para/> - 객체.멤버 꼴로 파라미터를 입력할 경우,
        /// <para/> 객체?.멤버 형태로 사용해야 함
        /// </summary>
        public static bool IsNull(params object[] targets)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i] == null) return true;
            }
            return false;
        }

        /// <summary>
        /// 파라미터들이 하나라도 Null이면 false 리턴
        /// <para/> 파라미터들이 모두 Null이 아니면 true 리턴
        /// <para/> * 제네릭 공통 타입
        /// <para/> * 왼쪽 파라미터부터 순차적 검사
        /// <para/> --------------------------------
        /// <para/> [주의사항]
        /// <para/> - 객체.멤버 꼴로 파라미터를 입력할 경우,
        /// <para/> 객체?.멤버 형태로 사용해야 함
        /// </summary>
        public static bool NotNull<T>(params T[] targets)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i] == null)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 파라미터들이 하나라도 Null이면 true 리턴
        /// <para/> 파라미터들이 모두 Null이 아니면 true 리턴
        /// <para/> * object 타입 - 박싱 발생
        /// <para/> * 왼쪽 파라미터부터 순차적 검사
        /// <para/> --------------------------------
        /// <para/> [주의사항]
        /// <para/> - 객체.멤버 꼴로 파라미터를 입력할 경우,
        /// <para/> 객체?.멤버 형태로 사용해야 함
        /// </summary>
        public static bool NotNull(params object[] targets)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i] == null)
                    return false;
            }
            return true;
        }

        #endregion // ==========================================================

        // Collider -> RitoCollider

        // Distance 3D -> RitoVector3

        // Math -> RitoMath

        // Raycast -> RitoRaycaster
    }
}