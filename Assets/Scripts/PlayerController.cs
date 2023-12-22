using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin,xMax,zMax,zMin;
}


public class PlayerController : MonoBehaviour
{
    Rigidbody physic;
    AudioSource audioPlayer;
    [SerializeField] int speed;
    [SerializeField] int tilt;
    [SerializeField] float nextFire;
    [SerializeField] float fireRate;
    public Boundary boundary;

    public GameObject shot;
    public GameObject shotSpawn;

    void Start()
    {
        physic = GetComponent<Rigidbody>();
        audioPlayer = GetComponent<AudioSource>();
    }

    void Update()
    {   
        if(Input.GetButton("Fire1") && Time.time > nextFire){
            nextFire = Time.time + fireRate;
            Quaternion spawnRotation = Quaternion.identity;

            Instantiate(shot,shotSpawn.transform.position,spawnRotation);
            audioPlayer.Play();
           
        }
    }
    void FixedUpdate()
    {
        //Hareket Kodları
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movment = new Vector3(moveHorizontal,0,moveVertical);

        physic.velocity = movment * speed;


        // Hareketi sınırlandırmak için yazılan kodlar
        Vector3 position = new Vector3(
            Mathf.Clamp(physic.position.x , boundary.xMin , boundary.xMax),
            1,
            Mathf.Clamp(physic.position.z , boundary.zMin , boundary.zMax)
            );

        physic.position = position;

        //Hareket animasyonı kodları

        physic.rotation = Quaternion.Euler(physic.velocity.z * tilt/2,0,physic.velocity.x * -tilt);

        
    
    }
}
