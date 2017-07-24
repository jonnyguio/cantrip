﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandbagController : MonoBehaviour {

    public float speed;
    public float maxHP;

    private float hp;
    private GameObject player;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        hp = maxHP;
	}
	
	void Update () {
        Move();
	}

    void Move ()
    {
        // Vai na direção do jogador
        Vector2 dir;
        dir = player.transform.position - transform.position;
        if (dir.magnitude > 0.001f)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, (speed * Time.deltaTime));
        }
    }

    public void TakeDamage (float damageTaken)
    {
        hp -= damageTaken;
    }

}
