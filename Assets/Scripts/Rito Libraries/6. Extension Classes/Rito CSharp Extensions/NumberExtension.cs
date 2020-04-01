using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rito
{
    // 2020. 01. 26. 최초 작성
    // 2020. 01. 26. Clamp, Pow, Digitalize, Range 테스트 완료

    /// <summary>
    /// 2020. 01. 26.
    /// <para/> int, float, double => 정수, 실수 타입에 대해 확장
    /// <para/> -----------------------------------------------------------------------------------
    /// <para/> [목록]
    /// <para/> Ex_Clamp(min, max) : 변수 값 범위 제한(실제로 값 변경)
    /// <para/> Ex_Pow(n) : n제곱수 리턴
    /// <para/> Ex_Digitalize() : 0 또는 1로 디지털화하여 리턴
    /// <para/> Ex_SignedDigitalize() : -1, 0, 1로 디지털화하여 리턴
    /// <para/> Ex_Range(min, max) : 변수의 값이 닫힌 범위 내에 있는지 검사하여 결과 리턴
    /// <para/> 
    /// <para/> 
    /// </summary>
    public static class NumberExtension
    {
        #region Clamp

        /// <summary>
        /// 변수의 최소, 최댓값 제한
        /// <para/> * 변수의 값을 실제로 변경시킴(ref)
        /// </summary>
        public static int Ex_Clamp(ref this int value, in int min, in int max)
        {
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }

        /// <summary>
        /// 변수의 최소, 최댓값 제한
        /// <para/> * 변수의 값을 실제로 변경시킴(ref)
        /// </summary>
        public static float Ex_Clamp(ref this float value, in float min, in float max)
        {
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }

        /// <summary>
        /// 변수의 최소, 최댓값 제한
        /// <para/> * 변수의 값을 실제로 변경시킴(ref)
        /// </summary>
        public static double Ex_Clamp(ref this double value, in double min, in double max)
        {
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }

        #endregion // ==========================================================

        #region Pow

        /// <summary>
        /// n제곱 결과 리턴
        /// </summary>
        public static int Ex_Pow(in this int value, in int n)
        {
            int result = value;
            for (int i = 1; i < n; i++)
                result *= value;
            return result;
        }

        /// <summary>
        /// n제곱 결과 리턴
        /// </summary>
        public static float Ex_Pow(in this float value, in int n)
        {
            float result = value;
            for (int i = 1; i < n; i++)
                result *= value;
            return result;
        }

        /// <summary>
        /// n제곱 결과 리턴
        /// </summary>
        public static double Ex_Pow(in this double value, in int n)
        {
            double result = value;
            for (int i = 1; i < n; i++)
                result *= value;
            return result;
        }

        #endregion // ==========================================================

        #region Digitalize

        /// <summary>
        /// 0, 1 값으로 디지털화하여 리턴
        /// </summary>
        public static int Ex_Digitalize(in this int value)
        {
            return value == 0 ? 0 : 1;
        }

        /// <summary>
        /// 0, 1 값으로 디지털화하여 리턴
        /// </summary>
        public static float Ex_Digitalize(in this float value)
        {
            return value == 0f ? 0f : 1f;
        }

        /// <summary>
        /// 0, 1 값으로 디지털화하여 리턴
        /// </summary>
        public static double Ex_Digitalize(in this double value)
        {
            return value == 0.0 ? 0.0 : 1.0;
        }


        /// <summary>
        /// -1, 0, 1 값으로 디지털화하여 리턴
        /// </summary>
        public static int Ex_SignedDigitalize(in this int value)
        {
            if (value < 0) return -1;
            if (value > 0) return 1;
            return 0;
        }

        /// <summary>
        /// -1, 0, 1 값으로 디지털화하여 리턴
        /// </summary>
        public static float Ex_SignedDigitalize(in this float value)
        {
            if (value < 0f) return -1f;
            if (value > 0f) return 1f;
            return 0;
        }

        /// <summary>
        /// -1, 0, 1 값으로 디지털화하여 리턴
        /// </summary>
        public static double Ex_SignedDigitalize(in this double value)
        {
            if (value < 0.0) return -1.0;
            if (value > 0.0) return 1.0;
            return 0.0;
        }

        #endregion // ==========================================================

        #region Range

        /// <summary>
        /// 변수의 값이 닫힌 범위 내에 있는지 검사
        /// </summary>
        public static bool Ex_Range(this in int value, in int min, in int max)
        {
            return min <= value && value <= max;
        }

        /// <summary>
        /// 변수의 값이 닫힌 범위 내에 있는지 검사
        /// </summary>
        public static bool Ex_Range(this in float value, in float min, in float max)
        {
            return min <= value && value <= max;
        }

        /// <summary>
        /// 변수의 값이 닫힌 범위 내에 있는지 검사
        /// </summary>
        public static bool Ex_Range(this in double value, in double min, in double max)
        {
            return min <= value && value <= max;
        }

        #endregion // ==========================================================
    }
}
