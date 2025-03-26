#!/bin/sh
set -e



# .env
if [ ! -f .env ]; then
  cp .env.example .env
fi

# Laravel
php artisan config:clear
php artisan key:generate
php artisan migrate --force
php artisan config:cache
php artisan route:cache
php artisan view:cache

# Serveur Laravel
exec php-fpm
