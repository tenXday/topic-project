using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : MonoBehaviour
{
    [SerializeField, Header("子彈")]
    private GameObject bullet;
    [SerializeField, Header("槍口")]
    private GameObject muz;
    [SerializeField, Header("槍管")]
    private GameObject gun;
    [SerializeField, Header("眼睛")]
    private GameObject eyes;

    [SerializeField, Header("偵測距離"), Range(0, 100.0f)]
    private float check_direction;
    [SerializeField, Header("射擊間隔"), Range(0, 5.0f)]
    private float fireRate = 0.5f;
    [SerializeField, Header("子彈速度"), Range(0, 30.0f)]
    private float bulletspeed = 1.0f;
    [SerializeField, Header("砲塔旋轉限制角度"), Range(0, 90.0f)]
    private float limitangle = 0f;
    [SerializeField, Header("砲塔旋轉速度"), Range(0, 50.0f)]
    private float rotatespeed = 0f;
    private float nextFire = 0.0f;

    private bool changedir;
    void Start()
    {
        float a = Random.value;
        if (a > 0.5)
        {
            changedir = true;
        }
        else
        {
            changedir = false;
        }
        this.gameObject.GetComponent<LineRenderer>().SetPosition(0, this.gameObject.transform.position);
    }
    void Update()
    {
        Vector2 gunner_r = new Vector2(muz.transform.position.x - this.gameObject.transform.position.x, muz.transform.position.y - this.gameObject.transform.position.y);
        Collider2D playercheck = Physics2D.OverlapCircle(this.gameObject.transform.position, check_direction, 1 << LayerMask.NameToLayer("player"));
        if (playercheck)
        {

            Attacking(playercheck, gunner_r);
        }
        else
        {
            /*Empty(gunner_r);*/
        }

    }
    void Attacking(Collider2D hittarget, Vector2 gunner_r)
    {
        Vector2 raypoint;
        RaycastHit2D checkhit = Physics2D.Raycast(muz.transform.position, gunner_r, check_direction, 1 << LayerMask.NameToLayer("Ground"));

        //射線顯示和射擊
        if (checkhit)
        {
            this.gameObject.GetComponent<LineRenderer>().SetPosition(1, checkhit.point);
            raypoint = checkhit.point;

            Fire(gunner_r);
        }
        else
        {
            //Debug.Log("no shoioi");
            Vector2 targetpos = (muz.transform.position - this.gameObject.transform.position).normalized * check_direction;
            targetpos.x += muz.transform.position.x;
            targetpos.y += muz.transform.position.y;
            this.gameObject.GetComponent<LineRenderer>().SetPosition(1, targetpos);
            raypoint = checkhit.point;
            //Debug.Log(targetpos);
        }

        //Debug.Log("hittarrget:" + hittarget.gameObject.transform.position.x + "raypoint:" + raypoint.x);
        Debug.DrawLine(hittarget.gameObject.transform.position, muz.transform.position);
        Vector2 shootcheckline = new Vector2(muz.transform.position.x - hittarget.gameObject.transform.position.x, muz.transform.position.y - (hittarget.gameObject.transform.position.y + 0.5f));

        if (shootcheckline.x / shootcheckline.y < gunner_r.x / gunner_r.y)
        {
            gun.transform.RotateAround(this.gameObject.transform.position, new Vector3(0, 0, 1), rotatespeed * Time.deltaTime);
            eyes.transform.Translate(new Vector3(0.0001f, 0, 0));
        }
        else
        {
            gun.transform.RotateAround(this.gameObject.transform.position, new Vector3(0, 0, 1), -rotatespeed * Time.deltaTime);
            eyes.transform.Translate(new Vector3(-0.0001f, 0, 0));
        }
        //Physics2D.Raycast(muz.transform.position, new Vector2(muz.transform.position.x - this.gameObject.transform.position.x, muz.transform.position.y - this.gameObject.transform.position.y), check_direction, 1 << LayerMask.NameToLayer("player"));
    }
    void Fire(Vector2 gunner_r)
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            //Debug.Log("Shoot!");
            GameObject tempbullet = Instantiate(bullet, muz.transform.position, Quaternion.identity);
            tempbullet.GetComponent<Rigidbody2D>().velocity = gunner_r.normalized * bulletspeed;
            this.gameObject.GetComponent<LineRenderer>().startWidth = 0.3f;
            this.gameObject.GetComponent<LineRenderer>().endWidth = 0.3f;
        }
    }
    /*void Empty(Vector2 gunner_r)
    {

        //Debug.Log(gun.transform.rotation.eulerAngles.z);
        //在指定角度內來回擺盪
        if ((gun.transform.rotation.eulerAngles.z < limitangle && gun.transform.rotation.eulerAngles.z >= 0.0f) || (gun.transform.rotation.eulerAngles.z <= 360.0f && gun.transform.rotation.eulerAngles.z > 360.0f - limitangle))
        {

            if (!changedir)
            {
                gun.transform.RotateAround(this.gameObject.transform.position, new Vector3(0, 0, 1), rotatespeed * Time.deltaTime);
                eyes.transform.Translate(new Vector3(0.0001f, 0, 0));
            }
            else
            {
                gun.transform.RotateAround(this.gameObject.transform.position, new Vector3(0, 0, 1), -rotatespeed * Time.deltaTime);
                eyes.transform.Translate(new Vector3(-0.0001f, 0, 0));
            }
        }
        else
        {

            if (gun.transform.rotation.eulerAngles.z > limitangle && gun.transform.rotation.eulerAngles.z < limitangle + 10)
            {
                changedir = true;
            }
            if (gun.transform.rotation.eulerAngles.z < 360.0f - limitangle && gun.transform.rotation.eulerAngles.z > 360.0f - limitangle - 10)
            {
                changedir = false;
            }
            if (!changedir)
            {
                gun.transform.RotateAround(this.gameObject.transform.position, new Vector3(0, 0, 1), rotatespeed * Time.deltaTime);
                eyes.transform.Translate(new Vector3(0.0001f, 0, 0));
            }
            else
            {
                gun.transform.RotateAround(this.gameObject.transform.position, new Vector3(0, 0, 1), -rotatespeed * Time.deltaTime);
                eyes.transform.Translate(new Vector3(-0.0001f, 0, 0));
            }
        }


        RaycastHit2D checkhit = Physics2D.Raycast(muz.transform.position, gunner_r, check_direction, 1 << LayerMask.NameToLayer("playground"));
        if (checkhit)
        {
            //Debug.Log(checkhit.point);
            this.gameObject.GetComponent<LineRenderer>().SetPosition(1, checkhit.point);
        }
        else
        {
            Vector3 targetpos = (muz.transform.position - this.gameObject.transform.position).normalized * check_direction;
            targetpos.x += muz.transform.position.x;
            targetpos.y += muz.transform.position.y;
            this.gameObject.GetComponent<LineRenderer>().SetPosition(1, targetpos);
            //Debug.Log(targetpos);
        }
        this.gameObject.GetComponent<LineRenderer>().startWidth = 0.1f;
        this.gameObject.GetComponent<LineRenderer>().endWidth = 0.1f;
    }*/
}
