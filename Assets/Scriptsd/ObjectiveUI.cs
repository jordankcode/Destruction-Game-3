using UnityEngine;
using TMPro;
using System.Collections;

public class ObjectiveUI : MonoBehaviour
{
    public static ObjectiveUI Instance;

    public TextMeshProUGUI objectiveLabel;
    public TextMeshProUGUI progressLabel;
    public TextMeshProUGUI completeBanner;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        completeBanner.gameObject.SetActive(false);
        Objective first = ObjectiveManager.Instance.GetActiveObjective();
        if (first != null) DisplayObjective(first);
    }

    void DisplayObjective(Objective obj)
    {
        objectiveLabel.text = obj.displayName;
        progressLabel.text = obj.currentCount + " / " + obj.targetCount;
    }

    public void UpdateProgress(Objective obj)
    {
        progressLabel.text = obj.currentCount + " / " + obj.targetCount;
    }

    public void ShowCompleted(Objective obj)
    {
        completeBanner.gameObject.SetActive(true);
        completeBanner.text = "COMPLETE!  +" + obj.bonusCoins + " COINS";
        StartCoroutine(HideBannerThenNext(obj));
    }

    IEnumerator HideBannerThenNext(Objective completedObj)
    {
        yield return new WaitForSeconds(2f);
        completeBanner.gameObject.SetActive(false);

        Objective next = ObjectiveManager.Instance.GetActiveObjective();
        if (next != null)
            DisplayObjective(next);
        else
            objectiveLabel.text = "All objectives complete!";
    }
}