using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayboardEvent : MonoBehaviour
{
    public delegate void Healthchange(float health);
    public static event Healthchange _Healthchange;
    public static event Healthchange _BossHealthchange;
    public static event Healthchange _Healthchangeeffect;


    public delegate void Mission(int missionID);
    public static event Mission MissionStart;
    public static event Mission MissionEnd;

    public delegate void Game();
    public static event Game GameStart;
    public static event Game GamePause;
    public static event Game GameContinue;
    public static event Game _PlayerRelive;

    public delegate void GameOver(bool isSuccessed);
    public static event GameOver GameEnd;

    public delegate void Record(Vector3 Pos);
    public static event Record SavePoint;
    public delegate void Collection();
    public static event Collection _CollectInk;
    public delegate void Movie();
    public static event Movie _MovieStart;
    public static event Movie _MovieEnd;

    public static void CallHealthChange(float hp)
    {
        if (_Healthchange != null)
        {
            _Healthchange(hp);
        }
    }
    public static void CallHealthChangeEffect(float hp)
    {
        if (_Healthchangeeffect != null)
        {
            _Healthchangeeffect(hp);
        }
    }
    public static void CallBossHealthChange(float hp)
    {
        if (_BossHealthchange != null)
        {
            _BossHealthchange(hp);
        }
    }
    public static void CallMissionStart(int ID)
    {
        if (MissionStart != null)
        {
            MissionStart(ID);
        }
    }
    public static void CallMissionEnd(int ID)
    {
        if (MissionEnd != null)
        {
            MissionEnd(ID);
        }
    }

    public static void CallGamePause()
    {
        if (GamePause != null)
        {
            GamePause();

        }
    }
    public static void CallGameContinue()
    {
        if (GameContinue != null)
        {
            GameContinue();

        }
    }
    public static void CallGameStart()
    {

        if (GameStart != null)
        {
            GameStart();
        }
    }
    public static void CallGameEnd(bool isSuccessed)
    {
        if (GameEnd != null)
        {
            GameEnd(isSuccessed);
        }
    }
    public static void CallPlayerRelive()
    {
        if (_PlayerRelive != null)
        {
            _PlayerRelive();

        }
    }
    public static void CallSavePoint(Vector3 pos)
    {
        if (SavePoint != null)
        {
            SavePoint(pos);
        }
    }
    public static void CallCollectInk()
    {
        if (_CollectInk != null)
        {
            _CollectInk();
        }
    }
    public static void CallMovieStart()
    {
        if (_MovieStart != null)
        {
            _MovieStart();
        }
    }
    public static void CallMovieEnd()
    {
        if (_MovieEnd != null)
        {
            _MovieEnd();
        }
    }
}
