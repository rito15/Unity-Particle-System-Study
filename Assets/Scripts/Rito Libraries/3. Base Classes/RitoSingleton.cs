using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito
{
    /// <summary>
    /// 간편 싱글톤 상속용 부모 클래스 <para/>
    /// 사용법 : public class 싱글톤클래스명 : RitoSingleton&lt;싱글톤클래스명&gt; <para/>
    /// <typeparam name="T">T : MonoBehaviour 자식</typeparam> <para/>
    /// ==================================================<para/>
    /// [필수]
    /// <para/> 자식 클래스 Awake를 다음처럼 변경 : 
    /// <para/>protected override void Awake()
    /// <para/>{
    /// <para/>    base.Awake();
    /// <para/>}
    /// </summary>
    public class RitoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {

        #region Test Cases(2020.01.04) - 테스트 완료

        /*
            [Test Cases - Component]

            1. 게임 오브젝트에 AddComponent<싱글톤>
             1) 다른 오브젝트에 컴포넌트가 이미 존재하는 상태에서
              => [정상 동작] : 다른 오브젝트에 기존재하는 싱글톤 컴포넌트는 정상 동작,
                 Instance 정상 할당, 새롭게 AddComponent되는 컴포넌트는 즉시 파괴

             2) 내 오브젝트에 컴포넌트가 이미 존재하는 상태에서
              => [정상 동작] : 내 오브젝트에 기존재하는 싱글톤 컴포넌트는 정상 동작,
                 Instance 정상 할당, 새롭게 AddComponent되는 컴포넌트는 즉시 파괴


            [Test Cases - Instance]

            1. 씬에서 게임오브젝트에 컴포넌트로 넣은 뒤 Instance 참조
             => [정상 동작] : 게임오브젝트 내 컴포넌트로 존재, Instance 정상 할당

            2. 게임 내에서 동적으로 AddComponent한 뒤 Instance 참조
             => [정상 동작] : 게임오브젝트 내 컴포넌트로 추가, Instance 정상 할당

            3. 게임 내에서 동적으로 Instance 참조
             => [정상 동작] : Singleton Object 아래에 생성, Instance 정상 할당


            [Test Cases - ContainerObject]

            1. 씬에서 게임오브젝트에 컴포넌트로 넣은 뒤 ContainerObject 참조
             => [정상 동작] : 게임오브젝트 내 컴포넌트로 존재, ContainerObject 정상 할당

            2. 게임 내에서 동적으로 AddComponent한 뒤 ContainerObject 참조
             => [정상 동작] : 게임오브젝트 내 컴포넌트로 추가, ContainerObject 정상 할당

            3. 게임 내에서 동적으로 ContainerObject 참조
             => [정상 동작] : Singleton Object 아래에 생성, ContainerObject 정상 할당
         */

        #endregion // ==========================================================


        #region Public Static Properties

        /// <summary> 싱글톤 인스턴스 Getter </summary>
        public static T Instance
        {
            get
            {
                if (instance == null)    // 체크 1 : 인스턴스가 없는 경우
                    CheckExsistence();

                return instance;
            }
        }

        /// <summary> 싱글톤 인스턴스의 또다른 이름 </summary>
        public static T Sin { get => Instance; }
        /// <summary> 싱글톤 인스턴스의 또다른 이름 </summary>
        public static T Ins { get => Instance; }
        /// <summary> 싱글톤 인스턴스의 또다른 이름 </summary>
        public static T I { get => Instance; }

        /// <summary>
        /// 싱글톤 게임오브젝트의 참조
        /// </summary>
        public static GameObject ContainerObject
        {
            get
            {
                if (containerObject == null)
                    CreateContainerObject();

                return containerObject;
            }
        }

        #endregion // ==========================================================

        #region Private Static Variables

        /// <summary> 싱글톤 인스턴스 </summary>
        private static T instance;
        private static GameObject containerObject;

        #endregion // ==========================================================

        #region Public Static Methods

        /// <summary>
        /// 싱글톤을 그저 생성하기 위한 정적 메소드
        /// </summary>
        public static void Call_()
        {
            if (instance == null)
                CheckExsistence();
        }

        #endregion // ==========================================================

        #region Public Methods

        /// <summary>
        /// 싱글톤을 그저 호출, 생성하기 위한 메소드
        /// </summary>
        public void Call() { }

        #endregion // ==========================================================

        #region Private Static Methods

        // (정적) 싱글톤 인스턴스 존재 여부 확인 (체크 2)
        private static void CheckExsistence()
        {
            // 싱글톤 검색
            instance = FindObjectOfType<T>();

            // 인스턴스 가진 오브젝트가 존재하지 않을 경우, 빈 오브젝트를 임의로 생성하여 인스턴스 할당
            if (instance == null)
            {
                // 게임 오브젝트에 클래스 컴포넌트 추가 후 인스턴스 할당
                instance = ContainerObject.GetComponent<T>();
            }
        }

        // (정적) 싱글톤 컴포넌트를 담을 게임 오브젝트 생성
        private static void CreateContainerObject()
        {
            // null이 아니면 Do Nothing
            if (containerObject != null) return;


            // 부모 오브젝트 "Singleton Objects" 찾기 or 생성
            GameObject parentContainer = GameObject.Find("Singleton Objects");

            if (parentContainer == null)
                parentContainer = new GameObject("Singleton Objects");

            // 빈 게임 오브젝트 생성
            containerObject = new GameObject($"[Singleton] {typeof(T)} Container");

            // 부모 오브젝트에 넣어주기
            containerObject.transform.parent = parentContainer.transform;

            // 인스턴스가 없던 경우, 새로 생성
            if (instance == null)
                instance = ContainerObject.AddComponent<T>();
        }

        #endregion // ==========================================================

        #region Private Methods

        // (동적) 싱글톤 스크립트를 미리 오브젝트에 담아 사용하는 경우를 위한 로직
        private void CheckInstance()
        {
            // 싱글톤 인스턴스가 미리 존재하지 않았을 경우, 본인으로 초기화
            if (instance == null)
            {
                Debug.Log($"싱글톤 생성 : {typeof(T).ToString()}" +
                    $" | 게임 오브젝트 : {name}");

                // 싱글톤 컴포넌트 초기화
                instance = this as T;

                // 싱글톤 컴포넌트를 담고 있는 게임오브젝트로 초기화
                containerObject = gameObject;
            }

            // 싱글톤 인스턴스가 존재하는데, 본인이 아닐 경우, 스스로(컴포넌트)를 파괴
            if (instance != null && instance != this)
            {
                Debug.Log($"이미 {typeof(T).ToString()} 싱글톤이 존재하므로 오브젝트를 파괴합니다.");
                Destroy(this);
            }
        }

        #endregion // ==========================================================


        /// <summary>
        /// 자식 클래스에서 다음처럼 사용 :
        /// <para/>protected override void Awake()
        /// <para/>{
        /// <para/>    base.Awake();
        /// <para/>}
        /// </summary>
        protected virtual void Awake()
        {
            CheckInstance();
        }
    }
}