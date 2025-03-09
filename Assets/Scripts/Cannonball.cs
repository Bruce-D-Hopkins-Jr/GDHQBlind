using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    [SerializeField] GameObject _hitVFX, _enemyHitParticle;

    private void Start()
    {
        Invoke("HitFX", 10f);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.TryGetComponent<IGetHit>(out var hit))
        {
            hit.GetHit();

            Debug.Log("Cannonball hit Something");

            Instantiate(_enemyHitParticle, this.transform.position, Quaternion.identity);
        }

        HitFX();
    }

    private void HitFX()
    {
        Debug.Log("Destroying Cannonball");
        Instantiate(_hitVFX, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
