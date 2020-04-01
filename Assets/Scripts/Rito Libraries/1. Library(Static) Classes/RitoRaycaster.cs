using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito
{
    // 2020. 01. 15. 매개변수 순서 한정 : layerMask, distance / 이름 깔끔히 정리 + 설명 추가 / in 한정자 추가
    // 2020. 01. 26. UI 레이캐스트 확인 메소드 추가

    /// <summary>
    /// [간편 레이캐스트]
    /// <para/> -----------------------------------------------------
    /// <para/> 1. 레이캐스트 여부 확인(return bool)
    /// <para/> CamToMouse : 활성화된 카메라 -> 마우스 위치
    /// <para/> CamToPos : 활성화된 카메라 -> 특정 위치
    /// <para/> AtoB : 위치 A -> 위치 B
    /// <para/> SkyToPos : height만큼 높은 위치 -> 해당 위치
    /// <para/> -----------------------------------------------------
    /// <para/> 2. 레이캐스트 지점 리턴(return Vector3)
    /// <para/> * 레이캐스트 실패 시, Vector3.NegativeInfinity 리턴
    /// <para/> GetCamToMouse : 활성화된 카메라 -> 마우스 위치
    /// <para/> GetCamToPos : 활성화된 카메라 -> 특정 위치
    /// <para/> GetAtoB : 위치 A -> 위치 B
    /// <para/> GetSkyToPos : height만큼 높은 위치 -> 해당 위치
    /// <para/> 
    /// </summary>
    public static class RitoRaycaster// : MonoBehaviour
    {
        // 레이캐스트 거리를 지정하지 않았을 때 지정할 기본 거리
        public const float DefaultDistance = 500f;

        #region RayCast : Camera -> Mouse, return bool

        /// <summary> 활성화된 카메라로부터 마우스 위치에 레이캐스트 검사 </summary>
        public static bool CamToMouse(out RaycastHit hit, in int layerMask = -1, in float distance = DefaultDistance)
        {
            return Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, distance, layerMask);
        }
        /// <summary> 활성화된 카메라로부터 마우스 위치에 레이캐스트 검사 </summary>
        public static bool CamToMouse(in int layerMask = -1, in float distance = DefaultDistance)
        {
            return Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _, distance, layerMask);
        }

        #endregion // ================================================================

        #region RayCast : Camera -> Position, return bool

        /// <summary> 활성화된 카메라로부터 특정 위치로 레이캐스트 </summary>
        public static bool CamToPos(out RaycastHit hit, in Vector3 pos, in int layerMask = -1, in float distance = DefaultDistance)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            Ray ray = new Ray(cameraPos, (pos - cameraPos).normalized);

            return Physics.Raycast(ray, out hit, distance, layerMask);
        }
        /// <summary> 활성화된 카메라로부터 특정 위치로 레이캐스트 </summary>
        public static bool CamToPos(in Vector3 pos, in int layerMask = -1, in float distance = DefaultDistance)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            Ray ray = new Ray(cameraPos, (pos - cameraPos).normalized);

            return Physics.Raycast(ray, out _, distance, layerMask);
        }

        #endregion // ================================================================

        #region RayCast : A -> B, return bool

        /// <summary> A 위치에서 B 위치로 레이캐스트 </summary>
        public static bool AtoB(out RaycastHit hit, in Vector3 posA, in Vector3 posB, in int layerMask = -1, in float distance = DefaultDistance)
        {
            Ray ray = new Ray(posA, (posB - posA).normalized);

            return Physics.Raycast(ray, out hit, distance, layerMask);
        }
        /// <summary> A 위치에서 B 위치로 레이캐스트 </summary>
        public static bool AtoB(in Vector3 posA, in Vector3 posB, in float distance = DefaultDistance, in int layerMask = -1)
        {
            Ray ray = new Ray(posA, (posB - posA).normalized);

            return Physics.Raycast(ray, out _, distance, layerMask);
        }

        #endregion // ================================================================

        #region RayCast : Sky -> Position, return bool

        /// <summary> 해당 위치의 수직 꼭대기에서 해당 위치로 레이캐스트</summary>
        public static bool SkyToPos(out RaycastHit hit, in Vector3 pos, in int layerMask = -1, in float height = DefaultDistance)
        {
            Vector3 sky = new Vector3(pos.x, pos.y + height, pos.z);
            Ray ray = new Ray(sky, (pos - sky).normalized);

            return Physics.Raycast(ray, out hit, height * 2f, layerMask);
        }
        /// <summary> 해당 위치의 수직 꼭대기에서 해당 위치로 레이캐스트</summary>
        public static bool SkyToPos(in Vector3 pos, in int layerMask = -1, in float height = DefaultDistance)
        {
            Vector3 sky = new Vector3(pos.x, pos.y + height, pos.z);
            Ray ray = new Ray(sky, (pos - sky).normalized);

            return Physics.Raycast(ray, out _, height * 2f, layerMask);
        }

        #endregion // ================================================================

        #region RayCast - Get Vector3

        /// <summary>
        /// 활성화된 카메라로부터 마우스 위치로 레이캐스트하여 3D 좌표 얻어내기
        /// <para/> 레이캐스트 실패하는 경우 Vector3.negativeInfinity 리턴
        /// </summary>
        public static Vector3 GetCamToMouse(in int layerMask = -1, in float distance = DefaultDistance)
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, distance, layerMask))
                return hit.point;

            return Vector3.negativeInfinity;
        }

        /// <summary>
        /// 활성화된 카메라로부터 특정 위치로 레이캐스트하여 3D 좌표 얻어내기
        /// <para/> 레이캐스트 실패하는 경우 Vector3.negativeInfinity 리턴
        /// </summary>
        public static Vector3 GetCamToPos(in Vector3 pos, in int layerMask = -1, in float distance = DefaultDistance)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            Ray ray = new Ray(cameraPos, (pos - cameraPos).normalized);

            if (Physics.Raycast(ray, out RaycastHit hit, distance, layerMask))
                return hit.point;

            return Vector3.negativeInfinity;
        }

        /// <summary>
        /// 해당 위치의 수직 꼭대기에서 해당 위치로 레이캐스트(-y 방향)하여 3D 좌표 얻어내기
        /// <para/> height : 해당 위치로부터 수직 높이
        /// <para/> 레이캐스트 실패하는 경우 Vector3.negativeInfinity 리턴
        /// </summary>
        public static Vector3 GetSkyToPos(in Vector3 pos, in int layerMask = -1, in float height = DefaultDistance)
        {
            Vector3 sky = new Vector3(pos.x, pos.y + height, pos.z);
            Ray ray = new Ray(sky, (pos - sky).normalized);

            if (Physics.Raycast(ray, out RaycastHit hit, height * 2f, layerMask))
                return hit.point;

            return Vector3.negativeInfinity;
        }

        /// <summary>
        /// 위치 A에서 B로 레이캐스트하여 3D 좌표 얻어내기
        /// <para/> 레이캐스트 실패하는 경우 Vector3.negativeInfinity 리턴
        /// </summary>
        public static Vector3 GetAtoB(in Vector3 posA, in Vector3 posB, in int layerMask = -1, in float height = DefaultDistance)
        {
            Ray ray = new Ray(posA, (posB - posA).normalized);

            if (Physics.Raycast(ray, out RaycastHit hit, height, layerMask))
                return hit.point;

            return Vector3.negativeInfinity;
        }

        #endregion // ================================================================

        #region UI Raycast

        /// <summary>
        /// 마우스가 UI 위에 있는지 검사 <para/>
        /// 이벤트 시스템이 없는 경우, 빈 게임오브젝트를 생성해서 자동으로 추가해줌
        /// <returns></returns>
        /// </summary>
        public static bool MouseIsOverUI()
        {
            // 이벤트 시스템이 없는 경우 생성
            if (UnityEngine.EventSystems.EventSystem.current == null)
            {
                GameObject eventSystem = new GameObject("EventSystem");
                eventSystem.AddComponent<UnityEngine.EventSystems.EventSystem>();
                eventSystem.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
            }

            return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
        }

        #endregion // ==========================================================
    }
}