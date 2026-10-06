# GitHub Copilot Instructions for RealDining (Continued)

## Mod Overview and Purpose

RealDining (Continued) is a mod that enhances the dining experience of colonists in RimWorld by providing more realistic food choices and dining behaviors. It is an update of the original mod by 222821750, aimed at adding depth to how colonists manage their meals, including introducing a dedicated "dinner time" that players can configure. The mod requires Harmony as a dependency and is designed to be compatible with other food-adding mods.

## Key Features and Systems

- **Smart Food Selection**: Colonists will prioritize foods of similar quality and prefer different meals if options are available. They'll avoid eating the same dish repeatedly.
- **Dinner Time**: Introduces a "dinner time" feature, allowing colonists to schedule meals. During this period, they will prefer to eat, socialize, or enjoy desserts.
- **Mood-Based Choices**: Colonists tend to choose food that boosts mood when they're feeling down.
- **Food Priority Adjustments**: The proximity and spoilage of food items affect their selection priority.
- **Settings Customization**: Players can tweak settings, including randomness, repeat food priority, and meal threshold for dinner time.

## Coding Patterns and Conventions

1. **C# Naming Conventions**: Classes and methods follow PascalCase, while local variables use camelCase. For example, `JobGiver_GetFood_GetPriority` and `tryGiveJobFromJoyGiverDefDirect`.
2. **XML Configuration**: XML files are used to manage mod settings, translations, and patches. They are organized under specific directories for clear separation of concerns.
3. **Code Structure**: The mod's source code is neatly divided into subfolders, such as `Patch`, `Components`, and `TranslationTemplate`.

## XML Integration

- **Thoughts and Needs**: Managed through XML files like `Patch_Thoughts_Situation_Needs.xml` to define new thoughts and situations related to dining.
- **Component Definitions**: Located in `RealDining_Components.xml`, defining custom components used in the mod.
- **Translation Templates**: These XML files such as `Thoughts_Memory_Eat.xml` and `TimeAssignments.xml` support localization and time assignment definitions.

## Harmony Patching

- **Harmony Pre/Postfix**: The mod extensively uses Harmony patches to inject behavior into existing game methods without altering the base code. For example, `Prefix` and `Postfix` methods in various C# files like `FoodUtility_BestFoodInInventory.cs` and `JobGiver_GetFood_GetPriority.cs`.
- **Targeting Specific Methods**: Patches are precisely targeted at methods managing food logic and colonist jobs to enhance dining behavior while ensuring compatibility.

## Suggestions for Copilot

1. **Enhanced Food Logic**: When suggesting code for food logic, incorporate quality, mood, and spoilage factors.
2. **Adjustable Parameters**: Ensure that adjustments via mod settings are reflected in code suggestions, particularly for customizable parameters like food priority.
3. **Harmony Compliance**: Patches should follow best practices in Harmony patching, with clear indication of where Prefix or Postfix methods should be applied.
4. **Localization**: Assist in maintaining and expanding localization files for new features and community-contributed translations.
5. **Debugging Aids**: Provide suggestions for potential debugging aids, such as logging mechanisms, to help mod developers identify issues.

By following these instructions, Copilot can aid in maintaining and expanding the RealDining (Continued) mod with reliable and consistent code and configurations.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

