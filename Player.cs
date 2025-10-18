using Godot;
using System;
using System.Collections.Generic;

public partial class Player : Node2D
{
	public string PlayerName { get; set; }
	public int ChipCount { get; private set; }  // Fixed: removed space in "Chip Count"
	public int CurrentBet { get; private set; }
	public bool HasFolded { get; private set; }  // Fixed: added semicolon
	public bool IsDealer { get; set; }  // Fixed: added semicolon
	public bool IsAI { get; set; }  // Fixed: added semicolon
	
	private List<Card> hand = new List<Card>();
	
	private Vector2 card1Position;
	private Vector2 card2Position;
	
	public override void _Ready()
	{
		
	}
	
	public void Initialize(string name, int startingChips, bool isAI = false)
	{
		PlayerName = name;
		ChipCount = startingChips;
		IsAI = isAI;
	}
	
	public void ReceiveCard(Card card)
{
	hand.Add(card);
	AddChild(card);
	
	// Scale cards down
	card.Scale = new Vector2(0.3f, 0.3f);
	
	// Position cards side by side
	if (hand.Count == 1)
	{
		// First card - slightly to the left
		card.Position = new Vector2(-60, 0);
	}
	else if (hand.Count == 2)
	{
		// Second card - slightly to the right
		card.Position = new Vector2(60, 0);
	}
	
	// Show human cards, hide AI cards
	if (IsAI)
	{
		card.FlipDown();
	}
	else
	{
		card.FlipUp();
	}
}
	
	public List<Card> GetHand()
	{
		return hand;  // Fixed: added return statement
	}
	
	public void ClearHand()
	{
		foreach (Card card in hand)
		{
			card.QueueFree();
		}
		hand.Clear();
	}
	
	public void Fold()
	{
		HasFolded = true;
	}
	
	public void Check()
	{
		
	}
	
	public bool Call(int amountToCall)
	{
		if (ChipCount >= amountToCall)
		{
			ChipCount -= amountToCall;
			CurrentBet += amountToCall;
			return true;
		}
		return false;
	}
	
	public bool Raise(int raiseAmount)  // Fixed: typo "publci" -> "public"
	{ 
		if (ChipCount >= raiseAmount)
		{
			ChipCount -= raiseAmount;
			CurrentBet += raiseAmount;
			return true;
		}
		return false;
	}
	
	public void AddChips(int amount)  // Fixed: capitalized method name
	{ 
		ChipCount += amount;
	}
	
	public void ResetForNewRound()
	{
		CurrentBet = 0;
		HasFolded = false;
	}
	
	public void ShowCards()
	{
		foreach (Card card in hand)
		{
			card.FlipUp();
		}
	}
	
	public void HideCards()
	{
		foreach (Card card in hand)
		{
			card.FlipDown();
		}
	}
	
	public string MakeAIDecision(int currentBet, int potSize)  // Fixed: capitalized AI
	{ 
		return "fold";
	}
	
	public string GetPlayerInfo()
	{ 
		return $"{PlayerName}: {ChipCount} chips";  // Fixed: added semicolon
	}
}
