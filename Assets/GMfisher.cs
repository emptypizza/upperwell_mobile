using UnityEngine;
using System.Collections;


public class GMfisher : MonoBehaviour
{
    public Transform player;
    public GameObject laserPrefab;
    public Transform laserSpawnPoint;
    public float stareDuration = 3f;
    public float laserShootDuration = 2f;
    public float floatUpSpeed = 2f;
    public float laserSpeed = 5f;

    private Rigidbody2D rb;
    private bool eventEnded = false;
    public UIManager uiManager;


    void Start()
    {
        //if(null == uiManager)
        uiManager = GameObject.FindAnyObjectByType<UIManager>();

         rb = GetComponent<Rigidbody2D>();
        StartCoroutine(FisherEvent());
    }

    IEnumerator FisherEvent()
    {
        ballrumble playerScript = player.GetComponent<ballrumble>();
        playerScript.enabled = false; // Disable player control
        playerScript.RotatePlayer(180f); // Rotate player 180 degrees on z axis
        uiManager.HideText(); // Hide UI Text

        yield return new WaitForSeconds(stareDuration); // Stare at the player
        rb.velocity = new Vector2(floatUpSpeed, floatUpSpeed); // Float upwards
        GameObject laser = Instantiate(laserPrefab, laserSpawnPoint.position, Quaternion.identity);
       // Rigidbody2D laserRb = laser.GetComponent<Rigidbody2D>();
        Vector2 laserDirection = (player.position - transform.position).normalized;
      //  laserRb.velocity = laserDirection * laserSpeed;
        laser.transform.Translate(laserDirection * laserSpeed);

        yield return new WaitForSeconds(laserShootDuration); // Wait while shooting the laser

        playerScript.enabled = true; // Re-enable player control
        playerScript.RotatePlayer(180); // Rotate player 180 degrees on z axis
        uiManager.ShowText(); // Show UI Text
     
        eventEnded = true;
    }

    void Update()
    {
        if (eventEnded) Destroy(this); // If the event has ended, destroy this script
    }
}
