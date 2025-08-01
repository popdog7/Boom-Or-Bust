using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class workerPurchaseUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI worker_name;
    [SerializeField] Image worker_sprite;
    [SerializeField] TextMeshProUGUI worker_bonuses;
    [SerializeField] TextMeshProUGUI cost;

    private WorkerStats worker_info;
    private bool can_purchase = true;

    public event Action<workerPurchaseUI, int> onPurchase;

    public void setWorkerUI(WorkerStats stats)
    {
        worker_info = stats;

        worker_name.text = worker_info.worker_name;
        worker_sprite.sprite = worker_info.worker_img;

        string bonuses = "";

        foreach(var bonus in worker_info.worker_specialization)
        {
            bonuses += "+ " + bonus.ToString() + ", ";
        }

        worker_bonuses.text = bonuses;

        cost.text = "$" + worker_info.worker_cost.ToString();
    }

    public void purchase()
    {
        if (can_purchase)
        {
            onPurchase?.Invoke(this, worker_info.worker_cost);
            can_purchase = false;
        }
    }

    public void markPurchaseFailure()
    {
        can_purchase = true;
    }

    public void markPurchaseSuccess()
    {

    }

    public WorkerStats getWorkerStats()
    {
        return worker_info;
    }
    public void setCanPurchase(bool val)
    {
        can_purchase = val;
    }

}
