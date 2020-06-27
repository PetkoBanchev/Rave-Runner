using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PowerUps : MonoBehaviour
{

    public float scoreMultiplier;
    public int audioBand;
    public bool isDebuff;
    float prefabScore;
    
    // Start is called before the first frame update
    void Start()
    {
        if (isDebuff)
        {
            prefabScore = (AudioController.audioBands[audioBand] * scoreMultiplier) * - 1;
        } 
        else
        {
            prefabScore = (AudioController.audioBands[audioBand] * scoreMultiplier);
        }
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {    
            PlayerMovement.score += Mathf.CeilToInt(prefabScore);
            
            switch (gameObject.name)
            {
                case "Beer(Clone)":
                    PlayerMovement.speed += 1;
                    PlayerMovement.beerCounter +=1;
                    break;
                case "Cigarettes(Clone)":
                    PlayerMovement.minJumpForce -= 50;
                    PlayerMovement.maxJumpForce -= 50;
                    PlayerMovement.cigarettesCounter +=1;
                    break;
                case "Rock(Clone)":
                    PlayerMovement.minJumpForce += 10;
                    PlayerMovement.maxJumpForce += 10;
                    break;
                case "Discoball(Clone)":
                    SceneManager.LoadScene("GameOver");
                    break;
                case "Loudspeaker(Clone)":
                    SceneManager.LoadScene("GameOver");
                    break;
                case "Table(Clone)":
                    SceneManager.LoadScene("GameOver");
                    break;
            }
            Destroy(gameObject);
        }
	}
}
