using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI TscoreText;
    public TextMeshProUGUI BscoreText;
    public TextMeshProUGUI MscoreText;
    public TextMeshProUGUI IscoreText;

    public void RenderScore(int Tscore, int Bscore, int Mscore, int Iscore, int Tgoal, int Bgoal, int Mgoal, int Igoal) { 
        TscoreText.text = $"{Tscore}/{Tgoal}";
        BscoreText.text = $"{Bscore}/{Bgoal}";
        MscoreText.text = $"{Mscore}/{Mgoal}";
        IscoreText.text = $"{Iscore}/{Igoal}";
    }
}
