using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public void RenderScore(int current, int expected) { // �������� ����� ����� �� ����� ����� ���� ���������, �������� ����� ��������� �����
        scoreText.text = $"{current}/{expected}";
    }
}
