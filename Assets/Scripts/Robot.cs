using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Robotype { R, G, B, Y }

public class Robot : MonoBehaviour
{
    public Robotype robotype;
    public int TSkill;
    public int BSkill;
    public int MSkill;
    public int ISkill;
    public GraphNode node;

    private void Awake()
    {
        SetupParametrs();
    }

    private void SetupParametrs() {  // вообще данные в коде это грех, но сегодня можно
        switch (robotype)
        {
            case Robotype.R:
                TSkill = 10;
                BSkill = 6;
                MSkill = 3;
                ISkill = 1;
                break;
            case Robotype.G:
                TSkill = 3;
                BSkill = 1;
                MSkill = 10;
                ISkill = 6;
                break;
            case Robotype.B:
                TSkill = 6;
                BSkill = 3;
                MSkill = 1;
                ISkill = 10;
                break;
            case Robotype.Y:
                TSkill = 1;
                BSkill = 10;
                MSkill = 6;
                ISkill = 3;
                break;
        }    
    }
}
