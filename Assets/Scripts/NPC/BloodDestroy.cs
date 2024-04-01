using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDestroy : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject , 2f);
    }
}
