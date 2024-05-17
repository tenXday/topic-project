using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 currentpos;
    RaycastHit2D hit;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentpos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.1f));
        gameObject.transform.position = currentpos;
        hit = Physics2D.Raycast(currentpos, new Vector2(0, 0), 1.0f, LayerMask.GetMask("Ground"));

        if (hit)
        {
            //Debug.Log(hit.transform.name);
            //Select stage    
            if (hit.transform.tag == "Line")
            {
                Destroy(hit.collider.gameObject);
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Destroy(this.gameObject);
        }
    }
}
