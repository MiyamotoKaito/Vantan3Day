using System;
using UnityEngine;

/// <summary>
/// 入国審査
/// </summary>
public class ImmigrationInspection : MonoBehaviour
{
    [Header("VisitorGeneration")] 
    [SerializeField] private VisitorGeneration _visitorGeneration;

    private void Update()
    {
        ReviewInput();
    }

    /// <summary>
    /// 審査の入力
    /// </summary>
    private void ReviewInput()
    {
        //TODO：仮の入力を実装
        //TODO：のちに、InputSystemで対応させる
        if (Input.GetKeyDown(KeyCode.Q))
        {
            
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            
        }
    }
    
    /// <summary>
    /// 審査の判定を行う
    /// </summary>
    private void ExaminationJudgment()
    {
        var data = _visitorGeneration.CurrentVisitor.VisitorType;
        switch (data)
        {
            case VisitorType.Human:
                break;
            case VisitorType.Alien:
                break;
            case VisitorType.ArtificialHuman:
                break;
        }
    }
}
