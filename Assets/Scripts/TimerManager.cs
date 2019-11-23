using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager instance;

    private void Start()
    {
        instance = this;
    }
    public IEnumerator Respawn(Player player, float delay)
    {
        print("Respawn coroutine called");
        yield return new WaitForSeconds(delay);
        print("Respawn delay over");
        player.Spawn();
        print("Respawn coroutine exited");
    }
}
