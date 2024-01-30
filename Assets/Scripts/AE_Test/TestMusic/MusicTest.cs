using AE_Framework;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class MusicTest : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioClip bkClip;

    private void Start()
    {
        MusicMgr.Instance.ChageBKVolume(1);
    }

    private void OnGUI()
    {
        var style = new GUIStyle("Button");
        style.fontSize = 30;
        float heigth = 0;
        if (GUI.Button(new Rect(0, 0, 200, 100), "播放背景音乐", style))
        {
            MusicMgr.Instance.PlayBKMusic(bkClip);
        }
        heigth += 100;
        if (GUI.Button(new Rect(0, heigth, 200, 100), "暂停背景音乐", style))
        {
            MusicMgr.Instance.PauseBKMusic();
        }
        heigth += 100;
        if (GUI.Button(new Rect(0, heigth, 200, 100), "停止背景音乐", style))
        {
            MusicMgr.Instance.StopBkMusic();
        }
        heigth += 100;
        if (GUI.Button(new Rect(0, heigth, 200, 100), "播放音效", style))
        {
            source = MusicMgr.Instance.PlaySoundMusic(clip);
        }
        heigth += 100;
        if (GUI.Button(new Rect(0, heigth, 200, 100), "停止音效", style))
        {
            source?.Stop();
        }
    }
}