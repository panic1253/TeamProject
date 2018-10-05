using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject Player;

    Vector3 startPos;
    Quaternion startRotation;
    bool isStart = false;
    static bool isEnded = false;

    static int stageLevel = 0;

    void Awake()
    {
        Time.timeScale = 0f;
    }

    // Use this for initialization
    void Start () {
        //Player = GameObject.FindGameObjectWithTag("Player");
        startPos = GameObject.FindGameObjectWithTag("Start").transform.position;
        startRotation = GameObject.FindGameObjectWithTag("Start").transform.rotation;
        
	}

    void OnGUI()
    {
        if (!isStart && stageLevel == 0)
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            GUILayout.Label("Ready?");

            if (GUILayout.Button("Start"))
            {
                isStart = true;
                StartGame();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
        else if (isEnded&&(stageLevel == 2)) {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            GUILayout.Label("End!!");

            if (GUILayout.Button("ReStart?"))
            {
                stageLevel = 0;
                isEnded = false;
                SceneManager.LoadScene(0, LoadSceneMode.Single);
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }

    void StartGame() {
        Time.timeScale = 1f;

        GameObject standingCamera = GameObject.FindGameObjectWithTag("MainCamera");
        standingCamera.SetActive(false);

        startPos = new Vector3(startPos.x, startPos.y, startPos.z);
        Instantiate(Player, startPos, startRotation);
    }

    void Update () {
        if (stageLevel != 0)
            Time.timeScale = 1f;
    }

    public static void Restart() {
        Time.timeScale = 0f;

        SceneManager.LoadScene(stageLevel, LoadSceneMode.Single);

        
    }

    public static void EndGame() {
        Time.timeScale = 0f;

        stageLevel++;

        if (stageLevel == 2)
            isEnded = true;
        else
            SceneManager.LoadScene(stageLevel, LoadSceneMode.Single);
    }
}
