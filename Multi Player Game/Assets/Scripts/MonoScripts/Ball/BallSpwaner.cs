using UnityEngine;

public class BallSpwaner : MonoBehaviour
{
    // Private
    [SerializeField] private Transform ballSpawnPoint;
    private BallObjectPooler ballPooler;

    private void Start()
    {
        ballPooler = BallObjectPooler.instance;
    }

    private void FixedUpdate()
    {
        ballPooler.SpawnFromPool("plainball", ballSpawnPoint.position, Quaternion.identity);
    }
}
