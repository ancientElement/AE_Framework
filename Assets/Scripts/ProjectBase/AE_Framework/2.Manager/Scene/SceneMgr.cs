using AE_Framework;
using System;
using System.Collections;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AE_Framework
{
    public class SceneMgr : BaseManager<SceneMgr>
    {

        /// <summary>
        /// 同步加载场景
        /// </summary>
        /// <param name="name">场景名</param>
        /// <param name="fun">加载完场景需要做的事</param>
        public void LoadScene(string name, Action fun)
        {
            //清理缓存数据 仅限projectBase里的
            Clear();
            //同步加载不能进行其他操作只能等待场景加载
            SceneManager.LoadScene(name);
            fun();
        }

        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fun"></param>
        public void LoadSceneAsync(string name, Action fun)
        {
            //清理缓存数据 仅限projectBase里的
            Clear();
            //异步加载可以进行其他操作比如加载
            //用MonoMgr开启协程
            MonoMgr.Instance.StartCoroutine(RealLoadScene(name, fun));
        }

        private IEnumerator RealLoadScene(string name, Action fun)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(name);
            //当没加载完时做一些操作
            //如 触发 场景更新进度 事件, 将 场景更新进度 传到关心的地方
            //比如可以做进度条
            while (!ao.isDone)
            {
                //事件中心往外分发 场景更新进度
                EventCenter.Instance.TriggerEvent(EVENTNAME.LOAD_SCENE_ING, ao.progress);
                yield return ao.progress;
            }
            fun();
        }

        /// <summary>
        /// 清除缓存数据
        /// </summary>
        private void Clear()
        {
            //TODO:确认需要清楚的缓存
            //1.对象池的游戏物体
            PoolMgr.Instance.Clear(true, false);
        }
    }
}