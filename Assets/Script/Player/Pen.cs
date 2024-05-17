using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pen : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject currentpen;
    private LineRenderer penrenderer;
    private List<Vector2> pointlist = new List<Vector2>();
    private List<Vector3> positionlist = new List<Vector3>();
    private Vector3 currentpos, basepos;
    private float ftime;
    public GameObject pental;
    void Start()
    {
        currentpos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.1f));
        transform.position = currentpos;
        basepos = transform.position;
        currentpen = Instantiate(pental, Vector3.zero, Quaternion.identity);
        penrenderer = currentpen.GetComponent<LineRenderer>();
        AddLinePoint();
    }

    // Update is called once per frame
    void Update()
    {
        ftime += Time.deltaTime;
        if (ftime >= 0.1f)
        {
            AddLinePoint();
        }

        currentpos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        transform.position = currentpos;
        //Debug.Log("Brushpos:" + transform.position);
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            currentpen.AddComponent<EdgeCollider2D>();
            currentpen.GetComponent<EdgeCollider2D>().SetPoints(pointlist);
            currentpen.GetComponent<EdgeCollider2D>().edgeRadius = 0.05f;
            // currentpen.AddComponent<Rigidbody2D>();
            // currentpen.GetComponent<Rigidbody2D>().freezeRotation = true;
            Destroy(this.gameObject);
        }
    }
    void AddLinePoint()
    {
        pointlist.Add(new Vector2(transform.position.x, transform.position.y));
        //Debug.Log("ADD New Point(" + transform.position.x + "," + transform.position.y + ")");
        positionlist.Add(new Vector3(transform.position.x, transform.position.y, 0f));
        penrenderer.positionCount = positionlist.Count;
        penrenderer.SetPositions(positionlist.ToArray());
        ftime = 0f;
    }
}
