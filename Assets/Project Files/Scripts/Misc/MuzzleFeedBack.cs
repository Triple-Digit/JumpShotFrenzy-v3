using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFeedBack : Feedback
{
    [SerializeField] GameObject m_object;
    [SerializeField] float duration = 0.2f, strength = 1, randomness = 90;
    [SerializeField] int vibrato = 10;
    [SerializeField] bool snapping = false, fadeout = true;

    public override void CompletePreviousFeedback()
    {
        m_object.transform.DOComplete();
    }

    public override void CreateFeedback()
    {
        CompletePreviousFeedback();
        m_object.transform.DOShakePosition(duration, strength, vibrato, randomness, snapping, fadeout);
    }
}
