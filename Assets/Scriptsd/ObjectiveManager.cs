using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Objective
{
    public string displayName;
    public string targetTag;
    public int targetCount;
    public int bonusCoins;
    [HideInInspector] public int currentCount = 0;
    [HideInInspector] public bool isCompleted = false;
}

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;

    public List<Objective> objectives = new List<Objective>();

    private int activeIndex = 0;

    void Awake()
    {
        Instance = this;
    }

    public Objective GetActiveObjective()
    {
        if (activeIndex < objectives.Count)
            return objectives[activeIndex];
        return null;
    }

    public void ReportDestruction(string tag)
    {
        Objective obj = GetActiveObjective();
        if (obj == null || obj.isCompleted) return;
        if (obj.targetTag != tag) return;

        obj.currentCount++;

        if (obj.currentCount >= obj.targetCount)
        {
            obj.isCompleted = true;

            // Award bonus coins through CoinCollection
            CoinCollection cc = FindObjectOfType<CoinCollection>();
            if (cc != null) cc.AddBonusCoins(obj.bonusCoins);

            ObjectiveUI.Instance.ShowCompleted(obj);
            activeIndex++;
        }
        else
        {
            ObjectiveUI.Instance.UpdateProgress(obj);
        }
    }
}