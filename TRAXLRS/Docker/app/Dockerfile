FROM php:8.3-fpm

# Install system dependencies
RUN apt-get update && apt-get install -y \
    git \
    curl \
    unzip \
    libpng-dev \
    libjpeg-dev \
    libfreetype6-dev \
    libonig-dev \
    libxml2-dev \
    libzip-dev \
    libpq-dev \
    zip \
    nodejs \
    npm

# PHP extensions
RUN docker-php-ext-install pdo pdo_pgsql pgsql mbstring zip bcmath exif pcntl gd

# Composer
COPY --from=composer:latest /usr/bin/composer /usr/bin/composer

# Set working directory
WORKDIR /var/www

# Copy existing app files
COPY . .

# Install PHP dependencies
#RUN composer install --no-interaction

# Build frontend assets
#RUN npm install && npm run build

# Permissions
RUN chown -R www-data:www-data /var/www storage bootstrap/cache \
&& chmod -R 775 storage bootstrap/cache

# Entrypoint
COPY ./Docker/scripts/entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh

CMD ["/entrypoint.sh"]
