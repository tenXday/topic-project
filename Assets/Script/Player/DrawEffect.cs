using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerModel p_model;
    public PlayerController p_con;
    public bool isdrawing;
    public bool iserase;

    public GameObject pen;
    public GameObject eraser;
    public GameObject showeffect;
    [SerializeField]
    GameObject ink_pen;
    public float ink;

    private GameObject currentpen;
    private GameObject currenteraser;
    bool isPause;
    [SerializeField] Texture2D pen_cursor;
    [SerializeField] Texture2D eraser_cursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    void OnEnable()
    {

        PlayboardEvent.GamePause += GamePause;
        PlayboardEvent.GameContinue += GameContinue;

    }
    void OnDisable()
    {

        PlayboardEvent.GamePause -= GamePause;
        PlayboardEvent.GameContinue -= GameContinue;
    }
    void Start()
    {

        if (pen == null)
        {
            Debug.LogError("未設置畫筆");

        }

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
        if (!isPause)
        {
            if (!p_con.issquat && p_con.isgrounded)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    ink = ink_pen.GetComponent<CollectionUI>().C_number;
                    if (ink > 0 && isdrawing == false)
                    {
                        p_model.StartSkill();
                        Debug.Log("P");
                        Cursor.SetCursor(pen_cursor, hotSpot, cursorMode);
                        isdrawing = true;
                    }
                    else if (isdrawing == true)
                    {
                        p_model.ReturnIdle();
                        Cursor.SetCursor(null, hotSpot, cursorMode);
                        isdrawing = false;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && isdrawing == true)
            {
                p_model.Skilling();
                Vector3 mousepos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
                currentpen = Instantiate(pen, mousepos, Quaternion.identity);

                ink_pen.GetComponent<CollectionUI>().C_number--;

                ink_pen.GetComponent<CollectionUI>().UpdateCollect();
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                p_model.ReturnIdle();
                Cursor.SetCursor(null, hotSpot, cursorMode);
                isdrawing = false;
            }

            //----------------------------------------------------------------------------------
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Cursor.SetCursor(eraser_cursor, hotSpot, cursorMode);
                iserase = true;
                Vector3 mousepos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
                currenteraser = Instantiate(eraser, mousepos, Quaternion.identity);

            }
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                iserase = false;
                Cursor.SetCursor(null, hotSpot, cursorMode);
            }
            showeffect.GetComponent<Image>().color = isdrawing ? Color.white : Color.gray;
        }
    }

}
