using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito
{
    /// <summary>
    /// 직접 설정 : 게임 내 유저 입력 기능
    /// </summary>
    public enum UserAction
    {
        // 키보드 - 싱글 키
        /// <summary> 공격 키 : 기본 [A]</summary>
        Attack,
        /// <summary> 정지 키 : 기본 [S]</summary>
        Stop,
        /// <summary> 홀드 키 : 기본 [H]</summary>
        Hold,
        /// <summary> 정찰 키 : 기본 [P]</summary>
        Patrol,
        /// <summary> 채집 키 : 기본 [G]</summary>
        Gather,

        /// <summary> 부대 선택 0번 : 기본 [0]</summary>
        SelectTroop0,
        /// <summary> 부대 선택 1번 : 기본 [1]</summary>
        SelectTroop1,
        /// <summary> 부대 선택 2번 : 기본 [2]</summary>
        SelectTroop2,
        /// <summary> 부대 선택 3번 : 기본 [3]</summary>
        SelectTroop3,
        /// <summary> 부대 선택 4번 : 기본 [4]</summary>
        SelectTroop4,
        /// <summary> 부대 선택 5번 : 기본 [5]</summary>
        SelectTroop5,
        /// <summary> 부대 선택 6번 : 기본 [6]</summary>
        SelectTroop6,
        /// <summary> 부대 선택 7번 : 기본 [7]</summary>
        SelectTroop7,
        /// <summary> 부대 선택 8번 : 기본 [8]</summary>
        SelectTroop8,
        /// <summary> 부대 선택 9번 : 기본 [9]</summary>
        SelectTroop9,

        // 키보드 - 듀얼 키
        /// <summary> 부대 지정 0번 : 기본 [LCtrl + 0]</summary>
        AssignTroop0,
        /// <summary> 부대 지정 1번 : 기본 [LCtrl + 1]</summary>
        AssignTroop1,
        /// <summary> 부대 지정 2번 : 기본 [LCtrl + 2]</summary>
        AssignTroop2,
        /// <summary> 부대 지정 3번 : 기본 [LCtrl + 3]</summary>
        AssignTroop3,
        /// <summary> 부대 지정 4번 : 기본 [LCtrl + 4]</summary>
        AssignTroop4,
        /// <summary> 부대 지정 5번 : 기본 [LCtrl + 5]</summary>
        AssignTroop5,
        /// <summary> 부대 지정 6번 : 기본 [LCtrl + 6]</summary>
        AssignTroop6,
        /// <summary> 부대 지정 7번 : 기본 [LCtrl + 7]</summary>
        AssignTroop7,
        /// <summary> 부대 지정 8번 : 기본 [LCtrl + 8]</summary>
        AssignTroop8,
        /// <summary> 부대 지정 9번 : 기본 [LCtrl + 9]</summary>
        AssignTroop9,

        /// <summary> 부대 지정 추가 0번 : 기본 [LShift + 0]</summary>
        AddTroop0,
        /// <summary> 부대 지정 추가 1번 : 기본 [LShift + 1]</summary>
        AddTroop1,
        /// <summary> 부대 지정 추가 2번 : 기본 [LShift + 2]</summary>
        AddTroop2,
        /// <summary> 부대 지정 추가 3번 : 기본 [LShift + 3]</summary>
        AddTroop3,
        /// <summary> 부대 지정 추가 4번 : 기본 [LShift + 4]</summary>
        AddTroop4,
        /// <summary> 부대 지정 추가 5번 : 기본 [LShift + 5]</summary>
        AddTroop5,
        /// <summary> 부대 지정 추가 6번 : 기본 [LShift + 6]</summary>
        AddTroop6,
        /// <summary> 부대 지정 추가 7번 : 기본 [LShift + 7]</summary>
        AddTroop7,
        /// <summary> 부대 지정 추가 8번 : 기본 [LShift + 8]</summary>
        AddTroop8,
        /// <summary> 부대 지정 추가 9번 : 기본 [LShift + 9]</summary>
        AddTroop9,

        // 마우스 - 싱글 키
        /// <summary> 유닛 선택 키 : 기본 [마우스 좌클릭]</summary>
        Choose,
        /// <summary> 유닛 이동 키 : 기본 [마우스 우클릭]</summary>
        Move,

        // 마우스 - 듀얼 키
        /// <summary> 유닛 추가 선택 키 1 : 기본 [LCtrl + 마우스 좌클릭]</summary>
        ChoosePlus1,
        /// <summary> 유닛 추가 선택 키 2 : 기본 [LShift + 마우스 좌클릭]</summary>
        ChoosePlus2,
        /// <summary> 이동 + 적 조우 시 공격 키 : 기본 [LCtrl + 마우스 우클릭]</summary>
        MoveAndAttack,
    }

    /// <summary>
    /// <para/> 생성일 : 2020.01.01
    /// <para/> 게임 내 사용자 키보드, 마우스 입력 커스터마이징
    /// <para/> ----------------------------
    /// <para/> [1] 키 입력 확인(Input.GetKey류 대신 사용) : 
    /// <para/> if(UserInput.Get(UserAction, InputType)) { ... }
    /// <para/> 또는
    /// <para/> (유지) : UserInput.Press(UserAction, 콜백);
    /// <para/> (누름) : UserInput.Down(UserAction, 콜백);
    /// <para/> (뗌) : UserInput.Up(UserAction, 콜백);
    /// <para/> .
    /// <para/> ----------------------------
    /// <para/> [2] 기능 - 버튼 등록 : 
    /// <para/> (싱글) UserInput.Set(UserAction, KeyCode 또는 MouseButton)
    /// <para/> (듀얼) UserInput.Set(UserAction, Keycode, KeyCode 또는 MouseButton)
    /// <para/> .
    /// <para/> ----------------------------
    /// <para/> [3] 기능에서 버튼 해제 :
    /// <para/> UserInput.Clear(UserAction)
    /// </summary>
    public sealed class UserInput : RitoSingleton<UserInput>
    {
        #region Singleton 

        public static UserInput Key { get => Instance; }

        protected override void Awake()
        {
            base.Awake();
        }

        #endregion

        #region User key Settings (Dictionary)
        // 게임 내 키들을 직접 세팅

        /// <summary>
        /// 키보드 입력 세팅 딕셔너리 1 : 싱글 키
        /// </summary>
        public static Dictionary<UserAction, KeyCode> dict_SingleKeyboardSettings { get; private set; }
            = new Dictionary<UserAction, KeyCode>
            {
                { UserAction.Attack,  KeyCode.A },
                { UserAction.Stop,    KeyCode.S },
                { UserAction.Hold,    KeyCode.H },
                { UserAction.Patrol,  KeyCode.P },
                { UserAction.Gather,  KeyCode.G },

                { UserAction.SelectTroop0,  KeyCode.Alpha0 },
                { UserAction.SelectTroop1,  KeyCode.Alpha1 },
                { UserAction.SelectTroop2,  KeyCode.Alpha2 },
                { UserAction.SelectTroop3,  KeyCode.Alpha3 },
                { UserAction.SelectTroop4,  KeyCode.Alpha4 },
                { UserAction.SelectTroop5,  KeyCode.Alpha5 },
                { UserAction.SelectTroop6,  KeyCode.Alpha6 },
                { UserAction.SelectTroop7,  KeyCode.Alpha7 },
                { UserAction.SelectTroop8,  KeyCode.Alpha8 },
                { UserAction.SelectTroop9,  KeyCode.Alpha9 },
            };

        /// <summary>
        /// 키보드 입력 세팅 딕셔너리 2 : 보조 키 + 주요 키
        /// <para/>(보조 키를 누른 채로 주요 키 입력)
        /// </summary>
        public static Dictionary<UserAction, (KeyCode subKey, KeyCode priKey)> dict_DualKeyboardSettings { get; private set; }
            = new Dictionary<UserAction, (KeyCode subKey, KeyCode priKey)>
            {
                { UserAction.AssignTroop0,  (KeyCode.LeftControl, KeyCode.Alpha0) },
                { UserAction.AssignTroop1,  (KeyCode.LeftControl, KeyCode.Alpha1) },
                { UserAction.AssignTroop2,  (KeyCode.LeftControl, KeyCode.Alpha2) },
                { UserAction.AssignTroop3,  (KeyCode.LeftControl, KeyCode.Alpha3) },
                { UserAction.AssignTroop4,  (KeyCode.LeftControl, KeyCode.Alpha4) },
                { UserAction.AssignTroop5,  (KeyCode.LeftControl, KeyCode.Alpha5) },
                { UserAction.AssignTroop6,  (KeyCode.LeftControl, KeyCode.Alpha6) },
                { UserAction.AssignTroop7,  (KeyCode.LeftControl, KeyCode.Alpha7) },
                { UserAction.AssignTroop8,  (KeyCode.LeftControl, KeyCode.Alpha8) },
                { UserAction.AssignTroop9,  (KeyCode.LeftControl, KeyCode.Alpha9) },

                { UserAction.AddTroop0,     (KeyCode.LeftShift, KeyCode.Alpha0) },
                { UserAction.AddTroop1,     (KeyCode.LeftShift, KeyCode.Alpha1) },
                { UserAction.AddTroop2,     (KeyCode.LeftShift, KeyCode.Alpha2) },
                { UserAction.AddTroop3,     (KeyCode.LeftShift, KeyCode.Alpha3) },
                { UserAction.AddTroop4,     (KeyCode.LeftShift, KeyCode.Alpha4) },
                { UserAction.AddTroop5,     (KeyCode.LeftShift, KeyCode.Alpha5) },
                { UserAction.AddTroop6,     (KeyCode.LeftShift, KeyCode.Alpha6) },
                { UserAction.AddTroop7,     (KeyCode.LeftShift, KeyCode.Alpha7) },
                { UserAction.AddTroop8,     (KeyCode.LeftShift, KeyCode.Alpha8) },
                { UserAction.AddTroop9,     (KeyCode.LeftShift, KeyCode.Alpha9) },
            };

        /// <summary>
        /// 마우스 입력 세팅 딕셔너리 1 : 싱글 키
        /// </summary>
        public static Dictionary<UserAction, MouseButton> dict_SingleMouseSettings { get; private set; }
            = new Dictionary<UserAction, MouseButton>
            {
                { UserAction.Choose,  MouseButton.Left },
                { UserAction.Move,    MouseButton.Right },
            };

        /// <summary>
        /// 마우스 입력 세팅 딕셔너리 2 : 보조 키(키보드) + 주요 키(마우스)
        /// <para/>(보조 키를 누른 채로 주요 키 입력)
        /// </summary>
        public static Dictionary<UserAction, (KeyCode subKey, MouseButton priKey)> dict_DualMouseSettings { get; private set; }
            = new Dictionary<UserAction, (KeyCode subKey, MouseButton priKey)>
            {
                { UserAction.ChoosePlus1,       (KeyCode.LeftControl, MouseButton.Left) },
                { UserAction.ChoosePlus2,       (KeyCode.LeftShift,   MouseButton.Left) },
                { UserAction.MoveAndAttack,     (KeyCode.LeftShift,   MouseButton.Right) },
            };

        #endregion //--------------------------------------------------------------

        #region public Static Fuctions - Input

        /// <summary>
        /// 유저 입력 검사(Input.GetKey~ 대신 사용)
        /// </summary>
        public static bool Get(UserAction userAction, InputType input)
        {
            return Instance[userAction, input];
        }

        /// <summary>
        /// 유저 입력 검사(Input.GetKey~ 대신 사용)
        /// <para/> -> 키 지속 누름 이벤트 검사
        /// </summary>
        public static bool Press(UserAction userAction)
        {
            return Instance[userAction, InputType.Press];
        }

        /// <summary>
        /// 유저 입력 검사(Input.GetKey~ 대신 사용)
        /// <para/> -> 키 지속 누름 이벤트 검사
        /// </summary>
        public static bool Continue(UserAction userAction)
        {
            return Instance[userAction, InputType.Press];
        }

        /// <summary>
        /// 유저 입력 검사(Input.GetKey~ 대신 사용)
        /// <para/> -> 키 한번 누름 이벤트 검사
        /// </summary>
        public static bool Down(UserAction userAction)
        {
            return Instance[userAction, InputType.Down];
        }

        /// <summary>
        /// 유저 입력 검사(Input.GetKey~ 대신 사용)
        /// <para/> -> 키 뗌 이벤트 검사
        /// </summary>
        public static bool Up(UserAction userAction)
        {
            return Instance[userAction, InputType.Up];
        }

        #endregion //--------------------------------------------------------------

        #region public Static Fuctions

        /// <summary>
        /// [기능 - 키보드 버튼] 등록(싱글 키),
        /// <para/> 기존에 이미 해당 기능에 버튼이 등록되어 있었으면 기존 기능 해제
        /// </summary>
        public static void Set(UserAction userAction, KeyCode key)
        {
            // 중복 키 설정 방지 :
            // 기존에 이미 해당 기능에 버튼이 등록되어 있었으면 해제
            Clear(userAction);

            dict_SingleKeyboardSettings[userAction] = key;
        }

        /// <summary>
        /// [기능 - 키보드 버튼] 등록(듀얼 키 = 보조 키 + 주요 키),
        /// <para/> 기존에 이미 해당 기능에 버튼이 등록되어 있었으면 기존 기능 해제
        /// </summary>
        public static void Set(UserAction userAction, KeyCode subKey, KeyCode priKey)
        {
            // 중복 키 설정 방지
            Clear(userAction);

            dict_DualKeyboardSettings[userAction] = (subKey, priKey);
        }

        /// <summary>
        /// [기능 - 마우스 버튼] 등록,
        /// <para/> 기존에 이미 해당 기능에 버튼이 등록되어 있었으면 기존 기능 해제
        /// </summary>
        public static void Set(UserAction userAction, MouseButton mouseButton)
        {
            // 중복 키 설정 방지
            Clear(userAction);

            dict_SingleMouseSettings[userAction] = mouseButton;
        }

        /// <summary>
        /// [기능 - 마우스 버튼] 등록(듀얼 키 = 보조 키(키보드) + 주요 키(마우스)),
        /// <para/> 기존에 이미 해당 기능에 버튼이 등록되어 있었으면 기존 기능 해제
        /// </summary>
        public static void Set(UserAction userAction, KeyCode subKey, MouseButton mouseButton)
        {
            // 중복 키 설정 방지
            Clear(userAction);

            dict_DualMouseSettings[userAction] = (subKey, mouseButton);
        }

        /// <summary>
        /// 해당 기능에 설정된 마우스/키보드 버튼 해제
        /// </summary>
        public static void Clear(UserAction userAction)
        {
            if (dict_SingleKeyboardSettings.ContainsKey(userAction))
                dict_SingleKeyboardSettings[userAction] = KeyCode.None;

            if (dict_DualKeyboardSettings.ContainsKey(userAction))
                dict_DualKeyboardSettings[userAction] = (KeyCode.None, KeyCode.None);

            if (dict_SingleMouseSettings.ContainsKey(userAction))
                dict_SingleMouseSettings[userAction] = MouseButton.None;

            if (dict_DualMouseSettings.ContainsKey(userAction))
                dict_DualMouseSettings[userAction] = (KeyCode.None, MouseButton.None);
        }

        /// <summary>
        /// 해당 기능에 키가 등록되어 있는지 검사
        /// </summary>
        public static bool IsAssigned(UserAction userAction)
        {
            return
                dict_SingleKeyboardSettings.ContainsKey(userAction) ||
                dict_DualKeyboardSettings.ContainsKey(userAction) ||
                dict_SingleMouseSettings.ContainsKey(userAction) ||
                dict_DualMouseSettings.ContainsKey(userAction);
        }

        #endregion //--------------------------------------------------------------

        #region Indexer (Do Not Touch)

        // Indexer
        public bool this[UserAction key, InputType inputType]
        {
            get
            {
                // 키보드 입력 처리 1 : 싱글 키
                if (dict_SingleKeyboardSettings.ContainsKey(key))
                {
                    switch (inputType)
                    {
                        case InputType.Press:
                            return Input.GetKey(dict_SingleKeyboardSettings[key]);

                        case InputType.Down:
                            return Input.GetKeyDown(dict_SingleKeyboardSettings[key]);

                        case InputType.Up:
                            return Input.GetKeyUp(dict_SingleKeyboardSettings[key]);
                    }
                }
                // 키보드 입력 처리 2 : 듀얼 키
                if (dict_DualKeyboardSettings.ContainsKey(key))
                {
                    switch (inputType)
                    {
                        case InputType.Press:
                            return 
                                Input.GetKey(dict_DualKeyboardSettings[key].subKey) &&
                                Input.GetKey(dict_DualKeyboardSettings[key].priKey);

                        case InputType.Down:
                            return
                                Input.GetKey(dict_DualKeyboardSettings[key].subKey) &&
                                Input.GetKeyDown(dict_DualKeyboardSettings[key].priKey);

                        case InputType.Up:
                            return
                                Input.GetKey(dict_DualKeyboardSettings[key].subKey) &&
                                Input.GetKeyUp(dict_DualKeyboardSettings[key].priKey);
                    }
                }

                // 마우스 입력 처리
                else if (dict_SingleMouseSettings.ContainsKey(key))
                {
                    switch (inputType)
                    {
                        case InputType.Press:
                            return Input.GetMouseButton((int)dict_SingleMouseSettings[key]);

                        case InputType.Down:
                            return Input.GetMouseButtonDown((int)dict_SingleMouseSettings[key]);

                        case InputType.Up:
                            return Input.GetMouseButtonUp((int)dict_SingleMouseSettings[key]);
                    }
                }
                // 마우스 입력 처리 2 : 듀얼 키
                if (dict_DualMouseSettings.ContainsKey(key))
                {
                    switch (inputType)
                    {
                        case InputType.Press:
                            return
                                Input.GetKey(dict_DualMouseSettings[key].subKey) &&
                                Input.GetMouseButton((int)dict_DualMouseSettings[key].priKey);

                        case InputType.Down:
                            return
                                Input.GetKey(dict_DualMouseSettings[key].subKey) &&
                                Input.GetMouseButtonDown((int)dict_DualMouseSettings[key].priKey);

                        case InputType.Up:
                            return
                                Input.GetKey(dict_DualMouseSettings[key].subKey) &&
                                Input.GetMouseButtonUp((int)dict_DualMouseSettings[key].priKey);
                    }
                }

                // 잘못된 입력
                return false;
            }
        }

        #endregion //--------------------------------------------------------------

        #region KeyDown Callback Events

        /// <summary>
        /// 기능에 따른 키 누름 유지 이벤트를 콜백으로 사용
        /// </summary>
        public static void Press(UserAction key, System.Action action)
        {
            if (Get(key, InputType.Press))
                action();
        }

        /// <summary>
        /// 기능에 따른 키 누름 유지 이벤트를 콜백으로 사용
        /// </summary>
        public static void Continue(UserAction key, System.Action action)
        {
            if (Get(key, InputType.Press))
                action();
        }

        /// <summary>
        /// 기능에 따른 키 누름 이벤트를 콜백으로 사용
        /// </summary>
        public static void Down(UserAction key, System.Action action)
        {
            if (Get(key, InputType.Down))
                action();
        }

        /// <summary>
        /// 기능에 따른 키 뗌 이벤트를 콜백으로 사용
        /// </summary>
        public static void Up(UserAction key, System.Action action)
        {
            if (Get(key, InputType.Up))
                action();
        }



        /// <summary>
        /// 기본 Input 클래스와 함께 사용 : key 누를 경우 action 수행
        /// </summary>
        public static void KeyDown(KeyCode key, System.Action action)
        {
            if (Input.GetKeyDown(key))
                action();
        }

        /// <summary>
        /// 기본 Input 클래스와 함께 사용 : subKey 누른 채로 key 누를 경우 action 수행
        /// </summary>
        public static void KeyDown(KeyCode subKey, KeyCode key, System.Action action)
        {
            if (Input.GetKey(subKey) && Input.GetKeyDown(key))
                action();
        }

        /// <summary>
        /// 기본 Input 클래스와 함께 사용 : key 유지할 경우 action 수행
        /// </summary>
        public static void KeyPress(KeyCode key, System.Action action)
        {
            if (Input.GetKey(key))
                action();
        }

        /// <summary>
        /// 기본 Input 클래스와 함께 사용 : subKey 누른 채로 key 유지할 경우 action 수행
        /// </summary>
        public static void KeyPress(KeyCode subKey, KeyCode key, System.Action action)
        {
            if (Input.GetKey(subKey) && Input.GetKey(key))
                action();
        }

        /// <summary>
        /// 기본 Input 클래스와 함께 사용 : key 뗄 경우 action 수행
        /// </summary>
        public static void KeyUp(KeyCode key, System.Action action)
        {
            if (Input.GetKeyUp(key))
                action();
        }

        /// <summary>
        /// 기본 Input 클래스와 함께 사용 : subKey 누른 채로 key 뗄 경우 action 수행
        /// </summary>
        public static void KeyUp(KeyCode subKey, KeyCode key, System.Action action)
        {
            if (Input.GetKey(subKey) && Input.GetKeyUp(key))
                action();
        }

        #endregion //--------------------------------------------------------------

        #region Region



        #endregion //--------------------------------------------------------------

        #region Region



        #endregion //--------------------------------------------------------------

    }

    /// <summary>
    /// 입력 종류
    /// </summary>
    public enum InputType
    {
        /// <summary> 버튼 누름 유지 (Input.GetKey) </summary>
        Press,
        /// <summary> 버튼 누르는 순간 (Input.GetKeyDown) </summary>
        Down,
        /// <summary> 버튼 떼는 순간 (Input.GetKeyUp) </summary>
        Up,
    }

    /// <summary>
    /// 마우스 입력 종류
    /// </summary>
    public enum MouseButton
    {
        None  = -1,

        Left  = 0,
        Right = 1,
    }
}