AI-Based Word Chain Game – Project Proposal


1. Project Proposal
This project implements an AI-based Word Chain Game where players take turns saying words.
 Each new word must start with the last letter of the previous word.
 If a player cannot produce a valid word, they lose the game. The system will support multiple modes such as User vs User, User vs AI, and AI vs AI.
 Two AI levels will be implemented: a limited AI that selects words randomly from a subset of the dictionary, and a stronger AI that explores possible moves using a simple search strategy to reduce the chance of losing.
 The project demonstrates fundamental Artificial Intelligence concepts such as decision making, state-space representation, and adversarial interaction without relying on machine learning.

2. PEAS Description
• Performance Measure: Winning the game, maximizing valid moves, avoiding losing states.
• Environment: Turn-based, fully observable, deterministic, discrete, multi-agent environment.
• Actuators: Selecting and submitting a valid word during the AI turn.
• Sensors: Reading the last required letter, previously used words, and available dictionary words.

3. ODESA Environment Classification
• Observability: Fully Observable – all players know the last letter and previously used words.
• Determinism: Strategic – the outcome depends on the opponent’s actions.
• Episodic/Sequential: Sequential – each move affects future moves.
• Static/Dynamic: Static – the environment does not change unless a player acts.
• Agents: Multi-Agent – interaction occurs between a user and AI or between two AI agents.

4. Agent Type
The AI used in this project can be considered a goal-based agent.
 The agent analyzes the current state of the game and selects a word that satisfies the game rules while trying to avoid losing states.
 The stronger AI level explores possible future moves using a search-based strategy, making decisions based on the expected outcome of different actions.
