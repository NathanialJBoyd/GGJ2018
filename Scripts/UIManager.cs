using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour 
{
    public static UIManager Instance;

    public SpeechBubble CustomerSpeechBubble;
    public SpeechBubble ManagerSpeechBubble;

    void Awake()
    {
        Instance = this;
    }
}
