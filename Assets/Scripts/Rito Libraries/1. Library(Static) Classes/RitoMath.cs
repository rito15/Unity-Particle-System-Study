using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito
{
    // 2020. 01. 15. - 주석 추가, in 한정자 추가(ref로 오버로딩 하는 메소드는 in 안씀)

    /// <summary>
    /// 수학, 계산 정적 메소드
    /// <para/> ----------------------------------------
    /// <para/> [메소드 목록]
    /// <para/> GetRandom : min ~ max 범위 내 난수 리턴
    /// <para/> Digitalize : 값을 0 또는 1로 디지털화하여 리턴
    /// <para/> SignedDigitalize : 값을 -1, 0, 1로 디지털화하여 리턴
    /// <para/> InRange : 변수 값이 범위 내에 있는지 검사
    /// <para/> Clamp : 변수 값을 범위 내로 보정
    /// <para/> 
    /// </summary>
    public static class RitoMath// : MonoBehaviour
    {
        #region Random

        private static int GetSeed()
        {
            var rand = System.Security.Cryptography.RandomNumberGenerator.Create();
            byte[] data = new byte[100];

            rand.GetBytes(data);

            return System.Math.Abs(System.BitConverter.ToInt32(data, 4));
        }

        /// <summary>
        /// min ~ max 범위 내의 난수 생성하기
        /// <para/> * min, max 포함
        /// </summary>
        public static int GetRandom(int min, int max)
        {
            if (min < int.MinValue) min = int.MinValue;
            if (max > int.MaxValue) max = int.MaxValue;

            return new System.Random(GetSeed()).Next(min, max + 1);
        }

        public static int GetRandom(float min, float max)
        {
            return GetRandom((int)min, (int)max);
        }

        #endregion // ================================================================

        #region Digitalize

        /// <summary>
        /// 정수를 0, 1 값으로 디지털화하여 리턴
        /// </summary>
        public static int Digitalize(in int intValue)
        {
            return intValue == 0 ?
                0: 
                1;
        }

        /// <summary>
        /// 실수를 0, 1 값으로 디지털화하여 리턴
        /// </summary>
        public static double Digitalize(in double doubleValue)
        {
            return doubleValue == 0.0 ?
                0.0:
                1.0;
        }

        /// <summary>
        /// 정수를 -1, 0, 1 값으로 디지털화하여 리턴
        /// </summary>
        public static int SignedDigitalize(in int intValue)
        {
            if (intValue < 0) return -1;
            if (intValue > 0) return 1;
            return 0;
        }

        /// <summary>
        /// 실수를 -1, 0, 1 값으로 디지털화하여 리턴
        /// </summary>
        public static double SignedDigitalize(in double doubleValue)
        {
            if (doubleValue < 0.0) return -1.0;
            if (doubleValue > 0.0) return 1.0;
            return 0.0;
        }

        #endregion // ================================================================

        #region Range, Clamp

        /// <summary>
        /// 값이 범위 내에 있는지 검사(정점 포함)
        /// <para/> 파라미터 : (대상 변수, 최솟값, 최댓값)
        /// </summary>
        public static bool InRange(in float variable, in float min, in float max)
        {
            return (variable >= min && variable <= max);
        }
        /// <summary>
        /// 값이 범위 내에 있는지 검사(정점 포함)
        /// <para/> 파라미터 : (최솟값, ref 대상 변수, 최댓값)
        /// </summary>
        public static bool InRange(float min, ref float variable, float max)
        {
            return (variable >= min && variable <= max);
        }

        /// <summary>
        /// Clamp : 변수 값의 범위를 제한
        /// <para/> 파라미터 : (ref 변수, 최솟값, 최댓값)
        /// </summary>
        public static T Clamp<T>(ref T variable, in T min, in T max) where T : struct
        {
            switch (variable)
            {
                case int v when (min is int a && max is int b):
                    if (v < a) variable = min;
                    if (v > b) variable = max;
                    break;

                case float v when (min is float a && max is float b):
                    if (v < a) variable = min;
                    if (v > b) variable = max;
                    break;

                case double v when (min is double a && max is double b):
                    if (v < a) variable = min;
                    if (v > b) variable = max;
                    break;
            }

            return variable;
        }
        /// <summary>
        /// Clamp : 변수 값의 범위를 제한
        /// <para/> 파라미터 : (최솟값, ref 변수, 최댓값)
        /// </summary>
        public static T Clamp<T>(T min, ref T variable, T max) where T : struct
        {
            return Clamp(ref variable, min, max);
        }
        /// <summary>
        /// Clamp : 변수 값의 범위를 제한
        /// <para/> 파라미터 : (최솟값, ref 변수)
        /// </summary>
        public static T Clamp<T>(T min, ref T variable) where T : struct
        {
            return Clamp(ref variable, min, variable);
        }
        /// <summary>
        /// Clamp : 변수 값의 범위를 제한
        /// <para/> 파라미터 : (최솟값, 최댓값)
        /// </summary>
        public static T Clamp<T>(ref T variable, T max) where T : struct
        {
            return Clamp(ref variable, variable, max);
        }

        #endregion // ================================================================

    }
}