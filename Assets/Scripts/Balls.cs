using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Balls : MonoBehaviour
{
    PlayerManager playerManager;

    public bool isPlayer, isUsed, isDestroyed;

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

        if (other.transform.CompareTag("RedDoor") && !isDestroyed)
        {
            Debug.Log("destroying");
            isDestroyed = true;
            playerManager.RemoveBalls();
            other.gameObject.GetComponent<RedGate>().ReduceHealth();
            playerManager.counter++;
        }

        //if (playerManager.list_balls.Count == 0)
        //{
        //    MovePlayer.movePlayerInstance.startGame = false;
        //    Time.timeScale = 0;
        //}
    }
}
