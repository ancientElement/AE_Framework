using System;
using System.Collections;
using UnityEngine;

namespace AE_Framework
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    /// <summary>
    /// UI元素的特性
    /// 每个UI窗口都应该添加
    /// </summary>
    public class UIElementAttribute : Attribute
    {
        public bool isCache;//是否缓存
        public string prefabAssetName;//预制体名用于加载
        public int layerNum;//层级

        public UIElementAttribute(bool isCache, string prefabAssetName, int layerNum)
        {
            this.isCache = isCache;
            this.prefabAssetName = prefabAssetName;
            this.layerNum = layerNum;
        }
    }
}