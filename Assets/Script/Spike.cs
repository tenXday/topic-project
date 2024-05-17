using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [Range(0f, 5f)]
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.gameObject.tag == "Player")
        {
            PlayboardEvent.CallHealthChange(-damage);
            other.gameObject.GetComponent<PlayerController>().ReturnSavePoint();

        }
    }
}
