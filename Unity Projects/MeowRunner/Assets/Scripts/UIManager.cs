using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private TMP_Text unitLevel;
    [SerializeField] private TMP_Text goldLevel;
    [SerializeField] private TMP_Text goldCount;
    [SerializeField] private TMP_Text unitCount;
    [SerializeField] private TMP_Text goldPrice;
    [SerializeField] private TMP_Text unitPrice;
    
    private SwarmController _swarmController;
    private GameManager _gameManager;
    
    private void Start()
    {
        _swarmController = FindObjectOfType<SwarmController>();
        _gameManager = FindObjectOfType<GameManager>();
        unitLevel.text = _gameManager.UnitLevel + "\nLVL";
        goldLevel.text = _gameManager.GoldLevel + "\nLVL";
        goldPrice.text = _gameManager.GoldPrice.ToString();
        unitPrice.text = _gameManager.UnitPrice.ToString();
        goldCount.text = _gameManager.GoldCount.ToString();
    }

    public void Button_Settings()
    {
        startPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void GamePanel()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void StartPanel()
    {
        gamePanel.SetActive(false);
        startPanel.SetActive(true);
    }

    public void Button_AddUnit()
    {
        _swarmController.SpawnUnit(1);
        _gameManager.UnitLevel += 1;
        unitLevel.text = _gameManager.UnitLevel + "\nLVL";
        _gameManager.GoldCount -= _gameManager.UnitPrice;
        goldCount.text = _gameManager.GoldCount.ToString();
        _gameManager.UnitPrice += 100;
        unitPrice.text = _gameManager.UnitPrice.ToString();
    }

    public void Button_AddGold()
    {
        _gameManager.GoldCount -= _gameManager.UnitPrice;
        goldCount.text = _gameManager.GoldCount.ToString();
        _gameManager.GoldFactor += .05f;
        _gameManager.GoldLevel += 1;
        goldLevel.text = _gameManager.GoldLevel + "\nLVL";
        _gameManager.GoldPrice += 100;
        goldPrice.text = _gameManager.GoldPrice.ToString();
    }
    
    
    
}
