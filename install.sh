#!/bin/bash

# Run docker compose
docker volume create pgdata
docker volume create pmdata
docker volume create grdata
docker volume create mtaildata
#mkdir -p /var/lib/docker/volumes/nginxdata/_data/mtail
#touch /var/lib/docker/volumes/nginxdata/_data/mtail/access.log
docker-compose up -d

#ansible-galaxy install nginxinc.nginx
#ansible-playbook nginx_deploy.yml -K