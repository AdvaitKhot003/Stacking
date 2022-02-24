using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager playerManagerInstance;
     
    public List<Transform> balls = new List<Transform>();

    [SerializeField]
    private float ballDistance = 10f;

    public GameObject newBall;

    private void Awake()
    {
        playerManagerInstance = this;
    }

    private void Start()
    {
        balls.Add(transform);
    }

    private void Update()
    {
        if (balls.Count > 1)
        {
            for (int i = 1; i < balls.Count; i++)
            {
                Transform firstBall = balls.ElementAt(i - 1);
                Transform secondBall = balls.ElementAt(i);

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            other.transform.parent = null;
            other.gameObject.AddComponent<Rigidbody>().isKinematic = true;
            other.gameObject.AddComponent<StackManager>();
            other.gameObject.GetComponent<Collider>().isTrigger = true;
            other.tag = gameObject.tag;
            other.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
            balls.Add(other.transform);
        }

        if (other.transform.CompareTag("Green"))
        {
            Int16 addBalls = Int16.Parse(other.transform.GetChild(0).name);

            for (int i = 0; i < addBalls; i++)
            {
                GameObject ball = Instantiate(newBall, balls.ElementAt(balls.Count - 1).position +
                     new Vector3(0f, 0f, -0.5f), Quaternion.identity);

                balls.Add(ball.transform);
            }

            other.GetComponent<Collider>().enabled = false;
        }

        if (other.transform.CompareTag("Red") && balls.Count > 0)
        {
            balls.ElementAt(0).gameObject.SetActive(false);
            balls.RemoveAt(0);
            balls.ElementAt(0).transform.SetParent(MovePlayer.movePlayerInstance.transform);
        }

        if(balls.Count == 0)
        {
            MovePlayer.movePlayerInstance.startGame = false;
            Time.timeScale = 0;
        }
    }
}
