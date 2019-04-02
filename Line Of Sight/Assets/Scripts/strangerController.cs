using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strangerController : MonoBehaviour {
    
    public float moveSpeed = 2f;
    public float rotSpeed = 100f;
    public float timeSeen = 0f;
    public float dist = 0f;

    public Transform player;

    private bool seen = false;
    private bool isWandering = false;
    private bool isRotLeft = false;
    private bool isRotRight = false;
    private bool isWalking = false;

    void OnBecameVisible()
    {
        seen = true;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (seen)
        {
            timeSeen += Time.deltaTime;
            dist = Vector3.Distance(gameObject.transform.position, player.position);

        }

        if (!isWandering)
        {
            StartCoroutine(Wander());
        }
        if (isRotLeft)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
        }
        if (isRotRight)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
        }
        if (isWalking)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }

    IEnumerator Wander()
    {
        int rotTime = Random.Range(1, 3);
        int rotWait = Random.Range(1, 4);
        int rotateLorR = Random.Range(0, 3);
        int walkWait = Random.Range(1, 4);
        int walkTime = Random.Range(1, 5);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotWait);
        if (rotateLorR == 1)
        {
            isRotRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotRight = false;
        }
        if (rotateLorR == 2)
        {
            isRotLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotLeft = false;
        }
        isWandering = false;
    }
}
