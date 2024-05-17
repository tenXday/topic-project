using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<GameObject> followpoint;

    [SerializeField]
    float checkRadius;
    [SerializeField]
    LayerMask platLayer;
    [SerializeField]
    float speed;
    bool isarrive;
    [SerializeField]
    bool canmove;
    bool isstop;
    int currentfollow;
    [SerializeField]
    PlatStat plat_stat;
    [SerializeField]
    GameObject PlatView;
    void Start()
    {
        canmove = false;
        isstop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canmove)
        {
            isstop = false;
            Vector2 targetpos = followpoint[currentfollow].transform.position;

            Vector2 checkpos = new Vector2(targetpos.x, targetpos.y);
            isarrive = Physics2D.OverlapCircle(checkpos, checkRadius, platLayer);

            this.transform.position = Vector2.MoveTowards(this.transform.position, targetpos, speed * Time.deltaTime);

            /*if (isarrive)
            {
                currentfollow++;

                if (currentfollow > followpoint.Count - 1)
                {
                    currentfollow = 0;

                }

            }*/
        }
        else
        {


            if (!isstop)
            {
                Vector2 targetpos = followpoint[1].transform.position;
                Vector2 checkpos = new Vector2(targetpos.x, targetpos.y);
                isarrive = Physics2D.OverlapCircle(checkpos, checkRadius, platLayer);
                this.transform.position = Vector2.MoveTowards(this.transform.position, targetpos, speed * Time.deltaTime);

            }

            if (isarrive)
            {
                isstop = true;
                currentfollow = 0;

            }



        }

    }
    public void Move_switch()
    {
        canmove = !canmove;
        if (plat_stat == PlatStat.Horizontal)
        {
            PlatView.transform.Rotate(0, -180, 0);
        }
        else if (plat_stat == PlatStat.Vertical)
        {
            PlatView.transform.Rotate(180, 0, 0);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = isarrive ? Color.red : Color.green;
        for (int i = 0; i < followpoint.Count; i++)
        {
            Gizmos.DrawSphere(followpoint[i].transform.position, checkRadius);
        }
    }
    public enum PlatStat
    {
        Horizontal,
        Vertical
    }
}
