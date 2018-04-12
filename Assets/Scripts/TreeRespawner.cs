using System.Collections;
using UnityEngine;

public class TreeRespawner : MonoBehaviour
{
    ParticleSystem dirtParticles;
    ParticleSystem leafParticles;

    float treeScale;
    GameObject treeClone;
    float respawnTime;
    bool waitingForRespawn;

    public AnimationCurve AnimationCurve;

    public void PrepareRespawn()
    {
        waitingForRespawn = true;
        respawnTime = Time.time + 5;
    }
    
    void Awake()
    {
        dirtParticles = transform.Find("DirtParticles").GetComponent<ParticleSystem>();
        var dpMain = dirtParticles.main;
        dpMain.loop = true;
        leafParticles = transform.Find("LeafParticles").GetComponent<ParticleSystem>();
        var lpMain = leafParticles.main;
        lpMain.loop = true;
        CloneTree();
    }
	
	void Update()
    {
        if (waitingForRespawn && Time.time > respawnTime)
        {
            waitingForRespawn = false;
            treeClone.name = "Tree";
            StartCoroutine(ScaleUp());
        }
    }

    void CloneTree()
    {
        treeClone = Instantiate(transform.parent.Find("Tree").gameObject, transform.parent);
        treeClone.transform.Find("Foliage").GetComponent<SphereCollider>().enabled = false;
        treeScale = treeClone.transform.localScale.x;
        treeClone.name = "TreeClone";
        treeClone.transform.localScale = new Vector3(0, 0, 0);
    }

    IEnumerator ScaleUp()
    {
        dirtParticles.Play();
        leafParticles.Play();

        var foliageTransform = treeClone.transform.Find("Foliage");

        for (var f = 0f; f <= 1; f += 0.005f)
        {
            var curveValue = AnimationCurve.Evaluate(f);
            var scaleValue = curveValue * treeScale;
            treeClone.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
            leafParticles.transform.position = new Vector3(foliageTransform.position.x, foliageTransform.position.y - 1, foliageTransform.position.z);
            yield return null;
        }

        treeClone.transform.Find("Foliage").GetComponent<SphereCollider>().enabled = true;
        dirtParticles.Stop();
        leafParticles.Stop();
        CloneTree();
    }
}
