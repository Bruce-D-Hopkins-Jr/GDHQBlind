using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private Renderer turtleEye1;
    [SerializeField]
    private Renderer turtleEye2;

    private float distance;

    private Color redEmission = Color.red;
    private Color yellowEmission = Color.yellow;
    private Color greenEmission = Color.green;

    // Update is called once per frame
    void Update()
    {
        distance = CheckDistance();

        if (distance > 45.0f)
        {
            ChangeEyeColor(redEmission);
        }
        else if (distance <= 45.0f && distance > 30.0f)
        {
            ChangeEyeColor(yellowEmission);
        }
        else if (distance <= 30.0f)
        {
            ChangeEyeColor(greenEmission);
        }
    }

    private float CheckDistance()
    {
        return Vector3.Distance(transform.position, player.position);
    }

    private void ChangeEyeColor(Color emissionColor)
    {
        SetEmission(turtleEye1, emissionColor);
        SetEmission(turtleEye2, emissionColor);
    }

    private void SetEmission(Renderer rend, Color emissionColor)
    {
        Material mat = rend.material; // creates a unique instance of the material
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", emissionColor);
    }
}
