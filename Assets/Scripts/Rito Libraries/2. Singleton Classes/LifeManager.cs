using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito
{
    // 2020. 01. 26. 최초 작성
    // 2020. 01. 26. Destroy_(), Destroy(, n), Save(), Save(, n), Load() 기능 테스트 완료
    // 2020. 01. 27. Save(Transform) 추가

    /// <summary>
    /// <para/> 2020. 01. 26
    /// <para/> [게임오브젝트 생성, 파괴 관리]
    /// <para/> ---------------------------------------------------------------------------
    /// <para/> * 싱글톤 
    /// <para/> * 
    /// <para/> * 1. 생성
    /// <para/> * * 1) 인스턴스화 또는 오브젝트 풀에서 로드 : Create(obj, pos, rot, parent)
    /// <para/> * * 2) 오브젝트 풀에서 로드(이름 참조) : Load(name, pos, rot, parent)
    /// <para/> * 
    /// <para/> * 2. 파괴
    /// <para/> * * 1) 오브젝트 풀에 저장 : Save(GameObject)
    /// <para/> * * 2) 점진적 파괴 : Destroy_(GameObject), Collect(GameObject)
    /// <para/> * 
    /// <para/> * 3. 수명 부여
    /// <para/> * * 1) n초 후 오브젝트 풀에 저장 : Save(GameObject, float n)
    /// <para/> * * 2) n초 후 점진적 파괴 : Destroy_(GameObject, float n), Collect(..)
    /// <para/> * 
    /// <para/> ---------------------------------------------------------------------------
    /// <para/> * [주의사항]
    /// <para/> * 이름으로 구분하여 풀링을 하기 때문에,
    /// <para/> * 게임오브젝트 또는 프리팹의 이름이 중복되면 안된다.
    /// <para/> 
    /// </summary>
    public class LifeManager : RitoSingleton<LifeManager>
    {
        /// <summary> 오브젝트 풀 크기(각 오브젝트마다 n개) </summary>
        public const int _PoolMaxSize = 20;


        #region Unity Events

        protected override void Awake()
        {
            base.Awake();

            Init_Creator();
            Init_Cleaner();
        }

        private void Start()
        {
            _cleanerRoutine = StartCoroutine(CleanerRoutine());
        }

        #endregion // =====================================================================================

        #region 1. Object Pool - Fields

        /// <summary> 오브젝트 풀 게임오브젝트 이름 </summary>
        private const string _PoolContainerName = "[Object Pool]";

        /// <summary> 오브젝트 풀 </summary>
        private Dictionary<string, Queue<GameObject>> _pool = new Dictionary<string, Queue<GameObject>>();

        /// <summary> 오브젝트 풀 컨테이너 오브젝트 </summary>
        private GameObject _poolContainerObj = null;

        #endregion // =====================================================================================

        #region 1. Object Pool - Methods

        /// <summary>
        /// <para/> [Private]
        /// <para/> Init
        /// </summary>
        private void Init_Creator()
        {
            _poolContainerObj = new GameObject(_PoolContainerName);
        }

        /// <summary>
        /// <para/> [게임 오브젝트 생성]
        /// <para/> * 인스턴스화 또는 오브젝트 풀에서 로드
        /// <para/> - target : 복제할 게임오브젝트 또는 프리팹
        /// <para/> - pos : 생성할 위치
        /// <para/> - rot : 생성 당시 회전(기본 = default)
        /// <para/> - parent : 부모로 설정할 오브젝트 (기본 = null)
        /// <para/> -------------------------------------------------------
        /// <para/> * 호출 시, 오브젝트 풀을 확인하여 해당 오브젝트가 존재할 경우 풀에서 로드하고,
        /// <para/> 오브젝트 풀 내에 존재하지 않을 경우 새롭게 생성하여 리턴한다.
        /// </summary>
        public GameObject Create(in GameObject target, in Vector3 pos, in Quaternion rot = default,
            in Transform parent = null)
        {
            if (target == null) return null;

            string targetName = target.name;

            // 1. 풀에 해당 오브젝트가 저장되어 있을 경우 : 풀에서 꺼내기
            if (_pool.ContainsKey(targetName))
            {
                if (_pool[targetName].Count > 0)
                {
                    GameObject poolTarget = _pool[targetName].Dequeue();
                    poolTarget.transform.SetParent(parent);
                    poolTarget.transform.position = pos;
                    poolTarget.transform.rotation = rot;
                    poolTarget.SetActive(true);

                    return poolTarget;
                }
            }

            // 2. 풀에 해당 오브젝트의 여분이 없는 경우 : 새로 생성
            var newTarget = GameObject.Instantiate(target, pos, rot, parent);
            newTarget.name = target.name;

            return newTarget;
        }

        /// <summary>
        /// <para/> [오브젝트 풀에서 로드]
        /// <para/> * 이름으로 참조
        /// <para/> - targetName : 오브젝트 풀에서 참조할 이름
        /// <para/> - pos : 생성할 위치
        /// <para/> - rot : 생성 당시 회전(기본 = default)
        /// <para/> - parent : 부모로 설정할 오브젝트 (기본 = null)
        /// <para/> -------------------------------------------------------
        /// <para/> * 오브젝트 풀에 해당 오브젝트가 존재하지 않으면 null 리턴
        /// </summary>
        public GameObject Load(in string targetName, in Vector3 pos, in Quaternion rot = default,
            in Transform parent = null)
        {
            // 1. 풀에 해당 오브젝트가 저장되어 있을 경우 : 풀에서 꺼내어 리턴
            if (_pool.ContainsKey(targetName))
            {
                if (_pool[targetName].Count > 0)
                {
                    GameObject poolTarget = _pool[targetName].Dequeue();
                    poolTarget.transform.SetParent(parent);
                    poolTarget.transform.position = pos;
                    poolTarget.transform.rotation = rot;
                    poolTarget.SetActive(true);

                    return poolTarget;
                }
            }

            return null;
        }

        /// <summary>
        /// <para/> [게임 오브젝트를 오브젝트 풀에 저장]
        /// <para/> 
        /// <para/> - target : 파괴하지 않고 오브젝트 풀에 저장할 게임오브젝트
        /// <para/> 
        /// <para/> 
        /// </summary>
        public void Save(in GameObject target)
        {
            if (target == null) return;

            string targetName = target.name;

            // 1. 오브젝트 풀에 해당 키가 있는지 검사하여, 없으면 생성
            if(_pool.ContainsKey(targetName) == false)
            {
                _pool.Add(targetName, new Queue<GameObject>());
            }

            // +++ 이미 큐에 있으면 Do Nothing
            // 매우 중요 - 중복으로 Enqueue되는 경우, Missing 상태로 큐에 복제되어 들어감
            if (_pool[targetName].Contains(target))
                return;

            // 2. 풀 저장 한도 확인하여, 한도를 넘었을 경우 쓰레기통으로 넘기기
            if (_pool[targetName].Count > _PoolMaxSize)
            {
                Destroy_(target);
                return;
            }

            // 3. 비활성화 및 부모 설정
            target.transform.SetParent(_poolContainerObj.transform);
            target.SetActive(false);

            // 혹시 몰라 요상한 곳에 위치 설정
            target.transform.position = new Vector3(0, -300, 0);

            // 4. 풀에 저장
            _pool[targetName].Enqueue(target);
        }

        /// <summary>
        /// <para/> [게임 오브젝트를 오브젝트 풀에 저장]
        /// <para/> 
        /// <para/> - target : 파괴하지 않고 오브젝트 풀에 저장할 트랜스폼
        /// </summary>
        public void Save(in Transform target)
        {
            Save(target.gameObject);
        }

        /// <summary>
        /// <para/> [게임 오브젝트를 n초 후 오브젝트 풀에 저장]
        /// <para/> 
        /// <para/> - target : 파괴하지 않고 오브젝트 풀에 저장할 게임오브젝트
        /// <para/> - n : 초
        /// </summary>
        public void Save(in GameObject target, in float n)
        {
            StartCoroutine(SaveAfterNSecRoutine(target, n));
        }

        /// <summary>
        /// <para/> [게임 오브젝트를 n초 후 오브젝트 풀에 저장]
        /// <para/> 
        /// <para/> - target : 파괴하지 않고 오브젝트 풀에 저장할 트랜스폼
        /// <para/> - n : 초
        /// </summary>
        public void Save(in Transform target, in float n)
        {
            StartCoroutine(SaveAfterNSecRoutine(target.gameObject, n));
        }

        /// <summary>
        /// <para/> 코루틴 : n초 후 오브젝트 풀에 저장
        /// <para/> 
        /// </summary>
        private IEnumerator SaveAfterNSecRoutine(GameObject target, float n)
        {
            yield return new WaitForSeconds(n);
            Save(target);
        }

        #endregion // =====================================================================================

        #region 2. GameObject Cleaner - Fields

        Coroutine _cleanerRoutine;

        /// <summary> 쓰레기통 게임오브젝트 이름 </summary>
        private const string _TrashBinContainerName = "[Trash Bin]";


        /// <summary> 현재 쓰레기 개수 </summary>
        public int TrashCount
        {
            private set
            {
                _trashCount = value;

                if (_trashCount < 0)
                    _trashCount = 0;
            }
            get => _trashCount;
        }
        private int _trashCount = 0;


        /// <summary> 쓰레기통 큐 </summary>
        private readonly Queue<GameObject> _trashBinQueue = new Queue<GameObject>();

        /// <summary> 쓰레기통 컨테이너 오브젝트 </summary>
        private GameObject _trashBinContainerObj = null;

        #endregion // =====================================================================================

        #region 2. GameObject Cleaner - Methods

        private void Init_Cleaner()
        {
            _trashBinContainerObj = new GameObject(_TrashBinContainerName);
        }

        /// <summary> 쓰레기 수집(점진적 파괴) </summary>
        public void Destroy_(in GameObject go)
        {
            if (go == null) return;

            // 1. 오브젝트 비활성화
            go.SetActive(false);

            // 2. 쓰레기통의 자식으로 설정
            go.transform.SetParent(_trashBinContainerObj.transform);

            // 3. 쓰레기통 큐에 넣어주기
            _trashBinQueue.Enqueue(go);

            // 4. 개수 증가
            TrashCount++;

            // 5. 자식 쓰레기통에 개수 표시
            _trashBinContainerObj.name = $"{_TrashBinContainerName} [{_trashCount}]";
        }
        /// <summary> 쓰레기 수집 </summary>
        public void Destroy_(in Transform tr)
        {
            Destroy_(tr.gameObject);
        }
        /// <summary> 쓰레기 수집 </summary>
        public void Collect(in GameObject go)
        {
            Destroy_(go);
        }
        /// <summary> 쓰레기 수집 </summary>
        public void Collect(in Transform tr)
        {
            Destroy_(tr.gameObject);
        }

        /// <summary> n초 후 오브젝트 파괴 </summary>
        public void Destroy_(in GameObject target, float n)
        {
            StartCoroutine(DestroyAfterNSecRoutine(target, n));
        }
        /// <summary> n초 후 오브젝트 파괴 </summary>
        public void Destroy_(in Transform target, float n)
        {
            StartCoroutine(DestroyAfterNSecRoutine(target.gameObject, n));
        }
        /// <summary> n초 후 오브젝트 파괴 </summary>
        public void Collect(in GameObject target, float n)
        {
            StartCoroutine(DestroyAfterNSecRoutine(target, n));
        }
        /// <summary> n초 후 오브젝트 파괴 </summary>
        public void Collect(in Transform target, float n)
        {
            StartCoroutine(DestroyAfterNSecRoutine(target.gameObject, n));
        }


        /// <summary>
        /// 혹시나 모를 상황 대비 : 쓰레기 처리 재가동 <para/>
        /// 쓰레기 코루틴 재시작 : 만일의 상황에만 사용
        /// </summary>
        [Obsolete("쓰레기 코루틴 재시작 : 만일의 상황에만 사용")]
        public void ResetCollectors()
        {
            StopCoroutine(_cleanerRoutine);
            _cleanerRoutine = StartCoroutine(CleanerRoutine());
        }

        /// <summary> 코루틴 : 주기적으로 쓰레기 처리 </summary>
        IEnumerator CleanerRoutine()
        {
            yield return new WaitForEndOfFrame();

            // 쓰레기 처리 주기
            WaitForSeconds[] Wait =
            {
                new WaitForSeconds(3.0f),
                new WaitForSeconds(1.0f),
                new WaitForSeconds(0.3f),
                new WaitForSeconds(0.1f)
            };

            // Thread
            while (true)
            {
                // 적응형 쓰레기 처리 주기
                if     (TrashCount == 0)  yield return Wait[0];
                else if(TrashCount < 10)  yield return Wait[1];
                else if(TrashCount < 100) yield return Wait[2];
                else                      yield return Wait[3];

                if (_trashBinQueue.Count > 0)                      // 쓰레기통이 비어 있지 않으면
                    GameObject.Destroy(_trashBinQueue.Dequeue());  // 맨 앞 쓰레기 꺼내서 파괴

                // 개수 변경
                TrashCount--;

                // 개수 표시
                _trashBinContainerObj.name = $"{_TrashBinContainerName} [{_trashCount}]";
            }
        }

        /// <summary> 코루틴 : n초 후 오브젝트 파괴 </summary>
        private IEnumerator DestroyAfterNSecRoutine(GameObject target, float n)
        {
            yield return new WaitForSeconds(n);
            Destroy_(target);
        }

        #endregion // =====================================================================================
    }
}