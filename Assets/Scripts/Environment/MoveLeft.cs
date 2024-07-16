using System.Collections;
using UnityEngine;
public class MoveLeft : MonoBehaviour
{
    float speed = 2.0f; // 2.0f original
    float maxTime = 0.5f;
    float counter = 0;
    [SerializeField] Etana etana;
    bool ifHiding; // fetch from Etana -script
    public bool stopMovingBG;
    private void Update()
    {
        ifHiding = etana.GetComponent<Etana>().ifHiding;
        if(!stopMovingBG)
        {
            if (!ifHiding)
            {
                if (maxTime > counter)
                    counter += Time.deltaTime;
                else
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
            else
                counter = 0;
        }
    }
}