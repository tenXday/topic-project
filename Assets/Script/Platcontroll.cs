using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platcontroll : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    MPlatform Current_P;
    [SerializeField]


    public bool controllable;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (controllable)
        {

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.gameObject.tag == "Player")
        {
            Current_P.Move_switch();
            controllable = true;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Current_P.Move_switch();
            controllable = false;


        }
    }

}
