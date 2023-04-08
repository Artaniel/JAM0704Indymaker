using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MovesManager : MonoBehaviour
{
    public int moveIndex = 0;
    public GameObject[] graphPrefabs;    
    public GameObject spawnField; 
    public GameObject graph;
    public List<GameObject> allRobots = new List<GameObject>();
    public Button readyButton;
    public Narrative narrative;
    public GameObject winScreen;
    
    public Action OnLevelStarted;

    private void Awake()
    {
        if (!GameObject.FindWithTag("Level"))
            InitMove(moveIndex);
        else
        {
            graph = GameObject.FindWithTag("Level");            
        }
    }

    private void InitMove(int moveIndex) {
        if (narrative && narrative.panelTransform!= null) narrative.RunPreMoveText(moveIndex-1);
        SpawnGraph(moveIndex);
        InitNewRobots();
        OnLevelStarted?.Invoke();
    }

    private void SpawnGraph(int moveIndex) {
        graph = Instantiate(graphPrefabs[moveIndex-1]);
    }

    private void InitNewRobots() {
        int i = 0;
        foreach (GameObject robotPrefab in graph.GetComponent<GraphField>().spawnList)
        {
            if (robotPrefab != null)
            {
                GameObject robot = Instantiate(robotPrefab);
                robot.transform.position = spawnField.GetComponent<SpawnField>().spawnPoints[i].transform.position;
                allRobots.Add(robot);
            }
            i++;
        }
    }

    private void Wipe() {
        GameObject robot;
        while (allRobots.Count > 0) {            
            robot = allRobots[0];
            allRobots.Remove(robot);
            Destroy(robot);
        }
        Destroy(graph);
    }

    public void NextMove() {        
        readyButton.interactable = false;
        Wipe();
        moveIndex++;
        if (moveIndex <= graphPrefabs.Length)
        {
            InitMove(moveIndex);
        }
        else {
            Debug.Log("out of levels");
            winScreen.SetActive(true);
            narrative.RunWintext(winScreen);
        }
    }
}