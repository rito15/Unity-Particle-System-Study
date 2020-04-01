using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito
{
    // 2020. 01. 15. 마지막 검토 - SmartVector3 때문에 거의 방치 예정

    public class RitoVector3// : MonoBehaviour
    {
        #region Distance 3D

        /// <summary> 두 벡터 위치가 지정 거리 이내에 있는지 검사 </summary>
        public static bool DistanceInRange(Vector3 pos1, Vector3 pos2, float range)
        {
            return Vector3.Distance(pos1, pos2) <= range;
        }
        public static bool DistanceInRange(Transform tr1, Vector3 pos2, float range)
        {
            if (tr1 == null)
            {
                RitoDebug.Log("Transform 1 Is Missing");
                return false;
            }

            return Vector3.Distance(tr1.position, pos2) <= range;
        }
        public static bool DistanceInRange(Vector3 pos1, Transform tr2, float range)
        {
            if (tr2 == null)
            {
                RitoDebug.Log("Transform 2 Is Missing");
                return false;
            }

            return Vector3.Distance(pos1, tr2.position) <= range;
        }
        public static bool DistanceInRange(Transform tr1, Transform tr2, float range)
        {
            if (tr1 == null)
            {
                RitoDebug.Log("Transform 1 Is Missing");
                return false;
            }
            if (tr2 == null)
            {
                RitoDebug.Log("Transform 2 Is Missing");
                return false;
            }

            return Vector3.Distance(tr1.position, tr2.position) <= range;
        }

        // 두 오브젝트 사이 거리 검사
        public static float Distance(GameObject go1, GameObject go2, float exceptionalDistance = 0f)
        {
            // exceptionalDistance : 예외가 발생했을 때 임시로 리턴할 거리 값

            if (go1 == null)
            {
                RitoDebug.Log("GameObject 1 Is Missing");
                return exceptionalDistance;
            }
            if (go2 == null)
            {
                RitoDebug.Log("GameObject 2 Is Missing");
                return exceptionalDistance;
            }

            return Vector3.Distance(go1.transform.position, go2.transform.position);
        }
        public static float Distance(Transform tr1, Transform tr2, float exceptionalDistance = 0f)
        {
            // exceptionalDistance : 예외가 발생했을 때 임시로 리턴할 거리 값

            if (tr1 == null)
            {
                RitoDebug.Log("Transform 1 Is Missing");
                return exceptionalDistance;
            }
            if (tr2 == null)
            {
                RitoDebug.Log("Transform 2 Is Missing");
                return exceptionalDistance;
            }

            return Vector3.Distance(tr1.position, tr2.position);
        }

        #endregion // ================================================================

        #region Direction

        // 크기 1짜리 방향 벡터 리턴
        public static Vector3 Direction(Vector3 from, Vector3 to)
        {
            return (to - from).normalized;
        }
        public static Vector3 Direction(Transform from, Vector3 to)
        {
            if (from == null)
            {
                RitoDebug.Log("Transform(From) Is Missing");
                return Vector3.forward;
            }

            return (to - from.position).normalized;
        }
        public static Vector3 Direction(Vector3 from, Transform to)
        {
            if (to == null)
            {
                RitoDebug.Log("Transform(To) Is Missing");
                return Vector3.forward;
            }

            return (to.position - from).normalized;
        }
        public static Vector3 Direction(Transform from, Transform to)
        {
            if (from == null)
            {
                RitoDebug.Log("Transform(From) Is Missing");
                return Vector3.forward;
            }
            if (to == null)
            {
                RitoDebug.Log("Transform(To) Is Missing");
                return Vector3.forward;
            }

            return (to.position - from.position).normalized;
        }
        public static Vector3 Direction(GameObject from, Vector3 to)
        {
            if (from == null)
            {
                RitoDebug.Log("Game Object((From) Is Missing");
                return Vector3.forward;
            }

            return (to - from.transform.position).normalized;
        }
        public static Vector3 Direction(Vector3 from, GameObject to)
        {
            if (to == null)
            {
                RitoDebug.Log("Game Object((To) Is Missing");
                return Vector3.forward;
            }

            return (to.transform.position - from).normalized;
        }
        public static Vector3 Direction(GameObject from, GameObject to)
        {
            if (from == null)
            {
                RitoDebug.Log("Game Object((From) Is Missing");
                return Vector3.forward;
            }
            if (to == null)
            {
                RitoDebug.Log("Game Object((To) Is Missing");
                return Vector3.forward;
            }

            return (to.transform.position - from.transform.position).normalized;
        }

        #endregion // ================================================================

        #region Angle, Rotate

        /* 
         * 두 방향 벡터 사이의 각도 구하기 : Vector3.Angle(Vector3 from, Vector3 to)
         * 
         * ==> from, to는 정규화할 필요도 없고 두 방향벡터(0,0,0 시작점 기준) 사이의 각을 degree로 리턴함
         * 
         * * 그래도 찾아쓰기 귀찮을 수 있으니 캐싱하듯 추가해놓았음
         */
        float Angle(Vector3 from, Vector3 to)
        {
            return Vector3.Angle(from, to);
        }

        // 특정 좌표를 기준으로 특정 각도(degree)만큼 회전시킨 벡터 구하기
        // 회전 기준점(axisPoint)을 지정하지 않을 경우, 기준은 (0,0,0)
        // => 그럼 x 회전은 x축 기준, .... 이렇게 됨
        // +
        // 회전 기준점을 지정할 경우, (0,0,0)을 기준으로 회전시킨 뒤 다시 상대좌표로 옮겨서 리턴해줌
        // +
        // 활용 방안 : 1. 좌표 벡터를 회전 / 2. 방향 벡터를 회전
        // +
        // http://www.devkorea.co.kr/bbs/board.php?bo_table=m03_qna&wr_id=46332
        public static Vector3 Rotate(Vector3 originVector, float xDeg, float yDeg, float zDeg, Vector3? axisPoint = null)
        {
            /* xDeg = x축 기준으로 회전시킬 값, y z 마찬가지
             * 
             * x, y, z 모두 값이 주어질 경우 회전축 우선순위
             * : z -> x -> y
             *   (z축으로 회전시키고 x축으로 다시 회전시킨 다음, 마지막으로 y축 회전)
             * 
             */

            // 회전 기준 좌표이자 상대 좌표
            Vector3 transitionVector = (axisPoint == null) ? Vector3.zero : (Vector3)axisPoint;

            // 1. 상대 좌표 이동
            originVector -= transitionVector;

            // 회전계수 = 회전각(x,y,z)
            Quaternion rotationCoef = Quaternion.Euler(xDeg, yDeg, zDeg);

            // 2. x,y,z 각각의 "축"으로 회전
            originVector = rotationCoef * originVector;

            // 3. 다시 좌표 복귀
            originVector += transitionVector;

            return originVector;
        }

        // y축(기본) 또는 특정 좌표 기준으로 우측으로 rightDegree(도) 만큼 회전한 벡터 리턴
        public static Vector3 RotateRight(Vector3 originVector, float rightDegree, Vector3? axisPoint = null)
        {
            return Rotate(originVector, 0f, rightDegree, 0f, axisPoint);
        }

        // y축(기본) 또는 특정 좌표 기준으로 좌측으로 leftDegree(도) 만큼 회전한 벡터 리턴
        public static Vector3 RotateLeft(Vector3 originVector, float leftDegree, Vector3? axisPoint = null)
        {
            return Rotate(originVector, 0f, -leftDegree, 0f, axisPoint);
        }

        // 특정 좌표를 기준으로 위로 upDegree(도) 만큼 회전한 벡터 리턴
        // 기준 좌표가 꼭 필요함 !!
        // 월드<->로컬 좌표계 변환 매트릭스를 이용해야 할듯(로컬의 0,0,n 벡터를 x축 회전시키고 다시 월드로 가져오게)
        //public static Vector3 RotateUp(Vector3 originVector, float upDegree, Vector3 axisPoint)
        //{
        //
        //}

        // RotateDown

        #endregion // ================================================================

        #region Convert 3D -> 2D

        // 3D 좌표를 2D 스크린 좌표로 변환하여 리턴
        public static Vector2 ConvertTo2D(Vector3 position3D)
        {
            return Camera.main.WorldToScreenPoint(position3D);
        }

        #endregion
    }
}