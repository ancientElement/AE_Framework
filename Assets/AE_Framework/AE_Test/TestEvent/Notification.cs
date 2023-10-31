using AE_Framework;
using UnityEngine;

public class Notification : MonoBehaviour
{
    private void Awake()
    {
        EventCenter.AddEventListener("PLAYER_DIE", PlayerDie);
    }

    public void PlayerDie()
    {
        Debug.Log(gameObject.name);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveEventListener("PLAYER_DIE", PlayerDie);
    }
}