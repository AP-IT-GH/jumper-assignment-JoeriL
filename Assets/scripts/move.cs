using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public GameObject target;
    public bool Move;
    public float movementmultiplier;
    static float random;
    // Start is called before the first frame update
    void Start()
    {
        random = Random.value;
    }

    // Update is called once per frame
    void Update()
    {
        Move = CubeAgent.mover;
        if (Move)
        {
            target.transform.Translate(Vector3.left * Time.deltaTime * ((movementmultiplier * random)+ 3));
        }
    }
    public static void newrandom()
    {
        random = Random.value;
    }
}
