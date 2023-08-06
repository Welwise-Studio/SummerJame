using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private string _startSceneName;

    public void StartGame()
    {
        SceneManager.LoadScene(_startSceneName);
    }
}
