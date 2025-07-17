#!/bin/bash
# Claude Code tool-call-complete hook for repomix automation
# Triggers after tool execution that might have changed code

# Configuration
REPO_PATH="/Users/wangkanai/Sources/wangkanai"
HOOK_LOG="$REPO_PATH/.claude/hooks/claude-hook.log"
LAST_PACK_FILE="$REPO_PATH/.claude/hooks/last-repomix-pack"

# Function to log messages
log_message() {
    echo "[$(date '+%Y-%m-%d %H:%M:%S')] POST-TOOL: $1" >> "$HOOK_LOG"
}

# Function to check if tool might have changed code
tool_changes_code() {
    local tool_name="$1"
    
    # Tools that commonly change code
    local code_changing_tools=(
        "Edit" "MultiEdit" "Write" "NotebookEdit"
        "mcp__jetbrains__replace_selected_text"
        "mcp__jetbrains__replace_current_file_text"
        "mcp__jetbrains__replace_file_text_by_path"
        "mcp__jetbrains__replace_specific_text"
        "mcp__jetbrains__create_new_file_with_text"
        "mcp__github__create_or_update_file"
        "mcp__github__push_files"
    )
    
    for changing_tool in "${code_changing_tools[@]}"; do
        if [[ "$tool_name" == *"$changing_tool"* ]]; then
            log_message "Tool '$tool_name' is code-changing"
            return 0
        fi
    done
    
    return 1
}

# Function to check if enough time has passed since last pack
should_repack() {
    if [ ! -f "$LAST_PACK_FILE" ]; then
        return 0
    fi
    
    local last_pack_time=$(cat "$LAST_PACK_FILE" 2>/dev/null || echo "0")
    local current_time=$(date +%s)
    local time_diff=$((current_time - last_pack_time))
    
    # Only repack if at least 2 minutes have passed
    if [ $time_diff -gt 120 ]; then
        return 0
    else
        log_message "Skipping repack - only $time_diff seconds since last pack (minimum 120s)"
        return 1
    fi
}

# Function to trigger repomix packing
trigger_repomix_pack() {
    log_message "Triggering repomix pack after tool execution"
    
    cd "$REPO_PATH" || {
        log_message "ERROR: Cannot change to repository directory"
        return 1
    }
    
    # Create background process to avoid blocking Claude
    {
        # Wait a moment to ensure tool execution is complete
        sleep 3
        
        # Check if repomix config exists
        if [ ! -f "repomix.config.json" ]; then
            log_message "ERROR: repomix.config.json not found"
            return 1
        fi
        
        # Try to use repomix via Claude MCP first
        if command -v claude-code >/dev/null 2>&1; then
            log_message "Attempting to trigger repomix via Claude MCP after tool execution"
            
            # Create a signal file that Claude can detect
            echo "$(date +%s)" > "$REPO_PATH/.claude/repomix-tool-trigger"
            
        elif command -v repomix >/dev/null 2>&1; then
            log_message "Using direct repomix command after tool execution"
            repomix --config repomix.config.json
        else
            log_message "WARNING: Neither Claude Code nor repomix command found"
            return 1
        fi
        
        # Update last pack timestamp
        echo "$(date +%s)" > "$LAST_PACK_FILE"
        log_message "Repomix pack after tool execution completed"
        
    } &
}

# Main execution
main() {
    local tool_name="$1"
    local tool_result="$2"
    
    log_message "Claude tool-call-complete hook triggered"
    log_message "Tool: $tool_name"
    log_message "Result: ${tool_result:0:100}..."
    
    # Check if tool might have changed code
    if tool_changes_code "$tool_name"; then
        log_message "Tool execution might have changed code - checking if repack needed"
        
        # Check if enough time has passed
        if should_repack; then
            trigger_repomix_pack
        fi
    else
        log_message "Tool execution unlikely to have changed code - skipping repomix"
    fi
    
    log_message "Claude tool-call-complete hook completed"
}

# Execute main function with tool information as arguments
main "$@"