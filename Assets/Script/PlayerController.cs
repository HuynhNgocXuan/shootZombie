using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    int damge = 1;
    float fireTime = 0.2f;
    float lastFireTime = 0;
    Animator anima;
    GameObject obj;
    public GameObject gunHead;
    public GameObject smokeShoot;
    private AudioSource audio;
    float playerBlood = 100;
    GameObject gameController;
    public Slider sliderBar;


    // Start is called before the first frame update
    void Start()
    {
        obj = gameObject;
        UpdateFireTime();
        anima = obj.GetComponent<Animator>();
        audio = obj.GetComponent<AudioSource>();
        gameController = GameObject.FindGameObjectWithTag("GameController");
        sliderBar.minValue = 0;
        sliderBar.maxValue = 100;
        sliderBar.value = playerBlood;
    }

    void UpdateFireTime()
    {
        lastFireTime = Time.time;
    }

    void FireAnima(bool isShoot)
    {
        anima.SetBool("isShoot", isShoot);
    }

    void InstantiateSmoke()
    {
        //GameObject smoke = Instantiate(smokeShoot, gunHead.transform.position, gunHead.transform.rotation);
        //Destroy(smoke, 0.2f);
    }

    void Fire()
    {
        if (Time.time >= lastFireTime + fireTime)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

#if UNITY_IOS || UNITY_ANDROID

            RaycastHit hit; 
            if (Physics.Raycast(ray, out hit))
            {
                FireAnima(true);
                InstantiateSmoke();
                if (hit.transform.gameObject.tag.Equals("Zombie"))
                {
                    hit.transform.gameObject.GetComponent<ZombieController>().GetHit(damge);
                }
            }
#else 
            RaycastHit hit; 
            if (Physics.Raycast(gunHead.transform.position, gunHead.transform.forward, out hit))
            {
                FireAnima(true);
                InstantiateSmoke();
                audio.Play();
                if (hit.transform.gameObject.tag.Equals("Zombie"))
                {
                    hit.transform.gameObject.GetComponent<ZombieController>().GetPlayerHit(damge);
                }
            }
#endif
            UpdateFireTime();
        }
       
        else
        {
            FireAnima(false);
        }
    }

    public void GetZombieHit(int damge) 
    {
        playerBlood -= damge;
        sliderBar.value = playerBlood;

        if (playerBlood <= 0)
        {
            gameController.GetComponent<GameController>().EndGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Fire();
        }
    }
}
