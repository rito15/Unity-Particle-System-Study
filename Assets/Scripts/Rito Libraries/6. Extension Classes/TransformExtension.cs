using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RitoExtension
{
    // 2020. 01. 26. 최초 작성
    // 2020. 01. 27. Mono, GameObject, Transform 공통 메소드
    //               - Set Local/Global Pos/Rot, RotateLeft/Right/Up/Down 작성, 테스트 완료

    /// <summary>
    /// 2020. 01. 26.
    /// <para/> Transform 확장
    /// <para/> -----------------------------------------------------------------------------------
    /// <para/> [목록]
    /// <para/> 
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
    /// <para/> 
    /// </summary>
    public static class TransformExtension
    {
        #region Mono, GameObject, Transform 공통 - SetPosition

        /// <summary> 게임오브젝트 로컬 위치 지정 </summary>
        public static void Ex_SetLocalPosition(this Transform target, in Vector3 pos)
        {
            target.localPosition = pos;
        }

        /// <summary> 게임오브젝트 로컬 위치 지정 </summary>
        public static void Ex_SetLocalPosition(this Transform target,
            in float x, in float y, in float z)
        {
            target.localPosition = new Vector3(x, y, z);
        }


        /// <summary> 게임오브젝트 글로벌 위치 지정 </summary>
        public static void Ex_SetGlobalPosition(this Transform target, in Vector3 pos)
        {
            target.position = pos;
        }

        /// <summary> 게임오브젝트 글로벌 위치 지정 </summary>
        public static void Ex_SetGlobalPosition(this Transform target,
            in float x, in float y, in float z)
        {
            target.position = new Vector3(x, y, z);
        }

        #endregion // ==========================================================

        #region Mono, GameObject, Transform 공통 - SetRotation

        /// <summary> 게임오브젝트 로컬 회전 지정 </summary>
        public static void Ex_SetLocalRotation(this Transform target, in Quaternion rot)
        {
            target.localRotation = rot;
        }

        /// <summary> 게임오브젝트 로컬 회전 지정 </summary>
        public static void Ex_SetLocalRotation(this Transform target, in Vector3 rot)
        {
            target.localRotation = Quaternion.Euler(rot);
        }

        /// <summary> 게임오브젝트 로컬 회전 지정 </summary>
        public static void Ex_SetLocalRotation(this Transform target,
            in float x, in float y, in float z)
        {
            target.localRotation = Quaternion.Euler(x, y, z);
        }


        /// <summary> 게임오브젝트 글로벌 회전 지정 </summary>
        public static void Ex_SetGlobalRotation(this Transform target, in Quaternion rot)
        {
            target.rotation = rot;
        }

        /// <summary> 게임오브젝트 글로벌 회전 지정 </summary>
        public static void Ex_SetGlobalRotation(this Transform target, in Vector3 rot)
        {
            target.rotation = Quaternion.Euler(rot);
        }

        /// <summary> 게임오브젝트 글로벌 회전 지정 </summary>
        public static void Ex_SetGlobalRotation(this Transform target,
            in float x, in float y, in float z)
        {
            target.rotation = Quaternion.Euler(x, y, z);
        }

        #endregion // ==========================================================

        #region Mono, GameObject, Transform 공통 - Rotate

        /// <summary>
        /// 자기 좌표계에서 Y축을 기준으로 우측으로 degree만큼 회전
        /// <para/> * Y축 상단에서 바라봤을 때, 시계방향
        /// </summary>
        public static void Ex_RotateRightSelf(this Transform target, in float degree)
        {
            target.Rotate(Vector3.up, degree, Space.Self);
        }

        /// <summary>
        /// 자기 좌표계에서 Y축을 기준으로 좌측으로 degree만큼 회전
        /// <para/> * Y축 상단에서 바라봤을 때, 반시계방향
        /// </summary>
        public static void Ex_RotateLeftSelf(this Transform target, in float degree)
        {
            target.Rotate(Vector3.up, -degree, Space.Self);
        }

        /// <summary>
        /// 자기 좌표계에서 +Y 방향을 향해 위로 회전
        /// <para/> * 고개를 든다.
        /// </summary>
        public static void Ex_RotateUpSelf(this Transform target, in float degree)
        {
            target.Rotate(Vector3.left, degree, Space.Self);
        }

        /// <summary>
        /// 자기 좌표계에서 -Y 방향을 향해 아래로 회전
        /// <para/> * 고개를 내린다.
        /// </summary>
        public static void Ex_RotateDownSelf(this Transform target, in float degree)
        {
            target.Rotate(Vector3.left, -degree, Space.Self);
        }


        /// <summary>
        /// 글로벌 좌표계에서 Y축을 기준으로 우측으로 degree만큼 회전
        /// <para/> * Y축 상단에서 바라봤을 때, 시계방향
        /// </summary>
        public static void Ex_RotateRightGlobal(this Transform target, in float degree)
        {
            target.Rotate(Vector3.up, degree, Space.World);
        }

        /// <summary>
        /// 글로벌 좌표계에서 Y축을 기준으로 좌측으로 degree만큼 회전
        /// <para/> * Y축 상단에서 바라봤을 때, 반시계방향
        /// </summary>
        public static void Ex_RotateLeftGlobal(this Transform target, in float degree)
        {
            target.Rotate(Vector3.up, -degree, Space.World);
        }

        #endregion // ==========================================================
    }
}