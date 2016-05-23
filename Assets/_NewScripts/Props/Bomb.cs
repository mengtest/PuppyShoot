using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
    public Vector2 horForce = new Vector2(-10, 0);
	public float bombRadius = 10f;			// Radius within which enemies are killed.
	//public float bombForce = 100f;			// Force that enemies are thrown from the blast.
	public AudioClip boom;					// Audioclip of explosion.
	public AudioClip fuse;					// Audioclip of fuse.
	public float fuseTime = 1.0f;
	public GameObject explosion;			// Prefab of explosion effect.
	private ParticleSystem explosionFX;		// Reference to the particle system of the explosion effect.
    private AudioSource audioPlayer;
    private GameObject m_exploreEffect;


    void OnEnable()
    {
        //一出现就获得向-x轴的加速度
        this.GetComponent<Rigidbody2D>().velocity = horForce;
        StartCoroutine(BombCount());
    }

    void Awake()
    {
        audioPlayer = this.GetComponent<AudioSource>();
    }

    //void Awake ()
    //{
    //    // Setting up references.
    //    explosionFX = GameObject.FindGameObjectWithTag("ExplosionFX").GetComponent<ParticleSystem>();
    //    if(GameObject.FindGameObjectWithTag("Player"))
    //        layBombs = GameObject.FindGameObjectWithTag("Player").GetComponent<LayBombs>();
    //}

    //void Start ()
    //{
		
    //    // If the bomb has no parent, it has been laid by the player and should detonate.
    //    if(transform.root == transform)
    //        StartCoroutine(BombDetonation());
    //}


    //IEnumerator BombDetonation()
    //{
    //    // Play the fuse audioclip.
    //    AudioSource.PlayClipAtPoint(fuse, transform.position);

    //    // Wait for 2 seconds.
    //    yield return new WaitForSeconds(fuseTime);

    //    // Explode the bomb.
    //    Explode();
    //}

    IEnumerator BombCount()
    {
        audioPlayer.Play();
        yield return new WaitForSeconds(fuseTime);
        Explode();
    }


    public void Explode()
    {
        //播放爆炸声音
        AudioSource.PlayClipAtPoint(boom, this.gameObject.transform.position, 1.0f);

        //从对象池获取爆炸特效
        m_exploreEffect = ObjectPooler.Dequeue(PoolKeys.ExplosionEffect).gameObject;
        m_exploreEffect.gameObject.transform.position = (this.gameObject.transform.position);
        m_exploreEffect.gameObject.SetActive(true);
        m_exploreEffect.gameObject.GetComponent<ParticleSystem>().Play();
        //延迟特效入池
        m_exploreEffect.gameObject.GetComponentInParent<ExplosionEffect>().DelayEnqueue();

        ObjectPooler.Enqueue(this.GetComponent<Poolable>());
    }

    public void Initial(Vector3 position)
    {
        this.transform.position = position;
    }
}
