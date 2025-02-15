#!/bin/bash

# Docker Hub Username
DOCKER_USERNAME="gahantognoli"

# Define the services and Dockerfiles
services=(
  "fiap.faseum.techchallenge.api"
  "fiap.faseum.techchallenge.worker"
)

# Build and push each service's Docker image
for service in "${services[@]}"; do
  # Set the context and Dockerfile paths based on service
  if [[ "$service" == "fiap.faseum.techchallenge.api" ]]; then
    CONTEXT="."
    DOCKERFILE="src/FIAP.FaseUm.TechChallenge.Api/Dockerfile"
  elif [[ "$service" == "fiap.faseum.techchallenge.worker" ]]; then
    CONTEXT="."
    DOCKERFILE="src/FIAP.FaseUm.TechChallenge.Worker/Dockerfile"
  else
    CONTEXT="."
    DOCKERFILE="Dockerfile"  # Assuming the same Dockerfile for other services
  fi

  # Build the Docker image
  echo "Building Docker image for $service..."
  docker build -t "$DOCKER_USERNAME/$service" -f "$DOCKERFILE" "$CONTEXT"

  # Push the image to Docker Hub
  echo "Pushing $service image to Docker Hub..."
  docker push "$DOCKER_USERNAME/$service"

done

# Ensure Docker Hub login is required before this script is run
echo "Script execution completed!"
