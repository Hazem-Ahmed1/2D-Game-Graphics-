using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attakCooldown;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && cooldownTimer > attakCooldown && playerMovement.canAttak())
            Attak();

        cooldownTimer += Time.deltaTime;
    }

    public void Attak()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

    }
}