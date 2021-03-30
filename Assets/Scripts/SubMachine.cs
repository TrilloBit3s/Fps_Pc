using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMachine : MonoBehaviour
{
    public float range;
	public Camera mainCamera;
    
    public Animation subMachineAnimation;
    public AnimationClip shootAnim;
    public AnimationClip reloadAnim;
	public AnimationClip drawAnim;
	public AnimationClip miraAnim;
	public AnimationClip backMiraAnim;
	
	public float fireRate;
	private float currentRateToFire;
	public float timeToReload;
	private float currentRateToReload;
	public bool canshoot = true;

	public bool isDraw;

	public AudioSource subMachineAudio;
	public AudioClip shootSound;

	public ParticleSystem MuzzleFlash;
	public ParticleSystem CartridgeEject;

	public GameObject impactTemp;

	public int bullets = 10; 
	public int cartuchos = 3;
	private int startBullets;

    public float intensity;
    public float smooth;
    public bool isMine;
    private Quaternion origin_rotation;	

	public Text currentammoText; //script HUD balas
	public Text currentcartText; //script HUD cartucho

    void Start()
	{
		startBullets = bullets;

		currentRateToFire = fireRate;
		currentRateToReload = timeToReload;

		origin_rotation = transform.localRotation;
    }

    void Update()
	{				
		currentammoText.text = bullets.ToString();//script HUD
		currentcartText.text = cartuchos.ToString();//script HUD 
		currentRateToFire += Time.deltaTime;
		currentRateToReload += Time.deltaTime;
		
		if(subMachineAnimation.IsPlaying(drawAnim.name))
		{
			isDraw = true;			
		}
		else
		{
			isDraw = false;
		}
				
		if(currentRateToReload >= timeToReload)
		{
			canshoot = true;
		}	
		//Input.GetButton("Fire1") //Input.GetKey(KeyCode.Mouse0)
		if(Input.GetButton("Fire1") && currentRateToFire >= fireRate && canshoot && isDraw == false && bullets > 0 )
		{
			Shoot();
		}
		
		//use esta linha caso queira que a arma recarregue sozinha quando acabar a munição
	/*	else if
		if(Input.GetButton("Fire1")&& currentRateToFire >= fireRate && canshoot && bullets == 0 && cartuchos > 0 )
		{
			Shoot();
		}
	*/
        if(Input.GetKeyDown(KeyCode.R) && cartuchos > 0)
		{
		    Reload();
    	}

		if(Input.GetButton("Fire2"))
		{
		    Mira();
			//subMachineAnimation.Play(PlayMode.StopAll);

    	}
		
		UpdateSway();//adicionado
    }  

	//adicionado Método
	private void UpdateSway ()
    {
        //controles
        float t_x_mouse = Input.GetAxis("Mouse X");
        float t_y_mouse = Input.GetAxis("Mouse Y");

        if(!isMine)
        {
            t_x_mouse = 0;
            t_y_mouse = 0;
        }

        //calcula a rotação alvo
        Quaternion t_x_adj = Quaternion.AngleAxis(-intensity * t_x_mouse, Vector3.up);
        Quaternion t_y_adj = Quaternion.AngleAxis(intensity * t_y_mouse, Vector3.right);
        Quaternion target_rotation = origin_rotation * t_x_adj * t_y_adj;

        //girar em direção à rotação alvo
        transform.localRotation = Quaternion.Lerp(transform.localRotation, target_rotation, Time.deltaTime * smooth);
    }

    void Shoot()
	{
		bullets --;
		MuzzleFlash.Play();
		CartridgeEject.Play();
		currentRateToFire = 0;
		subMachineAnimation.clip = shootAnim;
    	subMachineAnimation.Play();
		subMachineAudio.clip = shootSound;
	 	subMachineAudio.Play();

        RaycastHit hit;
        if (Physics.Raycast (mainCamera.transform.position, mainCamera.transform.forward, out hit, range))
		{
			Instantiate (impactTemp, hit.point, Quaternion.LookRotation(hit.normal)); 
	       // Debug.Log ("Acertou" + hit.transform.name);
        }
    }

    void Reload ()
	{
		cartuchos --;
		bullets += startBullets;
		currentRateToReload = 0;
		canshoot = false;
        subMachineAnimation.clip = reloadAnim;
		subMachineAnimation.Play(PlayMode.StopAll);
    }

	void Mira ()
	{	
		subMachineAnimation.clip = miraAnim;
		subMachineAnimation.Play();
		subMachineAnimation.clip = backMiraAnim;
		subMachineAnimation.Play();
	}
}