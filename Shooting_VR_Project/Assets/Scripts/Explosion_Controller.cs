using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Controller : MonoBehaviour
{
    [SerializeField]
    float range = 5.0f;
    [SerializeField]
    float damage = 3.0f;

    protected LayerMask layer = 0;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.Instance_Sound(transform.position, "explosion1", 1);

        layer = layer | (1 << LayerMask.NameToLayer("Enemy"));

        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider obj in colliders)
        {
            if (CompareLayer(layer, obj.gameObject.layer))
            {
                obj.GetComponent<AirFighter>().Damage(damage);
            }
        }

    }

    //LayerMaskに対象のLayerが含まれているかチェックする
    private bool CompareLayer(LayerMask layerMask, int layer)
    {
        return ((1 << layer) & layerMask) != 0;
    }

    // Update is called once per frame
    void Update()
    {
        ParticleSystem particle = new ParticleSystem();
        foreach (Transform child in transform)
        {
            particle = child.gameObject.GetComponent<ParticleSystem>();
        }

        if (particle == null) return;
        if (!particle.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }
}
