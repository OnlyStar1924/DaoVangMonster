using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VangScript : MonoBehaviour {
	public bool isMoveFollow = false;
	public float maxY;
	public int score;
	public float speed;
	public bool isCatched;
	public Slider slider;

	public float health;
	private float curTime = 0f;
	private float veloHealth = 0f;
	// Use this for initialization
	void Start () {
		isMoveFollow = false;
        isCatched = false;
		health = 0;
    }
	
	// Update is called once per frame
	void Update () {
        bool istouch = Input.GetMouseButtonDown(0);
        if (istouch && isCatched == true)
		{
			health += 25;
			checkCatched();
        }
		

    }

	void FixedUpdate() {
		moveFllowTarget(GameObject.Find("luoiCau").transform);
		if (isCatched == true && curTime < 1)
		{
			curTime += Time.deltaTime;
		}
		else if (curTime >= 1)
		{
			veloHealth += 1;
			curTime = 0;
		}

		if (isCatched == true)
		{ 
			slider.enabled = true;

			slider.value = health/ score;
			health -= 0.1f*veloHealth;
		}

        //Debug.Log(health + "/" + score);

        if (health < -20) {
            isMoveFollow = true;
            isCatched = false;
            GameObject.Find("luoiCau").GetComponent<LuoiCauScript>().speed = 5;
			health = 0;
			veloHealth = 0;
            slider.value = 0;
        }
    }

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.name == "luoiCau") {
			//isMoveFollow = true;
			isCatched = true;

            GameObject.Find("dayCau").GetComponent<DayCauScript>().typeAction = TypeAction.KeoCau;
            GameObject.Find("luoiCau").GetComponent<LuoiCauScript>().velocity = -GameObject.Find("luoiCau").GetComponent<LuoiCauScript>().velocity;
            //GameObject.Find("luoiCau").GetComponent<LuoiCauScript>().speed -= this.speed;
            GameObject.Find("luoiCau").GetComponent<LuoiCauScript>().speed = 0;
        }
    }

	void moveFllowTarget(Transform target) {
		if(isMoveFollow && isCatched == true) 
		{
				Quaternion tg = Quaternion.Euler(target.parent.transform.rotation.x, 
				                                 target.parent.transform.rotation.y, 
				                                 90 + target.parent.transform.rotation.z);
//				transform.rotation = Quaternion.Slerp(transform.rotation, tg, 0.5f);
				transform.position = new Vector3(target.position.x, 
				                                 target.position.y - gameObject.GetComponent<Collider2D>().GetComponent<Collider2D>().bounds.size.y / 2, 
				                                 transform.position.z);	
			if(GameObject.Find("dayCau").GetComponent<DayCauScript>().typeAction == TypeAction.Nghi) {
				GameObject.Find("GamePlay").GetComponent<GamePlayScript>().score += this.score;
				Destroy(gameObject);
			}


        }
    }

	void checkCatched()
	{
		if (health >= score)
		{
			isMoveFollow = true;
			GameObject.Find("luoiCau").GetComponent<LuoiCauScript>().speed = 3;
            slider.value = 1;

        }
    }
}
