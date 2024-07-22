using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleEnemy : Enemy
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.LogFormat("Hit Player: {0}!!!", collision.gameObject.name);
            Die();
        }
    }
}
