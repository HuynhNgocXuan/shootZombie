using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    int zombieBlood = 2;
    GameObject obj;
    Animator anima;
    bool isShoted;
    float shotedTime = 0.4f;
    float lastShotedTime = 0;
        
    public bool IsShoted { 
        get => isShoted; 
        set
        {
            isShoted = value;
            ShotedAnimation(isShoted);
            UpdateLastShotedTime();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        obj = gameObject;
        anima = obj.GetComponent<Animator>();
        IsShoted = false;
        anima.SetBool("isDead", false);
    }

    void UpdateLastShotedTime()
    {
        lastShotedTime = Time.time;
    }

    void ShotedAnimation(bool isShoted)
    {
        anima.SetBool("isShoted", isShoted);
    }



    public void GetPlayerHit(int damge)
    {
        IsShoted = true;

        zombieBlood -= damge;
        if (zombieBlood <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        anima.SetBool("isDead", true);
        Destroy(obj, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsShoted && Time.time >= lastShotedTime + shotedTime)
        {
            IsShoted = false;
        }
    }
}


