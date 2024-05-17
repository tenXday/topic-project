using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    void Destroyself()
    {
        Destroy(this.gameObject);
    }
}
