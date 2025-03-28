#!/bin/bash

export INVALIDATE_CACHE=$(date +%s)
docker compose up --build -d
