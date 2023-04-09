using System;
using UnityEngine;

public class RotationAround : MonoBehaviour
{
    private Transform robot;

    private void Start()
    {
        robot = this.transform;
    }

    private void Update()
    {
        robot.Rotate(0,0.05f,0);
    }
}
