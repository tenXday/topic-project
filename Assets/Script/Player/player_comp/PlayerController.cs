using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRigidbody2D;
    BoxCollider2D playercollider;
    [SerializeField]
    PlayerStatus p_status;
    [SerializeField]
    DrawEffect p_de;
    [SerializeField, Header("玩家模型")]
    PlayerModel p_model;
    public SkeletonAnimation p_skeletonAnimation;
    [SerializeField, Header("滑牆速度"), Range(0, 10)]
    float wallSlidingSpeed;

    [SerializeField, Header("移動速度"), Range(0, 10)]
    float speed;

    [SerializeField, Header("跳躍力道")]
    float jumpforce;

    [SerializeField, Header("感應距離")]

    float checkRadius;


    [SerializeField, Header("場地圖層")]

    LayerMask groundLayer;
    [SerializeField, Header("偵測地板點")]

    List<Transform> groundCheck;

    [SerializeField, Header("右偵測接觸點")]

    List<Transform> r_touchCheck;
    [SerializeField, Header("左偵測接觸點")]
    List<Transform> l_touchCheck;


    [SerializeField, Header("偵測頂部點")]
    Transform topcheck;
    [SerializeField, Header("趴下偵測頂點")]
    Transform sqtopcheck;
    //--------------------------------------------
    [SerializeField, Header("蹬牆力道")]
    float ywallforce;
    [SerializeField]
    float xwallforce;
    //--------------------------------------------
    bool walljumping;
    bool wallSliding;

    public bool isgrounded, istouching, r_istouch, l_istouch;

    public bool istoptouching, issquattouching;

    //---------------------------------------------------------

    [SerializeField]
    GameObject Camera;

    bool isfaceright;
    public bool issquat, isdead;
    Vector2 OriginalSize, TargetSize;
    Vector2 OriginalOffset, Targetoffset;

    Vector3 CurrentSavePoint, ClassSavePoint;
    public GameObject now_ground, now_Ltouch, now_Rtouch, now_toptouch;

    public bool isPause = false;

    void OnEnable()
    {
        PlayboardEvent.SavePoint += SavePoint;
        PlayboardEvent._Healthchange += HpChangeEffect;
        PlayboardEvent.GamePause += GamePause;
        PlayboardEvent.GameContinue += GameContinue;

    }
    void OnDisable()
    {
        PlayboardEvent.SavePoint -= SavePoint;
        PlayboardEvent._Healthchange -= HpChangeEffect;
        PlayboardEvent.GamePause -= GamePause;
        PlayboardEvent.GameContinue -= GameContinue;
    }
    void Start()
    {
        SavePoint(this.transform.position);
        ClassSavePoint = this.transform.position;
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        playercollider = GetComponent<BoxCollider2D>();

        OriginalSize = playercollider.size;
        TargetSize = new Vector2(9.018541f, 4.610281f);

        OriginalOffset = playercollider.offset;
        Targetoffset = new Vector2(-0.1388359f, 2.547168f);
    }
    void GamePause()
    {
        isPause = true;
    }
    void GameContinue()
    {
        isPause = false;
    }
    // Update is called once per frame
    void Update()
    {

        float input = Input.GetAxisRaw("Horizontal");
        if (p_status.Playerlife < 1)
        {
            isdead = true;
            issquat = false;
            //Debug.Log("Dead");
            ReturnSavePoint();

        }
        if (!isdead && !isPause)
        {

            if (!issquat && p_de.isdrawing != true)
            {

                //------------------------------------------------------------------前後移動
                p_model.TryMove(input);
                for (int i = 0; i < groundCheck.Count; i++)
                {
                    if (Physics2D.OverlapCircle(groundCheck[i].position, checkRadius, groundLayer))
                    {
                        now_ground = Physics2D.OverlapCircle(groundCheck[i].position, checkRadius, groundLayer).gameObject;
                        isgrounded = true;
                        break;
                    }
                    now_ground = null;
                    isgrounded = false;
                }

                //------------------------------------------------------------------滑牆
                for (int i = 0; i < r_touchCheck.Count; i++)
                {
                    if (Physics2D.OverlapCircle(r_touchCheck[i].position, checkRadius, groundLayer) && p_model.facingLeft)
                    {
                        now_Rtouch = Physics2D.OverlapCircle(r_touchCheck[i].position, checkRadius, groundLayer).gameObject;
                        r_istouch = true;

                        break;
                    }
                    now_Rtouch = null;
                    r_istouch = false;
                }

                for (int i = 0; i < l_touchCheck.Count; i++)
                {
                    if (Physics2D.OverlapCircle(l_touchCheck[i].position, checkRadius, groundLayer) && !p_model.facingLeft)
                    {
                        now_Ltouch = Physics2D.OverlapCircle(l_touchCheck[i].position, checkRadius, groundLayer).gameObject;
                        l_istouch = true;

                        break;
                    }
                    now_Ltouch = null;
                    l_istouch = false;
                }

                istouching = r_istouch || l_istouch;

                istoptouching = Physics2D.OverlapCircle(topcheck.position, checkRadius, groundLayer);
                if (istoptouching)
                {
                    now_toptouch = Physics2D.OverlapCircle(topcheck.position, checkRadius, groundLayer).gameObject;
                }
                else
                {
                    now_toptouch = null;
                }

                if (istouching == true && isgrounded == false && input != 0)
                {
                    wallSliding = true;
                    playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, Mathf.Clamp(playerRigidbody2D.velocity.y, -wallSlidingSpeed, float.MaxValue));
                }
                else
                {
                    wallSliding = false;
                    playerRigidbody2D.velocity = new Vector2(input * speed, playerRigidbody2D.velocity.y);

                }

                //-------------------------------------------------------------------跳躍
                if (Input.GetKeyDown(KeyCode.Space) && isgrounded == true && istoptouching == false)
                {


                    p_model.TryJump(jumpforce);
                }
                if (Input.GetKeyDown(KeyCode.Space) && wallSliding == true && istoptouching == false)
                {
                    p_model.TryJump(jumpforce);

                    playerRigidbody2D.velocity = new Vector2(xwallforce * -input, ywallforce);
                }

            }
            issquattouching = Physics2D.OverlapCircle(sqtopcheck.position, checkRadius, groundLayer);
            //--------------------------------------------------------------------蹲下
            if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && issquat == true && issquattouching == false && p_de.isdrawing == false)
            {
                playercollider.size = OriginalSize;
                playercollider.offset = OriginalOffset;
                p_model.ReturnIdle();
                issquat = false;
            }
            else
            if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && isgrounded && issquat == false && p_de.isdrawing == false)
            {
                //Debug.Log(isgrounded);
                playercollider.size = TargetSize;
                playercollider.offset = Targetoffset;
                issquat = true;
            }
            if (issquat)
            {
                p_model.TrySquat(input);
                playerRigidbody2D.velocity = new Vector2(input * speed, playerRigidbody2D.velocity.y);
            }


        }
    }



    //---------------------------------------------------------------儲存點
    void SavePoint(Vector3 savepos)
    {
        Debug.Log("Setsavepos");
        CurrentSavePoint = savepos;
    }
    public void ReturnSavePoint()
    {
        if (p_status.Playerlife < 1)
        {


            ReturnCSavePoint();
        }
        else
        if (!isdead)
        {


            //Debug.Log("TP");
            this.transform.position = CurrentSavePoint;



        }



    }
    void ReturnCSavePoint()
    {
        p_model.Dead(CurrentSavePoint, p_status);
    }

    void HpChangeEffect(float hp_c)
    {
        if (!isdead)
        {
            PlayboardEvent.CallHealthChangeEffect(hp_c);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = isgrounded ? Color.red : Color.green;
        for (int i = 0; i < groundCheck.Count; i++)
        {
            Gizmos.DrawSphere(groundCheck[i].position, checkRadius);
        }
        Gizmos.color = r_istouch ? Color.red : Color.green;
        for (int i = 0; i < r_touchCheck.Count; i++)
        {
            Gizmos.DrawSphere(r_touchCheck[i].position, checkRadius);
        }
        Gizmos.color = l_istouch ? Color.red : Color.green;
        for (int i = 0; i < l_touchCheck.Count; i++)
        {
            Gizmos.DrawSphere(l_touchCheck[i].position, checkRadius);
        }
        Gizmos.color = istoptouching ? Color.red : Color.green;
        Gizmos.DrawSphere(topcheck.position, checkRadius);
        Gizmos.color = issquattouching ? Color.red : Color.green;
        Gizmos.DrawSphere(sqtopcheck.position, checkRadius);

    }
}
