using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[ExecuteAlways]
public class SpatialAwareness : MonoBehaviour
{
    private BoxCollider2D _box;
    private Rigidbody2D _rb;
    [SerializeField] private SpatialAwarenessSO saObject;
    
    [SerializeField] private bool debugNormals;
    [SerializeField] private bool debugGroundDetection;
    

    public BoxCollider2D box { get {return _box;} }
    public Rigidbody2D rb { get {return _rb;} }
    public float skinWidth { get { return saObject.skinWidth; } }
    public bool grounded { get {return _grounded;} }
    public List<KeyValuePair<Vector2, float>> groundNormals { get {return _groundNormals;} }
    public Vector2 groundNormal { get {return _groundNormal;} }
    public float distanceToGround { get {return _distanceToGround;} }
    public float timeLastGrounded { get {return _timeLastGrounded;} }

    private bool _grounded;
    private List<KeyValuePair<Vector2, float>> _groundNormals;
    private List<DebugRay> _groundDebugRays;
    private Vector2 _groundNormal;
    private float _distanceToGround;
    private float _timeLastGrounded;
    private BoxRayOffsets _rayOffsets;
    private Vector2 _firstGroundRay => (Vector2)transform.position + _rayOffsets.bottomLeft;
    private Vector2 _lastGroundRay => (Vector2)transform.position + _rayOffsets.bottomRight;
    
    public void Awake() 
    {
        _rb = GetComponent<Rigidbody2D>();
        _box = GetComponent<BoxCollider2D>();
        CalculateRayOffsets();
        _groundNormal = Vector2.zero;
    }

    public void OnEnable() 
    {    
        _groundDebugRays = new List<DebugRay>();
        _groundNormals = new List<KeyValuePair<Vector2, float>>();
    }

    public void FixedUpdate() 
    {
        DetectGround();
        CalculateGroundNormal();
    }

    public void OnDrawGizmos()
    {
        /* 
        TODO: Run these commands ONLY if in EDITOR
        */
        DetectGround();
        CalculateGroundNormal();

        DrawGroundRays();
        DrawAverageRay();
    }

    public void DrawGroundRays()
    {
        if (_groundDebugRays.Count > 0)
        {
            for (int i = 0; i < _groundDebugRays.Count; i++)
            {
                DebugRay ray = _groundDebugRays[i];
                Debug.DrawRay(ray.origin, ray.direction, ray.color);
            }
        }
    }
    public void DrawAverageRay()
    {
        Debug.DrawRay(transform.position, groundNormal, Color.black);
    }

    /*
    * Summary: CalculateGroundNormals
    * Tries to average together the ground normals
    * If no normals exist (not above ground), will return Vec2.zero
    */
    public void CalculateGroundNormal()
    {
        if (_groundNormals.Count == 0)
        {
            _groundNormal = Vector2.zero;
            return;
        }


        Vector2 n;
        bool isLocalMaxima = false;

        float leftMostDist = _groundNormals[0].Value;
        float rightMostDist = _groundNormals[_groundNormals.Count - 1].Value;

        KeyValuePair<Vector2, float> nearestNormal = _groundNormals[0];
        for (int i = 1; i < _groundNormals.Count; i++)
        {
            nearestNormal = (_groundNormals[i].Value < nearestNormal.Value) ? _groundNormals[i] : nearestNormal;
            isLocalMaxima |= (leftMostDist > _groundNormals[i].Value) && (rightMostDist > _groundNormals[i].Value);
        }

        n = (!isLocalMaxima) ? nearestNormal.Key : Vector2.up;
        
        _groundNormal = n;
    }

    /*
    * Summary
    * Casts up to saObject.numRaysX rays using CastGroundRay, adds normals to the list
    */
    public void DetectGround() 
    {
        _groundNormals.Clear();
        _groundDebugRays.Clear();
        _distanceToGround = 0.0f;

        CalculateRayOffsets();
        bool collision = false;
        RaycastHit2D raycastHit;

        float dist = saObject.hitDistance * 2.0f;
        for (int i = 0; i < saObject.numRaysX; i++)
        {
            collision |= CastGroundRay((float)i/((float)saObject.numRaysX - 1.0f), out raycastHit);
            if (raycastHit.collider != null)
            {
                dist = math.min(raycastHit.distance, dist);
                _groundNormals.Add(
                    new KeyValuePair<Vector2, float>(raycastHit.normal, raycastHit.distance)
                    );
                if (debugNormals)
                    AddNormalDebugRay(raycastHit);
            }
        }

        if (dist <= saObject.hitDistance)
            _distanceToGround = dist;
        _grounded = collision;
        
        if (_grounded)
            _timeLastGrounded = Time.time;
    }

    public void AddNormalDebugRay(RaycastHit2D hit)
    {
        _groundDebugRays.Add(new DebugRay(hit.point, hit.normal, Color.blue));
    }

    public bool CastGroundRay(float t, out RaycastHit2D hit)
    {
        Vector2 rayOrigin = math.lerp(_firstGroundRay, _lastGroundRay, t);

        
        int layerMask = 1<<6;
        
        hit = Physics2D.Raycast(rayOrigin, Vector2.down, saObject.hitDistance, layerMask);

        if (debugGroundDetection)
            AddGroundDebugRay(rayOrigin, Vector2.down, hit);
        
        return (hit.collider != null && hit.distance <= saObject.skinWidth);
    }

    public void AddGroundDebugRay(Vector2 origin, Vector2 direction, RaycastHit2D hit)
    {
            if (hit.collider != null)
            {
                if (hit.distance <= saObject.skinWidth)
                    _groundDebugRays.Add(new DebugRay(origin, Vector2.down * hit.distance, Color.green));
                else
                    _groundDebugRays.Add(new DebugRay(origin, Vector2.down * hit.distance, Color.yellow));
            } 
            else 
            {
                _groundDebugRays.Add(new DebugRay(origin, Vector2.down, Color.red));
            }
    }
    public void CalculateRayOffsets()
    {
        _rayOffsets = new BoxRayOffsets(_box.bounds, transform.position);
    }
}

public struct BoxRayOffsets
{
    public BoxRayOffsets(Bounds bounds, Vector2 origin)
    {
        bottomLeft = new Vector2(bounds.min.x, bounds.min.y) - origin;
        bottomRight = new Vector2(bounds.max.x, bounds.min.y) - origin;
        topLeft = new Vector2(bounds.min.x, bounds.max.y) - origin;
        topRight = new Vector2(bounds.max.x, bounds.max.y) - origin;
    }

    public Vector2 bottomLeft {get; }
    public Vector2 bottomRight {get; }
    public Vector2 topLeft {get; }
    public Vector2 topRight {get; }
}
