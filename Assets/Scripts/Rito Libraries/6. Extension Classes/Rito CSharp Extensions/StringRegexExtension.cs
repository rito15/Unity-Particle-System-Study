using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;   // Regex

namespace Rito
{
    // 2020. 01. 26. 최초 작성
    // 2020. 01. 26. IsOnly~, CheckFormat, Equals, Contains, Replace 모두 테스트 완료
    // 2020. 02. 17. Ex_SubstringLeft(), Ex_SubstringRight() 작성, 테스트 완료
    // 2020. 02. 18. Ex_Repeat() 작성, 테스트 완료

    /// <summary>
    /// 2020. 01. 26.
    /// <para/> 스트링 확장 : string과 Regex 중 성능이 더 좋은 기능으로 제공
    /// <para/> -----------------------------------------------------------------------------------
    /// <para/> [목록]
    /// <para/> Ex_IsOnly~ : 알파벳, 소문자, 대문자, 정수, 실수 등 포맷 검사
    /// <para/> Ex_CheckFormat : 문자열에 정규표현식 패턴을 직접 검사
    /// <para/> Ex_Equals : Equals()와 같지만, 대소문자 구분 여부 선택 가능(기본: X)
    /// <para/> Ex_Contains : Contains()와 같지만, 대소문자 구분 여부 선택 가능(기본: X)
    /// <para/> Ex_Replace : 대상 문자열을 문자열 또는 문자로 변환, 대소문자 구분 여부 선택 가능(기본: X)
    /// <para/> Ex_SubstringLeft : 문자열의 시작부터 특정 문자열까지 서브스트링 리턴
    /// <para/> Ex_SubstringRight : 문자열의 특정 분자열부터 끝부분까지 서브스트링 리턴
    /// <para/> 
    /// <para/> 
    /// </summary>
    public static class StringRegexExtension
    {
        #region Regex Fields

        public static readonly Regex _onlyAlphabet      = new Regex(@"^[A-Za-z]+$");    // 알파벳만 포함
        public static readonly Regex _onlyUpperAlphabet = new Regex(@"^[A-Z]+$");       // 알파벳 대문자만 포함
        public static readonly Regex _onlyLowerAlphabet = new Regex(@"^[a-z]+$");       // 알파벳 소문자만 포함
        public static readonly Regex _onlyAlphaDigit    = new Regex(@"^[A-Za-z0-9]+$"); // 알파벳 + 정수만 포함
        public static readonly Regex _onlyDigit         = new Regex(@"^\d+$");          // 정수(10진수 문자)만 포함
        public static readonly Regex _onlyInteger       = new Regex(@"^(-|\+)?\d+$");  // 정수만 포함(양수, 음수)
        public static readonly Regex _onlyRealNumber    = new Regex(@"^(-|\+)?(\d)+\.\d+$");      // 실수만 포함(양수, 음수) -> 정수 허용 X
        public static readonly Regex _onlyNumber        = new Regex(@"^(-|\+)?(\d)+(\.\d)?\d*$"); // 정수, 실수만 포함(양수, 음수)

        public static readonly Regex _onlyPositiveNumber = new Regex(@"^\+?(\d)+(\.\d)?\d*$"); // 양수인 정수 또는 실수만 포함
        public static readonly Regex _onlyNegativeNumber = new Regex(@"^-(\d)+(\.\d)?\d*$");   // 음수인 정수 또는 실수만 포함

        #endregion // ==========================================================

        #region Format Checker

        /// <summary>
        /// 문자열이 알파벳(대문자, 소문자)만 포함하는지 검사
        /// </summary>
        public static bool Ex_IsOnlyAlphabet(this string value)
        {
            return _onlyAlphabet.IsMatch(value);
        }

        /// <summary>
        /// 문자열이 알파벳 대문자만 포함하는지 검사
        /// </summary>
        public static bool Ex_IsOnlyUpperAlphabet(this string value)
        {
            return _onlyUpperAlphabet.IsMatch(value);
        }

        /// <summary>
        /// 문자열이 알파벳 소문자만 포함하는지 검사
        /// </summary>
        public static bool Ex_IsOnlyLowerAlphabet(this string value)
        {
            return _onlyLowerAlphabet.IsMatch(value);
        }

        /// <summary>
        /// 문자열이 알파벳(대문자, 소문자), 정수만 포함하는지 검사
        /// </summary>
        public static bool Ex_IsOnlyAlphaDigit(this string value)
        {
            return _onlyAlphaDigit.IsMatch(value);
        }

        /// <summary>
        /// 문자열이 정수(0~9)만 포함하는지 검사
        /// </summary>
        public static bool Ex_IsOnlyDigit(this string value)
        {
            return _onlyDigit.IsMatch(value);
        }

        /// <summary>
        /// 문자열이 정수(양수, 음수)만 포함하는지 검사
        /// </summary>
        public static bool Ex_IsOnlyInteger(this string value)
        {
            return _onlyInteger.IsMatch(value);
        }

        /// <summary>
        /// 문자열이 실수(양수, 음수)만 포함하는지 검사 (정수 허용 X)
        /// </summary>
        public static bool Ex_IsOnlyRealNumber(this string value)
        {
            return _onlyRealNumber.IsMatch(value);
        }

        /// <summary>
        /// 문자열이 정수, 실수(양수, 음수)만 포함하는지 검사
        /// </summary>
        public static bool Ex_IsOnlyNumber(this string value)
        {
            return _onlyNumber.IsMatch(value);
        }

        /// <summary>
        /// 문자열이 양수인 정수, 실수만 포함하는지 검사
        /// </summary>
        public static bool Ex_IsOnlyPositiveNumber(this string value)
        {
            return _onlyPositiveNumber.IsMatch(value);
        }

        /// <summary>
        /// 문자열이 음수인 정수, 실수만 포함하는지 검사
        /// </summary>
        public static bool Ex_IsOnlyNegativeNumber(this string value)
        {
            return _onlyNegativeNumber.IsMatch(value);
        }

        #endregion // ==========================================================

        #region Manual Regex Checker

        /// <summary>
        /// 정규식 포맷을 직접 검사
        /// </summary>
        public static bool Ex_CheckFormat(this string value, in string formatString)
        {
            Regex regex = new Regex(formatString);
            return regex.IsMatch(value);
        }

        /// <summary>
        /// 정규식 포맷을 직접 검사
        /// </summary>
        public static bool Ex_CheckFormat(this string value, in Regex regex)
        {
            return regex.IsMatch(value);
        }

        #endregion // ==========================================================

        #region Equals, Contains

        /// <summary>
        /// 문자열이 타겟 문자열과 일치하는지 검사(기본 : 대소문자 구분X)
        /// <para/> ---------------------------------------------------
        /// <para/> [파라미터]
        /// <para/> target : 대상 문자열
        /// <para/> isCaseSensitive : 대소문자 구분 여부(기본 false)
        /// </summary>
        public static bool Ex_Equals(this string value, in string target, bool isCaseSensitive = false)
        {
            // 1. 정확한 문자열 검사 - string
            if (isCaseSensitive)
            {
                return value.Equals(target);
            }
            // 2. 대소문자 구분 없이 검사 - Regex
            else
            {
                return Regex.IsMatch(value, $@"^(?i){target}$");
            }
        }

        /// <summary>
        /// 문자열이 타겟 문자열을 포함하고 있는지 검사(기본 : 대소문자 구분X)
        /// <para/> ---------------------------------------------------
        /// <para/> [파라미터]
        /// <para/> target : 부분 문자열
        /// <para/> isCaseSensitive : 대소문자 구분 여부(기본 false)
        /// </summary>
        public static bool Ex_Contains(this string value, in string target, bool isCaseSensitive = false)
        {
            // 1. 정확한 문자열 찾기 - string
            if (isCaseSensitive)
            {
                return value.Contains(target);
            }
            // 2. 대소문자 구분 없이 패턴 검사 - Regex
            else
            {
                return Regex.IsMatch(value, $@"(?i){target}");
            }
        }

        /// <summary>
        /// 문자열이 타겟 문자열들을 모두 포함하고 있는지 검사(기본 : 대소문자 구분X)
        /// <para/> ---------------------------------------------------
        /// <para/> [파라미터]
        /// <para/> target : 부분 문자열 리스트
        /// <para/> isCaseSensitive : 대소문자 구분 여부(기본 false)
        /// <para/> ---------------------------------------------------
        /// <para/> * 패턴이 아닌 정확한 문자열 검사의 경우, Regex보다 string의 성능이 더 좋다.
        /// </summary>
        public static bool Ex_Contains(this string value, in string[] target, bool isCaseSensitive = false)
        {
            // 1. 정확한 문자열 검사 - string
            if (isCaseSensitive)
            {
                for (int i = 0; i < target.Length; i++)
                {
                    if (value.Contains(target[i]) == false)
                        return false;
                }
                return true;
            }
            // 2. 패턴 검사 - Regex
            else
            {
                for (int i = 0; i < target.Length; i++)
                {
                    if (Regex.IsMatch(value, $"(?i){target[i]}") == false)
                        return false;
                }
                return true;
            }
        }

        #endregion // ==========================================================

        #region Replace

        // * Replace는 string보다 Regex의 성능이 좋다

        /// <summary>
        /// 문자열 내의 부분 문자열을 다른 문자열로 변경하여 리턴
        /// <para/> ---------------------------------------------------
        /// <para/> [파라미터]
        /// <para/> source : 원본 문자열
        /// <para/> target : 변경 대상 문자열
        /// <para/> replacement : 대체 문자열
        /// <para/> isCaseSensitive : 대소문자 구분 여부(기본 false)
        /// <para/> ---------------------------------------------------
        /// <para/> [리턴]
        /// <para/> source에서 target->replacement로 교체한 결과 문자열
        /// </summary>
        public static string Ex_Replace(this string value, string target, in string replacement, bool isCaseSensitive = false)
        {
            if (isCaseSensitive == false)
                target = $"(?i){target}";

            return Regex.Replace(value, target, replacement);
        }

        /// <summary>
        /// 문자열 내의 부분 문자열을 다른 문자열로 변경하여 리턴
        /// <para/> ---------------------------------------------------
        /// <para/> [파라미터]
        /// <para/> source : 원본 문자열
        /// <para/> target : 변경 대상 문자열
        /// <para/> replacement : 대체 문자열
        /// <para/> isCaseSensitive : 대소문자 구분 여부(기본 false)
        /// <para/> ---------------------------------------------------
        /// <para/> [리턴]
        /// <para/> source에서 target->replacement로 교체한 결과 문자열
        /// </summary>
        public static string Ex_Replace(this string value, in string[] target, in string replacement, bool isCaseSensitive = false)
        {
            string targetString = string.Join("|", target);

            if (isCaseSensitive == false)
                targetString = $"(?i){targetString}";

            return Regex.Replace(value, targetString, replacement);
        }

        /// <summary>
        /// 문자열 내의 부분 문자열을 모두 특정 한가지 문자로 변경하여 리턴
        /// <para/> ---------------------------------------------------
        /// <para/> [파라미터]
        /// <para/> source : 원본 문자열
        /// <para/> target : 변경 대상 문자열
        /// <para/> replacement : 대체 문자
        /// <para/> isCaseSensitive : 대소문자 구분 여부(기본 false)
        /// <para/> ---------------------------------------------------
        /// <para/> [리턴]
        /// <para/> source에서 target 문자들을 replacement로 교체한 결과 문자열
        /// <para/> ---------------------------------------------------
        /// <para/> [예시]
        /// <para/> Replace("AbcABCdeAbCdEf", "cde", '*', false)
        /// <para/> "AbcABCdeAbCdEf" -> "AbcAB***Ab***f"
        /// </summary>
        public static string Ex_Replace(this string value, string target, in char replacement, bool isCaseSensitive = false)
        {
            string replaceString = "";

            for (int j = 0; j < target.Length; j++)
                replaceString += replacement;

            if (isCaseSensitive == false)
                target = $"(?i){target}";

            value = Regex.Replace(value, target, replaceString);
            return value;
        }

        /// <summary>
        /// 문자열 내의 부분 문자열을 모두 특정 한가지 문자로 변경하여 리턴
        /// <para/> ---------------------------------------------------
        /// <para/> [파라미터]
        /// <para/> source : 원본 문자열
        /// <para/> target : 변경 대상 문자열들
        /// <para/> replacement : 대체 문자
        /// <para/> isCaseSensitive : 대소문자 구분 여부(기본 false)
        /// <para/> ---------------------------------------------------
        /// <para/> [리턴]
        /// <para/> source에서 target 문자들을 replacement로 교체한 결과 문자열
        /// <para/> ---------------------------------------------------
        /// <para/> [예시]
        /// <para/> Replace("AbcABCdeAbCdEf", new string[]{"a", "cde"}, '*', false)
        /// <para/> "AbcABCdeAbCdEf" -> "*bc*B****b***f"
        /// </summary>
        public static string Ex_Replace(this string value, in string[] target, in char replacement, bool isCaseSensitive = false)
        {
            string replaceString;

            for (int i = 0; i < target.Length; i++)
            {
                replaceString = "";
                for (int j = 0; j < target[i].Length; j++)
                    replaceString += replacement;

                if (isCaseSensitive == false)
                    target[i] = $"(?i){target[i]}";

                value = Regex.Replace(value, target[i], replaceString);
            }
            return value;
        }

        #endregion // ==========================================================

        #region Substring

        /// <summary> 
        /// 문자열 내의 첫부분부터 특정 문자열까지의 서브스트링
        /// <para/> -----------------------------------------------------------------
        /// <para/> [파라미터]
        /// <para/> * subString : 서브스트링이 시작될 문자열
        /// <para/> * containSub : subString을 포함할지 여부
        /// <para/> -----------------------------------------------------------------
        /// <para/> * 예시 : "abcdAbcd".Ex_SubstringLeft("A") => "abcdA"
        /// <para/> * 예시 : "abcdAbcd".Ex_SubstringLeft("A", false) => "abcd"
        /// </summary>
        public static string Ex_SubstringLeft(this string target, in string subString, in bool containSub = true)
        {
            int firstIndex = target.IndexOf(subString);

            if (firstIndex > -1)
            {
                if (containSub)
                    return target.Substring(0, firstIndex + subString.Length);
                else
                    return target.Substring(0, firstIndex);
            }

            return target;
        }

        /// <summary> 
        /// 문자열 내의 특정한 문자열부터 끝부분까지의 서브스트링
        /// <para/> -----------------------------------------------------------------
        /// <para/> [파라미터]
        /// <para/> * subString : 서브스트링이 시작될 문자열
        /// <para/> * containLast : subString을 포함할지 여부
        /// <para/> -----------------------------------------------------------------
        /// <para/> * 예시 : "abcdAbcd".Ex_SubstringRight("A") => "Abcd"
        /// <para/> * 예시 : "abcdAbcd".Ex_SubstringRight("A", false) => "bcd"
        /// </summary>
        public static string Ex_SubstringRight(this string target, in string subString, in bool containSub = true)
        {
            int lastIndex = target.LastIndexOf(subString);

            if (lastIndex > -1)
            {
                if (containSub)
                    return target.Substring(lastIndex);
                else
                    return target.Substring(lastIndex + subString.Length);
            }

            return target;
        }

        #endregion // ==========================================================

        #region Cute Extensions (string)

        /// <summary>
        /// <para/> 해당 스트링을 n회 반복한 스트링 리턴
        /// <para/> * count : 반복 횟수 (1이면 원본과 동일)
        /// </summary>
        public static string Ex_Repeat(this string target, int count = 2)
        {
            if (count < 1) count = 1;
            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < count; i++)
                sb.Append(target);

            return sb.ToString();
        }

        #endregion // ==========================================================
    }
}
