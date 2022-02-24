using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
//        if (other.gameObject.CompareTag("Ball"))
//        {
//            other.transform.parent = null;
//            other.gameObject.AddComponent<Rigidbody>().isKinematic = true;
//            other.gameObject.AddComponent<StackManager>();
//            other.gameObject.GetComponent<Collider>().isTrigger = true;
//            other.tag = gameObject.tag;
//            other.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
////            PlayerManager.playerManagerInstance.list_balls.Add(other.transform);
//        }

        //if (other.transform.CompareTag("GreenDoor"))
        //{
        //    Int16 addBalls = Int16.Parse(other.transform.GetChild(0).name);

        //    for (int i = 0; i < addBalls; i++)
        //    {
        //        GameObject ball = Instantiate(PlayerManager.playerManagerInstance.newBall,
        //            PlayerManager.playerManagerInstance.list_balls.ElementAt(PlayerManager.playerManagerInstance.list_balls.Count - 1).position +
        //             new Vector3(0f, 0f, 0.5f), Quaternion.identity);

        //        PlayerManager.playerManagerInstance.list_balls.Add(ball.transform);
        //    }

        //    other.GetComponent<Collider>().enabled = false;
        //}

        //if (other.transform.CompareTag("Red"))
        //{
        //    Int16 subBalls = Int16.Parse(other.transform.GetChild(0).name);

        //    if (PlayerManager.playerManagerInstance.balls.Count > subBalls)
        //    {
        //        for (int i = 0; i < subBalls; i++)
        //        {
        //            PlayerManager.playerManagerInstance.balls.ElementAt(PlayerManager.playerManagerInstance.balls.Count - 1).
        //                gameObject.SetActive(false);
        //        }
        //    }
        //}
    }
}
