using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    GameObject cam;
    [SerializeField, Range(0, 1)]
    float parallaxEffect;
    private float startpos;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;

    }

    // Update is called once per frame
    void Update()
    {
        float dist = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
    }
}
