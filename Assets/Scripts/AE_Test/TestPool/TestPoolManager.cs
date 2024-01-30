using AE_Framework;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SocialPlatforms;

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
            GameObject gameObject = PoolMgr.GetGameObj("Bullet");
            if (gameObject == null)
            {
                gameObject = ResMgr.AddressableLoad<GameObject>("Bullet");
            };
            Vector3 random = new Vector3(Random.Range(0, 5), 0, Random.Range(0, 5));
            gameObject.transform.SetLocalPositionAndRotation(random, Quaternion.identity);
            PoolMgr.PushGameObj("Bullet", gameObject, 1f);
        }
    }

    [Button]
    private void GetTestClass()
    {
        TestClass test = PoolMgr.GetObj<TestClass>();
        test.name = tempString;
        test.Print();
        PoolMgr.PushObj<TestClass>(test);
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