using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] private int StartBalance;
    [SerializeField] int currentBalance;
    [SerializeField] TextMeshProUGUI displayBalance;

    public int CurrentBalance { get { return currentBalance; } }

    void Awake()
    {
        currentBalance = StartBalance;
        UpdateDisplay();
        Debug.Log(currentBalance);
    }

    public void Deposite(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateDisplay();
        Debug.Log(currentBalance);
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        displayBalance.text = "Money: " + currentBalance;
    }
}
