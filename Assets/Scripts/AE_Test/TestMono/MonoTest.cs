using AE_Framework;
using System.Collections;
using UnityEngine;

public class NoMonoTestTest
{
    public void Update()
    {
        Debug.Log(nameof(NoMonoTestTest));
    }

    public IEnumerator Log123456()
    {
        yield return new WaitForSeconds(1);
        Debug.Log(123456);
    }
}

public class MonoTest : MonoBehaviour
{
    private void Start()
    {
        NoMonoTestTest noMonoTestTest = new NoMonoTestTest();
        MonoMgr.Instance.AddUpdateEventListener(noMonoTestTest.Update);
    }
}