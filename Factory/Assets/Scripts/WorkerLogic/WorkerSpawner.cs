using System;
using System.Collections.Generic;
using UnityEngine;

public class WorkerSpawner : MonoBehaviour
{
    [Header("Worker Attributs")]
    [SerializeField] private GameObject worker_prefab;
    [SerializeField] private List<WorkerStats> inital_workers = new List<WorkerStats>();

    [Header("Postion Attributes")]
    [SerializeField] private Transform spawn_position;
    [SerializeField] private float x_offset = 1f;
    [SerializeField] private float z_offset = 1f;

    private List<WorkerStats> worker_to_create = new List<WorkerStats>();

    public event Action<Worker> onWorkerCreated;

    private void Start()
    {
        foreach (var worker in inital_workers)
        {
            addWorker(worker);
        }
        createWorkers();
    }

    public void addWorker(WorkerStats stats)
    {
        worker_to_create.Add(stats);
    }

    public void createWorkers()
    {
        Vector3 spawn_point = spawn_position.position;

        foreach (var worker in worker_to_create)
        {
            GameObject obj = Instantiate(worker_prefab, spawn_point, Quaternion.identity);
            Worker created_worker = obj.GetComponent<Worker>();
            created_worker.initalize(worker);

            onWorkerCreated?.Invoke(created_worker);


            spawn_point += new Vector3(x_offset, 0, z_offset);
        }

        worker_to_create.Clear();
    }
}
