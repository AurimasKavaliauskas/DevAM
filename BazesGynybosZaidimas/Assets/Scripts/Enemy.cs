﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class Enemy : MonoBehaviour {
    //To access variables from different scripts variable should be changed to static
    public Animator anim;//animacija
    public GameObject damageTakenParticle;
    public float enemySpeed = 1;         // How fast enemy is moving
    public float curent_enemy_hp = 10;   //  public float start_hp;
    public int enemy_HP = 10;
    public int scoreValue = 17;
    public static string name = "enemy";
    public static int count_deaths_this_enemy;
    public Image HP;


    void OnCollisionEnter2D (Collision2D col)
    {
        if(col.gameObject.tag == "Bullet")
        {
			curent_enemy_hp -= Player.weaponDamage;
            Instantiate(damageTakenParticle, transform.position, transform.rotation);      
        }
        if (col.gameObject.tag == "Player" || col.gameObject.tag== "Wall")
        {
            Recoil();
        }
    }

    void HealthBar() {
        HP.fillAmount = curent_enemy_hp / enemy_HP;
    }
    void start() {
      anim= GetComponent<Animator>(); //animacija


    }
    // Update is called once per frame
    void Update(){
     //   float move = Input.GetAxis("Horizontal");
        Debug.Log(enemySpeed);
        // float move = 2.0f;
     anim.SetFloat("speed",enemySpeed);
        HealthBar();
        IsDead();
		Movement (); 		                                                                    
	}

	public void Movement(){		
		transform.Translate (Vector2.right * Time.deltaTime * enemySpeed);
	}

    public void Recoil(){
        transform.Translate(Vector2.left * Time.deltaTime * 15);
        enemySpeed = 1;
    }

    void IsDead()
    {
        if (curent_enemy_hp <= 0)
        {
            count_deaths_this_enemy++;
            Player.score += scoreValue;
            anim.SetBool("death", true);
            float a = 5f;
            Destroy(gameObject);
            Spawner.currentlyAlive--;
            if (Spawner.leftToSpawn == 0 && Spawner.currentlyAlive == 0)
                FindObjectOfType<Ending>().NextLevel();
        }
    }
    
    }
