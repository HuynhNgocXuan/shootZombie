using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    GameObject player;
    GameObject obj;
    GameObject atPlayer;
    float moveSpeed;
    public float minMoveSpeed;
    public float maxMoveSpeed;
    Animator anima;
    float distanceAttack = 1;

    // Start is called before the first frame update
    void Start()
    {
        minMoveSpeed = 0.1f;
        maxMoveSpeed = 0.5f;
        obj = gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        atPlayer = GameObject.FindGameObjectWithTag("AtPlayer");
        UpdateMoveSpeed();
        anima = obj.GetComponent<Animator>();
        AttackAnima(false);

    }

    void UpdateMoveSpeed()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
    }

    void Move()
    {
        if (player == null || atPlayer == null)
        {
            Debug.Log("Bug");
            return;
        }

        obj.transform.LookAt(player.transform.position);
        obj.transform.position = Vector3.Lerp(obj.transform.position, atPlayer.transform.position, Time.deltaTime * moveSpeed);
        
        if (Vector3.Distance(transform.position, player.transform.position) < distanceAttack)
        {
            AttackAnima(true);
            obj.GetComponent<MoveToPlayer>().enabled = false;
        }
    }

    void AttackAnima(bool isAttack)
    {
        anima.SetBool("isAttack", isAttack);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
