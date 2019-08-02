using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamAttack : Player
{
    public ParticleSystem beam;
    float BeamTime;
    [SerializeField]
    protected LayerMask layer = 0;
    protected GameObject explosion;
    protected float damege = 5;

    private void Start()
    {
        BeamTime = 0;
        beam = this.GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject obj)
    {
        Debug.Log("ビームが当たった1");
        if (obj.gameObject.GetComponent<AirFighter>() != null)
        {
            Debug.Log("ビームが当たった2");
            AirFighter fighter = obj.gameObject.GetComponent<AirFighter>();

            if (CompareLayer(layer, obj.gameObject.layer))
            {
                Debug.Log("ビームが当たった3");
                //ダメージを与える
                fighter.Damage(damege);
            }

            Explosion();
        }
    }

    //LayerMaskに対象のLayerが含まれているかチェックする
    protected bool CompareLayer(LayerMask layerMask, int layer)
    {
        return ((1 << layer) & layerMask) != 0;
    }

    protected void Explosion()
    {
        if (explosion != null)
        { Instantiate(explosion, this.transform.position, this.transform.rotation); }
    }

    private void Update()
    {
        beam.Play();
        BeamTime += Time.deltaTime;
        if (BeamTime > 5.0f)
        {
            Destroy(this.gameObject);
        }
    }

}
