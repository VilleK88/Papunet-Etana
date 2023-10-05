using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    float speed = 5;
    float maxTime = 0.5f;
    float counter = 0;

    [SerializeField] Etana etana;
    bool ifHiding; // fetch from Etana -script

    private void Update()
    {
        ifHiding = etana.GetComponent<Etana>().ifHiding;

        if(!ifHiding)
        {
            if(maxTime > counter)
            {
                counter += Time.deltaTime;
            }
            else
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
        }
        else
        {
            counter = 0;
        }
    }
}
