using AE_Framework;
using Sirenix.OdinInspector;
using UnityEngine;

public class TestUI : MonoBehaviour
{
    [Button]
    private void Show()
    {
        UIMgr.Instance.ShowPanel<StartPanel>();
    }

    [Button]
    private void Close()
    {
        UIMgr.Instance.HidePanel<StartPanel>();
    }
}