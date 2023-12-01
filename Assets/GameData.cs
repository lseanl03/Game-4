using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : BaseSave<GameData>
{
    public AxieName currentAxieName = AxieName.None;
    public int highScore;
    public int upgradePoint;
}
