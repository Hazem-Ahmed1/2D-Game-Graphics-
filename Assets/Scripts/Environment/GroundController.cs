using System.Collections;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject startPointObject;
    public GameObject endPointObject;
    private float spawnInterval = 10f;
    private float destructionDelay = 5f;

    void Start()
    {
        InvokeRepeating("SpawnObject", 0f, spawnInterval);
    }

    void SpawnObject()
    {

        Vector2 startPoint = startPointObject.transform.position;
        Vector2 endPoint = endPointObject.transform.position;

        Vector2 randomPosition = new Vector2(Random.Range(startPoint.x, endPoint.x),
                                             Random.Range(startPoint.y, endPoint.y));

        GameObject newObject = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);

        float destructionAnimationDuration = GetDestructionAnimationDuration(newObject);

        StartCoroutine(DestroyObjectWithDelay(newObject, destructionAnimationDuration));
    }

    float GetDestructionAnimationDuration(GameObject obj)
    {
        Animator animator = obj.GetComponent<Animator>();
        if (animator != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            return stateInfo.length;
        }

        return 0f;
    }

    IEnumerator DestroyObjectWithDelay(GameObject obj, float destructionAnimationDuration)
    {
        yield return new WaitForSeconds(destructionDelay);

        Animator animator = obj.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Destroy");
            yield return new WaitForSeconds(destructionAnimationDuration);
        }

        Destroy(obj);
    }
}
