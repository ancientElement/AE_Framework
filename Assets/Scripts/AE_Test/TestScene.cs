using AE_Framework;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScene : MonoBehaviour
{
    [SerializeField] string scene;
    [SerializeField] LoadSceneMode loadSceneMode;


    [Button]
    private  void LoadNextScene()
    {
        SceneMgr.LoadScene(scene, loadSceneMode);
    }
}
