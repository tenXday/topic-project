using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;


public class PlayerView : MonoBehaviour
{
    public float runani_speed = 1f;
    float currentspeed;
    public PlayerModel model;
    public SkeletonAnimation skeletonAnimation;
    PlayerModel.PlayerState previousViewState;
    public AnimationReferenceAsset run, idle, jump, inair, fall, squat, crawl, dead, skill_idle, skill_f, skilling;
    public bool isjump = false;

    [SerializeField]
    Color DamageVFX_color;
    [SerializeField]
    float D_vfx_sec;
    // Start is called before the first frame update
    void OnEnable()
    {
        PlayboardEvent._Healthchangeeffect += HpChangeVFX;
    }
    void OnDisable()
    {
        PlayboardEvent._Healthchangeeffect -= HpChangeVFX;
    }
    void Start()
    {
        currentspeed = skeletonAnimation.timeScale;

        PlayNewStableAnimation();
        if (!model.facingLeft)
        {
            Turn(model.facingLeft);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (skeletonAnimation == null) return;
        if (model == null) return;
        if ((skeletonAnimation.skeleton.ScaleX < 0) != model.facingLeft)
        {
            Turn(model.facingLeft);
        }
        var currentModelState = model.state;

        if (previousViewState != currentModelState)
        {
            PlayNewStableAnimation();
            previousViewState = currentModelState;
        }



    }
    void HpChangeVFX(float health)
    {
        if (health < 0)
        {
            StartCoroutine(HPRoutine());
        }

    }
    IEnumerator HPRoutine()
    {
        skeletonAnimation.Skeleton.SetColor(DamageVFX_color);
        yield return new WaitForSeconds(D_vfx_sec);
        skeletonAnimation.Skeleton.SetColor(Color.white);
    }
    public void Turn(bool facingLeft)
    {
        skeletonAnimation.Skeleton.ScaleX = facingLeft ? -1f : 1f;
        // Maybe play a transient turning animation too, then call ChangeStableAnimation.
    }
    public void PlayNewStableAnimation()
    {
        var newModelState = model.state;
        Spine.Animation nextAnimation = null;
        bool loop = true;
        switch (newModelState)
        {
            case PlayerModel.PlayerState.Jump:
                nextAnimation = jump;
                loop = false;
                break;
            case PlayerModel.PlayerState.InAir:
                nextAnimation = inair;
                loop = true;
                break;
            case PlayerModel.PlayerState.Fall:
                loop = false;
                nextAnimation = fall;
                break;
            case PlayerModel.PlayerState.Running:
                loop = true;
                nextAnimation = run;
                skeletonAnimation.timeScale = runani_speed;
                break;
            case PlayerModel.PlayerState.Squat:
                loop = false;
                nextAnimation = squat;
                break;
            case PlayerModel.PlayerState.Crawl:
                loop = true;
                nextAnimation = crawl;
                break;
            case PlayerModel.PlayerState.Idle:
                loop = true;
                nextAnimation = idle;
                skeletonAnimation.timeScale = currentspeed;
                break;
            case PlayerModel.PlayerState.Dead:
                loop = false;
                nextAnimation = dead;
                break;

            case PlayerModel.PlayerState.Skill:
                loop = true;
                nextAnimation = skill_idle;
                break;
            case PlayerModel.PlayerState.Skill_l:
                loop = true;
                nextAnimation = skilling;
                break;

        }
        if (newModelState == PlayerModel.PlayerState.Skill)
        {
            var testtrack = skeletonAnimation.AnimationState.SetAnimation(0, skill_f, false);
            testtrack.Complete += delegate (TrackEntry trackEntry)
            {
                skeletonAnimation.AnimationState.SetAnimation(0, nextAnimation, loop);
            };
        }
        else if (newModelState == PlayerModel.PlayerState.Jump)
        {

            var testtrack = skeletonAnimation.AnimationState;
            testtrack.Start += delegate (TrackEntry trackEntry)
            {
                isjump = true;

            };
            skeletonAnimation.AnimationState.SetAnimation(0, nextAnimation, loop);
            testtrack.Complete += delegate (TrackEntry trackEntry)
            {

                isjump = false;

            };


        }
        else if (newModelState == PlayerModel.PlayerState.Fall)
        {
            var testtrack = skeletonAnimation.AnimationState;
            skeletonAnimation.AnimationState.SetAnimation(0, nextAnimation, loop);
            testtrack.Complete += delegate (TrackEntry trackEntry)
            {
                model.jumping = false;
                model.is_inair = false;
            };
        }
        else if (newModelState == PlayerModel.PlayerState.Dead)
        {
            var testtrack = skeletonAnimation.AnimationState.SetAnimation(0, nextAnimation, loop);
            testtrack.Complete += delegate (TrackEntry trackEntry)
            {
                PlayboardEvent.CallPlayerRelive();
            };
        }
        else
        {
            skeletonAnimation.AnimationState.SetAnimation(0, nextAnimation, loop);
        }

    }



}

