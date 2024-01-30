using AE_Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[UIElement("StartPanel", 1)]
public class StartPanel : BasePanel
{
    [SerializeField] private Image AImage;
    [SerializeField] private Image BImage;
    [SerializeField] private Button btnStart;
    [SerializeField] private Button btnExit;

    public void Print(string message)
    {
        print(message);
    }
}