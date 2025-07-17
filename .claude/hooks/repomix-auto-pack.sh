#!/bin/bash
# Claude Code user-prompt-submit hook for automatic repomix packing
# Triggers when user submits a prompt that might involve code changes

# Configuration
REPO_PATH="/Users/wangkanai/Sources/wangkanai"
HOOK_LOG="$REPO_PATH/.claude/hooks/claude-hook.log"
LAST_PACK_FILE="$REPO_PATH/.claude/hooks/last-repomix-pack"
REPOMIX_OUTPUT="$REPO_PATH/repomix-output.xml"

# Function to log messages
log_message() {
    echo "[$(date '+%Y-%m-%d %H:%M:%S')] CLAUDE-HOOK: $1" >> "$HOOK_LOG"
}

# Function to check if repomix output is stale
is_repomix_stale() {
    if [ ! -f "$REPOMIX_OUTPUT" ]; then
        log_message "No repomix output file found - needs packing"
        return 0
    fi
    
    local output_age=$(stat -f %m "$REPOMIX_OUTPUT" 2>/dev/null || echo "0")
    local current_time=$(date +%s)
    local age_diff=$((current_time - output_age))
    
    # Consider stale if older than 10 minutes
    if [ $age_diff -gt 600 ]; then
        log_message "Repomix output is stale (${age_diff}s old) - needs refresh"
        return 0
    fi
    
    return 1
}

# Function to check if git working tree has changes
has_git_changes() {
    cd "$REPO_PATH" || return 1
    
    # Check for uncommitted changes
    if ! git diff --quiet || ! git diff --cached --quiet; then
        log_message "Git working tree has uncommitted changes"
        return 0
    fi
    
    # Check if we're ahead of origin
    local ahead=$(git rev-list --count origin/main..HEAD 2>/dev/null || echo "0")
    if [ "$ahead" -gt 0 ]; then
        log_message "Local branch is $ahead commits ahead of origin"
        return 0
    fi
    
    return 1
}

# Function to check if user prompt suggests code analysis
suggests_code_analysis() {
    local prompt="$1"
    
    # Convert to lowercase for case-insensitive matching
    local lower_prompt=$(echo "$prompt" | tr '[:upper:]' '[:lower:]')
    
    # Keywords that suggest code analysis or modification
    local keywords=(
        "analyze" "find" "search" "pattern" "implementation" "code" "class" "method"
        "function" "service" "controller" "builder" "extension" "interface" "namespace"
        "dependency" "injection" "configure" "add" "create" "implement" "refactor"
        "optimize" "improve" "fix" "bug" "error" "issue" "test" "mock" "assert"
        "architecture" "structure" "design" "framework" "library" "package" "module"
    )
    
    for keyword in "${keywords[@]}"; do
        if echo "$lower_prompt" | grep -q "$keyword"; then
            log_message "User prompt contains code-related keyword: $keyword"
            return 0
        fi
    done
    
    return 1
}

# Function to trigger repomix packing
trigger_repomix_pack() {
    log_message "Triggering repomix pack from Claude hook"
    
    cd "$REPO_PATH" || {
        log_message "ERROR: Cannot change to repository directory"
        return 1
    }
    
    # Create background process to avoid blocking Claude
    {
        # Small delay to ensure Claude is ready
        sleep 1
        
        # Check if repomix config exists
        if [ ! -f "repomix.config.json" ]; then
            log_message "ERROR: repomix.config.json not found"
            return 1
        fi
        
        # Try to use repomix via Claude MCP first
        if command -v claude-code >/dev/null 2>&1; then
            log_message "Attempting to trigger repomix via Claude MCP"
            
            # Create a signal file that Claude can detect
            echo "$(date +%s)" > "$REPO_PATH/.claude/repomix-trigger"
            
        elif command -v repomix >/dev/null 2>&1; then
            log_message "Using direct repomix command"
            repomix --config repomix.config.json
        else
            log_message "WARNING: Neither Claude Code nor repomix command found"
            return 1
        fi
        
        # Update last pack timestamp
        echo "$(date +%s)" > "$LAST_PACK_FILE"
        log_message "Repomix pack trigger completed"
        
    } &
}

# Main execution
main() {
    local user_prompt="$1"
    
    log_message "Claude user-prompt-submit hook triggered"
    log_message "User prompt: ${user_prompt:0:100}..."
    
    # Check if prompt suggests code analysis
    if suggests_code_analysis "$user_prompt"; then
        log_message "Prompt suggests code analysis - checking repomix status"
        
        # Check if repomix output is stale or git has changes
        if is_repomix_stale || has_git_changes; then
            trigger_repomix_pack
        else
            log_message "Repomix output is fresh and no git changes detected"
        fi
    else
        log_message "Prompt does not suggest code analysis - skipping repomix"
    fi
    
    log_message "Claude user-prompt-submit hook completed"
}

# Execute main function with user prompt as argument
main "$@"