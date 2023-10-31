using AE_Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[UIElement(false, "StartPanel", 1)]
public class StartPanel : BasePanel
{
    [SerializeField] private Image AImage;
    [SerializeField] private Image BImage;

    protected void Awake()
    {
        FindAllControl();
    }

    private void Start()
    {
        AddCustomEventListener();
    }

    private void AddCustomEventListener()
    {
        UIMgr.AddCustomEventListener(GetControl<Button>("btnStart"), EventTriggerType.PointerEnter, (res) =>
        {
            Debug.Log("进入");
        });
        UIMgr.AddCustomEventListener(GetControl<Button>("btnStart"), EventTriggerType.PointerExit, (res) =>
        {
            Debug.Log("退出");
        });
    }

    public override void ShowMe(params object[] args)
    {
        base.ShowMe();
    }

    protected override void OnColick(string btnName)
    {
        base.OnColick(btnName);
        switch (btnName)
        {
            case "btnStart":
                Debug.Log("start");
                break;

            case "btnQuit":
                Debug.Log("quit");
                break;
        }
    }
}