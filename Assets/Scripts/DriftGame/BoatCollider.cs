using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCollider : MonoBehaviour
{
    public Boat boat;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (boat.isMainBoat)
        {
            if (other.gameObject.tag == "Home")
            {
                if (!DriftManager.instance.isEnd)
                {
                    DriftManager.instance.EndGame(true);
                }
            }
            if (other.gameObject.tag == "Enemy1" || other.gameObject.tag == "Enemy2")
            {
                boat.health.TakeDamage(boat.riverIndex, 1);
            }
            if (other.gameObject.tag == "Wind")
            {
                RiverController.instance.MoveAnother(boat.riverIndex);
            }
            if (other.gameObject.tag == "Stone")
            {
                RiverController.instance.MoveAnother(boat.riverIndex);
                boat.health.TakeDamage(boat.riverIndex, 1);
            }
            return;
        }
        else
        {
            if (other.gameObject.tag == "Wind" || other.gameObject.tag == "Stone" || other.gameObject.tag == "Enemy2")
            {
                RiverController.instance.MoveAnother(boat.riverIndex);
                boat.health.TakeDamage(boat.riverIndex, 0);
            }
        }
    }
}
