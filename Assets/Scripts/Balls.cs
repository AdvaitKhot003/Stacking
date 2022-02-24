using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Balls : MonoBehaviour
{
    PlayerManager playerManager;

    public bool isPlayer, isUsed;

    private void Start()
    {
        playerManager = GameObject.Find("MainPlayer").GetComponent<PlayerManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPlayer)
            return;

        if (other.gameObject.CompareTag("Ball"))
        {
            playerManager.AddSingleBall(other.gameObject);
        }

        if (other.transform.CompareTag("GreenDoor"))
        {
            playerManager.AddMultipleBalls(other.gameObject);
        }

        if (other.transform.CompareTag("RedDoor") && playerManager.list_balls.Count > 0)
        {
            playerManager.RemoveBalls(other.gameObject);
        }

        if (playerManager.list_balls.Count == 0)
        {
            MovePlayer.movePlayerInstance.startGame = false;
            Time.timeScale = 0;
        }
    }
}
