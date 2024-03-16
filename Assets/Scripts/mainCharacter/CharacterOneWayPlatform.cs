using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class CharacterOneWayPlatform : MonoBehaviour
{
    private GameObject currentOneWayPlatform;
    [SerializeField] private BoxCollider2D playerCollider;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
            if(currentOneWayPlatform != null){
                StartCoroutine(DisableCollision());
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.CompareTag("oneWayPlatform")){
            currentOneWayPlatform = coll.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D coll){
        if(coll.gameObject.CompareTag("oneWayPlatform")){
            currentOneWayPlatform = null;
        }
    }

    private IEnumerator DisableCollision(){

        BoxCollider2D platformerCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(playerCollider,platformerCollider);
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreCollision(playerCollider,platformerCollider,false);

    }



}
