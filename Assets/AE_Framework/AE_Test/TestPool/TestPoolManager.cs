using AE_Framework;
using Sirenix.OdinInspector;
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

    private void OnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject obj;
            obj = PoolMgr.GetGameObj("Bullet");
        }

        if (Input.GetMouseButtonDown(1))
        {
            TestClass test = PoolMgr.GetObj<TestClass>();
            test.name = tempString;
            test.Print();
            PoolMgr.PushObj<TestClass>(test);
        }

        if (Input.GetMouseButtonDown(2))
        {
            TestClass test = PoolMgr.GetObj<TestClass>();
            test.Print();
            PoolMgr.PushObj<TestClass>(test);
        }
    }

    [Button]
    private void Clear()
    {
        PoolMgr.ClearGameObject("Bullet");
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