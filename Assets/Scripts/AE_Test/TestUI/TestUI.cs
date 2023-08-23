using UnityEngine;
using AE_Framework;
using Sirenix.OdinInspector;

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
