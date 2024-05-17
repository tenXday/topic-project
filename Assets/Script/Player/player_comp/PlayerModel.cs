using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerModel : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerState state;
    public PlayerController p_con;
    public bool facingLeft;
    [Range(-1f, 1f)]
    public float currentSpeed;
    [SerializeField] PlayerView p_view;
    [SerializeField]
    AudioSource AudioSource_effect;
    [SerializeField]
    AudioClip jump_SFX;
    [SerializeField]
    AudioClip run_SFX;

    Rigidbody2D playerRigidbody2D;
    public bool jumping = false;
    public bool is_inair = false;
    Vector3 ClassSavePoint;
    PlayerStatus p_status;
    void OnEnable()
    {
        PlayboardEvent._PlayerRelive += OverDead;
    }
    void OnDisable()
    {
        PlayboardEvent._PlayerRelive -= OverDead;
    }
    void Start()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        facingLeft = true;
        state = PlayerState.Idle;
    }
    void Update()
    {
        if (!p_con.isgrounded && !p_view.isjump && !p_con.isdead && state != PlayerState.Idle)
        {
            state = PlayerState.InAir;
            is_inair = true;
        }
        else if (p_con.isgrounded && state == PlayerState.InAir)
        {
            state = PlayerState.Fall;

        }

    }
    public void TryMove(float speed)
    {

        if (speed != 0)
        {
            bool speedIsNegative = (speed > 0f);
            facingLeft = speedIsNegative; // Change facing direction whenever speed is not 0.
        }
        if (state != PlayerState.Running)
        {
            //Debug.Log("Other");
            AudioSource_effect.clip = null;
            if (!jumping)
            {
                AudioSource_effect.Stop();
            }
        }
        if (state != PlayerState.Dead && !jumping && p_con.isgrounded && !is_inair)
        {

            currentSpeed = speed; // show the "speed" in the Inspector.
            if (state != PlayerState.Skill)
            {
                state = (speed == 0) ? PlayerState.Idle : PlayerState.Running;
                if (state == PlayerState.Running && !AudioSource_effect.isPlaying)
                {
                    AudioSource_effect.clip = run_SFX;
                    //Debug.Log("Running");
                    AudioSource_effect.Play();
                }

            }
        }



    }
    public void TrySquat(float speed)
    {
        currentSpeed = speed; // show the "speed" in the Inspector.

        if (speed != 0)
        {
            state = PlayerState.Crawl;
            bool speedIsNegative = (speed > 0f);
            facingLeft = speedIsNegative; // Change facing direction whenever speed is not 0.
        }
        else
        {
            state = PlayerState.Squat;
        }

    }
    public void TryJump(float jumpforce)
    {
        AudioSource_effect.PlayOneShot(jump_SFX);
        StartCoroutine(JumpRoutine(jumpforce));
    }
    IEnumerator JumpRoutine(float jumpforce)
    {
        if (state == PlayerState.Jump || state == PlayerState.InAir) yield break;   // Don't jump when already jumping.
        //Debug.Log("Jump");
        playerRigidbody2D.velocity = Vector2.up * jumpforce;
        jumping = true;
        if (jumping)
        {
            state = PlayerState.Jump;

        }



    }
    public void ReturnIdle()
    {

        state = PlayerState.Idle;
    }
    public void Dead(Vector3 CSPoint, PlayerStatus p_s)
    {
        state = PlayerState.Idle;
        ClassSavePoint = CSPoint;
        p_status = p_s;
        state = PlayerState.Dead;

    }

    void OverDead()
    {
        PlayboardEvent.CallHealthChangeEffect(p_status.Maxlife);
        this.transform.position = ClassSavePoint;
        //Debug.Log("RE");

        while (PlayerPrefs.GetFloat("PlayerInk") < 3)
        {
            PlayboardEvent.CallCollectInk();
        }



        state = PlayerState.Idle;
        p_con.isdead = false;
    }
    public void StartSkill()
    {
        state = PlayerState.Skill;
    }

    public void Skilling()
    {
        state = PlayerState.Skill_l;
    }

    public enum PlayerState
    {
        Idle,
        Skill,
        Skill_l,
        Running,
        Jump,
        InAir,
        Fall,
        Squat,
        Crawl,
        Dead
    }
}
