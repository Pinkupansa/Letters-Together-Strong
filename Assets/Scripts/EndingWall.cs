using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingWall : MonoBehaviour
{
    [SerializeField] private Transform targetDisplacement;
    [SerializeField] private AudioClip descendingWall;
    // Start is called before the first frame update
    private void Start()
    {
        GameEvents.current.levelComplete.AddListener(OnLevelComplete);
    }

    private void OnLevelComplete()
    {
        LeanTween.move(gameObject, (Vector2)targetDisplacement.position, 4f);
        SoundManager.current.PlaySound(descendingWall);
    }
}
