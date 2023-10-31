using AE_Framework;
using UnityEngine;

public class MusicTest : MonoBehaviour
{
    private AudioSource source;
    [SerializeField, Range(0, 1)] private float spatialBlend;

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
            MusicMgr.Instance.PlayBKMusic("BK1");
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
            MusicMgr.Instance.PlaySoundMusic("13_Ice_explosion_01", Vector3.right * 0.5f, (res) =>
            {
                source = res;
            }, spatialBlend: spatialBlend);
        }
        heigth += 100;
        if (GUI.Button(new Rect(0, heigth, 200, 100), "停止音效", style))
        {
            MusicMgr.Instance.StopSoundMusic(source);
        }
        heigth += 100;
        if (GUI.Button(new Rect(0, heigth, 200, 100), "清楚音效", style))
        {
            MusicMgr.Instance.ClearAllAudioSource();
        }
    }
}