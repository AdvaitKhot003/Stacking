using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<Transform> list_balls = new List<Transform>();

    [SerializeField]
    private float ballDistance = 10f;

    public GameObject newBall;

    public Color playerColor;

    [SerializeField]
    private GameObject firstBall;

    private void Start()
    {
        list_balls.Add(firstBall.transform);
    }

    private void Update()
    {
        if (list_balls.Count > 1)
        {
            for (int i = 1; i < list_balls.Count; i++)
            {
                Transform front = list_balls[i - 1];
                Transform back = list_balls[i];

                float desiredDistance = Vector3.Distance(back.position, front.position);

                if (desiredDistance <= ballDistance)
                {
                    back.position = new Vector3(Mathf.Lerp(
                        back.position.x, front.position.x, 10 * Time.deltaTime),
                        back.position.y,
                        Mathf.Lerp(back.position.z, front.position.z - 0.5f, 10 * Time.deltaTime));

                    //secondBall.position = new Vector3(Mathf.Lerp(
                    // secondBall.position.x, firstBall.position.x, 10 * Time.deltaTime),
                    // secondBall.position.y,
                    // firstBall.position.z - 0.5f);

                    //secondBall.position = new Vector3(
                    //    secondBall.position.x,
                    //    secondBall.position.y,
                    //    Mathf.Lerp(secondBall.position.z, firstBall.position.z - 0.5f, 10 * Time.deltaTime));
                }
            }
        }
    }

    public void AddSingleBall(GameObject ballToAdd)
    {
        Debug.Log("ballToAdd "+ ballToAdd.name);
        ballToAdd.transform.parent = null;
   
        ballToAdd.GetComponent<Renderer>().material.color = playerColor;
        list_balls.Add(ballToAdd.transform);

        Debug.Log("Adding single ball");
    }

    public void AddMultipleBalls(GameObject obj)
    {
        Int16 addBalls = Int16.Parse(obj.transform.GetChild(0).name);

        for (int i = 0; i < addBalls; i++)
        {
            GameObject ball = Instantiate(newBall, list_balls[list_balls.Count - 1].position +
                 new Vector3(0f, 0f, -0.5f), Quaternion.identity);

            list_balls.Add(ball.transform);
        }

        obj.GetComponent<Collider>().enabled = false;

        Debug.Log("Adding multiple balls");
    }

    public void RemoveBalls(int ballsToRemove)
    {
        list_balls[0].gameObject.SetActive(false);
        list_balls.RemoveAt(0);
        list_balls[0].transform.SetParent(transform);
        list_balls[0].GetComponent<Balls>().isPlayer = true;

        Debug.Log("removing ball");
    }
}
