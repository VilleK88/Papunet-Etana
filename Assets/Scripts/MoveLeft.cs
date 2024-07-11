using UnityEngine;
public class MoveLeft : MonoBehaviour
{
    float speed = 2.0f;
    float maxTime = 0.5f;
    float counter = 0;
    [SerializeField] Etana etana;
    bool ifHiding; // fetch from Etana -script
    bool dead;
    private void Update()
    {
        ifHiding = etana.GetComponent<Etana>().ifHiding;
        dead = etana.GetComponent<Etana>().dead;
        if(!ifHiding)
        {
            if(maxTime > counter)
                counter += Time.deltaTime;
            else if(!dead)
                transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else
            counter = 0;
    }
}