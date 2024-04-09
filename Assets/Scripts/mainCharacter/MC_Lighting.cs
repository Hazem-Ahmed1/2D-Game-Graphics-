using UnityEngine;

public class MC_Lighting : MonoBehaviour
{
    public MageShootingPlayer Mage;
    [SerializeField] GameObject InstantaitePoint;
    [SerializeField] GameObject InstantaitePrefab;
    public bool Lighting = false;
    private bool isInvoked = false;

    // Update is called once per frame
    void Update()
    {
        if (Mage.Charged && !Lighting && !isInvoked)
        {
            Invoke("getLight", 1.683f);
            isInvoked = true;
        }
    }

    public void getLight()
    {
        Instantiate(InstantaitePrefab, InstantaitePoint.transform.position, InstantaitePoint.transform.rotation);
        this.Lighting = true;
    }
}
