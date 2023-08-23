using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows;

namespace AE_Framework
{
    public class SaveMgr : BaseManager<SaveMgr>
    {
        public SaveMgrData saveMgrData { get; private set; }

        public SaveMgrData settingsData { get; private set; }

        // 存档的保存
        private const string saveDirName = "saveData/";
        // 设置的保存：
        // 1.全局数据的保存（分辨率、按键设置）
        // 2.存档的设置保存。
        // 常规情况下，存档管理器自行维护
        private const string settingDirName = "setting/";

        // 存档文件夹路径
        public static string RootPath => Application.persistentDataPath + "/";

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            saveMgrData = SaveMgrDataFactory.LoadSaveMgrData($"{RootPath}{saveDirName}", "saveData");


            settingsData = SaveMgrDataFactory.LoadSaveMgrData($"{RootPath}{settingDirName}", "settingData");
            if (saveMgrData.SaveItemList.Count == 0)
            {
                CreateSettingItem();
            }
        }

        #region Item

        public SaveItem CreateSaveItem(int saveItemID = -1)
        {
            return SaveItemFactory.CreateSaveItem(saveMgrData, saveItemID);
        }

        public void DeleteSaveItem(int saveItemID)
        {
            SaveItemFactory.DeleteSaveItem(saveMgrData, saveItemID);
        }

        public SaveItem GetSaveItem(int saveItemID)
        {
            return SaveItemFactory.GetSaveItem(saveMgrData, saveItemID);
        }

        public void SetCurrrentSaveItemID(int saveItemID)
        {
            SaveMgrDataFactory.SetCurrrentSaveItemID(saveMgrData, saveItemID);
        }

        #endregion

        #region object

        public T LoadObj<T>(int saveItemID = -1) where T : class
        {
            if (saveItemID == -1)
            {
                saveItemID = saveMgrData.currentSaveItemID;
            }
            return SaveItemObjectFactory.LoadObj<T>(GetSaveItem(saveItemID));
        }

        public void SaveObject<T>(object obj, int saveItemID = -1)
        {
            if (saveItemID == -1)
            {
                saveItemID = saveMgrData.currentSaveItemID;
            }
            SaveItemObjectFactory.SaveObject<T>(GetSaveItem(saveItemID), obj);
        }

        public void DeleteObject<T>(int saveItemID = -1)
        {
            if (saveItemID == -1)
            {
                saveItemID = saveMgrData.currentSaveItemID;
            }
            SaveItemObjectFactory.DeleteObject<T>(GetSaveItem(saveItemID));
        }

        #endregion

        #region Settings

        private SaveItem CreateSettingItem()
        {
            return SaveItemFactory.CreateSaveItem(settingsData, 0);
        }

        public SaveItem GetSettingItem()
        {
            return SaveItemFactory.GetSaveItem(settingsData, 0);
        }

        public T LoadSettingObj<T>() where T : class
        {
            return SaveItemObjectFactory.LoadObj<T>(GetSettingItem());
        }

        public void SaveSettingObject<T>(object obj)
        {
            SaveItemObjectFactory.SaveObject<T>(GetSettingItem(), obj);
        }

        public void DeleteSettingObject<T>()
        {
            SaveItemObjectFactory.DeleteObject<T>(GetSettingItem());
        }

        #endregion
    }
}