using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGate : MonoBehaviour
{
    public int health;

    public void ReduceHealth()
    {
        --health;

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
