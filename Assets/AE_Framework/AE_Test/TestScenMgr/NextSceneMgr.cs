using System;
using System.Collections;
using System.Collections.Generic;
using AE_Framework;
using UnityEngine;

public class NextSceneMgr : SingletonMonobehaviour<NextSceneMgr>
{
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        ResMgr.AddressableLoad<GameObject>("Test");
    }
}