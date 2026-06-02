using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour{

    private enum AIState {Patrol, Pursuing, Searching}
    private AIState state =  AIState.Patrol;
    private bool isSearching = false;

    [SerializeField] private Transform EnemyGFX;

    [Header("Patrol")]
    [SerializeField] private Transform[] patrolWaypoints;
    private int currentWaypoint = 0;

    [Header("Detection")]
    [SerializeField] private float detectionRadius = 5.0f;
    [SerializeField] private float visionAngle= 120.0f;
    [SerializeField] private float reachDistance = 0.5f;
    [SerializeField] private float searchTimeout = 3f; // give up searching after 3 seconds in case it gets stuck
    public LayerMask wallLayer; 

    private float searchTimer = 0f;
    private AIDestinationSetter destinationSetter;
    private Transform player;
    private Vector3 lastSeenPosition; 
    private AIPath aiPath;
    private Vector2 facingDirection = Vector2.right;
    

    // empty GameObject used to point A* at a world position
    private GameObject searchTarget;

    void Start(){
        destinationSetter = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();
        searchTarget = new GameObject("EnemySearchTarget");

        GameObject playerObj = GameObject.FindWithTag("Player");
        if(!playerObj){
            Debug.LogError("Can't find Player");
            return;
        }
        player = playerObj.transform;
        
        if(patrolWaypoints.Length > 0)
            destinationSetter.target = patrolWaypoints[currentWaypoint];
    }

    private bool isPlayerVisible(){
        // human like visibility a.k.a. cone 120 degrees
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if(distanceToPlayer > detectionRadius)
            return false;

        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float angle = Vector2.Angle(facingDirection, directionToPlayer);

        if(angle > visionAngle / 2.0f)
            return false;

        // check for walls in between Player and Enemy
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, distanceToPlayer, wallLayer);
        if (hit.collider != null)
            return false;

        return true;
    }

    private void Patrol(){
        if(patrolWaypoints.Length == 0)
            return;

        destinationSetter.target = patrolWaypoints[currentWaypoint];

        float distanceToWaypoint = Vector2.Distance(transform.position, patrolWaypoints[currentWaypoint].position);
        if(distanceToWaypoint <= reachDistance)
            currentWaypoint = (currentWaypoint + 1) % patrolWaypoints.Length;
    }

    private void Search(){
        if (searchTarget == null){
            state = AIState.Patrol;
            return;
        }

        searchTarget.transform.position = lastSeenPosition;
        destinationSetter.target = searchTarget.transform;

        // fail safe in case it gets stuck (which it will)
        if (aiPath.velocity.magnitude < 0.1f){
            searchTimer += Time.deltaTime;
            if (searchTimer >= searchTimeout){
                searchTimer = 0f;
                isSearching = false;
                state = AIState.Patrol;
                return;
            }
        }
        else searchTimer = 0f;

        float distanceToLastSeen = Vector2.Distance(transform.position, lastSeenPosition);

        if (distanceToLastSeen <= reachDistance){
            isSearching = false;
            state = AIState.Patrol;
        }

    }

    public void Update(){
        if(aiPath.velocity.magnitude > 0.1f){
            facingDirection = aiPath.velocity.normalized;

            // flip GFX based on horizontal movement
            if (facingDirection.x >= 0.01f)
                EnemyGFX.localScale = new Vector3(-1f, 1f, 1f);
            else if (facingDirection.x <= -0.01f)
                EnemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }

        if(isPlayerVisible()){
            state = AIState.Pursuing;
            isSearching = false;
        }
        else if(state == AIState.Pursuing && !isSearching){
            lastSeenPosition = player.position;
            state = AIState.Searching;
            searchTimer = 0f; 
            isSearching = true;
        }
        else if (state != AIState.Searching) 
            state = AIState.Patrol;
            
        switch(state){
            case AIState.Patrol:
                Patrol();
                break;
            case AIState.Pursuing:
                destinationSetter.target = player;
                break;
            case AIState.Searching:
                Search();
                break;
        }
        
    }

     void OnDrawGizmosSelected(){
        Gizmos.color = Color.yellow;
        
        Vector3 leftBound = Quaternion.Euler(0, 0, visionAngle / 2f) * (Vector3)facingDirection * detectionRadius;
        Vector3 rightBound = Quaternion.Euler(0, 0, -visionAngle / 2f) * (Vector3)facingDirection * detectionRadius;
        
        Gizmos.DrawLine(transform.position, transform.position + leftBound);
        Gizmos.DrawLine(transform.position, transform.position + rightBound);
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        if (patrolWaypoints == null || patrolWaypoints.Length == 0) 
            return;
        
        Gizmos.color = Color.blue;
        for (int i = 0; i < patrolWaypoints.Length; i++){
            if (patrolWaypoints[i] == null) continue;
            Gizmos.DrawSphere(patrolWaypoints[i].position, 0.2f);
            if (i + 1 < patrolWaypoints.Length)
                Gizmos.DrawLine(patrolWaypoints[i].position, patrolWaypoints[i + 1].position);
        }
    }
}