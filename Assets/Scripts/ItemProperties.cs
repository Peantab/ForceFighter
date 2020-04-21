using UnityEngine;

public class ItemProperties : MonoBehaviour
{
    [Tooltip("Mass of the item in kg.")]
    public float mass = 1;

    [Tooltip("An item with the frequency of 10 is thrown 10 times more often than an item with the frequency of 1. There are is no defined maximum. MUST be greater than 0.")]
    public float relativeFrequency = 20;

    [Tooltip("A force applied to the item to move it either left or right.")]
    public float forceSide = 50;

    [Tooltip("A force applied to the item to move it up when it's supposed to be thrown high.")]
    public float forceUpHigh = 290;

    [Tooltip("A force applied to the item to move it up when it's supposed to be thrown low.")]
    public float forceUpLow = 0;

    [Tooltip("A force applied to the item to move it towards the camera.")]
    public float forceBack = 300;
}
