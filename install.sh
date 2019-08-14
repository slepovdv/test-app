#!/bin/bash

# Run docker compose
docker volume create pgdata
docker volume create pmdata
docker-compose up -d

ansible-galaxy install nginxinc.nginxыукмшсу
ansible-playbook nginx_deploy.yml -K