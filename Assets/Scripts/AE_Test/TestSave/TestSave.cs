using AE_Framework;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TestSave : MonoBehaviour
{
    [SerializeField] private TestMap TestMap = new TestMap();
    //[SerializeField] private GameObject father;
    private List<GameObject> cubes = new List<GameObject>();

    [Button]
    private void Clear()
    {
        if (cubes.Count == 0) return;

        for (int i = cubes.Count - 1; i >= 0; i--)
        {
            PoolMgr.PushGameObj("Cube", cubes[i]);
        }
        PoolMgr.ClearGameObject("Cube");
        cubes.Clear();
    }

    [Button]
    private void Random(int number, float radius)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject obj = PoolMgr.GetGameObj("Cube");
            if (obj == null)
            {
                obj = ResMgr.AddressableLoad<GameObject>("Cube");
            }
            obj.transform.position = new Vector3(UnityEngine.Random.Range(0, radius),
                UnityEngine.Random.Range(0, radius), UnityEngine.Random.Range(0, radius));
            cubes.Add(obj);
        }
    }

    [Button]
    private void Save()
    {
        TestMap.Save(cubes);
        SaveMgr.SaveObject<TestMap>(TestMap);
    }

    [Button]
    private void Load()
    {
        TestMap = SaveMgr.LoadObj<TestMap>();
        for (int i = 0; i < TestMap.transforms.Count; i++)
        {
            GameObject obj = PoolMgr.GetGameObj("Cube");
            if (obj == null)
            {
                obj = ResMgr.AddressableLoad<GameObject>("Cube");
            }
            cubes.Add(obj);
        }
        TestMap.Load(cubes);
    }
}

[Serializable]
public class TestMap
{
    public List<Serialization_Vector3> transforms = new List<Serialization_Vector3>();

    public void Save(List<GameObject> gameObjects)
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