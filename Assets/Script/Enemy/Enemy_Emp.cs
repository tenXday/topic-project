using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class Enemy_Emp : MonoBehaviour
{
    // Start is called before the first frame update

    //Move-----------------------------------------------------------------------
    [SerializeField]
    float speed;
    [SerializeField]
    float max_movetime;
    [SerializeField]
    float checkRadius;
    [SerializeField]
    float random_radius, min_radius;
    Vector2 followpoint, p_followpoint;

    bool isarrive;
    bool c_faceL, p_faceL;
    //---------------------------------------------------------------------------
    [SerializeField]
    LayerMask enemyLayer, playerLayer;

    [SerializeField]
    float attackRadius;
    [SerializeField]
    GameObject bullet;
    public bool attack_check;
    //Animation------------------------------------------------------------------
    [SerializeField]
    AnimationReferenceAsset attack, move;
    [SerializeField]
    SkeletonAnimation skeletonAnimation;

    //Sound----------------------------------------------------------------------
    [SerializeField]
    AudioClip attack_audioclip;
    [SerializeField]
    AudioSource AudioSource_effect;
    EnemyState currentstate, previousstate;

    //--------------------------------------------------------------------

    bool isattacking, go_attack;
    float c_movetime, c_attacktime;
    TrackEntry anitrack;
    GameObject player_G;
    bool isatk = false;
    void Start()
    {
        player_G = GameObject.FindGameObjectWithTag("Player");
        p_followpoint = this.transform.position;
        NewRandomTargetPos();
    }

    void Update()
    {

        Vector2 targetpos = followpoint;
        Vector2 checkpos = new Vector2(targetpos.x, targetpos.y);

        isarrive = Physics2D.OverlapCircle(checkpos, checkRadius, enemyLayer);
        go_attack = Physics2D.OverlapCircle(this.transform.position, attackRadius, playerLayer);

        if (go_attack)
        {
            isattacking = true;

        }


        //Move            
        if (isarrive)
        {
            NewRandomTargetPos();
            c_movetime = 0;
        }
        else
        {
            c_movetime += Time.deltaTime;
            if (c_movetime < max_movetime)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, targetpos, speed * Time.deltaTime);
            }
            else
            {
                NewRandomTargetPos();
                c_movetime = 0;
            }
        }


        //Attack
        if (!isattacking)
        {
            currentstate = EnemyState.Move;
            isatk = false;
        }
        else
        {
            currentstate = EnemyState.Attack;

            if (anitrack.AnimationTime > 0.2)
            {
                if (!isatk)
                {
                    GameObject tempbullet = Instantiate(bullet, this.transform.position, Quaternion.identity);
                    AudioSource_effect.PlayOneShot(attack_audioclip);
                    isatk = true;
                }



            }
            anitrack.Complete += delegate (TrackEntry trackEntry)
            {

                isatk = false;
            };

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

    void NewRandomTargetPos()
    {

        bool ran_x = Random.value > 0.5;
        if (ran_x)
        {
            followpoint.x = Random.Range(player_G.transform.position.x + random_radius, player_G.transform.position.x + min_radius);
        }
        else
        {
            followpoint.x = Random.Range(player_G.transform.position.x - random_radius, player_G.transform.position.x - min_radius);
        }
        //flip
        if (followpoint.x > this.transform.position.x)
        {
            c_faceL = true;

        }
        else
        {
            c_faceL = false;
        }
        if (c_faceL != p_faceL)
        {
            Flip();
        }
        p_faceL = c_faceL;


        bool ran_y = Random.value > 0.5;
        if (ran_y)
        {
            followpoint.y = Random.Range(player_G.transform.position.y + random_radius, player_G.transform.position.y + min_radius);
        }
        else
        {
            followpoint.y = Random.Range(player_G.transform.position.y - random_radius, player_G.transform.position.y - min_radius);
        }
        p_followpoint = followpoint;
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
            case EnemyState.Move:
                loop = true;
                nextAnimation = move;

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
        Move
    }
    void OnDrawGizmos()
    {
        Gizmos.color = isarrive ? Color.red : Color.blue;

        Gizmos.DrawSphere(followpoint, checkRadius);

        Gizmos.color = go_attack ? Color.red : Color.green;
        if (attack_check)
        {
            Gizmos.DrawSphere(this.transform.position, attackRadius);
        }

    }
}
