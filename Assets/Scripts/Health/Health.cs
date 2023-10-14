using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Health : MonoBehaviour
{
    public int maxHealth;
    [SerializeField] HealthBar healthBar;
    [HideInInspector] public float currentHealth;
    public GameObject deathBloodVFX;
    public Animator animator;
    public GameObject damageBloodVFX;
    private GameObject player;
    public GameObject testLoseImage;
    public GameObject GameManagerObject;


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetBarValue(currentHealth, maxHealth);
        player = GameObject.FindGameObjectWithTag("Player");
        //StartCoroutine(time());

    }

    private void Update()
    {

    }
    public void GetDamage(int _count)
    {
        currentHealth -= _count;
        /*
        if (damageBloodVFX != null )
        {
            Instantiate(damageBloodVFX, new Vector3(0, 1, 0), Quaternion.identity);
        }
        */


        healthBar.SetBarValue(currentHealth, maxHealth);
        
        if (currentHealth <= 0)
        {
            if (gameObject.tag == "enemy")
            {
                
                //deathBloodVFX.SetActive(true);
                GameManagerObject.GetComponent<GameManager>().IncreaseScore(50);
                GameManagerObject.GetComponent<GameManager>().IncreaseEnemiesKilled(1);
                if (deathBloodVFX != null)
                {
                    deathBloodVFX.SetActive(true);
                }
                Invoke("Death", 1f);
            }

            if (gameObject.tag == "Player")
            {
                Camera.main.transform.DetachChildren();
                Camera.main.transform.parent = null;
                Camera.main.GetComponent<FirstPersonLook>().enabled = false;
                Camera.main.GetComponent<Zoom>().enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                testLoseImage.SetActive(true);
            }
            
        }


    }
    

    public void IncreaseHealth()
    {
        if (gameObject.tag == "Player")
        {
            if (currentHealth <= 100f)
            {
                currentHealth+=2f;
                healthBar.SetBarValue(currentHealth, maxHealth);
            }

        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}