using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _totalTime;
    private float _timeRemaining;
    private bool _isRunning;
    private MonoBehaviour[] _monoBehaviours;

    public Timer(float totalTime)
    {
        _totalTime = totalTime;
        _timeRemaining = totalTime;

    }

    private void Start()
    {
        _monoBehaviours = gameObject.GetComponents<MonoBehaviour>();
        StartCoroutine(DoTimer());
    }

    public void Set(float time)
    {
        Stop();
        _totalTime = time;
        _timeRemaining = time;
        StartCoroutine(DoTimer());

    }

    public void Stop()
    {
        StopCoroutine("DoTimer");
    }

    // void Update()
    // {
    //     if (_timeRemaining > 0)
    //     {
    //         _timeRemaining -= Time.deltaTime;
    //     }
    // }
    private void CheckExecutableActions()
    {
        foreach (MonoBehaviour monoBehaviour in _monoBehaviours)
        {
            if (monoBehaviour is IActionInterface)
            {
                IActionInterface actionableObject = (IActionInterface)monoBehaviour;
                actionableObject.Execute();
            }
        }
    }

    private IEnumerator DoTimer()
    {
        yield return new WaitForSeconds(_totalTime);
        CheckExecutableActions();
        yield return null;
    }
}