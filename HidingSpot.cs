using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HidingSpot : MonoBehaviour
{

    bool isSafe;
    Transform position;

    public bool CanHide(PlayerManager player)
    {
        if (player.isHiding)
        {
            return false;
        }
        if (!isSafe)
        {
            return false;
        }

        return true;
    }

    public void Enter(PlayerManager player)
    {
        player.isHiding = true;
        player.currentHidingSpot = this.transform;
    }

    public void Exit(PlayerManager player)
    {
        player.isHiding = false;
        player.currentHidingSpot = null;
    }

}
