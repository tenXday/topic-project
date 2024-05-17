using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Out");
            PlayboardEvent.CallHealthChange(-1);
            other.gameObject.GetComponent<PlayerController>().ReturnSavePoint();

        }
    }

}
