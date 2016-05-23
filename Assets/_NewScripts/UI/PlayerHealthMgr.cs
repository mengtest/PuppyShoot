using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerHealthMgr : MonoBehaviour {
    
    private GameObject m_PlayerHealthBar;
    private PlayerStatus m_PlayerStatus;
    public List<GameObject> m_listHeartIcons = new List<GameObject>();
    
    void Awake()
    {
        m_PlayerHealthBar = GameObject.Find("PlayerHealthBar");
        m_PlayerStatus = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerStatus>();
        this.RemoveObserver(Notifications.LOSE_HEALTH);
        this.RemoveObserver(Notifications.GET_HEAL);
        this.AddObserver(Notifications.GET_HEAL, OnGetHeal);
        this.AddObserver(Notifications.LOSE_HEALTH, OnLoseHealth);
    }

    void Start()
    {
        //InitHealthBar();
    }

    void OnDestory()
    {
        this.RemoveObserver(Notifications.LOSE_HEALTH);
        this.RemoveObserver(Notifications.GET_HEAL);
    }

    void OnGetHeal(object sender, EventArgs e)
    {
        for (int i = 0; i < m_listHeartIcons.Count; ++i)
        {
            m_listHeartIcons[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < m_PlayerStatus.playerHealth; ++i)
        {
            m_listHeartIcons[i].gameObject.SetActive(true);
        }
    }

    void OnLoseHealth(object sender, EventArgs e)
    {
        for(int i = 0 ; i < m_listHeartIcons.Count ; ++ i)
        {
            m_listHeartIcons[i].gameObject.GetComponent<Image>().enabled = false;
        }
        for(int i = 0 ; i < m_PlayerStatus.playerHealth ; ++ i)
        {
            m_listHeartIcons[i].gameObject.GetComponent<Image>().enabled = true;
        }
    }

    void InitHealthBar()
    {
        foreach(Transform child in m_PlayerHealthBar.transform)
        {
            m_listHeartIcons.Add(child.gameObject);
            child.gameObject.GetComponent<Image>().enabled = false;
        }

        for(int i = 0 ; i < m_PlayerStatus.playerHealth ; ++ i)
        {
            m_listHeartIcons[i].gameObject.GetComponent<Image>().enabled = true;
        }
    }
}
