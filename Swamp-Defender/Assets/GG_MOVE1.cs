using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GG_MOVE1 : MonoBehaviour
{
    private int Health, Stamina;
    private int[] Hits;
    float Ver, Hor, Jump,mouseX,xRotation,FB,LR;
    bool isGround;
    public float Speed = 10, JumpSpeed = 200,mouseSens=100f;
    Animator Ani;
    public void SetHealth(int bonus)
    {
        Health = Health+bonus;
    }
    public void SetStamina(int bonus)
    {
        Stamina = Stamina + bonus;
    }
    private void Start()
    {
        Ani=GetComponent<Animator>();
        Health = 100;
        Stamina = 100;
        Hits = new int[3];
        Hits[0] = -10; Hits[1] = -20; Hits[2] = -30;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }
    private void FixedUpdate()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
        FB = Input.GetAxis("Vertical");
        LR = Input.GetAxis("Horizontal");
        Ani.SetFloat("RunFB", FB);
        Ani.SetFloat("RunLR", LR);
        if (isGround)
        {
            Ver = Input.GetAxis("Vertical") * Time.deltaTime * Speed;
            Hor = Input.GetAxis("Horizontal") * Time.deltaTime * Speed;
            Jump = Input.GetAxis("Jump") * Time.deltaTime * JumpSpeed;
            GetComponent<Rigidbody>().AddForce(transform.up * Jump, ForceMode.Impulse);
           
        } transform.Translate(new Vector3(Hor, 0, Ver));
    }
    public void OnHit(int i)
    {
        SetHealth(Hits[i]);
        Debug.Log(Health);
        if (Health <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
