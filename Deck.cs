using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Deck : Node
{
	private List<Card> cards = new List<Card>();
	private Random random = new Random();
	
	public override void _Ready()
	{
		InitializeDeck();
	}
	
	// Create all 52 cards
	public void InitializeDeck()
	{
		cards.Clear();
		
		Card.Suit[] suits = { Card.Suit.Hearts, Card.Suit.Diamonds, Card.Suit.Clubs, Card.Suit.Spades };
		Card.Rank[] ranks = { 
			Card.Rank.Two, Card.Rank.Three, Card.Rank.Four, Card.Rank.Five, 
			Card.Rank.Six, Card.Rank.Seven, Card.Rank.Eight, Card.Rank.Nine, 
			Card.Rank.Ten, Card.Rank.Jack, Card.Rank.Queen, Card.Rank.King, Card.Rank.Ace
		};
		
		foreach (Card.Suit suit in suits)
		{
			foreach (Card.Rank rank in ranks)
			{
				Card card = new Card();
				card.Initialize(rank, suit);
				cards.Add(card);
			}
		}
		
		GD.Print($"Deck initialized with {cards.Count} cards");
	}
	
	// Shuffle the deck
	public void Shuffle()
	{
		// Fisher-Yates shuffle
		for (int i = cards.Count - 1; i > 0; i--)
		{
			int j = random.Next(0, i + 1);
			Card temp = cards[i];
			cards[i] = cards[j];
			cards[j] = temp;
		}
		
		GD.Print("Deck shuffled");
	}
	
	// Deal one card from the top
	public Card DealCard()
	{
		if (cards.Count == 0)
		{
			GD.PrintErr("No cards left in deck!");
			return null;
		}
		
		Card card = cards[0];
		cards.RemoveAt(0);
		return card;
	}
	
	// Reset the deck
	public void Reset()
	{
		// Remove all existing card nodes
		foreach (Card card in cards)
		{
			if (IsInstanceValid(card) && card.GetParent() != null)
			{
				card.GetParent().RemoveChild(card);
			}
			card.QueueFree();
		}
		
		InitializeDeck();
	}
	
	// Get number of cards remaining
	public int CardsRemaining()
	{
		return cards.Count;
	}
}