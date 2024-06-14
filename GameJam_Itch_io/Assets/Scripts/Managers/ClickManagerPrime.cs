using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class ClickManagerPrime : MonoBehaviour
{
    [SerializeField] private GameObject lightningEffect;

    private RaycastHit hit;
    private Transform previousHit;
    private Transform currentHitTransform;

    private GameObject selectedCard;
    private GameObject selectedEnemy;

    private CardStats lightningCard;
    private CardStats rockCard;
    private CardStats waterCard;
    private CardStats fireCard;
    private CardStats explosionCard;

    public List<GameObject> storedEnemies = new();

    private CardStats selectedCardStats;

    private int effectivenessModifier;

    private int playerActions;

    private E_ElementalTyping cardTyping;

    //Elemental card target storage
    private HealthSystem lightningTarget;
    private HealthSystem fireTarget;
    private HealthSystem waterTarget;
    private HealthSystem rockTarget;
    private HealthSystem explosionTarget;

    [Header("Extra Effects")]
    bool enemyTauntActive;
    bool firstFireUsed;
    bool lightningActive;
    //bool isLightningEffectActive = false;

    //private TypeChart enemyTyping;

    void Start()
    {
        currentHitTransform = null;
        selectedCard = null;
        firstFireUsed = false;
        lightningActive = false;
        playerActions = 0;
        lightningEffect.gameObject.SetActive(false);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 400.0f))
        {

            // Inform the previously hit object that the mouse is no longer hovering over it
            if (previousHit != null && previousHit != hit.transform)
            {
                previousHit.SendMessage("OnMouseExit", SendMessageOptions.DontRequireReceiver);
                previousHit = null;
            }

            // Inform the newly hit object that the mouse is now hovering over it
            currentHitTransform = hit.transform;
            currentHitTransform.SendMessage("OnMouseEnter", SendMessageOptions.DontRequireReceiver);

            // Store the current hit target as the previous hit target
            previousHit = currentHitTransform;

            //select playercard to use if not already defined
            if (Input.GetMouseButtonDown(0) && selectedCard == null && hit.transform.CompareTag("PlayerCard"))
            {
                selectedCard = hit.transform.gameObject;
                selectedCardStats = selectedCard.GetComponentInParent<CardStats>();
                //Check if player has enough mana
                if (ReferenceInstance.refInstance.playerStats.ReturnPlayerMana() < selectedCardStats.ReturnCost())
                {
                    Debug.Log("Not Enough Mana");
                    selectedCard = null;
                    selectedCardStats = null;
                }
                else
                {
                    cardTyping = selectedCardStats.Typing;
                    StoreCardTyping(); //Save card typing for later calculations
                    ReferenceInstance.refInstance.cardManager.SelectedCard(selectedCard);
                }
            }

            //Select enemy to attack if not already defined and then empty card and enemy selections
            if (Input.GetMouseButtonDown(0) && selectedEnemy == null && selectedCard != null && hit.transform.CompareTag("Enemy"))
            {
                DealDamage();
            }
            //Enemy is already selected so it attacks the enemy that is probably taunting
            else if (Input.GetMouseButtonDown(0) && selectedEnemy != null && selectedCard != null && hit.transform.CompareTag("Enemy"))
            {
                DealDamage();
            }
        }
        else
        {
            if (previousHit != null)
            {
                previousHit.SendMessage("OnMouseExit", SendMessageOptions.DontRequireReceiver);
                previousHit = null;
            }
        }
    }

    public bool HasSelectedEnemy() //Check if an enemy is already taunting
    {
        if (selectedEnemy != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Taunt(GameObject enemy)
    {
        selectedEnemy = enemy;
        enemyTauntActive = true;
    }

    public void RemoveTaunt()
    {
        selectedEnemy = null;
        enemyTauntActive = false;
    }

    private void StoreCardTyping()
    {
        if (cardTyping == E_ElementalTyping.Lightning)
        {
            lightningCard = selectedCardStats;
        }
        else if (cardTyping == E_ElementalTyping.Fire)
        {
            fireCard = selectedCardStats;
        }
        else if (cardTyping == E_ElementalTyping.Water)
        {
            waterCard = selectedCardStats;
        }
        else if (cardTyping == E_ElementalTyping.Rock)
        {
            rockCard = selectedCardStats;
        }
        else if (cardTyping == E_ElementalTyping.Neutral)
        {
            explosionCard = selectedCardStats;
        }
        else return;
    }

    private void PlayCard(CardStats card, HealthSystem target)
    {
        playerActions++;
        //Does extra lightning effect after first card is played
        if (lightningTarget != null && lightningCard != null && lightningActive == true)
        {
            ReferenceInstance.refInstance.eventManager.NextCardPlayed(card, target);
        }

        ApplyCardEffect(card, target);
        ReferenceInstance.refInstance.eventManager.CardPlayed(card, target);

    }

    //NOTE: Only lightning and fire have an extra effect currently, rest will be added later
    private void ApplyCardEffect(CardStats card, HealthSystem target)
    {
        target.TakeDamage(card.ReturnDamage() * CalculateDamageModifier());

        if (card.Typing == E_ElementalTyping.Lightning)
        {
            lightningTarget = target; //This will be the target for lightning DoTs, and will be overwritten if a new lightning card is used
            StartCoroutine(PlayLightningEffect(target));
            ReferenceInstance.refInstance.eventManager.useCardEvent += OnCardPlayed;

            lightningActive = true;
        }
        else if (card.Typing == E_ElementalTyping.Fire)
        {
            fireTarget = target; //Target for fire DoTs that take place on enemy turn, will also be overwritten if new fire card is used

            ReferenceInstance.refInstance.eventManager.useCardEvent += OnCardPlayed;
        }
        else if (card.Typing == E_ElementalTyping.Water)
        {
            waterTarget = target;

            ReferenceInstance.refInstance.eventManager.useCardEvent += OnCardPlayed;
        }
        else if (card.Typing == E_ElementalTyping.Rock)
        {
            rockTarget = target;

            ReferenceInstance.refInstance.eventManager.useCardEvent += OnCardPlayed;

        }
        else if (card.Typing == E_ElementalTyping.Neutral)
        {
            explosionTarget = target;

            ReferenceInstance.refInstance.eventManager.useCardEvent += OnCardPlayed;
        }

        if (lightningTarget != null && lightningCard != null && lightningActive == true)
        {
            ReferenceInstance.refInstance.eventManager.useNextCardEvent += OnSubsequentCardPlayed;
        }
    }
    //Function for cards that have effects that happen after they're initially played
    private void OnSubsequentCardPlayed(CardStats card, HealthSystem enemytarget)
    {
        if (lightningCard != null && lightningTarget != null && lightningActive == true)
        {
            HandleLightningDamage();
        }
    }

    private void OnCardPlayed(CardStats card, HealthSystem enemytarget)
    {
        if (waterCard != null)
        {
            HandleWaterDamage();
            waterCard = null;
        }

        if (rockCard != null)
        {
            HandleRockDamage();
        }

        if (fireCard != null && firstFireUsed == false)
        {
            HandleFireDamage();
            fireCard = null;
        }

        else if (fireCard != null && firstFireUsed == true)
        {
            HandleExtraFireDamage();
            fireCard = null;
        }

        if (explosionCard != null)
        {
            HandleExplosionDamage();
        }
    }

    private void HandleFireDamage()
    {
        if (fireCard != null && fireTarget != null)
        {
            fireCard.DealFireDamage(fireCard, fireTarget);
            AudioManager.Instance.Play("Fire Card Hit");
            firstFireUsed = true;
            UnsubscribeCardPlayed();
        }
    }

    private void HandleExtraFireDamage()
    {
        fireCard.DealExtraFireDamage(fireCard, fireTarget);
        AudioManager.Instance.Play("Fire Card Hit");
        firstFireUsed = false;
        fireCard = null;
        UnsubscribeCardPlayed();
    }

    private void HandleRockDamage()
    {
        rockCard.DealRockDamage(rockCard, rockTarget);
        ReferenceInstance.refInstance.playerStats.hasShield = true;
        rockCard = null;
        rockTarget = null;
        UnsubscribeCardPlayed();
    }

    private void HandleLightningDamage()
    {
        lightningCard.DealLightningDamage(lightningCard, lightningTarget);
        StartCoroutine(PlayLightningEffect(lightningTarget));
        if (lightningTarget.CurrentHealth <= 0)
        {
            lightningCard = null;
            lightningTarget = null;
            lightningActive = false;
            UnsubscribeExtraCardEffects();
        }
    }

    private IEnumerator PlayLightningEffect(HealthSystem enemyTarget)
    {
        lightningEffect.transform.position = enemyTarget.transform.position + new Vector3(0, 5, 0);
        lightningEffect.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        lightningEffect.gameObject.SetActive(false);
    }

    private void HandleWaterDamage()
    {
        if (waterCard != null && waterTarget != null)
        {
            waterCard.DealWaterDamage(waterCard, waterTarget);
            AudioManager.Instance.Play("Water Card Hit");
            UnsubscribeCardPlayed();
        }
    }

    private void HandleExplosionDamage()
    {
        if (explosionCard != null && explosionTarget != null)
        {
            explosionCard.DealExplosionDamage(explosionCard, explosionTarget);
            explosionTarget = null;
            explosionCard = null;
            UnsubscribeCardPlayed();
        }
    }

    public void UnsubscribeCardPlayed()
    {
        if (ReferenceInstance.refInstance.eventManager != null)
            ReferenceInstance.refInstance.eventManager.useCardEvent -= OnCardPlayed;
    }

    public void UnsubscribeExtraCardEffects()
    {
        if (ReferenceInstance.refInstance.eventManager != null)
        {
            lightningCard = null;
            lightningTarget = null;
            ReferenceInstance.refInstance.eventManager.useNextCardEvent -= OnSubsequentCardPlayed;
        }
    }

    private void DealDamage()
    {
        if (selectedEnemy == null) //Check if there is an enemy who was taunting
        {
            selectedEnemy = hit.transform.gameObject; //Stores enemy in gameobject variable
        }
        E_ElementalTyping enemyTyping = selectedEnemy.GetComponent<Ab_Enemy>().elementalType;  //Get enemy and fill in the enemy's typing
        HealthSystem hs = selectedEnemy.GetComponent<HealthSystem>(); //Get enemy health to deal damage to
        Ab_Enemy enemy = selectedEnemy.GetComponent<Ab_Enemy>();
        effectivenessModifier = TypingDictionary.GetEffectivenessModifier(cardTyping, enemyTyping); //Checks for playercard and enemy typings  
        PlayCard(selectedCardStats, hs);
        enemy.HitAnim();
        //enemyTyping = 0; //Resets calculation as fail-safe
        /*FinishedAttacking();*/ //Empties all variables that need no longer be filled
        StartCoroutine(WaitForAnim());
    }

    private float CalculateDamageModifier()
    {
        if (effectivenessModifier == 0) //Neutral damage dealt
        {
            return 1f;
        }
        if (effectivenessModifier == 1) //Extra damage dealt
        {
            Debug.Log("You did extra damage due to type advantage!");
            return 1.5f;
        }
        if (effectivenessModifier == 2) //Less damage dealt
        {
            Debug.Log("You dealt half the damage due to resistances...");
            return 0.5f;
        }
        else
        {
            Debug.Log("Couldn't calculate damage modifier so returned normal damage value");
            return 1f;
        }
    }

    private void FinishedAttacking()
    {
        ReferenceInstance.refInstance.playerStats.LoseMana(selectedCardStats.ReturnCost());
        ReferenceInstance.refInstance.cardManager.RemoveCard(selectedCard.transform.parent.parent.gameObject);
        ReferenceInstance.refInstance.turnManager.ChangeState(TurnState.PickCard);
        selectedCard = null;

        if (enemyTauntActive == false)
        {
            selectedEnemy = null;
        }
    }

    public GameObject ReturnPlayerCard()
    {
        if (selectedCard != null)
        {
            return selectedCard;
        }
        else
        {
            return null;
        }
    }

    private IEnumerator WaitForAnim()
    {
        yield return new WaitForSeconds(0.1f);
        FinishedAttacking();
    }
}
