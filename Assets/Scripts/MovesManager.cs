using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesManager : MonoBehaviour
{
    public int moveIndex = 0;
    public GameObject[] graphPrefabs;    

    public GameObject spawnField; //spawn field ���� ��� ������� �������� ��� ���

    private GameObject graph;
    private List<GameObject> allRobots;

    private void Awake()
    {
        //InitMove(moveIndex); // �������� ��������� ���� �������� � �������� ����� � �����, ����� ����� �������� ������� ����
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
            robot.transform.position = spawnField.transform.position;//����� �������� �� ������� ��������, ����� ��� �����
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
        //��� ����� ����������� ���������� �����������
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
