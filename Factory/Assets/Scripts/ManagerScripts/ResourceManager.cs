
using System;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private ResourceBank bank;
    [SerializeField] private ResourceUIController ui_controller;
    private ResourceData data;

    private void Awake()
    {
        data = new();
        bank.importResourceData(data);
        connectUI();
    }

    public void connectWorker(Worker employee)
    {
        employee.onCreateProduct += bank.addResource;
    }

    public void connectUI()
    {
        bank.updateResourceUI += ui_controller.updateElementUI;
    }


}
