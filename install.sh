#!/bin/bash

# Run docker compose
docker volume create pgdata
docker volume create pmdata
docker volume create grdata
docker-compose up -d

ansible-galaxy install nginxinc.nginx
ansible-playbook nginx_deploy.yml -K