using System.Collections;
using Managers;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject centerSpawn;
    public GameObject spawnPoint;
    public GameObject[] balls;
    private BallsChallenge[] _balls;
    public float angle = 0.2f;
    public float speed;
    public float radius;
    public int fieldMultiply = 1;
    public float restrictions = 1.35f;
    private int _a = 1;
    public int field;

    private void Start()
    {
        _balls = new BallsChallenge[balls.Length];
        for (int _ball = 0; _ball < balls.Length; _ball++)
        {
            _balls[_ball] = balls[_ball].GetComponent<BallsChallenge>();
        }
        StartCoroutine(Spawn());
    }

    private void Update()
    {
        angle += _a * Time.deltaTime;

        var _position = centerSpawn.transform.position;
        var _x = Mathf.Cos(angle * speed) * radius + _position.x;
        var _y = fieldMultiply * Mathf.Sin(angle * speed) * radius + _position.y;
        spawnPoint.transform.position = new Vector3(_x, _y, spawnPoint.transform.position.z);
        if ((_x < _position.x - restrictions && _a > 0) || (_x > _position.x + restrictions && _a < 0))
            _a = -_a;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
            Statistics.stats.lostBalls++;
            StartCoroutine(Spawn());
        }
        else if (collision.gameObject.CompareTag("FieldBall"))
        {
            int _i = field;
            while (true)
            {
                _i = _i==FieldManager.fields.isOpen.Length-1?0:_i+1;
                if (!FieldManager.fields.isOpen[_i]) continue;
                
                GameManager.instance.spawnPoints[_i].Spawn(collision.gameObject);
                collision.gameObject.GetComponent<TrailRenderer>().Clear();
                return;
            }
            
        }
    }

    private void Spawn(GameObject ball)
    {
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        ball.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        ball.transform.position = spawnPoint.transform.position;
        ball.GetComponent<BallsChallenge>().timeOnField = 0;
        ball.SetActive(true);
    }
    private IEnumerator Spawn()
    {
        //  var _child = balls[0].GetComponentsInChildren<TrailRenderer>();


        for (int _j = 0; _j <= ChallengeManager.progress.balls[field]; _j++)
        {
            if (balls[_j].activeSelf) continue;

            /*foreach (var _t in _child)
            {
                _t.GetComponent<TrailRenderer>().Clear();
            }*/
            _balls[_j].timeOnField = 0;
            balls[_j].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            balls[_j].GetComponent<Rigidbody2D>().angularVelocity = 0f;
            balls[_j].transform.localPosition = spawnPoint.transform.localPosition;
            balls[_j].SetActive(true);

            yield return new WaitForSeconds(0.8f);
        }
    }

 
}