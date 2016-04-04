using UnityEngine;
using System.Collections;

public class ExplosionEffect : MonoBehaviour {

    //默认2秒
    private float m_fDelayTime = 2.0f;

    void SetDelayTime(float time)
    {
        m_fDelayTime = time;
    }

    void DelayEnqueueMethod()
    {
        ObjectPooler.Enqueue(this.GetComponent<Poolable>());
    }

    public void DelayEnqueue()
    {
        Invoke("DelayEnqueueMethod", m_fDelayTime);
    }

}
