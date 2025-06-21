using UnityEngine;

public class Ball : MonoBehaviour
{
    // Private
    [SerializeField] private Rigidbody rb;
    // [SerializeField] private GameObject poofPrefab;
    private bool _isGhost;

    public void Init(Vector3 velocity, bool isGhost)
    {
        _isGhost = isGhost;
        rb.AddForce(velocity, ForceMode.Impulse);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (_isGhost) return;

    //    // Instantiate(poofPrefab, collision.contacts[0].point, Quaternion.Euler(collision.contacts[0].normal));
        
    //}

}
