using System;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;

namespace AE_Framework
{

    public class EVENTNAME
    {
        public const string LOAD_SCENE_ING = "LOAD_SCENE_DONE";
    }

    //空接口
    public interface IEventInfo
    {

    }

    //IEventInfo 的子类 使得IEventInfo 成为一个中转站
    //这样我们就不用在Dictionary里放泛型了
    public class EventInfo<T> : IEventInfo
    {
        //这里不加event修饰符的原因是
        //加event只能内部调用这个事件 而事件中心是需要外部触发事件的
        //所有不加event
        public Action<T> action;

        public EventInfo(Action<T> action)
        {
            this.action += action;
        }
    }

    public class EventInfo : IEventInfo
    {
        public Action action;

        public EventInfo(Action action)
        {
            this.action += action;
        }
    }

    /// <summary>
    /// 事件中心
    /// 储存事件并提供外部添加移除和触发事件
    /// 优化事件中心避免装箱拆箱
    /// </summary>
    public class EventCenter : BaseManager<EventCenter>
    {
        //储存事件的字典
        //Action<object> 改为储存他的接口父类
        //Dictionary<string, Action<object>> eventDic = new Dictionary<string, Action<object>>();
        Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();


        /// <summary>
        /// 添加事件监听的方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public void AddEventListener<T>(string name, Action<T> action)
        {
            //如果有这个事件
            //添加这个函数(委托)到事件里
            //事件 就是 一个委托列表 本质也是一个委托
            if (eventDic.ContainsKey(name))
            {
                //里氏转换 得到子类
                (eventDic[name] as EventInfo<T>).action += action;
            }
            //没有这个事件 就把这个委托作为事件也就创建了一个事件
            else
            {
                //在构造函数里初始化的到第一个action
                //里氏转换 父类装子类
                eventDic.Add(name, new EventInfo<T>(action));
            }
        }


        /// <summary>
        /// 无参添加事件监听的方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public void AddEventListener(string name, Action action)
        {
            //如果有这个事件
            //添加这个函数(委托)到事件里
            //事件 就是 一个委托列表 本质也是一个委托
            if (eventDic.ContainsKey(name))
            {
                //里氏转换 得到子类
                (eventDic[name] as EventInfo).action += action;
            }
            //没有这个事件 就把这个委托作为事件也就创建了一个事件
            else
            {
                //在构造函数里初始化的到第一个action
                //里氏转换 父类装子类
                eventDic.Add(name, new EventInfo(action));
            }
        }


        /// <summary>
        /// 移除事件监听的方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public void RemoveEventListener<T>(string name, Action<T> action)
        {
            if (eventDic.ContainsKey(name))
            {
                //里氏转换 得到子类
                (eventDic[name] as EventInfo<T>).action -= action;
            }
        }


        /// <summary>
        /// 无参移除事件监听的方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public void RemoveEventListener(string name, Action action)
        {
            if (eventDic.ContainsKey(name))
            {
                //里氏转换 得到子类
                (eventDic[name] as EventInfo).action -= action;
            }
        }


        /// <summary>
        /// 触发事件的方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="info"></param>
        public void TriggerEvent<T>(string name, T info)
        {
            if (eventDic.ContainsKey(name))
            {
                //?判空
                (eventDic[name] as EventInfo<T>).action?.Invoke(info);
            }
        }


        /// <summary>
        /// 无参触发事件的方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="info"></param>
        public void TriggerEvent(string name)
        {
            if (eventDic.ContainsKey(name))
            {
                //?判空
                (eventDic[name] as EventInfo).action?.Invoke();
            }
        }


        /// <summary>
        /// 不轻易调用清空所有事件
        /// </summary>
        public void Clear()
        {
            eventDic.Clear();
        }
    }
}