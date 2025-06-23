using UnityEngine;
using UnityEngine.SceneManagement;

public class Projection : MonoBehaviour
{
    // Private
    private Scene _simulationScene;
    private PhysicsScene _physicsScene;
    [SerializeField] private Transform _obstaclesParent;
    [SerializeField] private LineRenderer _line;
    [SerializeField] private int _maxPhysicsFrameIterations = 100;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreatePhysicsScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void CreatePhysicsScene()
    {
        _simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScene = _simulationScene.GetPhysicsScene();

        foreach(Transform obj in _obstaclesParent)
        {
            var ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
            ghostObj.GetComponent<Renderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(ghostObj, _simulationScene);
        }

    }

    public void SimulateTrajectory(Ball ballPrefab, Vector3 pos, Vector3 velocity)
    {
        var ghostObj = Instantiate(ballPrefab, pos, Quaternion.identity);
        ghostObj.GetComponent<Renderer>().enabled = false;
        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, _simulationScene);

        ghostObj.Init(velocity, true);

        _line.positionCount = _maxPhysicsFrameIterations;

        for (int i = 0; i < _maxPhysicsFrameIterations; i++)
        {
            _physicsScene.Simulate(Time.fixedDeltaTime);
            _line.SetPosition(i, ghostObj.transform.position);
        }

        Destroy(ghostObj.gameObject);
    }

    public void HideTrajectory()
    {
        // Example: If using LineRenderer
        _line.positionCount = 0;
    }
}
