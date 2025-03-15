using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _laserProjectile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //fire the laser projectile from this postition
            Instantiate(_laserProjectile, this.transform.position, Quaternion.identity);
        }
    }
}
