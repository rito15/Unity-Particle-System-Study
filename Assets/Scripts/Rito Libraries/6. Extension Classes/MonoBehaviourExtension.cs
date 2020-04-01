using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RitoExtension
{
    // 2020. 01. 26. 최초 작성
    // 2020. 01. 26. Ex_GetComponent, Ex_GetComponentInParent 2가지, Ex_FindChild,
    //               Ex_FindChildSubstring => 5가지 메소드 테스트 완료
    // 2020. 01. 27. Mono, GameObject, Transform 공통 메소드
    //               - Set Local/Global Pos/Rot, RotateLeft/Right/Up/Down 작성, 테스트 완료
    // 2020. 02. 02. 문서화 주석에서 <> -> &lt;&gt;로 변경

    /// <summary>
    /// 2020. 01. 26.
    /// <para/> MonoBehaviour 확장
    /// <para/> -----------------------------------------------------------------------------------
    /// <para/> [목록]
    /// <para/> Ex_GetComponent() : 컴포넌트를 찾고, 없으면 생성하여 리턴
    /// <para/>
    /// <para/> Ex_GetComponentInParent()
    /// <para/>  : 부모들에서 컴포넌트를 찾고, 없으면 바로 윗부모에 생성하여 리턴
    /// <para/>
    /// <para/> Ex_GetComponentInParent(string parentName)
    /// <para/>  : 부모들에서 컴포넌트를 찾고, 없으면 이름이 parentName인 부모에 생성하여 리턴
    /// <para/>
    /// <para/> Ex_FindChild(string childName)
    /// <para/>  : 이름이 childName인 자식 오브젝트를 찾고, 없으면 자식 오브젝트 생성하여 리턴
    /// <para/>
    /// <para/> Ex_FindChildSubstring(string substring, params string[] exception)
    /// <para/>  : 이름에 substring을 포함하는 자식을 찾아 리턴, 없으면 null 리턴
    /// <para/> .
    /// <para/> [Mono, GameObject, Transform 공통]
    /// <para/> Ex_SetLocalPosition() : 로컬(부모) 좌표계 기준으로 위치 변경
    /// <para/> Ex_SetGlobalPosition() : 월드 좌표계 기준으로 위치 변경
    /// <para/> Ex_SetLocalRotation() : 로컬(부모) 좌표계 기준으로 회전 변경
    /// <para/> Ex_SetGlobalRotation() : 월드 좌표계 기준으로 회전 변경
    /// <para/> .
    /// <para/> Ex_RotateRightSelf(float degree) : 자기 좌표계 기준으로 우측으로 회전
    /// <para/> Ex_RotateLeftSelf(float degree)  : 자기 좌표계 기준으로 좌측으로 회전
    /// <para/> Ex_RotateUpSelf(float degree)    : 자기 좌표계 기준으로 위쪽으로 회전
    /// <para/> Ex_RotateDownSelf(float degree)  : 자기 좌표계 기준으로 아래쪽으로 회전
    /// <para/> .
    /// <para/> Ex_RotateRightGlobal(float degree) : 월드 좌표계 기준으로 우측으로 회전
    /// <para/> Ex_RotateLeftGlobal(float degree) : 월드 좌표계 기준으로 좌측으로 회전
    /// <para/> 
    /// <para/> 
    /// <para/> 
    /// </summary>
    public static class MonoBehaviourExtension
    {
        #region GetComponent(Smart), FindChild

        /// <summary>
        /// <para/> 게임오브젝트 내에서 T 타입의 컴포넌트를 찾고, 없으면 생성하여 리턴
        /// <para/> 사용 예시 : this.Ex_SmartGetComponent&lt;Rigidbody&gt;();
        /// </summary>
        public static T Ex_GetComponent<T>(this MonoBehaviour target)
            where T : Component
        {
            var component = target.GetComponent<T>();
            if (component == null)
                return target.gameObject.AddComponent<T>();

            return component;
        }

        /// <summary>
        /// 부모 오브젝트들에서 컴포넌트를 찾고, 없으면 바로 윗부모에 생성하여 리턴
        /// <para/>* 자기 오브젝트에서부터 부모를 차례로 찾아 올라감
        /// <para/>* 자기 자신이 Root Object일 경우 null 리턴
        /// </summary>
        public static T Ex_GetComponentInParent<T>(this MonoBehaviour target)
            where T : Component
        {
            var parentComponent = target.GetComponentInParent<T>();

            // [1] 부모 오브젝트들에서 해당 컴포넌트를 바로 찾은 경우
            if (parentComponent)
                return parentComponent;

            // [2] 못찾은 경우

            // 부모 확인
            Transform parentTransform = target.transform.parent;

            // [2-1] 부모가 없는 경우 : null 리턴
            if (parentTransform == null)
                return null;

            // [2-2] 부모가 있는 경우 : 컴포넌트 추가하고 리턴
            return parentTransform.gameObject.AddComponent<T>();
        }

        /// <summary>
        /// 부모 오브젝트들에서 특정 컴포넌트를 찾아 리턴
        /// <para/>* 자기 오브젝트에서부터 부모를 차례로 찾아 올라감
        /// <para/>* 찾지 못할 경우, 부모들 중 이름에 parentName을 포함하는 오브젝트를 찾아
        /// <para/>  컴포넌트를 추가하고 리턴
        /// </summary>
        public static T Ex_GetComponentInParent<T>(this MonoBehaviour target, string parentName)
             where T : Component
        {
            var parentComponent = target.GetComponentInParent<T>();

            // [1] 부모 오브젝트들에서 해당 컴포넌트를 바로 찾은 경우
            if (parentComponent)
                return parentComponent;

            // [2] 못찾은 경우

            Transform parentTransform = target.transform;

            // 이름에 ancestorName를 포함하는 부모 오브젝트 확인
            while (parentTransform != null)
            {
                if (parentTransform.name.Contains(parentName))
                    break;

                parentTransform = parentTransform.parent;
            }

            // [2-1] 이름이 일치하는 부모 오브젝트가 없는 경우 : null 리턴
            if (parentTransform == null)
                return null;

            // [2-2] 이름이 일치하는 부모 오브젝트를 찾은 경우 : 컴포넌트 추가하고 리턴
            return parentTransform.gameObject.AddComponent<T>();
        }

        /// <summary>
        /// 이름이 childName(파라미터)인 자식 오브젝트 검색하여 리턴
        /// <para/>만약 찾지 못할 경우, 해당 이름으로 자식 오브젝트 생성하여 리턴
        /// </summary>
        public static GameObject Ex_FindChild(this MonoBehaviour target, string childName)
        {
            // 자식 오브젝트 검색
            var targetTransform = target.transform.Find(childName);

            // 바로 찾은 경우
            if (targetTransform != null)
                return targetTransform.gameObject;

            // 찾지 못한 경우 - 빈 오브젝트 생성하여 자식에 추가하고 리턴
            var targetObject = new GameObject(childName);
            targetObject.transform.SetParent(target.transform);

            return targetObject;
        }

        /// <summary>
        /// 이름에 substring을 포함하는 자식 오브젝트 검색하여 리턴
        /// <para/>* exception : 해당 단어들은 포함하지 않아야 함
        /// </summary>
        public static GameObject Ex_FindChildSubstring(this MonoBehaviour target,
            string substring, params string[] exception)
        {
            for (int i = 0; i < target.transform.childCount; i++)
            {
                var child = target.transform.GetChild(i);
                bool excepted = false;

                // 예외 단어 검사
                for (int j = 0; j < exception.Length; j++)
                {
                    if (child.gameObject.name.Contains(exception[j]))
                    {
                        excepted = true;
                        break;
                    }
                }

                if (excepted == false && child.gameObject.name.Contains(substring))
                    return child.gameObject;
            }

            return null;
        }

        #endregion // ==========================================================

        #region Mono, GameObject, Transform 공통 - SetPosition

        /// <summary> 게임오브젝트 로컬 위치 지정 </summary>
        public static void Ex_SetLocalPosition(this MonoBehaviour target, in Vector3 pos)
        {
            target.transform.localPosition = pos;
        }

        /// <summary> 게임오브젝트 로컬 위치 지정 </summary>
        public static void Ex_SetLocalPosition(this MonoBehaviour target,
            in float x, in float y, in float z)
        {
            target.transform.localPosition = new Vector3(x, y, z);
        }


        /// <summary> 게임오브젝트 글로벌 위치 지정 </summary>
        public static void Ex_SetGlobalPosition(this MonoBehaviour target, in Vector3 pos)
        {
            target.transform.position = pos;
        }

        /// <summary> 게임오브젝트 글로벌 위치 지정 </summary>
        public static void Ex_SetGlobalPosition(this MonoBehaviour target,
            in float x, in float y, in float z)
        {
            target.transform.position = new Vector3(x, y, z);
        }

        #endregion // ==========================================================

        #region Mono, GameObject, Transform 공통 - SetRotation

        /// <summary> 게임오브젝트 로컬 회전 지정 </summary>
        public static void Ex_SetLocalRotation(this MonoBehaviour target, in Quaternion rot)
        {
            target.transform.localRotation = rot;
        }

        /// <summary> 게임오브젝트 로컬 회전 지정 </summary>
        public static void Ex_SetLocalRotation(this MonoBehaviour target, in Vector3 rot)
        {
            target.transform.localRotation = Quaternion.Euler(rot);
        }

        /// <summary> 게임오브젝트 로컬 회전 지정 </summary>
        public static void Ex_SetLocalRotation(this MonoBehaviour target,
            in float x, in float y, in float z)
        {
            target.transform.localRotation = Quaternion.Euler(x, y, z);
        }


        /// <summary> 게임오브젝트 글로벌 회전 지정 </summary>
        public static void Ex_SetGlobalRotation(this MonoBehaviour target, in Quaternion rot)
        {
            target.transform.rotation = rot;
        }

        /// <summary> 게임오브젝트 글로벌 회전 지정 </summary>
        public static void Ex_SetGlobalRotation(this MonoBehaviour target, in Vector3 rot)
        {
            target.transform.rotation = Quaternion.Euler(rot);
        }

        /// <summary> 게임오브젝트 글로벌 회전 지정 </summary>
        public static void Ex_SetGlobalRotation(this MonoBehaviour target,
            in float x, in float y, in float z)
        {
            target.transform.rotation = Quaternion.Euler(x, y, z);
        }

        #endregion // ==========================================================

        #region Mono, GameObject, Transform 공통 - Rotate

        /// <summary>
        /// 자기 좌표계에서 Y축을 기준으로 우측으로 degree만큼 회전
        /// <para/> * Y축 상단에서 바라봤을 때, 시계방향
        /// </summary>
        public static void Ex_RotateRightSelf(this MonoBehaviour target, in float degree)
        {
            target.transform.Rotate(Vector3.up, degree, Space.Self);
        }

        /// <summary>
        /// 자기 좌표계에서 Y축을 기준으로 좌측으로 degree만큼 회전
        /// <para/> * Y축 상단에서 바라봤을 때, 반시계방향
        /// </summary>
        public static void Ex_RotateLeftSelf(this MonoBehaviour target, in float degree)
        {
            target.transform.Rotate(Vector3.up, -degree, Space.Self);
        }

        /// <summary>
        /// 자기 좌표계에서 +Y 방향을 향해 위로 회전
        /// <para/> * 고개를 든다.
        /// </summary>
        public static void Ex_RotateUpSelf(this MonoBehaviour target, in float degree)
        {
            target.transform.Rotate(Vector3.left, degree, Space.Self);
        }

        /// <summary>
        /// 자기 좌표계에서 -Y 방향을 향해 아래로 회전
        /// <para/> * 고개를 내린다.
        /// </summary>
        public static void Ex_RotateDownSelf(this MonoBehaviour target, in float degree)
        {
            target.transform.Rotate(Vector3.left, -degree, Space.Self);
        }

        
        /// <summary>
        /// 글로벌 좌표계에서 Y축을 기준으로 우측으로 degree만큼 회전
        /// <para/> * Y축 상단에서 바라봤을 때, 시계방향
        /// </summary>
        public static void Ex_RotateRightGlobal(this MonoBehaviour target, in float degree)
        {
            target.transform.Rotate(Vector3.up, degree, Space.World);
        }

        /// <summary>
        /// 글로벌 좌표계에서 Y축을 기준으로 좌측으로 degree만큼 회전
        /// <para/> * Y축 상단에서 바라봤을 때, 반시계방향
        /// </summary>
        public static void Ex_RotateLeftGlobal(this MonoBehaviour target, in float degree)
        {
            target.transform.Rotate(Vector3.up, -degree, Space.World);
        }

        #endregion // ==========================================================
    }
}
