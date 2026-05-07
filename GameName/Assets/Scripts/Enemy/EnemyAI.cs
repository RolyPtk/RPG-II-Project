using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200.0f;
    public float nextWaypointDistance = 3.0f;

    public Transform EnemyGFX;

    Path path;
    int currentwaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0.0f, 0.5f); //called every half a second
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)   
    {   
        if(!p.error){
            path = p;
            currentwaypoint = 0;
        }
    }

    // It's only called a fixed number of times so work with this when doing physics as well 
    void FixedUpdate()
    {
        if(path == null)
            return;

        if(currentwaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }   
        else reachedEndOfPath = false;

        Vector2 direction = ((Vector2)path.vectorPath[currentwaypoint] - rb.position).normalized; // always work with normalised vectors
        Vector2 force = direction * speed * Time.deltaTime; // a seperate vector for force, always use deltaTime so it's not frame dependent (1000 fps would make the enemy 10 times faster than 100 fps so)

        // don't forget to add air resistance in the editor by adding linear drag (wind resistance) tp like 1.5 maybe
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentwaypoint]); // for checking to see if we need to move to the next waypoint

        if(distance < nextWaypointDistance)
            currentwaypoint++;

        // currently useless, it will flip thee sprite left and right, up or down so it faces how it moves
        // should be modified later cause we will have 4 directions, not two but meh
        if(force.x >= 0.01f)
            EnemyGFX.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if(force.x <= -0.01f)
            EnemyGFX.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
}
