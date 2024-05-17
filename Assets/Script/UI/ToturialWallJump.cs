using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToturialWallJump : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] bool RE;
    bool isstay = false;

    void Start()
    {
        if (RE)
        {
            PlayerPrefs.SetInt(gameObject.name, 0);
        }
        if (PlayerPrefs.GetInt(gameObject.name) == 1)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
    void Update()
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && Input.GetKey(KeyCode.Space) && isstay)
        {
            Debug.Log(gameObject.name);
            PlayerPrefs.SetInt(gameObject.name, 1);
            this.gameObject.SetActive(false);
        }

    }
    private void OnTriggerStay2D(Collider2D other)
    {


        if (other.gameObject.tag == "Player")
        {
            isstay = true;
        }
    }
}