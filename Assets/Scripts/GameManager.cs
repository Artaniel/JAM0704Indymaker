using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int moveIndex = 0;
    public GameObject[] graphs;    

    public GameObject spawnField; //spawn field надо имя скрипта которого еще нет

    private GameObject graph;
    private List<GameObject> allRobots;

    private void Awake()
    {
        InitMove(moveIndex);
    }

    private void InitMove(int moveIndex) {
        SpawnGraph(moveIndex);
        InitNewRobots(moveIndex);
    }

    private void SpawnGraph(int moveIndex) {
        graph = Instantiate(graphs[moveIndex]);
    }

    private void InitNewRobots(int moveIndex) {        
       // foreach (GameObject robotPrefab in graphs[moveIndex].GetComponent <???????> ().spawnList) { // когда будет имя скрипта менеджера поля, доавить его и раскомментить всё
       //     GameObject robot = Instantiate(robotPrefab);
       //     robot.transform.position = spawnField.transform.position;//потом изменить на позиции площадок, когда они будут
       //     allRobots.Add(robot);
       // }
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
        if (moveIndex <= graphs.Length)
        {
            InitMove(moveIndex);
        }
    }
}
