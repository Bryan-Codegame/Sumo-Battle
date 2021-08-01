using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody _rigidbody;
    
    private GameObject player;

    private float limitHeight = 10.0f;

    public float moveForce = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //La magnitud del vector es lo mismo que mide este, por eso si aplicamos una fuerza la magnitud del vector sería la misma que el valor de la fuerza por lo tanto si el vector no se normaliza es decir que su magnitud siempre sea de uno entonces su tamañao sería en relación a la fuerza aplicada pero sólo queremos la direcció  
        Vector3 followDirection = (player.transform.position - this.transform.position).normalized;
        _rigidbody.AddForce(followDirection*moveForce, ForceMode.Force);

        //Muerte por caer al vacío
        if (this.transform.position.y < -limitHeight)
        {
            Destroy(gameObject);
        }
    }


}
