using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesManager : MonoBehaviour
{
    public int moveIndex = 0;
    public GameObject[] graphPrefabs;    

    public GameObject spawnField; //spawn field надо имя скрипта которого еще нет

    private GameObject graph;
    private List<GameObject> allRobots;

    private void Awake()
    {
        //InitMove(moveIndex); // временно отключено пока работаем с префабом прямо в сцене, потом будет спавнить префабы само
    }

    private void InitMove(int moveIndex) {
        SpawnGraph(moveIndex);
        InitNewRobots(moveIndex);
    }

    private void SpawnGraph(int moveIndex) {
        graph = Instantiate(graphPrefabs[moveIndex]);
    }

    private void InitNewRobots(int moveIndex) {
        foreach (GameObject robotPrefab in graph.GetComponent<GraphField>().spawnList)
        {
            GameObject robot = Instantiate(robotPrefab);
            robot.transform.position = spawnField.transform.position;//потом изменить на позиции площадок, когда они будут
            allRobots.Add(robot);
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
        if (moveIndex <= graphPrefabs.Length)
        {
            InitMove(moveIndex);
        }
        else { 
        //win?
        }
    }
}
