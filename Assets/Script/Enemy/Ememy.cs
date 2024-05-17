using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class Ememy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<GameObject> followpoint;
    [SerializeField]
    float speed;
    [SerializeField]
    float staytime, blocktime;
    [SerializeField]
    float checkRadius;
    [SerializeField]
    float attackRadius;
    [SerializeField]
    LayerMask enemyLayer, playerLayer;
    //------------------------------------------------------------------
    [SerializeField]
    AnimationReferenceAsset attack, run, idle;
    [SerializeField]
    SkeletonAnimation skeletonAnimation;
    [SerializeField]
    GameObject Attack_c, Weapon;
    //--------------------------------------------------------------------
    [SerializeField]
    AudioClip attack_audioclip;
    [SerializeField]
    AudioSource AudioSource_effect;
    //--------------------------------------------------------------------

    public bool attack_check;

    EnemyState currentstate, previousstate;

    int currentfollow = 0;
    bool isarrive, isstay;

    bool isattacking, go_attack;
    float c_staytime, c_attacktime;
    TrackEntry anitrack;
    bool audioplay = false;
    void Start()
    {

    }

    void Update()
    {

        Vector2 targetpos = followpoint[currentfollow].transform.position;
        Vector2 checkpos = new Vector2(targetpos.x, targetpos.y);

        isarrive = Physics2D.OverlapCircle(checkpos, checkRadius, enemyLayer);
        go_attack = Physics2D.OverlapCircle(Attack_c.transform.position, attackRadius, playerLayer);

        if (go_attack)
        {
            isattacking = true;

        }

        if (!isattacking)
        {
            Weapon.SetActive(false);
            if (!isstay)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, targetpos, speed * Time.deltaTime);
                currentstate = EnemyState.Run;
                c_staytime = 0;
            }
            if (isarrive)
            {
                isstay = true;
                c_staytime += Time.deltaTime;
                if (c_staytime < blocktime)
                {
                    this.transform.position = Vector2.MoveTowards(this.transform.position, targetpos, speed * Time.deltaTime);
                }
                else
                {
                    currentstate = EnemyState.Idle;

                }

                if (c_staytime > staytime + blocktime)
                {
                    currentfollow++;
                    isstay = false;
                    Flip();
                }
                if (currentfollow > followpoint.Count - 1)
                {
                    currentfollow = 0;

                }
            }

        }
        else
        {
            currentstate = EnemyState.Attack;
            if (anitrack.AnimationTime > 0.6)
            {
                Weapon.SetActive(true);
                if (!audioplay)
                {
                    AudioSource_effect.PlayOneShot(attack_audioclip);
                    audioplay = true;
                }

            }
            else
            {
                Weapon.SetActive(false);
                audioplay = false;
            }
        }

        if (previousstate != currentstate)
        {
            PlayNewStableAnimation();
            previousstate = currentstate;
        }


    }
    void Flip()
    {
        transform.Rotate(0, -180, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {

            PlayboardEvent.CallHealthChange(-1);
        }



    }

    public void PlayNewStableAnimation()
    {
        var newModelState = currentstate;
        Spine.Animation nextAnimation = null;
        bool loop = true;
        switch (newModelState)
        {
            case EnemyState.Attack:
                nextAnimation = attack;
                loop = true;
                break;
            case EnemyState.Run:
                loop = true;
                nextAnimation = run;

                break;
            case EnemyState.Idle:
                loop = true;
                nextAnimation = idle;

                break;

        }
        anitrack = skeletonAnimation.AnimationState.SetAnimation(0, nextAnimation, loop);
        if (newModelState == EnemyState.Attack)
        {

            anitrack.Complete += delegate (TrackEntry trackEntry)
            {

                isattacking = false;
            };

        }



    }
    public enum EnemyState
    {
        Attack,
        Run,
        Idle
    }
    void OnDrawGizmos()
    {
        Gizmos.color = isarrive ? Color.red : Color.green;
        for (int i = 0; i < followpoint.Count; i++)
        {
            Gizmos.DrawSphere(followpoint[i].transform.position, checkRadius);
        }
        Gizmos.color = go_attack ? Color.red : Color.green;
        if (attack_check)
        {
            Gizmos.DrawSphere(Attack_c.transform.position, attackRadius);
        }

    }
}
