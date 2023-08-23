using AE_Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestObj : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("ToThePool", 1);
        transform.position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
    }
    void ToThePool()
    {
        PoolMgr.Instance.PushGameObj("Bullet", this.gameObject);
    }
}