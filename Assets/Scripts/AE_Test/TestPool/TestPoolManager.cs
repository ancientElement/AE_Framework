using AE_Framework;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using UnityEngine;

public class TestPoolManager : MonoBehaviour
{
    public string tempString;

    private void Start()
    {

    }

    private void Update()
    {
        OnMouseClick();
    }

    private async void OnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject obj;
            obj = await PoolMgr.Instance.GetGameObjAsync("Bullet");
        }
        if (Input.GetMouseButtonDown(1))
        {
            TestClass test = PoolMgr.Instance.GetObj<TestClass>();
            test.name = tempString;
            test.Print();
            PoolMgr.Instance.PushObj<TestClass>(test);
        }
        if (Input.GetMouseButtonDown(2))
        {
            TestClass test = PoolMgr.Instance.GetObj<TestClass>();
            test.Print();
            PoolMgr.Instance.PushObj<TestClass>(test);
        }
    }

    [Button]
    private void Clear()
    {
        PoolMgr.Instance.ClearGameObject("Bullet");
    }
}

public class TestClass
{
    public string name = "@@@";
    public void Print()
    {
        Debug.Log(name);
    }
}