using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float moveForce = 5.0f;
    
    //PowerUps
    private bool hasPickUp;
    public float powerForce = 20;
    private float lifeTimePowerUp = 6.0f;

    public GameObject[] powerUpIndicator;

    private GameObject focalPoint;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocusCamera");
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");

        //El focal point hace referencia a la dirección frontal en relación al movimiento de la cámara es decir, cada que movemos la camara el forward toma otra dirección y hacia esta dirección es que se va a mover mi pelota.

        _rigidbody.AddForce(focalPoint.transform.forward*moveForce*verticalInput, ForceMode.Force);

        //Ubica al indicador en la posición del player todo el tiempo
        foreach (GameObject indicator in powerUpIndicator)
        {
            //0.5f*Vector3.down ubica un poco mas abajo al indicador de donde es el objeto.
            indicator.gameObject.transform.position = this.transform.position + 0.5f*Vector3.down;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("PickUp"))
        {
            hasPickUp = true;
            Destroy(other.gameObject);
            
            StartCoroutine(PowerUpCountDown());
        }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Foe") && hasPickUp)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            //Sin normalize 
            //Cuando generamos un vector entre dos puntos en este caso entre dos objetos que colisionan, no es necesario hacer de módulo 1 al vector porque cuando los objetos colisionan siempre va a existir la misma distancia entre ellos.
            //Genera la dirección del vector opuesto al personaje 
            Vector3 playerImpulseFoe = collision.gameObject.transform.position - this.transform.position;

            enemyRigidbody.AddForce(playerImpulseFoe*powerForce, ForceMode.Impulse);
        }
    }

    //Corutina que optimiza los procesos de frame a frame para no sobrecardar al update
    //Esta corutina desactiva el powerUP después de un tiempo determinado en este caso 5 segundos.
    IEnumerator PowerUpCountDown()
    {
        //El yield return finaliza la corutina a partir de una condición que en este caso es el waitforseconds también puede retornar null es decir sin alguna condición.
        
        //Cambia el indicador respecto pasa el tiempo
        foreach (GameObject indicator in powerUpIndicator)
        {
            indicator.gameObject.SetActive(true);
            yield return new WaitForSeconds(lifeTimePowerUp/powerUpIndicator.Length);
            indicator.gameObject.SetActive(false);
        }
        
        hasPickUp = false;
    }
}
