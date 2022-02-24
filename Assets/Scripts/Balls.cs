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
        if (isPlayer)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                if (!other.GetComponent<Balls>().isUsed)
                {
                    other.GetComponent<Balls>().isUsed = true;
                    playerManager.AddSingleBall(other.gameObject);
                }
            }

            if (other.transform.CompareTag("GreenDoor"))
            {
                playerManager.AddMultipleBalls(other.GetComponent<GreenGate>().health);
            }
        }

        if (other.transform.CompareTag("RedDoor"))
        {
            playerManager.RemoveBalls(1);
            other.gameObject.GetComponent<RedGate>().ReduceHealth();
        }

        //if (playerManager.list_balls.Count == 0)
        //{
        //    MovePlayer.movePlayerInstance.startGame = false;
        //    Time.timeScale = 0;
        //}
    }
}
