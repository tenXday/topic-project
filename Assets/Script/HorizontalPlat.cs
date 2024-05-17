using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlat : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.gameObject.tag == "Player")
        {

            other.gameObject.transform.SetParent(this.transform);
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.SetParent(null);


        }
    }
}
