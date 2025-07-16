# Copilot Instructions for RimWorld Mod Project

## Mod Overview and Purpose
This RimWorld mod aims to enhance the simulation of the pawns' daily routines and needs by introducing more nuanced behavior regarding food, rest, joy, and work activities. By implementing tailored job-givers and utility functions, the mod enriches the gameplay experience by making pawns' decision-making processes more realistic and context-sensitive.

## Key Features and Systems
- **Enhanced Food Management**: Implements custom logic for pawns to prioritize food based on their current needs and situations.
- **Joy Optimization**: Introduces enhanced decision-making for when and how pawns seek joy.
- **Dynamic Rest Management**: Provides pawns with improved strategies for prioritizing rest, ensuring they maintain optimal performance.
- **Custom Time Assignments**: Adds a new time assignment category, facilitating better management of daily pawn activities.
- **Food Preferences Tracking**: Keeps track of pawns' food preference history to influence future eating decisions.

## Coding Patterns and Conventions
- **Internal Classes**: Most functionalities are encapsulated within internal classes to limit visibility outside of the scope of the mod, promoting encapsulation.
- **Method Naming**: Methods are typically prefixed by function type or context (e.g., `TryGiveJob`, `GetPriority`) for clarity and consistency.
- **Conventions**: Utilize PascalCase for class names and method names, emphasizing readability and standard C# conventions.

## XML Integration
- The mod's configurations and settings are primarily managed via XML files that define parameters for pawn behaviors and jobs in conjunction with C# logic.
- XML tags are utilized to establish clear data structure hierarchies, ensuring seamless interaction with RimWorld's existing data-driven systems.

## Harmony Patching
- Harmony is extensively employed to modify game behavior without altering the original game code directly.
- Patches are systematically applied via the `PatchMain` class, which publicly exposes patching methods for integration at runtime.
- Ensure patches are specific and targeted to avoid unintended interactions with other mods or game behaviors.

## Suggestions for Copilot
- **Contextual Code Suggestions**: Encourage Copilot to suggest additional methods for task prioritization logic across new scenarios or derived classes.
- **XML Configuration Patterns**: Provide templates for XML setup files that align with the coding logic to potentially auto-generate or suggest configuration files.
- **Harmony Patching Techniques**: Recommend best practices for using Harmony, emphasizing the use of optional patches to maintain compatibility with other mods.
- **Utility Functions and Extensions**: Suggest utility functions that could optimize redundant code across the mod, making use of the `ListingExtension` for common actions.

By adhering to these guidelines and leveraging Copilot's capabilities, developers working on this mod can efficiently enhance RimWorld's gameplay dynamics, yielding a richer, more immersive experience for players.
