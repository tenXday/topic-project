using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    [SerializeField]
    AudioSource AudioSource_effect;
    [SerializeField]
    AudioClip collect_audioclip;

    [SerializeField]
    GameObject Pen_head;
    Vector2 target_pos;
    Animator Collect_ani;
    [SerializeField] bool fly = true;
    void Start()
    {
        Collect_ani = this.GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            fly = !fly;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioSource_effect.PlayOneShot(collect_audioclip);

            this.GetComponent<BoxCollider2D>().enabled = false;
            if (fly)
            {
                InvokeRepeating("CollectVFX", 0, 0.01f);

            }
            else
            {
                Collect_ani.SetBool("collect", true);
            }



        }
    }
    public void CollectVFXEnd()
    {

        Destroy(this.gameObject);

    }
    void CollectVFX()
    {
        target_pos = Camera.main.ScreenToWorldPoint(Pen_head.transform.position);

        this.transform.position = Vector2.MoveTowards(this.transform.position, target_pos, 5 * Time.deltaTime);
        if (Vector3.Distance(transform.position, target_pos) < 0.001f)
        {

            PlayboardEvent.CallCollectInk();
            CancelInvoke();
            Destroy(this.gameObject);
        }
    }


}
