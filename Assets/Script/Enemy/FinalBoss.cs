using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    [SerializeField, Range(0f, 5f)]
    float damage;
    [SerializeField]
    float attackRate;
    [SerializeField]
    GameObject bullet, muz;
    float nextAttack;
    Vector3 currentsavepoint;
    Camera MainCamera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Attacking();

    }
    private void OnCollisionEnter2D(Collision2D other)
    {


        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("hit");
            currentsavepoint = new Vector3(this.transform.position.x + 10, this.transform.position.y, this.transform.position.z);
            PlayboardEvent.CallSavePoint(currentsavepoint);
            other.gameObject.GetComponent<PlayerController>().ReturnSavePoint();
            PlayboardEvent.CallHealthChange(-damage);
        }
    }
    void Attacking()
    {
        this.transform.Translate(0.025f, 0, 0);
        if (Time.time > nextAttack)
        {
            nextAttack = Time.time + attackRate;
            Shoot();
        }
    }
    void Shoot()
    {
        Debug.Log("Shoot!");
        GameObject tempbullet = Instantiate(bullet, muz.transform.position, Quaternion.identity);

    }
}
