using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;


public class DebugCanvas : MonoBehaviour
{
    [SerializeField] GameObject Log_show;
    [SerializeField] GameObject Errortext;
    [SerializeField] PlayerController player_C;
    [SerializeField] DrawEffect player_draweffect;
    bool isopen;
    Scene scene;
    void Start()
    {
        scene = SceneManager.GetActiveScene();

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            if (!isopen)
            {
                PlayboardEvent.CallGamePause();
                Errortext.SetActive(true);
                Log_show.SetActive(true);
                isopen = true;
            }
            else
            {
                PlayboardEvent.CallGameContinue();
                Log_show.GetComponent<Text>().text = "";
                Errortext.SetActive(false);
                Log_show.SetActive(false);
                isopen = false;
            }
        }
    }
    public void OutputLog()
    {
        //--------------------------------------------寫入
        string date = DateTime.Now.ToString("yyyy年MM月dd日 HH_mm");

        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/" + "Log " + date + ".txt");

        sw.WriteLine("時間: " + DateTime.Now);
        sw.WriteLine("====================================");
        sw.WriteLine("問題描述:");
        sw.WriteLine(Errortext.GetComponent<InputField>().text);
        sw.WriteLine("====================================");
        sw.WriteLine("Scene: " + scene.name);
        sw.WriteLine("");
        sw.WriteLine("Pause: " + player_C.isPause);
        sw.WriteLine("");
        sw.WriteLine("Player:");
        sw.WriteLine("Transform   " + player_C.transform.position);
        sw.WriteLine("");
        sw.WriteLine("Grounded: " + player_C.isgrounded + "  now_ground: " + player_C.now_ground);
        sw.WriteLine("");
        sw.WriteLine("R_istouch: " + player_C.r_istouch + "  now_Rtouch: " + player_C.now_Rtouch);
        sw.WriteLine("");
        sw.WriteLine("L_istouch: " + player_C.l_istouch + "  now_Ltouch: " + player_C.now_Ltouch);
        sw.WriteLine("");
        sw.WriteLine("Squat: " + player_C.issquat);
        sw.WriteLine("");
        sw.WriteLine("Dead: " + player_C.isdead);
        sw.WriteLine("");
        sw.WriteLine("Ink: " + player_draweffect.ink);
        sw.WriteLine("");
        sw.WriteLine("Draw: " + player_draweffect.isdrawing);
        sw.Close();
        //--------------------------------------------顯示
        StreamReader sr = new StreamReader(Application.persistentDataPath + "/" + "Log " + date + ".txt");
        string Logs_string = "";
        string outputstring = "錯誤回報:\n";

        Logs_string = sr.ReadLine();
        while (Logs_string != null)
        {
            outputstring += Logs_string + "\n";
            Logs_string = sr.ReadLine();

        }
        Log_show.GetComponent<Text>().text = outputstring;
    }
}
