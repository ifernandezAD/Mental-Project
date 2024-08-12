# Game Project

## Overview

Welcome to the **Game Project**! This repository contains the core mechanics and systems of an innovative and strategically rich game designed to offer a unique and engaging experience. Our focus is not only on providing compelling gameplay but also on demonstrating advanced programming practices and thoughtful game design. 

## Features

### 1. **Modular and Scalable Architecture**
- **Component-Based Design**: The game leverages a component-based architecture, allowing for easy expansion and maintenance. Each game system is encapsulated in its own module, promoting reusability and separation of concerns.
- **Phase Management System**: The game utilizes a phase-based system to manage different gameplay states, ensuring clear and maintainable code flow. This approach allows for seamless transitions between game states like Draw, Battle, and Event phases.

### 2. **Event-Driven Programming**
- **EventManager**: Our event-driven architecture facilitates loose coupling between different game components. The `EventManager` allows various parts of the game to communicate and respond to player actions or game state changes without direct dependencies.
- **Dynamic Event Handling**: The system is designed to handle events dynamically, such as drawing random events, ally acquisition, and managing game rounds, which enhances flexibility and scalability.

### 3. **Advanced Randomization and Flexibility**
- **Customizable Probability Management**: The project features highly customizable randomization, where probabilities for events like ally acquisition can be tweaked through the Unity Inspector. This allows designers to balance the game without altering the underlying code.
- **Non-Repetitive Ally System**: Ensuring that the same ally is never instantiated twice, the system tracks already instantiated allies and removes them from the selection pool, enhancing gameplay variety.

### 4. **Robust Round and Act Management**
- **Dynamic Round Control**: The system is built to handle variable numbers of rounds per act, avoiding the pitfalls of hardcoded values and offering flexibility for future expansions or game modifications.
- **Smart Ally Allocation**: Allies are allocated in a strategic manner based on game progression, avoiding repetition and ensuring balanced gameplay across different acts and rounds.

### 5. **User-Friendly and Designer-Oriented**
- **Inspector-Driven Customization**: Many game parameters, such as event probabilities, round limits, and ally settings, are exposed to the Unity Inspector, allowing game designers to fine-tune the game without needing to dive into the code.
- **Clear Code Structure**: The project follows clean code principles, with well-documented methods and classes, making it easy for new developers to get up to speed quickly.

## Getting Started

### Prerequisites

- **Unity**: Ensure you have Unity installed (version X.X.X or later).
- **Git**: Basic knowledge of Git for version control.

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/yourprojectname.git
