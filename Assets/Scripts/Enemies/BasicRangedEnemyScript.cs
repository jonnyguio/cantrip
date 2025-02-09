﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRangedEnemyScript : EnemyController {

	public float atackRange;
	public GameObject projectile;
	public float atkSpeed;
	public GameObject aimObject;

	[HideInInspector]
	public bool isShooting;
	public int searchRange=14;
	[HideInInspector]
	public float clock;

	public override void Start() {
		base.Start();
	}

	public override void Update() {
		base.Update();

        

        if (SearchPlayerWide())
        {
            isShooting = true;
            aiControler.canMove = false;
        }
        else
        {
            isShooting = false;
            aiControler.canMove = true;
            clock = 0.0f;
        }

        if (isShooting)
        {

            RotateToPlayer();

            clock += Time.deltaTime;
            if (clock>= 1/atkSpeed)
            {
                Shot();
            }
        }

	}

    virtual public void Shot()
    {
        Instantiate(projectile, aimObject.transform.position, this.transform.rotation);
        clock = 0.0f;
    }

	virtual public void RotateToPlayer() {

        Vector3 direcao;
        float angle;
        direcao = player.transform.position - transform.position;
        angle = (Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg) - 90.0f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime *currentSpeed* 3.0f);

    }
	

	virtual public bool SearchPlayerWide() {
		int x = 32;
		int a;
		int b;
        bool playerFound=false;
                
		x = (x * 4) - 4;

		a = (x / 4) - 1;
		b = 0;

		Vector3 rayOrigin;
		Vector3 rayDirection;

		for (int i = 0; i < x; i++) {

			rayOrigin = aimObject.transform.position;
            //Atira raios em todas as direções
            if (i < x / 4)
            {
                rayDirection = aimObject.transform.up * a + aimObject.transform.right * b;
            }
            else if (i < 2 * x / 4)
            {

                if (i == x / 4)
                {
                    a = (x / 4) - 1;
                    b = 0;
                }

                rayDirection = aimObject.transform.up * -a + aimObject.transform.right * b;
            }
            else if (i < 3 * x / 4)
            {
                if (i == x * 2 / 4)
                {
                    a = (x / 4) - 1;
                    b = 0;
                }
                rayDirection = aimObject.transform.up * -a + aimObject.transform.right * -b;
            }
            else {
                if (i == x * 3 / 4)
                {
                    a = (x / 4) - 1;
                    b = 0;
                }
                rayDirection = aimObject.transform.up * a + aimObject.transform.right * -b;
            }

            a--;
			b++;
			
			RaycastHit2D[] ray;

            //Determina o "cone" válido de procura do inimigo
			if (a>=x/searchRange && (i<x/4 || i>= 3 * x / 4)) {
				Debug.DrawRay(rayOrigin, rayDirection, Color.blue);
				ray = Physics2D.RaycastAll(rayOrigin, rayDirection, atackRange);

                //Pega todos os objetos encontrados no Raycast
                    //Procura até ver uma parede ou o jogador                
                for(int j = 0; j < ray.Length; j++)
                {
                    if (ray[j].transform != null)
                    {
                        if(ray[j].transform.gameObject.tag == "Player")
                        {
                            playerFound = true;
                        }
                        if(ray[j].transform.gameObject.tag == "Wall")
                        {
                            break;
                        }
                    }
                }
            }
        }
               

		return playerFound;

	}

    public override void MlAtack()
    {
        //Feijoada.exe
    }

}
