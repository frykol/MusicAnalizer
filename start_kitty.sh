#!/bin/bash

SESSION_NAME="music_analizer_dev"
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

# Usuń poprzednią sesję
tmux kill-session -t "$SESSION_NAME" 2>/dev/null

# Database
tmux new-session -d \
    -s "$SESSION_NAME" \
    -n "Development" \
    -c "$SCRIPT_DIR" \
    "docker compose up; exec fish"

# Backend
tmux split-window -v \
    -t "$SESSION_NAME:Development" \
    -c "$SCRIPT_DIR/backend" \
    "dotnet watch run; exec fish"

# Frontend
tmux split-window -h \
    -t "$SESSION_NAME:Development.1" \
    -c "$SCRIPT_DIR/frontend" \
    "npm run dev; exec fish"

# Uruchom Kitty z obsługą sprzątania
kitty --detach bash -c '
    SESSION="$1"
    PROJECT="$2"

    tmux attach-session -t "$SESSION"

    echo "Stopping Docker..."
    docker compose -f "$PROJECT/compose.yaml" down

    echo "Removing tmux session..."
    tmux kill-session -t "$SESSION" 2>/dev/null
' _ "$SESSION_NAME" "$SCRIPT_DIR"