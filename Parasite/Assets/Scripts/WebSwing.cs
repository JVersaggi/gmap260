using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebSwing : MonoBehaviour
{
    public GameObject player;
    public CharacterController webHingeAnchor;
    public ConfigurableJoint webJoint;
    private bool webAttached;
    private Vector3 playerPosition;
    private Rigidbody webHingeAnchorRb;
    public SpriteRenderer arrowIndicator;
    public LayerMask webLayerMask;
    private float webMaxCastDistance = 20f;
    private List<Vector3> webPositions = new List<Vector3>();
    public LineRenderer webRenderer;
    private Vector3 initialPos;


    private float L;             /*Lenght of the rope*/
    private float g = 20.0f;             /*Gravity force*/

    private float theta0 = Mathf.PI/4f;/*Initial angle. Must be different from 0*/
    private float omega0 = 0;                /*Initial angular velocity*/
    private bool _________________;

    private float theta_k;                /*Theta value in step K*/
    private float omega_k;                /*Omega value in step K*/
    private float omega_k1;               /*Omega value in step K+1*/
    private float theta_k1;               /*Theta value in step K+1*/
    private Vector3 p, p0;
    private float a = -1f;
    private float b = -1f;


    void Awake()
    {
        //Destroy(webJoint);
        playerPosition = transform.position;
        //webHingeAnchorRb = webHingeAnchor.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(webAttached)
        {
            omega_k = omega_k1;
            theta_k = theta_k1;
            omega_k1 = omega_k - (g / L) * theta_k * Time.deltaTime;
            theta_k1 = theta_k + omega_k1 * Time.deltaTime;
            Debug.Log(theta_k1);
            webHingeAnchor.Move(new Vector3(Mathf.Abs(L * Mathf.Sin(theta_k1) * Time.deltaTime), 0, 0));
            Debug.Log(new Vector3(Mathf.Abs(L * Mathf.Sin(theta_k1)), 0, 0));
            if(theta_k1 < theta_k)
            {
                a = 1f;
            }
            else
            {
                a = -1f;
            }
            
            
        }

        Vector3 aimDirection = new Vector3(1, 1, 0);
        playerPosition = transform.position;

       if(Input.GetButton("Fire1"))
        {
            arrowIndicator.enabled = true;
        }
       else
        {
            arrowIndicator.enabled = false;
        }

       if(Input.GetButtonUp("Fire1"))
        {
            RaycastHit pos;
            bool hit = Physics.Raycast(playerPosition, aimDirection, out pos, webMaxCastDistance, webLayerMask);
            webRenderer.enabled = true;
            if(hit)
            {
                webAttached = true;
                Debug.Log("Hit shit");
                webPositions.Add(pos.point);
                Debug.Log(pos.point);
                Debug.Log(aimDirection);
                webJoint.projectionDistance = pos.distance;
                
                //webJoint.enabled = true;
                omega_k1 = omega0;
                theta_k1 = theta0;
                p0 = playerPosition;
                
                L = pos.distance;
                p0.x += L;
                player.GetComponent<Movement>().enabled = false;
                
            }
        }
    }
}
