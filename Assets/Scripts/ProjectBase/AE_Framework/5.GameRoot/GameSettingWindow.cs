using UnityEditor;
using UnityEngine;

namespace AE_Framework
{
    public class GameSettingWindow : EditorWindow
    {
        [MenuItem("Tools/AE_Framework/GameSettingWindow")]
        public static void ShowWindow()
        {
            Rect rect = new Rect(0, 0, 500, 500);
            GameSettingWindow window = GetWindow<GameSettingWindow>();
            GetWindowWithRect<GameSettingWindow>(rect);
            window.Show();
        }
    }
}