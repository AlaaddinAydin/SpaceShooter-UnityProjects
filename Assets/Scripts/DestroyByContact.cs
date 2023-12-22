using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject expolison;
    public GameObject playerExplosion;
    public GameController gameController;

    private void Start() {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();    
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Boundary")
        {
            return;
        }
        Instantiate(expolison,transform.position,transform.rotation);
        if(other.tag == "Player")
        {
            Instantiate(playerExplosion,other.transform.position,other.transform.rotation);
            gameController.GameOver();
        }
        
        Destroy(other.gameObject);
        Destroy(gameObject);
        gameController.UpdateScore();
    }
}
