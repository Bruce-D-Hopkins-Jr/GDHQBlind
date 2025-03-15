using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpearBehavior : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    void OnCollisionEnter(Collision col)
    {
        var localScale = transform.localScale;
        transform.parent = col.transform;
        transform.localScale = localScale;
        rb.isKinematic = true;
        //Above code attaches the spear to whatever it hits

        if (col.gameObject.TryGetComponent<IGetHit>(out var hit))
        {
            hit.GetHit();
        }
    }
}
