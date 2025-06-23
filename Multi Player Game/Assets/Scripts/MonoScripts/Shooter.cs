using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    // Public
    // public InputActionReference fire;
    // public Button fireBtn;

    // Private
    [SerializeField] private Projection _projection;
    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private Transform _ballSpawn;
    [SerializeField] private float force = 20;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        //fireBtn.onClick.AddListener(Fire);
    }

    // Update is called once per frame
    void Update()
    {
        // _projection.SimulateTrajectory(_ballPrefab, _ballSpawn.position, _ballSpawn.forward * force);

    }



    //private void OnEnable()
    //{
    //    fire.action.started += Fire;
    //}

    //private void OnDisable()
    //{
    //    fire.action.started -= Fire;
    //}

    

    public void Fire()
    {
        var spawned = Instantiate(_ballPrefab, _ballSpawn.position, _ballSpawn.rotation);

        spawned.Init(_ballSpawn.forward * force, false);

    }

}
