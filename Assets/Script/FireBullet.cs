using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [Range(0f, 5f)]
    public float damage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {

            PlayboardEvent.CallHealthChange(-damage);
        }
        Destroy(this.gameObject);
    }
    void Update()
    {
        this.transform.Translate(0.06f, 0, 0);
    }
}
