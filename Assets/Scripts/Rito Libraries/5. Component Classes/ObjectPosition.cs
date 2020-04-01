#define USE_AWAKE
#define USE_START
#define USE_UPDATE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito
{
    /// <summary>
    /// <para/> 생성일 : 2019-11-19 PM 11:02:52
    /// <para/> 오브젝트의 특정 위치(상단,중심,발끝)를 구하는 컴포넌트
    /// </summary>
    public class ObjectPosition : MonoBehaviour
    {
        #region Fields



        #endregion //--------------------------------------------------------------

        #region Instance Fields



        #endregion //--------------------------------------------------------------

        #region Inspector Option Fields

        [Header("(0,0,0) 좌표에 놓았을 때 발 끝 중심의 좌표")]
        public Vector3 relativeBottomPosition = new Vector3(0, 0, 0);

        [Header("키 = 기본 스케일(1,1,1) 기준 상단~하단 높이")]
        public float heightPerScale1 = 1f;

        #endregion //--------------------------------------------------------------

        #region Inspector Read-Only Fields



        #endregion //--------------------------------------------------------------


        #region Unity Events

#if USE_AWAKE

        private void Awake()
        {

        }

#endif

#if USE_START

        private void Start()
        {

        }

#endif

#if USE_UPDATE

        private void Update()
        {

        }

#endif

        #endregion //--------------------------------------------------------------

        #region Awake Methods



        #endregion //--------------------------------------------------------------

        #region Start Methods



        #endregion //--------------------------------------------------------------

        #region Update Methods



        #endregion //--------------------------------------------------------------


        #region Checker Methods

        // 아파요
        private bool IAmSick()
        {
            if (enabled == false ||
                gameObject.activeSelf == false)
            {
                Debug.Log(gameObject.name + "오브젝트의 상태가 정상이 아니므로 위치를 구할 수 없습니다.");
                return true;
            }

            return false;
        }

        #endregion //--------------------------------------------------------------

        #region Private Methods



        #endregion //--------------------------------------------------------------

        #region Public Methods

        // 오브젝트 상단 좌표
        public Vector3 GetTopPosition()
        {
            if (IAmSick()) return transform.position;

            return GetBottomPosition() + new Vector3(0f, heightPerScale1 * transform.lossyScale.y, 0f);
        }

        // 오브젝트 중심 좌표
        public Vector3 GetCenterPosition()
        {
            if (IAmSick()) return transform.position;

            return GetBottomPosition() + new Vector3(0f, heightPerScale1 / 2f * transform.lossyScale.y, 0f);
        }

        // 오브젝트 하단 좌표
        public Vector3 GetBottomPosition()
        {
            if (IAmSick()) return transform.position;

            return transform.position + relativeBottomPosition;
        }

        #endregion //--------------------------------------------------------------


        #region CoRoutines



        #endregion //--------------------------------------------------------------
    }
}