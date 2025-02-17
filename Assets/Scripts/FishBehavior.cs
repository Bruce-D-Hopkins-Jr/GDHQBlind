using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehavior : MonoBehaviour, IGetHit
{
    bool _canDie = true;
    public void GetHit()
    {
        if (!_canDie)
            return;
        _canDie = false;
        transform.parent = null;
        StartCoroutine(Die());
    }

    /// <summary>
    /// Slowly sink to the ground
    /// </summary>
    /// <returns></returns>
    IEnumerator Die()
    {
        Vector3 currentPos;
        while (transform.position.y > 0)
        {
            currentPos = transform.position;
            currentPos.y -= 1 * Time.deltaTime;
            transform.position = currentPos;
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
