using AE_Framework;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TestSave : MonoBehaviour
{
    [SerializeField] private TestMap TestMap = new TestMap();
    [SerializeField] private GameObject father;

    [Button]
    private void Clear()
    {
        if (father.transform.childCount == 0) return;

        for (int i = father.transform.childCount - 1; i >= 0; i--)
        {
            PoolMgr.PushGameObj("Cube", father.transform.GetChild(i).gameObject);
        }

        PoolMgr.ClearGameObject("Cube");
    }

    [Button]
    private void Random(int number, float radius)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject obj = PoolMgr.GetGameObj("Cube");
            obj.transform.position = new Vector3(UnityEngine.Random.Range(0, radius),
                UnityEngine.Random.Range(0, radius), UnityEngine.Random.Range(0, radius));
            obj.transform.SetParent(father.transform);
        }
    }

    [Button]
    private void Save()
    {
        List<Transform> gameObjects = new List<Transform>();
        for (int i = 0; i < father.transform.childCount; i++)
        {
            gameObjects.Add(father.transform.GetChild(i));
        }

        TestMap.Save(gameObjects);
        SaveMgr.SaveObject<TestMap>(TestMap);
    }

    [Button]
    private void Load()
    {
        TestMap = SaveMgr.LoadObj<TestMap>();
        List<GameObject> gameObjects = new List<GameObject>();
        for (int i = 0; i < TestMap.transforms.Count; i++)
        {
            GameObject obj = PoolMgr.GetGameObj("Cube");
            obj.transform.SetParent(father.transform);
            gameObjects.Add(obj);
        }

        TestMap.Load(gameObjects);
    }
}

[Serializable]
public class TestMap
{
    public List<Serialization_Vector3> transforms = new List<Serialization_Vector3>();

    public void Save(List<Transform> gameObjects)
    {
        transforms.Clear();
        for (int i = 0; i < gameObjects.Count; i++)
        {
            transforms.Add(gameObjects[i].transform.position.ConverToSerializationVector3());
        }
    }

    public void Load(List<GameObject> gameObjects)
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].transform.position = transforms[i].ConverToUnityVector3();
        }
    }
}