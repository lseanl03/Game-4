using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfoPanel : MonoBehaviour
{
    public Button closeEnemyInfoPanel;
    private void Awake()
    {
        closeEnemyInfoPanel.onClick.AddListener(EnemyInfoState);
    }
    void EnemyInfoState()
    {
        UIManager.instance.EnemyInfoPanelState(false);
    }
}
