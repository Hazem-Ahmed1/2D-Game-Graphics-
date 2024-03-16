using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject _player;
    public float _offset = 6f;
    public float _offsetSmoothing = 10f;
    private Vector3 _playerPosition;
 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _playerPosition = new Vector3(_player.transform.position.x + _offset, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, _playerPosition, _offsetSmoothing * Time.deltaTime);
    }
}