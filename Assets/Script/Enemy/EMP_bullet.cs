using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMP_bullet : MonoBehaviour
{
    [Range(0f, 5f)]
    public float damage;
    [SerializeField]
    float speed;
    [SerializeField]
    float distroytime;
    float c_staytime;
    GameObject player_G;
    Vector3 targetdir;
    // Update is called once per frame
    void Start()
    {
        player_G = GameObject.FindGameObjectWithTag("Player");
        targetdir = (player_G.gameObject.transform.position - this.gameObject.transform.position).normalized;
        //Debug.Log(targetdir);
    }
    void Update()
    {
        c_staytime += Time.deltaTime;
        if (c_staytime > distroytime)
        {
            Destroy(this.gameObject);
        }
        this.gameObject.transform.position += targetdir * speed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {

            PlayboardEvent.CallHealthChange(-damage);
            Destroy(this.gameObject);

        }
    }
}
