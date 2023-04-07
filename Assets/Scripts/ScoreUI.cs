using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public void RenderScore(int current, int expected) { // усложним когда будет √ƒ чтобы знать куда усложн€ть, возможно будет несколько типов
        scoreText.text = $"{current}/{expected}";
    }
}
