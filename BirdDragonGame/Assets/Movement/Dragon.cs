using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour {

    GameObject player;
    float speed;
    float rotationSpeed;
    Rigidbody rigidBody;

    List<GameObject> pathfindingLocations;

    Coroutine trackingUpdate;
    GameObject target;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        speed = 24.0f;
        rotationSpeed = 2.0f;
        rigidBody = gameObject.GetComponent<Rigidbody>();
        GameObject[] pathfindingPoints = GameObject.FindGameObjectsWithTag("PathfindPoint");
        pathfindingLocations = new List<GameObject>();

        foreach(GameObject point in pathfindingPoints)
        {
            pathfindingLocations.Add(point);
        }
        pathfindingLocations.Add(gameObject);
        trackingUpdate = null;
        target = player;
    }
	
	// Update is called once per frame
	void Update () {

        if(trackingUpdate == null)
        {
            trackingUpdate = StartCoroutine(findTarget());
        }
        GoTo(target);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManagementScript.instance.SetCurrentApplicationState("LOSEGAME");
        }
    }

    IEnumerator findTarget()
    {
        target = CalculateClosestPlayerPoint();

        if (target == gameObject)
        {
            target = player;
        }
        
        yield return new WaitForSeconds(2.0f);
        trackingUpdate = null;
    }

    GameObject CalculateClosestPlayerPoint()
    {   
        float distanceToCompare = 9999999999.9f;
        GameObject closestPoint = null;
        foreach (GameObject point in pathfindingLocations)
        {
            float distance = Vector3.Distance(point.transform.position, player.transform.position);
            if (distance < distanceToCompare)
            {
                closestPoint = point;
                distanceToCompare = distance;
            }
        }

        return closestPoint;
    }

    void GoTo(GameObject goTo)
    {
        Vector3 goHerePosition = goTo.transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(goHerePosition - transform.position);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        rigidBody.AddRelativeForce(0.0f, 0.0f, speed, ForceMode.Force);
    }
}
