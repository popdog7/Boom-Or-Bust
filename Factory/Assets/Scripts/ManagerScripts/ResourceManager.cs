
using System;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private ResourceBank bank;
    private ResourceData data;

    private void Awake()
    {
        data = new();
        bank.importResourceData(data);
    }

    public void connectWorker(Worker employee)
    {
        employee.onCreateProduct += bank.addResource;
    }


}
