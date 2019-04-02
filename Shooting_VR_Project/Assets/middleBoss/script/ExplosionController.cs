using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public GameObject Effect;

    public float Range = 10.0f;
    public float Force = 100.0f;

    [SerializeField] int Damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 hitPosition = collision.contacts[0].point;
        Instantiate(Effect, hitPosition, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(hitPosition, Range);
        foreach(Collider obj in colliders)
        {
            if(obj.gameObject.tag == "Beacon")
            {
                obj.GetComponent<Rigidbody>().AddExplosionForce(Force, hitPosition, Range);
                obj.GetComponent<Beacon>().Damage(Damage);
            }
        }

        Destroy(this.gameObject);
    }

}
