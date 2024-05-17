using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject EndMovie;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            EndMovie.SetActive(true);


        }


    }
}
