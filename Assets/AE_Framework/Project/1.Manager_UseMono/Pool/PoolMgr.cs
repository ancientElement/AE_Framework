using System;
using UnityEngine;
using Sirenix.OdinInspector;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using static AE_Framework.GameSettings;
using Sirenix.Utilities;
using System.Linq;

namespace AE_Framework
{
    /// <summary> 
    /// 缓存池(桌子)里的抽屉 游戏物体
    /// </summary>
    public class GameObjectPoolData
    {
        GameObject fatherObj;//场景上的父节点
        public List<GameObject> Datalist;//抽屉

        //当外部需要创建这个抽屉时
        //初始化抽屉
        //创建场景上的父节点(与物体同名) 挂载到场景上的pool
        //并放入第一个物体
        public GameObjectPoolData(GameObject poolObj, GameObject obj, string name)
        {
            Datalist = new List<GameObject>();
            fatherObj = new GameObject(name);
            fatherObj.transform.parent = poolObj.transform;
            PushObj(obj);
        }

        //拿到第一个抽屉里的物体
        //将物体 取消与场景上的父节点 的关联
        //激活物体
        //从抽屉里取出
        public GameObject GetObj()
        {
            GameObject obj = Datalist[0];
            obj.transform.parent = null;
            obj.SetActive(true);
            Datalist.RemoveAt(0);
            return obj;
        }

        //将物体失活
        //设置父级为fatherObj
        //放进抽屉里
        public void PushObj(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.parent = fatherObj.transform;
            Datalist.Add(obj);
        }

        /// <summary>
        /// 删除自己
        /// </summary>
        public void Destory()
        {
            for (int i = Datalist.Count - 1; i >= 0; i--)
            {
                if (GameRoot.Instance.GameSetting.assetLoadMethod == AssetLoadMethod.Addressables)
                    ResMgr.Instance.ReleaseInstance(Datalist[i]);
                else
                    GameObject.Destroy(Datalist[i]);
            }
            Datalist.Clear();
            GameObject.Destroy(fatherObj);
        }
    }

    /// <summary>
    /// 缓存池(桌子)里的抽屉 普通对象
    /// </summary>
    public class ObjectPoolData
    {
        public List<object> Datalist;//抽屉

        //当外部需要创建这个抽屉时
        //初始化抽屉
        //并放入第一个物体
        public ObjectPoolData(object obj)
        {
            Datalist = new List<object>();
            PushObj(obj);
        }


        //拿到第一个抽屉里的物体
        //从抽屉里取出
        public object GetObj()
        {
            object obj = Datalist[0];
            Datalist.RemoveAt(0);
            return obj;
        }


        //将物体失活
        //设置父级为fatherObj
        //放进抽屉里
        public void PushObj(object obj)
        {
            Datalist.Add(obj);
        }
    }


    /// <summary>
    /// 缓存池(桌子) 
    /// </summary>
    public class PoolMgr : SingletonMonoMgr<PoolMgr>
    {
        [LabelText("Resouse模式的资源路径")]
        private static readonly string GameObjectPoolResourcesDir = "GameObjectPool/";

        [LabelText("游戏对象缓存池父节点")]
        public GameObject poolObj;//场景上代表物体的空节点

        /// <summary>
        /// 游戏对象缓存池
        /// </summary>
        public Dictionary<string, GameObjectPoolData> gameObjectPoolDic = new Dictionary<string, GameObjectPoolData>();

        /// <summary>
        /// 普通对象缓存池
        /// </summary>
        public Dictionary<string, ObjectPoolData> objectPoolDic = new Dictionary<string, ObjectPoolData>();

        /// <summary>
        /// 缓存池初始化
        /// </summary>
        public override void Init()
        {
            base.Init();
        }

        #region 游戏对象缓存池

        /// <summary>
        /// 从池里拿东西 通过 抽屉名字
        /// </summary>
        /// <param name="name"></param>
        /// <param name="callback"></param>
        public async UniTask<GameObject> GetGameObjAsync(string name)
        {
            if (gameObjectPoolDic.ContainsKey(name) && gameObjectPoolDic[name].Datalist.Count > 0)
            {
                return gameObjectPoolDic[name].GetObj();
            }
            else
            {
                //如果是Resource加载模式
                if (GameRoot.Instance.GameSetting.assetLoadMethod == AssetLoadMethod.Reaourses)
                {
                    name = GameObjectPoolResourcesDir + name;
                    GameObject obj = await ResMgr.Instance.AutoLoadAsync<GameObject>(null, name);
                    return obj;
                }
                else if (GameRoot.Instance.GameSetting.assetLoadMethod == AssetLoadMethod.Addressables)
                {
                    GameObject obj = await ResMgr.Instance.AutoLoadAsync<GameObject>(name);
                    if (obj == null)
                    {
                        Debug.LogWarning($"{name}不存在请检测Addressable");
                    }
                    return obj;
                }
            }
            return null;
        }

        public GameObject GetGameObj(string name)
        {
            if (gameObjectPoolDic.ContainsKey(name) && gameObjectPoolDic[name].Datalist.Count > 0)
            {
                return gameObjectPoolDic[name].GetObj();
            }
            else
            {
                //如果是Resource加载模式
                if (GameRoot.Instance.GameSetting.assetLoadMethod == AssetLoadMethod.Reaourses)
                {
                    name = GameObjectPoolResourcesDir + name;
                    GameObject obj = ResMgr.Instance.ResourcesLoad<GameObject>(name);
                    return obj;
                }
                else if (GameRoot.Instance.GameSetting.assetLoadMethod == AssetLoadMethod.Addressables)
                {
                    GameObject obj = ResMgr.Instance.AddressableLoad<GameObject>(name);
                    if (obj == null)
                    {
                        Debug.LogWarning($"{name}不存在请检测Addressable");
                    }
                    return obj;
                }
            }
            return null;
        }

        /// <summary>
        /// 放进游戏对象缓存池
        /// </summary>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        public void PushGameObj(string name, GameObject obj)
        {
            if (poolObj == null)
                poolObj = new GameObject("Pool");
            obj.SetActive(false);
            if (gameObjectPoolDic.ContainsKey(name))
            {
                gameObjectPoolDic[name].PushObj(obj);
            }
            else
            {
                gameObjectPoolDic.Add(name, new GameObjectPoolData(poolObj, obj, name));
            }
        }
        #endregion

        #region 普通对象缓存池

        /// <summary>
        /// 从对象池拿到
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetObj<T>() where T : class, new()
        {
            string name = typeof(T).FullName;
            if (objectPoolDic.ContainsKey(name) && objectPoolDic[name].Datalist.Count > 0)
            {
                return (T)objectPoolDic[name].GetObj();
            }
            else
            {
                return new T();
            }
        }

        /// <summary>
        /// 放进对象池
        /// </summary>
        /// <param name="obj"></param>
        public void PushObj<T>(object obj)
        {
            string name = typeof(T).FullName;
            if (objectPoolDic.ContainsKey(name))
            {
                objectPoolDic[name].PushObj(obj);
            }
            else
            {
                objectPoolDic.Add(name, new ObjectPoolData(obj));
            }
        }

        #endregion

        #region 清理对象池

        public void Clear(bool isGameObject, bool isObject)
        {
            if (isGameObject)
            {
                foreach (var item in gameObjectPoolDic)
                {
                    item.Value.Destory();
                }
            }
            gameObjectPoolDic.Clear();

            if (isObject)
            {
                objectPoolDic.Clear();
            }
        }
        public void ClearAllGameObject()
        {
            Clear(true, false);
        }
        public void ClearGameObject(string name)
        {
            gameObjectPoolDic[name].Destory();
            gameObjectPoolDic.Remove(name);
        }

        public void ClearAllObject()
        {
            Clear(false, true);
        }
        public void ClearObject<T>()
        {
            objectPoolDic.Remove(typeof(T).FullName);
        }
        public void ClearObject(Type type)
        {
            objectPoolDic.Remove(type.FullName);
        }

        #endregion
    }
}
