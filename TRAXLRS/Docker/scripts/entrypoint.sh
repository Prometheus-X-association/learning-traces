#!/bin/sh
set -e

if [ ! -f vendor/autoload.php ]; then
  composer install --no-interaction --prefer-dist
fi

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
