using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesManager : MonoBehaviour
{
    public int moveIndex = 0;
    public GameObject[] graphPrefabs;    

    public GameObject spawnField; 

    public GameObject graph;
    private List<GameObject> allRobots;

    private void Awake()
    {
        allRobots = new List<GameObject>();
        if (!GameObject.FindWithTag("Level"))
            InitMove(moveIndex);
        else
        {
            graph = GameObject.FindWithTag("Level");            
        }
    }

    private void InitMove(int moveIndex) {
        SpawnGraph(moveIndex);
        InitNewRobots(moveIndex);
    }

    private void SpawnGraph(int moveIndex) {
        graph = Instantiate(graphPrefabs[moveIndex]);
    }

    private void InitNewRobots(int moveIndex) {
        int i = 0;
        foreach (GameObject robotPrefab in graph.GetComponent<GraphField>().spawnList)
        {
            GameObject robot = Instantiate(robotPrefab);
            robot.transform.position = spawnField.GetComponent<SpawnField>().spawnPoints[i].transform.position;
            allRobots.Add(robot);
            i++;
        }
    }

    private void Wipe() {
        Destroy(graph);
        GameObject robot;
        while (allRobots.Count > 0) {
            robot = allRobots[0];
            allRobots.Remove(robot);
            Destroy(robot);
        }
    }

    public void NextMove() {
        //тут както обрабатвать результаты расстановки
        Wipe();
        moveIndex++;
        if (moveIndex < graphPrefabs.Length)
        {
            InitMove(moveIndex);
        }
        else { 
        //win?
        }
    }
}
