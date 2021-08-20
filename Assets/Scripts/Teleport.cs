﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    public GameObject spawnPoint;
    public static GameObject SpawnPoint;
    public static GameObject[] mainballsstatic= new GameObject[6];
    public  GameObject[] mainballs;

    public Text point;
    public float angle=0.2f;
    public float speed;
    public float radius;
    static public int i = 0; //Кол-во шаров
    int a=1;

    private void Start()
    {
        SpawnPoint = spawnPoint;
        for (int j = 0; j < 6; j++)
        {
            mainballsstatic[j] = mainballs[j];
        }
    }
    private void Update()
    {
        angle += a*Time.deltaTime; 

        var x = Mathf.Cos(angle * speed) * radius+0;
        var y = Mathf.Sin(angle * speed) * radius+1.95f;
        spawnPoint.transform.position = new Vector2(x, y);
        if ((x < -1.35f && a>0)|| (x>1.35f&& a<0))
            a = -a;
      
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.SetActive(false);
        for (int j = 0; j < 6; j++)
        {
            if (mainballs[j].activeSelf)
                break;
            else if (j == 5)
            {
                GameManager.Point = 0;
                point.text = "" + 0;
                StartCoroutine(Spawn());
            }
        }
    }

    IEnumerator Spawn()
    {
        for (int j = 0; j <= i; j++)
        {
            mainballs[j].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            mainballs[j].GetComponent<Rigidbody2D>().angularVelocity = 0f;
            mainballs[j].transform.position = spawnPoint.transform.position;
            mainballs[j].SetActive(true);
            yield return new WaitForSeconds(1f);
        }
    }

   /* static public void Ressed()
    {
        mainballsstatic[i].transform.position = SpawnPoint.transform.position;
        mainballsstatic[i].SetActive(true);
    }
 */
}
