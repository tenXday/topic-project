using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(0f, 5f)]
    public float damage;
    [SerializeField]
    GameObject SmokeVFX;
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {

            PlayboardEvent.CallHealthChange(-damage);
            Instantiate(SmokeVFX, this.gameObject.transform.position, Quaternion.identity);
        }
        if (other.name != "camera outline")
        {
            Destroy(this.gameObject);
        }

    }
}
