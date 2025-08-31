# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Available MCP Commands (Ranked by Efficiency)

This project has access to several MCP (Model Context Protocol) servers. Commands are organized by priority and frequency of use for .NET development with Azure DevOps integration:

### 🔥 Tier 1: Essential Daily Development Commands

#### JetBrains IDE Integration (Most Used)

```bash
# Code editing and navigation (highest efficiency)
mcp__jetbrains__replace_specific_text          # Most efficient for precise edits
mcp__jetbrains__search_in_files_content        # Fast codebase search
mcp__jetbrains__find_files_by_name_substring   # Quick file location
mcp__jetbrains__get_file_text_by_path          # Direct file reading
mcp__jetbrains__list_directory_tree_in_folder  # Project structure overview

# Active development
mcp__jetbrains__get_current_file_errors        # Real-time error checking
mcp__jetbrains__get_project_problems           # Project-wide issues
mcp__jetbrains__run_configuration              # Execute builds/tests
mcp__jetbrains__execute_terminal_command       # Run dotnet commands

# Code formatting and quality
mcp__jetbrains__reformat_current_file          # Format current file
mcp__jetbrains__reformat_file                  # Format specific file

# Debugging
mcp__jetbrains__toggle_debugger_breakpoint     # Quick debugging setup
mcp__jetbrains__get_debugger_breakpoints       # Breakpoint management
```

### 🚀 Tier 2: High-Value Development Tools

#### Memory/Knowledge Graph

```bash
# Relationship tracking
mcp__memory__create_entities                  # Track system components
mcp__memory__create_relations                 # Document relationships
mcp__memory__search_nodes                     # Find related concepts
mcp__memory__read_graph                       # View entire knowledge graph
```

#### Code Analysis & Quality

```bash
# Repository analysis
mcp__repomix__pack_codebase                   # Analyze entire codebase
mcp__repomix__grep_repomix_output             # Search analysis results

# File operations
mcp__repomix__file_system_read_file           # Read files with security
mcp__repomix__file_system_read_directory      # List directory contents
```