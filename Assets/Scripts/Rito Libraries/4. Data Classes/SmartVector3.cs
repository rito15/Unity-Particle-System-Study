using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito
{
    // 2020. 01. 15. - GetScreenPoint2D 추가, 가능한 모든 파라미터 in 한정자 추가

    // 작성 원칙
    // 1. 파라미터, 리턴 타입 모두 SmartVector3
    // 2. 수정되지 않을 파라미터는 모두 in 한정자로 전달

    /// <summary>
    /// 2019.12.27 생성
    /// <para/> 스마트 벡터3
    /// <para/> : 기존의 Vector3를 더 손쉽고 간편한 형태로 제공
    /// <para/> . 
    /// <para/> * SmartVector3.목록 입력 시 모든 기능 목록 및 한글 설명 확인 가능
    /// </summary>
    [Serializable]
    public struct SmartVector3
    {
        /*
         * [개발 체크리스트]
         * 
         * [O] Vector3 동적 프로퍼티 오버라이딩
         * [O] Vector3 정적 프로퍼티 오버라이딩
         * [O] Vecotr3 인덱서 오버라이딩
         * [O] Vecotr3 연산자 오버라이딩
         * [O] Vecotr3 정적 메소드 오버라이딩
         * [O] Vecotr3 동적 메소드 오버라이딩
         * [O] Vector3 오버라이딩 모두 한글 문서화 주석 작성
         * [O] Vector3 오버라이딩 모두 한글 설명서 작성
         * [] 쿼터니언 연동
         * 
         * [+] 추가 기능들 개발
         * 
         */

        // Vector3 참고 페이지
        // https://docs.unity3d.com/kr/530/ScriptReference/Vector3.html

        // Vector Field
        [SerializeField]
        private Vector3 vec;

        #region 한글 설명서

        /// <summary>
        /// [프로퍼티 목록]
        /// <para/>---------------------------------------------------------------<para/>
        /// <para/> 1. 정적 프로퍼티
        /// <para/> Null, Zero : (0, 0, 0)
        /// <para/> One : (1, 1, 1)
        /// <para/> Forward : (0, 0, 1)
        /// <para/> Back : (0, 0, -1)
        /// <para/> Up : (0, 1, 0)
        /// <para/> Down : (0, -1, 0)
        /// <para/> Right : (1, 0, 0)
        /// <para/> Left : (-1, 0, 0) 리턴
        /// <para/> PositiveInfinity : (xyz : float 양의 최댓값) 리턴
        /// <para/> NegativeInfinity : (xyz : float 음의 최댓값) 리턴
        /// <para/>---------------------------------------------------------------<para/>
        /// 2. 동적 프로퍼티
        /// <para/> X, Y, Z, Vector3 : 기본 요소 반환
        /// <para/> Reverse, Minus : x, y, z 각각 부호를 반전한 벡터 반환
        /// <para/> Length, Magnitude : 벡터의 길이 반환((0, 0, 0)으로부터의 거리)
        /// <para/> SqrMagnitude : 벡터 길이의 제곱 반환
        /// <para/> Normalized : 
        /// <para/> Quaternion : 쿼터니언으로 변환하여 리턴
        /// </summary>
        public static void 목록_프로퍼티() { }

        /// <summary>
        /// [인덱서 목록]
        /// <para/>---------------------------------------------------------------<para/>
        /// <para/> ['x'], ['X'], ["x"], ["X"], [0] : x성분(float) 참조
        /// <para/> ['y'], ['Y'], ["y"], ["Y"], [1] : y성분(float) 참조
        /// <para/> ['z'], ['Z'], ["z"], ["Z"], [2] : z성분(float) 참조
        /// </summary>
        public static void 목록_인덱서() { }

        /// <summary>
        /// [생성자 목록]
        /// <para/>---------------------------------------------------------------<para/>
        /// <para/> Vector3 vec3
        /// <para/> Vector2 vec2
        /// <para/> SmartVector3 vec3
        /// <para/> float x, float y, float z
        /// <para/> float x, float y
        /// <para/> float x
        /// <para/> (float, float, float) f
        /// <para/> double x, double y, double z
        /// <para/> double x, double y
        /// <para/> double x
        /// <para/> (double, double, double) d
        /// <para/> 
        /// <para/> 
        /// </summary>
        public static void 목록_생성자() { }

        /// <summary>
        /// [단항 연산자 목록]
        /// <para/>---------------------------------------------------------------<para/>
        /// <para/> + : 그대로 리턴
        /// <para/> -, !, ~ : 벡터의 각 성분의 부호 변경
        /// <para/> ++ : 벡터의 각 성분에 1를 더함
        /// <para/> -- : 벡터의 각 성분에 1을 뺌
        /// <para/> 
        /// <para/> 
        /// <para/> 
        /// </summary>
        public static void 목록_단항_연산자() { }

        /// <summary>
        /// [이항 연산자 목록]
        /// <para/> (Notice) 이항 연산자 : SmartVector3, Vector3 모두 허용
        /// <para/>---------------------------------------------------------------
        /// <para/> 1. 논리(관계) 연산자
        /// <para/> == : 두 벡터가 같은지 검사
        /// <para/> != : 두 벡터가 다른지 검사
        /// <para/> &gt;, &lt;, &gt;=, &lt;= : 두 벡터의 길이를 비교
        /// <para/>---------------------------------------------------------------
        /// <para/> .
        /// <para/> 2. 산술 연산자
        /// <para/> +, -, *, / (벡터, 벡터) : 벡터의 각 성분끼리의 사칙연산
        /// <para/> +, -, *, / (벡터, float) : 벡터의 각 성분마다 같은 실수 값으로 사칙연산
        /// <para/> +, -, *, / (벡터, double) : 벡터의 각 성분마다 같은 실수 값으로 사칙연산
        /// <para/> 
        /// <para/> 
        /// </summary>
        public static void 목록_이항_연산자() { }

        /// <summary>
        /// [암시적 형변환 허용 목록]
        /// <para/>---------------------------------------------------------------<para/>
        /// <para/> SmartVector3 -> Vector3
        /// <para/> SmartVector3 -> Vector2
        /// <para/> SmartVector3 -> string
        /// <para/> SmartVector3 -> (float, float, float)
        /// <para/> SmartVector3 -> (float, float)
        /// <para/> SmartVector3 -> (double, double, double)
        /// <para/> SmartVector3 -> (double, double)
        /// <para/> SmartVector3 -> Quaternion
        /// <para/> .
        /// <para/> Vector3 -> SmartVector3
        /// <para/> Vector2 -> SmartVector3
        /// <para/> float -> SmartVector3 : x, y, z 성분에 같은 실수를 저장
        /// <para/> double -> SmartVector3 : x, y, z 성분에 같은 실수를 저장
        /// <para/> (float, float, float) -> SmartVector3 
        /// <para/> (float, float) -> SmartVector3 : z 성분에는 0 저장
        /// <para/> (double, double, double) -> SmartVector3 
        /// <para/> (double, double) -> SmartVector3 : z 성분에는 0 저장
        /// <para/> Quaternion -> SmartVector3
        /// <para/> 
        /// <para/> 
        /// </summary>
        public static void 목록_타입캐스팅() { }

        /// <summary>
        /// [정적 메소드 목록]
        /// <para/>------------------------------------------------------------------------------------<para/>
        /// <para/> Angle : 두 벡터 사이의 각도 반환
        /// <para/> SignedAngle : 두 벡터 사이의 각도 반환, 기준 축에 따라 부호 결정
        /// <para/> ClampMagnitude : 벡터의 최대 길이를 자르고, 결과 벡터를 반환
        /// <para/> Cross : 두 벡터의 외적 결과 벡터 반환
        /// <para/> Dot : 두 벡터의 내적 결과 값 반환
        /// <para/> Distance : 두 벡터 좌표 사이의 거리값 반환
        /// <para/> Lerp, LerpUnclamped : 두 벡터 좌표 사이를 선형보간
        /// <para/> Slerp, SlerpUnclamped : 두 벡터 좌표 사이를 구형보간
        /// <para/> Max : 두 벡터 (x1,y1,z1), (x2,y2,z2)를 받아 (Max(x1, x2), Max(y1, y2), Max(z1, z2)) 반환
        /// <para/> Min: 두 벡터 (x1,y1,z1), (x2,y2,z2)를 받아 (Min(x1, x2), Min(y1, y2), Min(z1, z2)) 반환
        /// <para/> MoveTowards : current로부터 target 지점을 향해 한 번 이동한 벡터 반환
        /// <para/> RotateTowards : 벡터를 방향벡터로 간주하고 target 방향을 향해 한 번 회전한 벡터 반환
        /// <para/> OrthoNormalize : 벡터를 모두 정규화하고, 각각 직교하도록 변경 수행
        /// <para/> Project : 벡터를 대상 법선벡터에 투영시킨 벡터 리턴
        /// <para/> ProjectOnPlane : 벡터를 대상 Plane에 투영시킨 벡터 리턴
        /// <para/> Reflect : inNormal(법선) 벡터를 기준으로 inDirection 벡터를 반전시킨 벡터 리턴
        /// <para/> Scale : 두 벡터의 x, y, z 성분을 각각 곱한 벡터 반환
        /// <para/> SmoothDamp : 시간 경과에 따라 벡터를 부드럽게 이동(대표적 사용 예시 : 카메라 이동)
        /// <para/>------------------------------[커스텀 정적 메소드 목록]-----------------------------
        /// <para/> Direction, FromTo : 방향 벡터(크기 1) 리턴
        /// <para/> Mid : 두 벡터 사이의 중간 좌표 리턴
        /// <para/> DebugLine : 두 좌표 사이에 디버그 라인 그리기
        /// <para/> DebugPoint : 해당 좌표를 별모양 디버그로 표시해주기
        /// <para/> GetScreenPoint2D : 3D 좌표를 화면의 2D 좌표로 리턴
        /// <para/> 
        /// <para/> 
        /// </summary>
        public static void 목록_정적_메소드() { }

        /// <summary>
        /// [동적 메소드 목록]
        /// <para/>------------------------------------------------------------------------------------<para/>
        /// <para/> Normalize : 현재 벡터를 정규화하고 리턴
        /// <para/> Scale : 현재 벡터의 x,y,z 성분에 sv 벡터의 x,y,z 성분을 각각 곱하고 리턴
        /// <para/> Set : 벡터에 새로운 값 할당
        /// <para/>------------------------------[커스텀 동적 메소드 목록]-----------------------------
        /// <para/> Dot : 다른 벡터와의 내적 결과 리턴
        /// <para/> Cross : 다른 벡터와의 외적 결과 리턴
        /// <para/> Direction : 타겟 벡터를 향하는 방향벡터(크기 1) 리턴
        /// <para/> Angle : 다른 벡터와의 각도 반환
        /// <para/> Distance : 다른 벡터와의 거리 반환
        /// <para/> ClampLength, ClampMagnitude : 벡터의 최대 길이를 maxLength로 자르고 리턴
        /// <para/> MoveTowards : 현재 위치로부터 target 지점을 향해 한 번 이동한 벡터 리턴
        /// <para/> RotateTowards : 벡터를 방향벡터로 간주하고 target 방향을 향해 한 번 회전한 벡터 리턴
        /// <para/> To : 현재 벡터로부터 다른 벡터를 향하는 방향벡터(크기 1) 리턴
        /// <para/> From : 다른 벡터로부터 현재 벡터를 향하는 방향벡터(크기 1) 리턴
        /// <para/> Rotate : 특정 좌표를 기준으로 특정 각도(degree)만큼 회전시킨 벡터 리턴
        /// <para/> RotateRight : y축(기본) 또는 특정 좌표 기준으로 우측으로 rightDegree(도) 만큼 회전한 벡터 리턴
        /// <para/> RotateLeft : y축(기본) 또는 특정 좌표 기준으로 좌측으로 leftDegree(도) 만큼 회전한 벡터 리턴
        /// <para/> RotateUp : 벡터를 방향벡터로 간주하고 Y축 방향(0, 1, 0)을 향해 degree만큼 회전한 벡터 리턴
        /// <para/> RotateDown : 벡터를 방향벡터로 간주하고 -Y축 방향(0, -1, 0)을 향해 degree만큼 회전한 벡터 리턴
        /// <para/> DebugLine : 현재 벡터와 원점 또는 다른 벡터 좌표 사이에 디버그 라인 그리기
        /// <para/> DebugPoint : 현재 좌표를 별모양 디버그로 표시해주기
        /// <para/> AngleInRange : 두 방향벡터의 각도가 최소~최대 각도 범위 이내인지 검사
        /// <para/> GetScreenPoint2D : 3D 좌표를 화면의 2D 좌표로 리턴
        /// <para/> 
        /// <para/> 
        /// </summary>
        public static void 목록_동적_메소드() { }

        #endregion // ==================================================================

        #region Static Properties

        /// <summary>
        /// (0, 0, 0)
        /// </summary>
        public static SmartVector3 Null
        {
            get => new SmartVector3(0f, 0f, 0f);
        }

        /// <summary>
        /// (0, 0, 0)
        /// </summary>
        public static SmartVector3 Zero
        {
            get => new SmartVector3(0f, 0f, 0f);
        }

        /// <summary>
        /// (1, 1, 1)
        /// </summary>
        public static SmartVector3 One
        {
            get => new SmartVector3(1f, 1f, 1f);
        }

        /// <summary>
        /// (0, 0, 1)
        /// </summary>
        public static SmartVector3 Forward
        {
            get => new SmartVector3(0f, 0f, 1f);
        }

        /// <summary>
        /// (0, 0, -1)
        /// </summary>
        public static SmartVector3 Back
        {
            get => new SmartVector3(0f, 0f, -1f);
        }

        /// <summary>
        /// (0, 1, 0)
        /// </summary>
        public static SmartVector3 Up
        {
            get => new SmartVector3(0f, 1f, 0f);
        }

        /// <summary>
        /// (0, -1, 0)
        /// </summary>
        public static SmartVector3 Down
        {
            get => new SmartVector3(0f, -1f, 0f);
        }

        /// <summary>
        /// (1, 0, 0)
        /// </summary>
        public static SmartVector3 Right
        {
            get => new SmartVector3(1f, 0f, 0f);
        }

        /// <summary>
        /// (-1, 0, 0)
        /// </summary>
        public static SmartVector3 Left
        {
            get => new SmartVector3(-1f, 0f, 0f);
        }

        /// <summary>
        /// x, y, z 각각 float의 양의 최댓값을 초기화하여 리턴
        /// </summary>
        public static SmartVector3 PositiveInfinity
        {
            get => new SmartVector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
        }
        /// <summary>
        /// x, y, z 각각 float의 음의 최댓값을 초기화하여 리턴
        /// </summary>
        public static SmartVector3 NegativeInfinity
        {
            get => new SmartVector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
        }

        #endregion // ==================================================================

        #region Properties

        public float X
        {
            set => vec.x = value;
            get => vec.x;
        }
        public float Y
        {
            set => vec.y = value;
            get => vec.y;
        }
        public float Z
        {
            set => vec.z = value;
            get => vec.z;
        }
        public Vector3 Vector3
        {
            get => vec;
        }

        /// <summary>
        /// 음수 벡터 반환
        /// </summary>
        public SmartVector3 Reverse
        {
            get => new SmartVector3(-X, -Y, -Z);
        }
        /// <summary>
        /// 음수 벡터 반환
        /// </summary>
        public SmartVector3 Minus
        {
            get => new SmartVector3(-X, -Y, -Z);
        }
        /// <summary>
        /// 크기가 1인 벡터 반환
        /// </summary>
        public SmartVector3 Normalized
        {
            get => new SmartVector3(vec.normalized);
        }

        /// <summary>
        /// 벡터의 길이 반환
        /// </summary>
        public float Magnitude
        {
            get => vec.magnitude;
        }
        /// <summary>
        /// 벡터의 길이 반환
        /// </summary>
        public float Length
        {
            get => vec.magnitude;
        }
        /// <summary>
        /// 벡터의 길이를 제곱한 값 반환
        /// </summary>
        public float SqrLength
        {
            get => vec.sqrMagnitude;
        }

        /// <summary>
        /// 쿼터니언으로 변환하여 리턴
        /// </summary>
        public Quaternion Quaternion
        {
            get => Quaternion.Euler(vec);
        }

        #endregion // ==================================================================

        #region Indexers

        // smartVector3['x'], ['y'], ['z']
        public float this[char cIndex]
        {
            set
            {
                switch (cIndex)
                {
                    case 'x':
                    case 'X':
                        vec.x = value;
                        break;

                    case 'y':
                    case 'Y':
                        vec.y = value;
                        break;

                    case 'z':
                    case 'Z':
                        vec.z = value;
                        break;
                }
            }
            get
            {
                switch (cIndex)
                {
                    case 'x':
                    case 'X':
                        return vec.x;

                    case 'y':
                    case 'Y':
                        return vec.y;

                    case 'z':
                    case 'Z':
                        return vec.z;

                    default:
                        return vec.x;
                }
            }
        }

        // smartVector3["x"], ["y"], ["z"]
        public float this[string sIndex]
        {
            set
            {
                switch (sIndex)
                {
                    case "x":
                    case "X":
                        vec.x = value;
                        break;

                    case "y":
                    case "Y":
                        vec.y = value;
                        break;

                    case "z":
                    case "Z":
                        vec.z = value;
                        break;
                }
            }
            get
            {
                switch (sIndex)
                {
                    case "x":
                    case "X":
                        return vec.x;

                    case "y":
                    case "Y":
                        return vec.y;

                    case "z":
                    case "Z":
                        return vec.z;

                    default:
                        return vec.x;
                }
            }
        }

        // smartVector3[0], [1], [2]
        public float this[int index]
        {
            set
            {
                switch (index)
                {
                    case 0:
                        vec.x = value;
                        break;

                    case 1:
                        vec.y = value;
                        break;

                    case 2:
                        vec.z = value;
                        break;
                }
            }
            get
            {
                switch (index)
                {
                    case 0:
                        return vec.x;

                    case 1:
                        return vec.y;

                    case 2:
                        return vec.z;

                    default:
                        return vec.x;
                }
            }
        }

        #endregion // ==================================================================

        #region Constructors

        public SmartVector3(Vector3 vec3)
        {
            vec = vec3;
        }

        public SmartVector3(Vector2 vec2)
        {
            vec = new Vector3(vec2.x, vec2.y);
        }

        public SmartVector3(SmartVector3 vec3)
        {
            vec = vec3.vec;
        }

        public SmartVector3(float x, float y, float z)
        {
            vec = new Vector3(x, y, z);
        }

        public SmartVector3(float x, float y)
        {
            vec = new Vector3(x, y, 0f);
        }

        public SmartVector3(float x)
        {
            vec = new Vector3(x, 0f, 0f);
        }

        public SmartVector3((float, float, float) f)
        {
            vec = new Vector3(f.Item1, f.Item2, f.Item3);
        }

        public SmartVector3(double x, double y, double z)
        {
            vec = new Vector3((float)x, (float)y, (float)z);
        }

        public SmartVector3(double x, double y)
        {
            vec = new Vector3((float)x, (float)y, 0f);
        }

        public SmartVector3(double x)
        {
            vec = new Vector3((float)x, 0f, 0f);
        }

        public SmartVector3((double, double, double) d)
        {
            vec = new Vector3((float)d.Item1, (float)d.Item2, (float)d.Item3);
        }

        // 정규식 이용하여 스트링 생성자 추가 : X, Y, Z 꼴(컴마 구분)

        #endregion // ==================================================================

        #region Operators

        // 1. 단항 연산자
        public static SmartVector3 operator +(in SmartVector3 sv) => sv;
        public static SmartVector3 operator -(in SmartVector3 sv) => sv.Reverse;
        public static SmartVector3 operator !(in SmartVector3 sv) => sv.Reverse;
        public static SmartVector3 operator ~(in SmartVector3 sv) => sv.Reverse;
        public static SmartVector3 operator ++(in SmartVector3 sv) => new SmartVector3(sv + 1f);
        public static SmartVector3 operator --(in SmartVector3 sv) => new SmartVector3(sv - 1f);

        // 2. 이항 연산자 - 논리
        public static bool operator ==(in SmartVector3 sv1, in SmartVector3 sv2)
            => (sv1.vec == sv2.vec);
        public static bool operator ==(in SmartVector3 sv1, Vector3 v2)
            => (sv1.vec == v2);
        public static bool operator ==(Vector3 v1, in SmartVector3 sv2)
            => (v1 == sv2);

        public static bool operator !=(in SmartVector3 sv1, in SmartVector3 sv2)
            => (sv1.vec != sv2.vec);
        public static bool operator !=(in SmartVector3 sv1, Vector3 v2)
            => (sv1.vec != v2);
        public static bool operator !=(Vector3 v1, in SmartVector3 sv2)
            => (v1 != sv2);

        public static bool operator <(in SmartVector3 sv1, in SmartVector3 sv2)
            => (sv1.Magnitude < sv2.Magnitude);
        public static bool operator <(in SmartVector3 sv1, Vector3 v2)
            => (sv1.Magnitude < v2.magnitude);
        public static bool operator <(Vector3 v1, in SmartVector3 sv2)
            => (v1.magnitude < sv2.Magnitude);

        public static bool operator >(in SmartVector3 sv1, in SmartVector3 sv2)
            => (sv1.Magnitude > sv2.Magnitude);
        public static bool operator >(in SmartVector3 sv1, Vector3 v2)
            => (sv1.Magnitude > v2.magnitude);
        public static bool operator >(Vector3 v1, in SmartVector3 sv2)
            => (v1.magnitude > sv2.Magnitude);

        public static bool operator <=(in SmartVector3 sv1, in SmartVector3 sv2)
            => (sv1.Magnitude <= sv2.Magnitude);
        public static bool operator <=(in SmartVector3 sv1, Vector3 v2)
            => (sv1.Magnitude <= v2.magnitude);
        public static bool operator <=(Vector3 v1, in SmartVector3 sv2)
            => (v1.magnitude <= sv2.Magnitude);

        public static bool operator >=(in SmartVector3 sv1, in SmartVector3 sv2)
            => (sv1.Magnitude >= sv2.Magnitude);
        public static bool operator >=(in SmartVector3 sv1, Vector3 v2)
            => (sv1.Magnitude >= v2.magnitude);
        public static bool operator >=(Vector3 v1, in SmartVector3 sv2)
            => (v1.magnitude >= sv2.Magnitude);

        // 3. 이항 연산자 - 산술 : 각 성분끼리 사칙연산
        // [+]
        public static SmartVector3 operator +(in SmartVector3 sv1, in SmartVector3 sv2)
            => (sv1.vec + sv2.vec);
        public static SmartVector3 operator +(in SmartVector3 sv1, Vector3 v2)
            => (sv1.vec + v2);
        public static SmartVector3 operator +(Vector3 v1, in SmartVector3 sv2)
            => (v1 + sv2);

        public static SmartVector3 operator +(in SmartVector3 sv1, float f2)
            => (sv1.vec.x + f2, sv1.vec.y + f2, sv1.vec.z + f2);
        public static SmartVector3 operator +(in SmartVector3 sv1, double d2)
            => (sv1.vec.x + (float)d2, sv1.vec.y + (float)d2, sv1.vec.z + (float)d2);

        // [-]
        public static SmartVector3 operator -(in SmartVector3 sv1, in SmartVector3 sv2)
            => (sv1.vec - sv2.vec);
        public static SmartVector3 operator -(in SmartVector3 sv1, Vector3 v2)
            => (sv1.vec - v2);
        public static SmartVector3 operator -(Vector3 v1, in SmartVector3 sv2)
            => (v1 - sv2);

        public static SmartVector3 operator -(in SmartVector3 sv1, float f2)
            => (sv1.vec.x - f2, sv1.vec.y - f2, sv1.vec.z - f2);
        public static SmartVector3 operator -(in SmartVector3 sv1, double d2)
            => (sv1.vec.x - (float)d2, sv1.vec.y - (float)d2, sv1.vec.z - (float)d2);

        // [*]
        public static SmartVector3 operator *(in SmartVector3 sv1, in SmartVector3 sv2)
            => ((sv1.vec.x * sv2.vec.x), (sv1.vec.y * sv2.vec.y), (sv1.vec.z * sv2.vec.z));
        public static SmartVector3 operator *(in SmartVector3 sv1, Vector3 v2)
            => ((sv1.vec.x * v2.x), (sv1.vec.y * v2.y), (sv1.vec.z * v2.z));
        public static SmartVector3 operator *(Vector3 v1, in SmartVector3 sv2)
            => ((v1.x * sv2.vec.x), (v1.y * sv2.vec.y), (v1.z * sv2.vec.z));

        public static SmartVector3 operator *(in SmartVector3 sv1, float f2)
            => (sv1.vec * f2);
        public static SmartVector3 operator *(in SmartVector3 sv1, double d2)
            => (sv1.vec * (float)d2);

        // [/]
        public static SmartVector3 operator /(in SmartVector3 sv1, in SmartVector3 sv2)
            => ((sv1.vec.x / sv2.vec.x), (sv1.vec.y / sv2.vec.y), (sv1.vec.z / sv2.vec.z));
        public static SmartVector3 operator /(in SmartVector3 sv1, Vector3 v2)
            => ((sv1.vec.x / v2.x), (sv1.vec.y / v2.y), (sv1.vec.z / v2.z));
        public static SmartVector3 operator /(Vector3 v1, in SmartVector3 sv2)
            => ((v1.x / sv2.vec.x), (v1.y / sv2.vec.y), (v1.z / sv2.vec.z));

        public static SmartVector3 operator /(in SmartVector3 sv1, float f2)
            => (sv1.vec / f2);
        public static SmartVector3 operator /(in SmartVector3 sv1, double d2)
            => (sv1.vec / (float)d2);

        #endregion // ==================================================================

        #region Type Castings

        // 암시적 변환 : Vector3 <- SmartVector3
        public static implicit operator Vector3(in SmartVector3 sv) => sv.vec;

        // 암시적 변환 : Vector2 <- SmartVector3
        public static implicit operator Vector2(in SmartVector3 sv) => new Vector2(sv.vec.x, sv.vec.y);

        // 암시적 변환 : string <- SmartVector3
        public static implicit operator string(in SmartVector3 sv) => $"({sv.X}, {sv.Y}, {sv.Z})";


        // 암시적 변환: (float, float, float) <- SmartVector3
        public static implicit operator (float, float, float)(in SmartVector3 sv)
            => (sv.X, sv.Y, sv.Z);

        // 암시적 변환: (float, float) <- SmartVector3
        public static implicit operator (float, float)(in SmartVector3 sv)
            => (sv.X, sv.Y);

        // 암시적 변환: (double, double, double) <- SmartVector3
        public static implicit operator (double, double, double)(in SmartVector3 sv)
            => (sv.X, sv.Y, sv.Z);

        // 암시적 변환: (double, double) <- SmartVector3
        public static implicit operator (double, double)(in SmartVector3 sv)
            => (sv.X, sv.Y);


        // 암시적 변환 : Quaternion <- SmartVector3
        public static implicit operator Quaternion(in SmartVector3 sv)
            => Quaternion.Euler(sv.vec);

        // =====

        // 암시적 변환 : SmartVector3 <- Vector3
        public static implicit operator SmartVector3(Vector3 vec3) => new SmartVector3(vec3);

        // 암시적 변환 : SmartVector3 <- Vector2
        public static implicit operator SmartVector3(Vector2 vec2) => new SmartVector3(vec2);

        // 암시적 변환 : SmartVector3 <- float
        // 예 : in SmartVector3 sv = 5.0f  ->  (5.0f, 5.0f, 5.0f) 저장
        public static implicit operator SmartVector3(float f) => new SmartVector3(f, f, f);

        // 암시적 변환 : SmartVector3 <- double
        // 예 : in SmartVector3 sv = 5.0  ->  (5.0f, 5.0f, 5.0f) 저장
        public static implicit operator SmartVector3(double d) => new SmartVector3(d, d, d);


        // 암시적 변환: SmartVector3 <- (float, float, float)
        public static implicit operator SmartVector3((float, float, float) f)
            => new SmartVector3(f.Item1, f.Item2, f.Item3);

        // 암시적 변환: SmartVector3 <- (float, float)
        public static implicit operator SmartVector3((float, float) f)
            => new SmartVector3(f.Item1, f.Item2, 0f);


        // 암시적 변환: SmartVector3 <- (double, double, double)
        public static implicit operator SmartVector3((double, double, double) d)
            => new SmartVector3(d.Item1, d.Item2, d.Item3);

        // 암시적 변환: SmartVector3 <- (double, double)
        public static implicit operator SmartVector3((double, double) d)
            => new SmartVector3(d.Item1, d.Item2, 0f);


        // 암시적 변환 : SmartVector3 <- Quaternion
        public static implicit operator SmartVector3(Quaternion q)
            => new SmartVector3(q.eulerAngles);

        #endregion // ==================================================================


        #region Static Methods - Vector3 Overriding
        // 벡터3 모든 메소드 오버라이딩

        // 1. Vector3 오버라이딩

        /// <summary>
        /// 두 벡터 사이의 각도(Degree) 반환
        /// </summary>
        public static float Angle(in SmartVector3 from, in SmartVector3 to)
        {
            return Vector3.Angle(from.vec, to.vec);
        }

        /// <summary>
        /// 두 벡터 사이의 각도(Degree) 반환, 기준 축에 따라 부호 결정
        /// </summary>
        public static float SignedAngle(in SmartVector3 from, in SmartVector3 to, in SmartVector3 axis)
        {
            return Vector3.SignedAngle(from.vec, to.vec, axis.vec);
        }

        /// <summary>
        /// 벡터의 최대 길이를 maxLength로 잘라 리턴 <para/>
        /// 현재 길이가 maxLength보다 작은 경우, 현재 벡터를 리턴
        /// </summary>
        public static SmartVector3 ClampMagnitude(in SmartVector3 sv, in float maxLength)
        {
            return Vector3.ClampMagnitude(sv.vec, maxLength);
        }

        /// <summary>
        /// 외적
        /// </summary>
        public static SmartVector3 Cross(in SmartVector3 sv1, in SmartVector3 sv2)
        {
            return Vector3.Cross(sv1.vec, sv2.vec);
        }

        /// <summary>
        /// 내적
        /// </summary>
        public static float Dot(in SmartVector3 sv1, in SmartVector3 sv2)
        {
            return Vector3.Dot(sv1.vec, sv2.vec);
        }

        /// <summary>
        /// 두 벡터 좌표 사이의 거리 반환 <para/>
        /// Distance(a,b)는 (a-b).Magnitude와 동일
        /// </summary>
        public static float Distance(in SmartVector3 sv1, in SmartVector3 sv2)
        {
            return Vector3.Distance(sv1.vec, sv2.vec);
        }

        /// <summary>
        /// 두 벡터(좌표) 사이를 선형보간 <para/>
        /// t : 0(sv1) ~ 1(sv2)
        /// </summary>
        public static SmartVector3 Lerp(in SmartVector3 sv1, in SmartVector3 sv2, in float t)
        {
            return Vector3.Lerp(sv1.vec, sv2.vec, t);
        }

        /// <summary>
        /// 두 벡터(좌표) 사이를 선형보간 <para/>
        /// t : 0(sv1) ~ 1(sv2)           <para/>
        /// Lerp와는 달리, t의 하한/상한이 존재하지 않는다.
        /// </summary>
        public static SmartVector3 LerpUnclamped(in SmartVector3 sv1, in SmartVector3 sv2, in float t)
        {
            return Vector3.LerpUnclamped(sv1.vec, sv2.vec, t);
        }

        /// <summary>
        /// 두 벡터(좌표) 사이를 구면선형보간 <para/>
        /// t : 0(sv1) ~ 1(sv2)               <para/>
        /// 선형보간과는 달리, 두 좌표 사이에 호를 그리며 보간한다. <para/>
        /// 활용 예시 : 캐릭터 회전
        /// </summary>
        public static SmartVector3 Slerp(in SmartVector3 sv1, in SmartVector3 sv2, in float t)
        {
            return Vector3.Slerp(sv1.vec, sv2.vec, t);
        }

        /// <summary>
        /// 두 벡터(좌표) 사이를 구면선형보간 <para/>
        /// t : 0(sv1) ~ 1(sv2) ~ 무한        <para/>
        /// 선형보간과는 달리, 두 좌표 사이에 호를 그리며 보간한다. <para/>
        /// Slerp와는 달리, t의 하한/상한이 존재하지 않는다.
        /// </summary>
        public static SmartVector3 SlerpUnclamped(in SmartVector3 sv1, in SmartVector3 sv2, in float t)
        {
            return Vector3.SlerpUnclamped(sv1.vec, sv2.vec, t);
        }

        /// <summary>
        /// 두 벡터 (x1,y1,z1), (x2,y2,z2)를 받아 <para/>
        /// (Max(x1, x2), Max(y1, y2), Max(z1, z2)) 리턴
        /// </summary>
        public static SmartVector3 Max(in SmartVector3 sv1, in SmartVector3 sv2)
        {
            return Vector3.Max(sv1.vec, sv2.vec);
        }

        /// <summary>
        /// 두 벡터 (x1,y1,z1), (x2,y2,z2)를 받아 <para/>
        /// (Min(x1, x2), Min(y1, y2), Min(z1, z2)) 리턴
        /// </summary>
        public static SmartVector3 Min(in SmartVector3 sv1, in SmartVector3 sv2)
        {
            return Vector3.Min(sv1.vec, sv2.vec);
        }

        /// <summary>
        /// current로부터 target 지점을 향해 한 번 이동한 벡터 리턴 <para/>
        /// 한 번의 이동 거리(Distance) = maxDistanceDelta<para/>
        /// maxDistanceDelta 예시 = Time.deltaTime * 2
        /// </summary>
        public static SmartVector3 MoveTowards(
            in SmartVector3 current, in SmartVector3 target, in float maxDistanceDelta)
        {
            return Vector3.MoveTowards(current.vec, target.vec, maxDistanceDelta);
        }

        /// <summary>
        /// 벡터를 방향벡터로 간주하고 target 방향을 향해 한 번 회전한 벡터 리턴 <para/>
        /// maxRadiansDelta : 한 번 회전할 각도(1 라디안 = 57.2958도) <para/>
        /// maxMagnitudeDelta : 한 번 회전할 때마다 방향벡터에 더해줄 길이
        /// </summary>
        // https://docs.unity3d.com/kr/530/ScriptReference/Vector3.RotateTowards.html
        public static SmartVector3 RotateTowards(in SmartVector3 current,
            in SmartVector3 target, in float maxRadiansDelta, in float maxMagnitudeDelta)
        {
            return Vector3.RotateTowards(current.vec, target.vec,
                maxRadiansDelta, maxMagnitudeDelta);
        }

        /// <summary>
        /// <para/> 1. normal(법선), tangent(접선) 벡터를 정규화
        /// <para/> 2. tangent 벡터가 normal 벡터에 직교하도록 변경
        /// <para/> .
        /// <para/> * 법선 벡터 : 기준 벡터
        /// <para/> * 접선 벡터 : 법선에 직교하는 벡터
        /// </summary>
        public static void OrthoNormalize(
            ref SmartVector3 normal, ref SmartVector3 tangent)
        {
            Vector3.OrthoNormalize(ref normal.vec, ref tangent.vec);
        }

        /// <summary>
        /// <para/> 1. normal(법선), tangent(접선), binormal(종법선) 벡터를 정규화
        /// <para/> 2. 세 벡터가 서로 직교하도록 tangent, binormal 벡터를 변경
        /// <para/> .
        /// <para/> * 법선 벡터 : 기준 벡터
        /// <para/> * 종법선 벡터 : 법선에 직교하는 기준 벡터
        /// <para/> * 접선 벡터 : 법선, 종법선에 모두에 직교하는 벡터 (= 법선과 종법선의 외적)
        /// </summary>
        public static void OrthoNormalize(
            ref SmartVector3 normal, ref SmartVector3 tangent, ref SmartVector3 binormal)
        {
            Vector3.OrthoNormalize(ref normal.vec, ref tangent.vec, ref binormal.vec);
        }

        // https://docs.unity3d.com/kr/530/ScriptReference/Vector3.Project.html
        /// <summary>
        /// vector를 onNormal 벡터에 투영시킨(겹친) 벡터 리턴
        /// <para/> 
        /// </summary>
        public static SmartVector3 Project(in SmartVector3 vector, in SmartVector3 onNormal)
        {
            return Vector3.Project(vector.vec, onNormal.vec);
        }

        /// <summary>
        /// vector를 Plane에 투영시킨 벡터 리턴
        /// </summary>
        public static SmartVector3 ProjectOnPlane(in SmartVector3 vector, in SmartVector3 planeNormal)
        {
            return Vector3.ProjectOnPlane(vector.vec, planeNormal.vec);
        }

        // https://docs.unity3d.com/kr/530/ScriptReference/Vector3.Reflect.html
        /// <summary>
        /// inNormal(법선) 벡터를 기준으로 inDirection 벡터를 반전시킨 벡터 리턴
        /// <para/> 크기는 같지만 방향이 법선 벡터를 기준으로 반전된다.
        /// </summary>
        public static SmartVector3 Reflect(in SmartVector3 inDirection, in SmartVector3 inNormal)
        {
            return Vector3.Reflect(inDirection.vec, inNormal.vec);
        }

        /// <summary>
        /// 두 벡터의 x, y, z 성분을 각각 곱한 벡터 리턴
        /// </summary>
        public static SmartVector3 Scale(in SmartVector3 sv1, in SmartVector3 sv2)
        {
            return Vector3.Scale(sv1.vec, sv2.vec);
        }

        // https://docs.unity3d.com/kr/530/ScriptReference/Vector3.SmoothDamp.html
        /// <summary>
        /// 시간 경과에 따라 벡터를 부드럽게 이동 <para/>
        /// 대표적 사용 예시 : 카메라 이동        <para/>
        /// [매개변수]
        /// <para/> current : 현재 위치
        /// <para/> target : 목표 위치
        /// <para/> currentVelocity : 현재 속도를 변수에 기억
        /// <para/> smoothTime : 작을수록 타겟에 빠르게 도달
        /// <para/> maxSpeed : 선택적(지정/미지정)으로 최대 속도 고정
        /// <para/> deltaTime : 이 함수가 마지막으로 호출되고난 후 경과 시간(기본 : Time.deltaTime)
        /// </summary>
        public static SmartVector3 SmoothDamp(
            in SmartVector3 current,
            in SmartVector3 target,
            ref SmartVector3 currentVelocity,
            in float smoothTime,
            in float maxSpeed = Mathf.Infinity,
            in float deltaTime = Mathf.NegativeInfinity)
        {
            return Vector3.SmoothDamp(
                current.vec,
                target.vec,
                ref currentVelocity.vec,
                smoothTime,
                maxSpeed,
                deltaTime == Mathf.NegativeInfinity ? Time.deltaTime : deltaTime);
        }

        #endregion // ==================================================================

        #region ★ Static Methods - Custom
        // 2. 커스텀 메소드

        /// <summary>
        /// 방향벡터(크기 1) <para/>
        /// 시작 : from      <para/>
        /// 도착 : to
        /// </summary>
        public static SmartVector3 Direction(in SmartVector3 from, in SmartVector3 to)
        {
            return (to - from).Normalized;
        }
        /// <summary>
        /// 방향벡터(크기 1) <para/>
        /// 시작 : from      <para/>
        /// 도착 : to
        /// </summary>
        public static SmartVector3 FromTo(in SmartVector3 from, in SmartVector3 to)
        {
            return (to - from).Normalized;
        }

        /// <summary>
        /// 두 벡터의 중간 좌표 반환
        /// </summary>
        public static SmartVector3 Mid(in SmartVector3 sv1, in SmartVector3 sv2)
        {
            return Vector3.Lerp(sv1.vec, sv2.vec, 0.5f);
        }

        /// <summary>
        /// 두 좌표 사이에 디버그 라인 그리기
        /// </summary>
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void DebugLine(in SmartVector3 from, in SmartVector3 to,
                                      in float duration = 5f, Color color = default)
        {
            if (color == default) color = Color.red;

            Debug.DrawLine(from.vec, to.vec, color, duration);
        }

        /// <summary>
        /// 해당 좌표를 별모양 디버그로 표시해주기 <para/>
        /// duration : 지속시간, size : 별 크기
        /// </summary>
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void DebugPoint(in SmartVector3 sv,
            in float duration = 5f, in float size = 0.2f, Color color = default)
        {
            if (color == default) color = Color.red;

            DebugLine((sv.X - size * 0.5, sv.Y, sv.Z), (sv.X + size * 0.5, sv.Y, sv.Z), duration, color);
            DebugLine((sv.X, sv.Y - size * 0.5, sv.Z), (sv.X, sv.Y + size * 0.5, sv.Z), duration, color);
            DebugLine((sv.X, sv.Y, sv.Z - size * 0.5), (sv.X, sv.Y, sv.Z + size * 0.5), duration, color);

            DebugLine((sv.X - size * 0.5, sv.Y - size * 0.5, sv.Z), (sv.X + size * 0.5, sv.Y + size * 0.5, sv.Z), duration, color);
            DebugLine((sv.X, sv.Y - size * 0.5, sv.Z - size * 0.5), (sv.X, sv.Y + size * 0.5, sv.Z + size * 0.5), duration, color);
            DebugLine((sv.X - size * 0.5, sv.Y, sv.Z - size * 0.5), (sv.X + size * 0.5, sv.Y, sv.Z + size * 0.5), duration, color);

            DebugLine((sv.X + size * 0.5, sv.Y - size * 0.5, sv.Z), (sv.X - size * 0.5, sv.Y + size * 0.5, sv.Z), duration, color);
            DebugLine((sv.X, sv.Y + size * 0.5, sv.Z - size * 0.5), (sv.X, sv.Y - size * 0.5, sv.Z + size * 0.5), duration, color);
            DebugLine((sv.X + size * 0.5, sv.Y, sv.Z - size * 0.5), (sv.X - size * 0.5, sv.Y, sv.Z + size * 0.5), duration, color);
        }

        /// <summary>
        /// <para/> [Public]
        /// <para/> 두 방향벡터의 각도가 최소~최대 각도 범위 이내인지 검사
        /// <para/> ------------------------------------------------------
        /// <para/> 2020. 01. 13
        /// </summary>
        public static bool AngleInRange(in SmartVector3 from, in SmartVector3 to,
            in float minDegree, in float maxDegree)
        {
            float angle = Vector3.Angle(from.vec, to.vec);
            if (angle < minDegree) return false;
            if (angle > maxDegree) return false;

            return true;
        }

        /// <summary>
        /// 3D 좌표를 2D 스크린 좌표로 변환하여 리턴
        /// <para/> * 2020. 01. 15
        /// </summary>
        /// <returns></returns>
        public static Vector2 GetScreenPoint2D(in SmartVector3 sv)
        {
            return Camera.main.WorldToScreenPoint(sv.vec);
        }

        #endregion // ==================================================================


        #region Dynamic Methods - Vector3 Overriding
        // 벡터3 모든 메소드 오버라이딩

        // 1. 기본 오버라이딩
        public override string ToString()
        {
            return $"({vec.x}, {vec.y}, {vec.z})";
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(Vector3 vec)
        {
            return this.vec.Equals(vec);
        }

        public bool Equals(in SmartVector3 sv)
        {
            return this.vec.Equals(sv.vec);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        // 2. Vector3 오버라이딩

        /// <summary>
        /// 현재 벡터를 정규화
        /// </summary>
        public SmartVector3 Normalize()
        {
            vec.Normalize();
            return this;
        }

        /// <summary>
        /// 현재 벡터의 x,y,z 성분에 sv 벡터의 x,y,z 성분을 각각 곱한 벡터 리턴
        /// <para/> ------------------------------------------------------------------------
        /// <para/> apply 파라미터가 true인 경우, 원본 벡터에 실제로 곱셈을 수행한다.
        /// </summary>
        public SmartVector3 Scale(in SmartVector3 sv, bool apply = false)
        {
            SmartVector3 result = (vec.x * sv.X, vec.y * sv.Y, vec.z * sv.Z);

            if (apply)
                this = result;

            return result;
        }

        /// <summary>
        /// 새로운 벡터 할당
        /// </summary>
        public void Set(in float newX, in float newY, in float newZ)
        {
            vec.x = newX;
            vec.y = newY;
            vec.z = newZ;
        }

        /// <summary>
        /// 새로운 벡터 할당
        /// </summary>
        public void Set(in SmartVector3 newSV)
        {
            vec = newSV.vec;
        }

        /// <summary>
        /// 새로운 벡터 할당
        /// </summary>
        public void Set(in Vector3 newVector)
        {
            vec = newVector;
        }

        #endregion // ==================================================================

        #region Methods - [Static -> Dynamic]
        // 3. 정적 메소드를 동적으로 옮겨온 메소드

        /// <summary>
        /// 내적
        /// </summary>
        public float Dot(in SmartVector3 sv)
        {
            return Dot(this, sv);
        }

        /// <summary>
        /// 외적
        /// </summary>
        public SmartVector3 Cross(in SmartVector3 sv)
        {
            return Cross(this, sv);
        }

        /// <summary>
        /// 방향벡터(크기 1) <para/>
        /// 시작 : this      <para/>
        /// 도착 : toVector
        /// </summary>
        public SmartVector3 Direction(in SmartVector3 toVector)
        {
            return new SmartVector3(toVector - this).Normalized;
        }

        /// <summary>
        /// 두 벡터 사이의 각도(Degree) 반환
        /// </summary>
        public float Angle(in SmartVector3 sv)
        {
            return Vector3.Angle(vec, sv.vec);
        }

        /// <summary>
        /// 두 벡터 좌표 사이의 거리 반환 <para/>
        /// Distance(a,b)는 (a-b).Magnitude와 동일
        /// </summary>
        public float Distance(in SmartVector3 sv)
        {
            return Vector3.Distance(vec, sv.vec);
        }

        /// <summary>
        /// 벡터의 최대 길이를 maxLength로 잘라 리턴 <para/>
        /// 현재 길이가 maxLength보다 작은 경우, 현재 벡터를 리턴 <para/>
        /// <para/> ------------------------------------------------------------------------
        /// <para/> apply 파라미터가 true인 경우, 원본 벡터를 실제로 자른다.
        public SmartVector3 ClampLength(in float maxLength, bool apply = false)
        {
            var result = Vector3.ClampMagnitude(vec, maxLength);

            if (apply)
                vec = result;

            return result;
        }

        /// <summary>
        /// 벡터의 최대 길이를 maxLength로 잘라 리턴 <para/>
        /// 현재 길이가 maxLength보다 작은 경우, 현재 벡터를 리턴 <para/>
        /// <para/> ------------------------------------------------------------------------
        /// <para/> apply 파라미터가 true인 경우, 원본 벡터를 실제로 자른다.
        public SmartVector3 ClampMagnitude(in float maxLength, bool apply = false)
        {
            var result = Vector3.ClampMagnitude(vec, maxLength);

            if (apply)
                vec = result;

            return result;
        }

        /// <summary>
        /// 현재 위치로부터 target 지점을 향해 한 번 이동한 벡터 리턴 <para/>
        /// 한 번의 이동 거리(Distance) = maxDistanceDelta<para/>
        /// maxDistanceDelta 예시 = Time.deltaTime * 2 <para/>
        /// <para/> ------------------------------------------------------------------------
        /// <para/> apply 파라미터가 true인 경우, 원본 벡터를 실제로 이동시킨다.
        /// </summary>
        public SmartVector3 MoveTowards(in SmartVector3 target, in float maxDistanceDelta, bool apply = false)
        {
            Vector3 result = Vector3.MoveTowards(vec, target.vec, maxDistanceDelta);

            if (apply)
                vec = result;

            return result;
        }

        /// <summary>
        /// 벡터를 방향벡터로 간주하고 target 방향을 향해 한 번 회전한 벡터 리턴 <para/>
        /// maxRadiansDelta : 한 번 회전할 각도(1 라디안 = 57.2958도) <para/>
        /// maxMagnitudeDelta : 한 번 회전할 때마다 방향벡터에 더해줄 길이 <para/>
        /// <para/> ------------------------------------------------------------------------
        /// <para/> apply 파라미터가 true인 경우, 원본 벡터를 실제로 회전시킨다.
        /// </summary>
        public SmartVector3 RotateTowards(in SmartVector3 target, in float maxRadiansDelta, in float maxMagnitudeDelta = 0f, bool apply = false)
        {
            Vector3 result = Vector3.RotateTowards(vec, target.vec, maxRadiansDelta, maxMagnitudeDelta);

            if (apply)
                vec = result;

            return result;
        }

        #endregion

        #region ★ Dynamic Methods - Custom

        /// <summary>
        /// 방향벡터(크기 1) <para/>
        /// 시작 : this      <para/>
        /// 도착 : toVector
        /// </summary>
        public SmartVector3 To(in SmartVector3 toVector)
        {
            return new SmartVector3(toVector - this).Normalized;
        }
        /// <summary>
        /// 방향벡터(크기 1)  <para/>
        /// 시작 : fromVector <para/>
        /// 도착 : this
        /// </summary>
        public SmartVector3 From(in SmartVector3 fromVector)
        {
            return new SmartVector3(this - fromVector).Normalized;
        }

        // http://www.devkorea.co.kr/bbs/board.php?bo_table=m03_qna&wr_id=46332
        /// <summary>
        /// <para/> 특정 좌표를 기준으로 특정 각도(degree)만큼 회전시킨 벡터 리턴
        /// <para/> 회전 기준점(axisPoint)을 지정하지 않을 경우, 기준은 (0,0,0)
        /// <para/> .
        /// <para/> 회전 기준점을 지정할 경우, 좌표를 옮겨 (0,0,0)을 기준으로 회전시킨 뒤 다시 상대좌표로 옮겨서 리턴
        /// <para/> .
        /// <para/> x, y, z 모두 값이 주어질 경우 회전축 우선순위
        /// <para/> : z -> x -> y
        /// <para/> (z축으로 회전시키고 x축으로 다시 회전시킨 다음, 마지막으로 y축 회전)
        /// <para/> ------------------------------------------------------------------------
        /// <para/> apply 파라미터가 true인 경우, 원본 벡터를 실제로 회전시킨다.
        /// </summary>
        public SmartVector3 Rotate(in float xDeg, in float yDeg, in float zDeg, in Vector3 axisPoint = default, bool apply = false)
        {
            // 회전 기준 좌표이자 상대 좌표
            Vector3 transitionVector = (axisPoint == default) ? Vector3.zero : axisPoint;
            Vector3 originVector = this;

            // 1. 상대 좌표 이동
            originVector -= transitionVector;

            // 회전계수 = 회전각(x,y,z)
            Quaternion rotationCoef = Quaternion.Euler(xDeg, yDeg, zDeg);

            // 2. x,y,z 각각의 "축"으로 회전
            originVector = rotationCoef * originVector;

            // 3. 다시 좌표 복귀
            originVector += transitionVector;

            // + 추가 : 원본 벡터 회전
            if (apply)
                this = originVector;

            return originVector;
        }

        /// <summary>
        /// y축(기본) 또는 특정 좌표 기준으로 우측으로 rightDegree(도) 만큼 회전한 벡터 리턴
        /// <para/> * 현재 벡터의 크기는 고정
        /// <para/> ------------------------------------------------------------------------
        /// <para/> apply 파라미터가 true인 경우, 원본 벡터를 실제로 회전시킨다.
        /// </summary>
        public SmartVector3 RotateRight(in float rightDegree, in Vector3 axisPoint = default, bool apply = false)
        {
            return Rotate(0f, rightDegree, 0f, axisPoint, apply);
        }

        /// <summary>
        /// y축(기본) 또는 특정 좌표 기준으로 좌측으로 leftDegree(도) 만큼 회전한 벡터 리턴
        /// <para/> * 현재 벡터의 크기는 고정
        /// <para/> ------------------------------------------------------------------------
        /// <para/> apply 파라미터가 true인 경우, 원본 벡터를 실제로 회전시킨다.
        /// </summary>
        public SmartVector3 RotateLeft(in float leftDegree, in Vector3 axisPoint = default, bool apply = false)
        {
            return Rotate(0f, -leftDegree, 0f, axisPoint, apply);
        }

        /// <summary>
        /// ★ 벡터를 방향벡터로 간주하고 Y축 방향(0, 1, 0)을 향해 degree만큼 회전한 벡터 리턴
        /// <para/> * 현재 벡터의 크기는 고정
        /// <para/> ------------------------------------------------------------------------
        /// <para/> apply 파라미터가 true인 경우, 원본 벡터를 실제로 회전시킨다.
        /// </summary>
        public SmartVector3 RotateUp(in float degree, bool apply = false)
        {
            return RotateTowards(Up, Mathf.Deg2Rad * degree, 0f, apply);
        }

        /// <summary>
        /// ★ 벡터를 방향벡터로 간주하고 -Y축 방향(0, -1, 0)을 향해 degree만큼 회전한 벡터 리턴
        /// <para/> * 현재 벡터의 크기는 고정
        /// <para/> ------------------------------------------------------------------------
        /// <para/> apply 파라미터가 true인 경우, 원본 벡터를 실제로 회전시킨다.
        /// </summary>
        public SmartVector3 RotateDown(in float degree, bool apply = false)
        {
            return RotateTowards(Down, Mathf.Deg2Rad * degree, 0f, apply);
        }

        /// <summary>
        /// 현재 벡터 좌표와 원점 사이에 디버그 라인 그리기
        /// </summary>
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public void DebugLine(in float duration = 5f, Color color = default)
        {
            if (color == default) color = Color.red;

            Debug.DrawLine(Vector3.zero, vec, color, duration);
        }

        /// <summary>
        /// 현재 벡터 좌표와 다른 벡터 좌표 사이에 디버그 라인 그리기
        /// </summary>
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public void DebugLine(in Vector3 other, float duration = 5f, Color color = default)
        {
            if (color == default) color = Color.red;

            Debug.DrawLine(vec, other, color, duration);
        }

        /// <summary>
        /// 현재 좌표를 별모양 디버그로 표시해주기 <para/>
        /// duration : 지속시간, size : 별 크기
        /// </summary>
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public void DebugPoint(in float duration = 5f, float size = 0.2f, Color color = default)
        {
            DebugPoint(vec, duration, size, color);
        }

        /// <summary>
        /// 3D 좌표를 2D 스크린 좌표로 변환하여 리턴
        /// <para/> * 2020. 01. 15
        /// </summary>
        public Vector2 GetScreenPoint2D()
        {
            return Camera.main.WorldToScreenPoint(vec);
        }

        #endregion // ==================================================================
    }
}