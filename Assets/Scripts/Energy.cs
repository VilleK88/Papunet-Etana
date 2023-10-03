using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] int startingEnergy = 100;
    public int currentEnergy;

    private void Awake()
    {
        currentEnergy = startingEnergy;
    }

    public void TakeDamage(int _damage)
    {
        currentEnergy = Mathf.Clamp(currentEnergy - _damage, 0, startingEnergy);

        if(currentEnergy > 0)
        {
            // Player hurt.
        }
        else
        {
            // Player dead.
        }
    }
}
