using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField] Animator SaveVFX;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("save");
            PlayboardEvent.CallSavePoint(this.transform.position);
            SaveVFX.SetBool("save", true);

        }
    }
    void VFX_End()
    {
        SaveVFX.SetBool("save", false);
    }
}
