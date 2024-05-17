using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoss : MonoBehaviour
{
    [SerializeField]
    GameObject bullet, muz;
    [SerializeField]
    float fireRate = 1;
    [SerializeField]
    float bulletspeed = 10;
    [SerializeField]
    List<GameObject> followpoint;
    [SerializeField]
    float movespeed;
    [SerializeField]
    float checkRadius;
    [SerializeField]
    LayerMask objectLayer;
    int currentfollow = 0;
    bool isarrive;
    float nextFire = 0;
    // Start is called before the first frame update

    void Update()
    {
        Vector3 targetpos = followpoint[currentfollow].transform.position;

        isarrive = Physics2D.OverlapCircle(targetpos, checkRadius, objectLayer);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetpos, movespeed * Time.deltaTime);
        if (isarrive)
        {
            currentfollow++;

            if (currentfollow > followpoint.Count - 1)
            {
                currentfollow = 0;

            }

        }
    }
    public void Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Debug.Log("Shoot!");
            GameObject tempbullet = Instantiate(bullet, muz.transform.position, Quaternion.identity);
            tempbullet.GetComponent<Rigidbody2D>().velocity = Vector2.down * bulletspeed;

        }
    }
}
