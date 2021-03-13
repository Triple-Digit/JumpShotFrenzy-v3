using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    [SerializeField] List<Feedback> m_feedbackToPlay = null;

    public void PlayFeedback()
    {
        FinishFeedback();
        foreach (var feedback in m_feedbackToPlay)
        {
            feedback.CreateFeedback();
        }
    }

    private void FinishFeedback()
    {
        foreach (var feedback in m_feedbackToPlay)
        {
            feedback.CompletePreviousFeedback();
        }
    }
}
