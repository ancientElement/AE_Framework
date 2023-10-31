using AE_Framework;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName = "9521";

    private void Start()
    {
        Invoke(nameof(PlayerDie), 1);
    }

    public void PlayerDie()
    {
        EventCenter.TriggerEvent("PLAYER_DIE");
    }
}