using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager playerManagerInstance;
     
    public List<Transform> list_balls = new List<Transform>();

    [SerializeField]
    private float ballDistance = 10f;

    public GameObject newBall;

    public Color playerColor;

    private void Awake()
    {
        playerManagerInstance = this;
    }

    private void Start()
    {
        list_balls.Add(transform);
    }

    private void Update()
    {
        if (list_balls.Count > 1)
        {
            for (int i = 1; i < list_balls.Count; i++)
            {
                Transform firstBall = list_balls.ElementAt(i - 1);
                Transform secondBall = list_balls.ElementAt(i);

                float desiredDistance = Vector3.Distance(secondBall.position, firstBall.position);

                if (desiredDistance <= ballDistance)
                {
                    secondBall.position = new Vector3(Mathf.Lerp(
                        secondBall.position.x, firstBall.position.x, 10 * Time.deltaTime),
                        secondBall.position.y,
                        Mathf.Lerp(secondBall.position.z, firstBall.position.z - 0.5f, 10 * Time.deltaTime));
                }
            }
        }
    }

    public void AddSingleBall(GameObject obj)
    {
        obj.transform.parent = null;
        obj.AddComponent<Rigidbody>().isKinematic = true;
        obj.AddComponent<StackManager>();
        obj.GetComponent<Collider>().isTrigger = true;
        obj.tag = gameObject.tag;
        obj.GetComponent<Renderer>().material.color = playerColor;
        list_balls.Add(obj.transform);
    }

    public void AddMultipleBalls(GameObject obj)
    {
        Int16 addBalls = Int16.Parse(obj.transform.GetChild(0).name);

        for (int i = 0; i < addBalls; i++)
        {
            GameObject ball = Instantiate(newBall, list_balls.ElementAt(list_balls.Count - 1).position +
                 new Vector3(0f, 0f, -0.5f), Quaternion.identity);

            list_balls.Add(ball.transform);
        }

        obj.GetComponent<Collider>().enabled = false;
    }

    public void RemoveBalls(GameObject obj)
    {
        list_balls.ElementAt(0).gameObject.SetActive(false);
        list_balls.RemoveAt(0);
        list_balls.ElementAt(0).transform.SetParent(MovePlayer.movePlayerInstance.transform);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Ball"))
    //    {
    //        other.transform.parent = null;
    //        other.gameObject.AddComponent<Rigidbody>().isKinematic = true;
    //        other.gameObject.AddComponent<StackManager>();
    //        other.gameObject.GetComponent<Collider>().isTrigger = true;
    //        other.tag = gameObject.tag;
    //        other.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
    //        list_balls.Add(other.transform);
    //    }

    //    if (other.transform.CompareTag("Green"))
    //    {
    //        Int16 addBalls = Int16.Parse(other.transform.GetChild(0).name);

    //        for (int i = 0; i < addBalls; i++)
    //        {
    //            GameObject ball = Instantiate(newBall, list_balls.ElementAt(list_balls.Count - 1).position +
    //                 new Vector3(0f, 0f, -0.5f), Quaternion.identity);

    //            list_balls.Add(ball.transform);
    //        }

    //        other.GetComponent<Collider>().enabled = false;
    //    }

    //    if (other.transform.CompareTag("Red") && list_balls.Count > 0)
    //    {
    //        list_balls.ElementAt(0).gameObject.SetActive(false);
    //        list_balls.RemoveAt(0);
    //        list_balls.ElementAt(0).transform.SetParent(MovePlayer.movePlayerInstance.transform);
    //    }

    //    if(list_balls.Count == 0)
    //    {
    //        MovePlayer.movePlayerInstance.startGame = false;
    //        Time.timeScale = 0;
    //    }
    //}
}
