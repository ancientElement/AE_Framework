using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using AE_Framework;

[UIElement(false, "StartPanel", 1)]
public class StartPanel : BasePanel
{

    [SerializeField] Image AImage;
    [SerializeField] Image BImage;

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

    public override void ShowMe()
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
