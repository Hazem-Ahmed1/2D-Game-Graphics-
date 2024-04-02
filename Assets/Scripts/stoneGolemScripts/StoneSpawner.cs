using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    public GameObject stones;
    public int numberOfStonesToSpawn = 7;
    public float spawnRate = 30f;
    private float timer = 0;
    public float widthOffset = 5f;
    public float yVariation = 0.5f;

    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            for (int i = 0; i < numberOfStonesToSpawn; i++)
            {
                SpawnStone();
            }
            timer = 0;
        }
    }

    void SpawnStone()
    {
        //float lowestPoint = transform.position.y - widthOffset;
        float highestPoint = transform.position.y + widthOffset;
        float randomX = Random.Range(transform.position.x - widthOffset, transform.position.x + widthOffset);

        float randomYOffset = Random.Range(-yVariation, yVariation);
        float yPos = highestPoint + randomYOffset;

        Vector3 spawnPosition = new(randomX, yPos, 0);
        Instantiate(stones, spawnPosition, Quaternion.identity);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
