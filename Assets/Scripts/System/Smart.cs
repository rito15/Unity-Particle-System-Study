using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 기존에 사용하던 기능들을 더 영리하고 편리하게 쓸 수 있게 해주는 클래스
/// </summary>
public class Smart : MonoBehaviour
{
    /// <summary>
    /// 오브젝트 내에서 T 타입의 컴포넌트를 찾고, 없으면 생성하여 리턴
    /// <para/> T 타입은 반드시 유니티 컴포넌트여야 함
    /// <para/>.
    /// <para/>[사용 예시]
    /// <para/>cEvent = Smart.GetComponent&lt;CharacterEvent&gt;(gameObject);
    /// </summary>
    public static T GetComponent<T>(GameObject myObject) where T : UnityEngine.Component
    {
        var component = myObject.GetComponent<T>();

        //return component ?? myObject.AddComponent<T>(); // 이거 안됨
        if (component == null)
            return myObject.AddComponent<T>();

        return component;
    }

    /// <summary>
    /// 게임오브젝트의 부모 오브젝트들에서 특정 컴포넌트를 찾아 리턴
    /// <para/>찾지 못할 경우, 바로 윗 부모에 컴포넌트를 추가하여 그 컴포넌트를 리턴
    /// <para/>자기 자신이 Root Object일 경우 null 리턴
    /// </summary>
    public static T GetComponentInParent<T>(in GameObject myObject)
        where T : UnityEngine.Component
    {
        var parentComponent = myObject.GetComponentInParent<T>();

        // [1] 부모 오브젝트들에서 해당 컴포넌트를 바로 찾은 경우
        if (parentComponent)
            return parentComponent;

        // [2] 못찾은 경우

        // 부모 확인
        Transform parentTransform = myObject.transform.parent;

        // [2-1] 부모가 없는 경우 : null 리턴
        if (parentTransform == null)
            return null;

        // [2-2] 부모가 있는 경우 : 컴포넌트 추가하고 리턴
        return parentTransform.gameObject.AddComponent<T>();
    }

    /// <summary>
    /// 게임오브젝트의 부모 오브젝트들에서 특정 컴포넌트를 찾아 리턴
    /// <para/>찾지 못할 경우, 부모들 중 이름에 ancestorName를 포함하는 오브젝트를 찾아
    /// <para/>컴포넌트를 추가하고 리턴
    /// </summary>
    public static T GetComponentInParent<T>(in GameObject myObject, string ancestorName)
         where T : UnityEngine.Component
    {
        var parentComponent = myObject.GetComponentInParent<T>();

        // [1] 부모 오브젝트들에서 해당 컴포넌트를 바로 찾은 경우
        if (parentComponent)
            return parentComponent;

        // [2] 못찾은 경우

        Transform ancestorTransform = myObject.transform;

        // 이름에 ancestorName를 포함하는 조상 오브젝트 확인
        while (ancestorTransform != null)
        {
            if (ancestorTransform.name.Contains(ancestorName))
                break;

            ancestorTransform = ancestorTransform.parent;
        }

        // [2-1] 이름이 일치하는 조상이 없는 경우 : null 리턴
        if (ancestorTransform == null)
            return null;

        // [2-2] 이름이 일치하는 조상 오브젝트를 찾은 경우 : 컴포넌트 추가하고 리턴
        return ancestorTransform.gameObject.AddComponent<T>();
    }

    /// <summary>
    /// 이름이 objName인 자식 오브젝트 검색하여 리턴
    /// <para/>만약 찾지 못할 경우, 해당 이름으로 자식 오브젝트 생성하여 리턴
    /// <para/>* GameObject 버전
    /// </summary>
    public static GameObject FindChild(in GameObject myObject, string objName)
    {
        // 자식 오브젝트 검색
        var targetTransform = myObject.transform.Find(objName);

        // 바로 찾은 경우
        if (targetTransform != null)
            return targetTransform.gameObject;

        // 찾지 못한 경우 - 빈 오브젝트 생성하여 자식에 추가하고 리턴
        var targetObject = new GameObject(objName);
        targetObject.transform.SetParent(myObject.transform);

        return targetObject;
    }

    /// <summary>
    /// 이름이 objName인 자식 오브젝트 검색하여 리턴
    /// <para/>만약 찾지 못할 경우, 해당 이름으로 자식 오브젝트 생성하여 리턴
    /// <para/>* Transform 버전
    /// </summary>
    public static Transform FindChild(in Transform myTransform, string objName)
    {
        // 자식 오브젝트(트랜스폼) 검색
        var targetTransform = myTransform.Find(objName);

        // 바로 찾은 경우
        if (targetTransform != null)
            return targetTransform;

        // 찾지 못한 경우 - 빈 오브젝트 생성하여 자식에 추가하고 리턴
        targetTransform = new GameObject(objName).transform;
        targetTransform.SetParent(myTransform);

        return targetTransform;
    }

    /// <summary>
    /// 이름에 substring을 포함하는 자식 오브젝트 검색하여 리턴
    /// <para/>* exception : 해당 단어들은 포함하지 않아야 함
    /// <para/>* GameObject 버전
    /// </summary>
    public static GameObject FindChildSubstring(in GameObject myObject, string substring, params string[] exception)
    {
        for (int i = 0; i < myObject.transform.childCount; i++)
        {
            var child = myObject.transform.GetChild(i);
            bool excepted = false;

            // 예외 단어 검사
            for (int j = 0; j < exception.Length; j++)
            {
                if (child.gameObject.name.Contains(exception[j]))
                {
                    excepted = true;
                    break;
                }
            }

            if (excepted == false && child.gameObject.name.Contains(substring))
                return child.gameObject;
        }

        return null;
    }

    /// <summary>
    /// 이름에 substring을 포함하는 자식 오브젝트 검색하여 리턴
    /// <para/>* Transform 버전
    /// </summary>
    public static Transform FindChildSubstring(in Transform myTransform, string substring)
    {
        for (int i = 0; i < myTransform.childCount; i++)
        {
            var child = myTransform.GetChild(i);
            if (child.gameObject.name.Contains(substring))
                return child;
        }

        return null;
    }

    /// <summary>
    /// GetComponentInChildren이 먹통인 경우 사용 : 자식을 모두 순회하며 직접 찾아옴
    /// </summary>
    public static T GetComponentInChildrenByForce<T>(in Transform myTransform) where T : Component
    {
        T target = null;

        // 자식들 순회하며 컴포넌트 찾아오기
        for (int i = 0; i < myTransform.childCount; i++)
        {
            target = myTransform.GetChild(i).GetComponent<T>();

            if (target != null)
                return target;

            //Debugger.Log($"자식 번호 : {i}, 자식 이름 : {myTransform.GetChild(i).name}");
        }

        return null;
    }
}
