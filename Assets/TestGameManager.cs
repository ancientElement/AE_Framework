using AE_Framework;
using UnityEngine;

public class TestGameManager : MonoBehaviour
{
    [SerializeField] GameRoot gameRoot;

    // Start is called before the first frame update
    void Awake()
    {
        gameRoot.Init();
    }
}
