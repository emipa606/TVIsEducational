# GitHub Copilot Instructions for TV is Educational

## Mod Overview and Purpose

TV is Educational is a RimWorld mod that allows children to gain education through watching TV or listening to the radio. The mod is designed to help children with disabilities who are unable to attend school during their recovery period. The education gained is influenced by the tech-level of the equipment, the amount of joy provided by the equipment, and the mood of the child. The mod supports various TV and radio equipment from different mods, including Vanilla Furniture Expanded, Vanilla Furniture Expanded - Spacer Module, and WallStuff.

## Key Features and Systems

- **TV and Radio Education**: Children can gain education by watching TV or listening to the radio.
- **Tech-Level Influence**: The education gained is influenced by the tech-level of the equipment.
- **Joy and Mood Influence**: The education gained is influenced by the amount of joy provided by the equipment and the mood of the child.
- **Equipment Support**: Supports TV and radio equipment from Vanilla Furniture Expanded, Vanilla Furniture Expanded - Spacer Module, and WallStuff.

## Coding Patterns and Conventions

### C# Files

- **TVTimeTracker.cs**: Contains the TVTimeTracker class which handles the tracking of TV time and triggers the education gain.
- **GameComponent_TVTimeTracker.cs**: Contains the GameComponent_TVTimeTracker class which exposes data for the TV time tracker.
- **CompProperties_TVIsEducational.cs**: Contains the CompProperties_TVIsEducational class which holds properties for the TV educational component.
- **CompTVIsEducational.cs**: Contains the CompTVIsEducational class which handles the TV educational component logic.

### XML Files

- **Defs_TVThought.xml**: Contains the ThoughtDef for the TV educational thought.
- **Patch_TV_Vanilla.xml**: Contains the Harmony patch for Vanilla TV equipment.
- **Patch_VFE.xml**: Contains the Harmony patch for Vanilla Furniture Expanded TV equipment.
- **Patch_VFESpacer.xml**: Contains the Harmony patch for Vanilla Furniture Expanded - Spacer Module TV equipment.
- **Patch_WallStuff.xml**: Contains the Harmony patch for WallStuff TV equipment.

## XML Integration

The XML files in the mod are used to define the thoughts and patches for the TV equipment. The `Defs_TVThought.xml` file contains the ThoughtDef for the TV educational thought. The patch files (`Patch_TV_Vanilla.xml`, `Patch_VFE.xml`, `Patch_VFESpacer.xml`, `Patch_WallStuff.xml`) are used to integrate the mod with different TV equipment from other mods.

## Harmony Patching

Harmony patching is used to modify the behavior of the vanilla game to support the TV educational feature. The patch files are located in the `Patches` directory and contain the necessary Harmony patches to integrate the mod with different TV equipment.

## Suggestions for Copilot

- Use the XML files as a reference for defining thoughts and patches for TV equipment.
- Use the C# files as a reference for implementing the TV educational component logic.
- Use Harmony patching to modify the behavior of the vanilla game to support the TV educational feature.
- Follow the coding patterns and conventions established in the mod for consistency.

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

