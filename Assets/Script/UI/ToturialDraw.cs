using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToturialDraw : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Next;
    void OnDisable()
    {
        Next.SetActive(true);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
