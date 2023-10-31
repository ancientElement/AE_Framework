using System.Collections;
using System.Collections.Generic;
using AE_Framework;
using UnityEngine;

public class Test : MonoBehaviour
{
    public void Init()
    {
        ResMgr.AddressableLoad<GameObject>("Test");
    }
}