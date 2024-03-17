using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public float offset;
    public float offsetSmoothing;
    private Vector3 playerPosition;
 
    // Start is called before the first frame update
    void Start()
    {
        offset = 6f;
        offsetSmoothing = 10f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerPosition = new Vector3(player.transform.position.x + offset, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
    }
}
