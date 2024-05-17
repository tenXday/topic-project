using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public float bossHp;
    // Start is called before the first frame update
    [SerializeField]
    List<GameObject> followpoint;
    [SerializeField]
    float speed;
    [SerializeField]
    float checkRadius;
    [SerializeField]
    LayerMask enemyLayer;
    int currentfollow = 0;
    bool isarrive;
    void Start()
    {

    }

    void Update()
    {
        Vector3 targetpos = followpoint[currentfollow].transform.position;
        isarrive = Physics2D.OverlapCircle(targetpos, checkRadius, enemyLayer);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetpos, speed * Time.deltaTime);
        if (isarrive)
        {
            currentfollow++;
            Flip();
            if (currentfollow > followpoint.Count - 1)
            {
                currentfollow = 0;

            }
            //Debug.Log(currentfollow);
        }
    }
    void Flip()
    {
        transform.Rotate(-0, -180, 0);
    }
}
