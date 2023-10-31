using AE_Framework;
using UnityEngine;

public class TestObj : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("ToThePool", 1);
        transform.position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
    }

    private void ToThePool()
    {
        PoolMgr.PushGameObj("Bullet", this.gameObject);
    }
}