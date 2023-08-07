using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    [SerializeField] private string _maniMenuName = "MainMenu";
    [SerializeField] private CanvasGroup _self;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _showDuration;
    [SerializeField] private GameObject _anyKey;

    private bool _isInit;
    private float _timer;
    private bool _readyToMainMenu;

    private void Awake()
    {
        _self.alpha = 0;
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        if (_isInit)
            return;
        gameObject.SetActive(true);
        _self.alpha = 1.0f;
        _animator.SetTrigger("In");
        _isInit = true;
    }


    private void Update()
    {
        if (_readyToMainMenu && Input.anyKeyDown)
            SceneManager.LoadScene(_maniMenuName);

        if (!_readyToMainMenu && _isInit && _timer >= _showDuration)
        {
            _readyToMainMenu = true;
            _anyKey.SetActive(true);
        }
        else
            _timer += Time.deltaTime;

    }
}
