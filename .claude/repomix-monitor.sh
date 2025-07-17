#!/bin/bash
# Repomix monitoring and automation script
# Monitors for trigger files and executes repomix packing

# Configuration
REPO_PATH="/Users/wangkanai/Sources/wangkanai"
MONITOR_LOG="$REPO_PATH/.claude/repomix-monitor.log"
TRIGGER_FILE="$REPO_PATH/.claude/repomix-trigger"
TOOL_TRIGGER_FILE="$REPO_PATH/.claude/repomix-tool-trigger"
PID_FILE="$REPO_PATH/.claude/repomix-monitor.pid"

# Function to log messages
log_message() {
    echo "[$(date '+%Y-%m-%d %H:%M:%S')] MONITOR: $1" >> "$MONITOR_LOG"
}

# Function to pack codebase using repomix
pack_codebase() {
    local trigger_type="$1"
    
    log_message "Starting repomix pack (trigger: $trigger_type)"
    
    cd "$REPO_PATH" || {
        log_message "ERROR: Cannot change to repository directory"
        return 1
    }
    
    # Check if config file exists
    if [ ! -f "repomix.config.json" ]; then
        log_message "ERROR: repomix.config.json not found"
        return 1
    fi
    
    # Execute repomix
    if command -v repomix >/dev/null 2>&1; then
        log_message "Executing repomix with configuration"
        
        # Capture output and errors
        local output=$(repomix --config repomix.config.json 2>&1)
        local exit_code=$?
        
        if [ $exit_code -eq 0 ]; then
            log_message "Repomix pack completed successfully"
            log_message "Output: $output"
            
            # Update timestamp
            echo "$(date +%s)" > "$REPO_PATH/.claude/last-successful-pack"
            
            return 0
        else
            log_message "ERROR: Repomix pack failed with exit code $exit_code"
            log_message "Error output: $output"
            return 1
        fi
    else
        log_message "ERROR: repomix command not found"
        return 1
    fi
}

# Function to monitor for trigger files
monitor_triggers() {
    log_message "Starting repomix trigger monitoring"
    
    while true; do
        # Check for user prompt trigger
        if [ -f "$TRIGGER_FILE" ]; then
            log_message "User prompt trigger detected"
            
            # Remove trigger file
            rm -f "$TRIGGER_FILE"
            
            # Pack codebase
            pack_codebase "user-prompt"
            
            # Wait a bit before next check
            sleep 5
        fi
        
        # Check for tool execution trigger
        if [ -f "$TOOL_TRIGGER_FILE" ]; then
            log_message "Tool execution trigger detected"
            
            # Remove trigger file
            rm -f "$TOOL_TRIGGER_FILE"
            
            # Pack codebase
            pack_codebase "tool-execution"
            
            # Wait a bit before next check
            sleep 5
        fi
        
        # Sleep before next check
        sleep 2
    done
}

# Function to start monitoring daemon
start_daemon() {
    if [ -f "$PID_FILE" ]; then
        local existing_pid=$(cat "$PID_FILE")
        if kill -0 "$existing_pid" 2>/dev/null; then
            log_message "Monitor daemon already running with PID $existing_pid"
            return 0
        else
            log_message "Removing stale PID file"
            rm -f "$PID_FILE"
        fi
    fi
    
    log_message "Starting repomix monitor daemon"
    
    # Start monitoring in background
    monitor_triggers &
    local monitor_pid=$!
    
    # Save PID
    echo "$monitor_pid" > "$PID_FILE"
    
    log_message "Monitor daemon started with PID $monitor_pid"
    echo "Repomix monitor daemon started with PID $monitor_pid"
}

# Function to stop monitoring daemon
stop_daemon() {
    if [ -f "$PID_FILE" ]; then
        local monitor_pid=$(cat "$PID_FILE")
        if kill -0 "$monitor_pid" 2>/dev/null; then
            log_message "Stopping monitor daemon with PID $monitor_pid"
            kill "$monitor_pid"
            rm -f "$PID_FILE"
            echo "Repomix monitor daemon stopped"
        else
            log_message "Monitor daemon not running"
            rm -f "$PID_FILE"
            echo "Monitor daemon was not running"
        fi
    else
        log_message "No PID file found"
        echo "Monitor daemon is not running"
    fi
}

# Function to check daemon status
check_status() {
    if [ -f "$PID_FILE" ]; then
        local monitor_pid=$(cat "$PID_FILE")
        if kill -0 "$monitor_pid" 2>/dev/null; then
            echo "Repomix monitor daemon is running with PID $monitor_pid"
            return 0
        else
            echo "Repomix monitor daemon is not running (stale PID file)"
            return 1
        fi
    else
        echo "Repomix monitor daemon is not running"
        return 1
    fi
}

# Function to manually trigger repomix
manual_trigger() {
    log_message "Manual repomix trigger requested"
    pack_codebase "manual"
}

# Main execution
main() {
    local command="$1"
    
    case "$command" in
        "start")
            start_daemon
            ;;
        "stop")
            stop_daemon
            ;;
        "status")
            check_status
            ;;
        "trigger")
            manual_trigger
            ;;
        "monitor")
            monitor_triggers
            ;;
        *)
            echo "Usage: $0 {start|stop|status|trigger|monitor}"
            echo "  start   - Start the repomix monitor daemon"
            echo "  stop    - Stop the repomix monitor daemon"
            echo "  status  - Check daemon status"
            echo "  trigger - Manually trigger repomix pack"
            echo "  monitor - Run monitor in foreground (for debugging)"
            exit 1
            ;;
    esac
}

# Execute main function
main "$@"