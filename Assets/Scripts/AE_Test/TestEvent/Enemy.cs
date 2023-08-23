using AE_Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    private void Awake()
    {
        EventCenter.Instance.AddEventListener("PLAYER_DIE", PlayerDie);
    }

    public void PlayerDie()
    {
        Debug.Log(gameObject.name);
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("PLAYER_DIE", PlayerDie);
    }
}
