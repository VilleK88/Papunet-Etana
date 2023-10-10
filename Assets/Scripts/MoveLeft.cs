using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    float speed = 2.0f;
    float maxTime = 0.5f;
    float counter = 0;

    [SerializeField] Etana etana;
    bool ifHiding; // fetch from Etana -script
    bool dead;

    float firstCounter = 0;
    float firstMaxTime = 0.65f;

    float secondCounter = 0;
    float secondMaxTime = 0.2f;

    private void Update()
    {
        ifHiding = etana.GetComponent<Etana>().ifHiding;
        dead = etana.GetComponent<Etana>().dead;

        if(!ifHiding)
        {
            if(maxTime > counter)
            {
                counter += Time.deltaTime;
            }
            else if(!dead)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);

                /*if (firstCounter < firstMaxTime)
                {
                    firstCounter += Time.deltaTime;
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
                }
                else
                {
                    if(secondCounter < secondMaxTime)
                    {
                        secondCounter += Time.deltaTime;
                    }
                    else
                    {
                        firstCounter = 0;
                        secondCounter = 0;
                    }
                }*/
            }
        }
        else
        {
            counter = 0;
            //firstCounter = 0;
            //secondCounter = 0;
        }
    }
}
