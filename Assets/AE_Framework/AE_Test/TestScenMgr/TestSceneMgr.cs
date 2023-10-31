using AE_Framework;
using Sirenix.OdinInspector;
using UnityEngine;

public class TestSceneMgr : MonoBehaviour
{
    [Button]
    public void NextLever()
    {
        SceneMgr.LoadScene("Next");
    }
}