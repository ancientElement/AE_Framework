using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEditor.EditorTools;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using static AE_Framework.GameSettings;

namespace AE_Framework
{
    /// <summary>
    /// 资源加载管理器
    /// 自动实例化GameObject
    /// </summary>
    public class ResMgr : BaseManager<ResMgr>
    {
        //资源加载方式
        private AssetLoadMethod assetLoadMethod => GameRoot.Instance.GameSetting.assetLoadMethod;

        #region 自动判断
        /// <summary>
        /// 同步加载
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public T AutoLoad<T>(string name) where T : UnityEngine.Object
        {
            if (assetLoadMethod == AssetLoadMethod.Addressables)
            {
                return AddressableLoad<T>(name);
            }
            else if (assetLoadMethod == AssetLoadMethod.Reaourses)
            {
                return ResourcesLoad<T>(name);
            }
            return null;
        }

        /// <summary>
        /// 同步加载
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public T AutoLoad<T>(AssetReference assetReference, string name) where T : UnityEngine.Object
        {
            if (assetLoadMethod == AssetLoadMethod.Addressables)
            {
                return AddressableLoad<T>(assetReference);
            }
            else if (assetLoadMethod == AssetLoadMethod.Reaourses)
            {
                return ResourcesLoad<T>(name);
            }
            return null;
        }

        /// <summary>
        /// 异步加载
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async UniTask<T> AutoLoadAsync<T>(string name) where T : UnityEngine.Object
        {
            if (assetLoadMethod == AssetLoadMethod.Addressables)
            {
                return await AddressableLoadUniTaskAsync<T>(name);
            }
            else if (assetLoadMethod == AssetLoadMethod.Reaourses)
            {
                return await ResourcesLoadAsync<T>(name);
            }
            return null;
        }

        /// <summary>
        /// 异步加载
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async UniTask<T> AutoLoadAsync<T>(AssetReference assetReference, string name) where T : UnityEngine.Object
        {
            if (assetLoadMethod == AssetLoadMethod.Addressables)
            {
                return await AddressableLoadUniTaskAsync<T>(assetReference);
            }
            else if (assetLoadMethod == AssetLoadMethod.Reaourses)
            {
                return await ResourcesLoadAsync<T>(name);
            }
            return null;
        }
        #endregion

        #region Resources
        /// <summary>
        /// Resources 同步资源加载
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public T ResourcesLoad<T>(string assetName, Transform parent = null) where T : UnityEngine.Object
        {
            T res = Resources.Load<T>(assetName);
            //判断资源是否是GameObject对象 
            //是则实例化后方返回
            if (res is GameObject)
                return GameObject.Instantiate(res, parent);
            else
                return res;
        }

        //这里我们要约束 泛型Ｔ　为UnityEngine.Object
        //因为 Resources 加载的是 UnityEngine.Object
        //使用回调函数传递参数
        //因为在协程函数里不能直接return回去
        /// <summary>
        /// Resources异步加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <param name="callback"></param>
        public void ResourcesLoadAsync<T>(string assetName, Action<T> callback, Transform parent = null) where T : UnityEngine.Object
        {
            MonoMgr.Instance.StartCoroutine(ResourcesLoadAsycIEnumerator<T>(assetName, callback, parent));
        }
        private IEnumerator ResourcesLoadAsycIEnumerator<T>(string name, Action<T> callback, Transform parent = null) where T : UnityEngine.Object
        {
            ResourceRequest res = Resources.LoadAsync<T>(name);

            //等待加载完毕
            yield return res;

            //加载完毕后判断资源是否是GameObject对象
            //是则实例化后方返回
            if (res.asset is GameObject)
                callback(GameObject.Instantiate(res.asset, parent) as T);
            else
                callback(res.asset as T);
        }

        /// <summary>
        /// Resources异步加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <param name="callback"></param>
        public async UniTask<T> ResourcesLoadAsync<T>(string assetName, Transform parent = null) where T : UnityEngine.Object
        {
            ResourceRequest res = Resources.LoadAsync<T>(assetName);
            //等待加载完毕
            await res;
            //加载完毕后判断资源是否是GameObject对象
            //是则实例化后方返回
            if (res.asset is GameObject)
                return GameObject.Instantiate(res.asset, parent) as T;
            else
                return res.asset as T;
        }

        #endregion 

        #region Addressable

        /// <summary>
        /// Addressable同步加载游戏物体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public T AddressableLoad<T>(string assetName, Transform parent = null) where T : UnityEngine.Object
        {
            T obj = Addressables.LoadAssetAsync<T>(assetName).WaitForCompletion();
            if (obj is GameObject)
            {
                return GameObject.Instantiate(obj, parent) as T;
            }
            return obj;
        }

        /// <summary>
        /// Addressable同步加载游戏物体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public T AddressableLoad<T>(AssetReference assetName, Transform parent = null) where T : UnityEngine.Object
        {
            T obj = Addressables.LoadAssetAsync<T>(assetName).WaitForCompletion();
            if (obj is GameObject)
            {
                return GameObject.Instantiate(obj, parent) as T;
            }
            return obj;
        }

        /// <summary>
        ///  Addressable异步加载游戏物体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <param name="callBack"></param>
        /// <param name="parent"></param>
        public void AddressableLoadAsync<T>(string assetName, Action<T> callBack = null, Transform parent = null) where T : class
        {
            // 不通过缓存池
            MonoMgr.Instance.StartCoroutine(AddressableDoLoadAsync<T>(assetName, callBack, parent));
        }
        static IEnumerator AddressableDoLoadAsync<T>(string assetName, Action<T> callBack = null, Transform parent = null) where T : class
        {
            // 不通过缓存池
            if (typeof(T) == typeof(GameObject))
            {
                var request = Addressables.InstantiateAsync(assetName, parent);
                callBack?.Invoke(request.Result as T);
                yield return request;
            }
            else
            {
                var request = Addressables.LoadAssetAsync<T>(assetName);
                callBack?.Invoke(request.Result);
                yield return request;
            }
        }

        /// <summary>
        /// Addressable异步加载游戏物体
        /// </summary>
        /// <typeparam name="T">具体的组件</typeparam>
        public void AddressableLoadAsync<T>(AssetReference assetReference, Action<T> callBack = null, Transform parent = null) where T : class
        {
            // 不通过缓存池
            MonoMgr.Instance.StartCoroutine(AddressableDoLoadGameObjectAsync<T>(assetReference, callBack, parent));
        }
        static IEnumerator AddressableDoLoadGameObjectAsync<T>(AssetReference assetReference, Action<T> callBack = null, Transform parent = null) where T : class
        {
            // 不通过缓存池
            if (typeof(T) == typeof(GameObject))
            {
                var request = Addressables.InstantiateAsync(assetReference, parent);
                callBack?.Invoke(request.Result as T);
                yield return request;
            }
            else
            {
                var request = Addressables.LoadAssetAsync<T>(assetReference);
                callBack?.Invoke(request.Result);
                yield return request;
            }
        }

        /// <summary>
        /// UniTask Addressable异步加载游戏物体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetReference"></param>
        /// <param name="callBack"></param>
        /// <param name="parent"></param>
        public async UniTask<T> AddressableLoadUniTaskAsync<T>(string assetName, Action<GameObject> callBack = null, Transform parent = null) where T : class
        {
            // 不通过缓存池
            if (typeof(T) == typeof(GameObject))
            {
                var handel = Addressables.InstantiateAsync(assetName);
                await handel;
                return handel.Result as T;
            }
            else
            {
                var handel = Addressables.LoadAssetAsync<T>(assetName);
                await handel;
                return handel.Result as T;
            }
        }

        /// <summary>
        /// UniTask Addressable异步加载游戏物体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetReference"></param>
        /// <param name="callBack"></param>
        /// <param name="parent"></param>
        public async UniTask<T> AddressableLoadUniTaskAsync<T>(AssetReference assetReference, Action<GameObject> callBack = null, Transform parent = null) where T : class
        {
            // 不通过缓存池
            if (typeof(T) == typeof(GameObject))
            {
                var handel = Addressables.InstantiateAsync(assetReference);
                await handel;
                return handel.Result as T;
            }
            else
            {
                var handel = Addressables.LoadAssetAsync<T>(assetReference);
                await handel;
                return handel.Result as T;
            }
        }

        /// <summary>
        /// 释放Addressables资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void Release<T>(T obj)
        {
            Addressables.Release(obj);
        }

        /// <summary>
        /// 释放实例
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public bool ReleaseInstance(GameObject gameObject)
        {
            return Addressables.ReleaseInstance(gameObject);
        }

        #endregion
    }
}