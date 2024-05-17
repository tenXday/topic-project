using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KeyBoardShow : MonoBehaviour
{
    [SerializeField]
    GameObject A, S, D, Space, R, control;
    bool show = true;

    // Update is called once per frame
    void Update()
    {
        control.SetActive(show);
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            A.GetComponent<Image>().color = Color.gray;
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            A.GetComponent<Image>().color = Color.white;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            S.GetComponent<Image>().color = Color.gray;
        }
        else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            S.GetComponent<Image>().color = Color.white;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            D.GetComponent<Image>().color = Color.gray;
        }
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            D.GetComponent<Image>().color = Color.white;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Space.GetComponent<Image>().color = Color.gray;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Space.GetComponent<Image>().color = Color.white;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            R.GetComponent<Image>().color = Color.gray;
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            R.GetComponent<Image>().color = Color.white;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (show)
            {
                show = false;
            }
            else
            {
                show = true;
            }
        }
    }



}
