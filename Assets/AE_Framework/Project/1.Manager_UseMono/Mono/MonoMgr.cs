﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;

namespace AE_Framework
{
    public class MonoMgr : SingletonMonoMgr<MonoMgr>
    {
        //更新事件
        private event Action updateEvent = null;
        private event Action lateUpdateEvent = null;
        private event Action fixedUpdateEvent = null;

        private void Update()
        {
            //调用帧更新事件
            updateEvent?.Invoke();
        }
        private void LateUpdate()
        {
            lateUpdateEvent?.Invoke();
        }
        private void FixedUpdate()
        {
            fixedUpdateEvent?.Invoke();
        }

        /// <summary>
        /// 添加更新事件监听
        /// </summary>
        /// <param name="action"></param>
        public void AddUpdateEventListener(Action action)
        {
            updateEvent += action;
        }

        /// <summary>
        /// 移除更新事件监听
        /// </summary>
        /// <param name="action"></param>
        public void RemoveUpdateEventListener(Action action)
        {
            updateEvent -= action;
        }

        /// <summary>
        /// 添加LateUpdate监听
        /// </summary>
        /// <param name="action"></param>
        public void AddLateUpdateListener(Action action)
        {
            lateUpdateEvent += action;
        }

        /// <summary>
        /// 移除LateUpdate监听
        /// </summary>
        /// <param name="action"></param>
        public void RemoveLateUpdateListener(Action action)
        {
            lateUpdateEvent -= action;
        }

        /// <summary>
        /// 添加FixedUpdate监听
        /// </summary>
        /// <param name="action"></param>
        public void AddFixedUpdateListener(Action action)
        {
            fixedUpdateEvent += action;
        }

        /// <summary>
        /// 移除FixedUpdate监听
        /// </summary>
        /// <param name="action"></param>
        public void RemoveFixedUpdateListener(Action action)
        {
            fixedUpdateEvent -= action;
        }

        /// <summary>
        /// 没有弄清除不要轻易调用 会清除InputMgr的事件 最好用RemoveUpdateEventListener移除
        /// </summary>
        public void Clear()
        {
            updateEvent = null;
            lateUpdateEvent = null;
            fixedUpdateEvent = null;
        }
    }
}