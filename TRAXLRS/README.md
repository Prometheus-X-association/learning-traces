# TRAX LRS

## About TRAX LRS

TRAX LRS is an xAPI conformant **Learning Record Store (LRS)**.
It’s not a Learning Analytics solution. It’s a pure LRS. It focuses on storing and managing learning data, and that’s it.
For further information, visit http://traxlrs.com.

This project allows you to simultaneously deploy two components:
- **The LRS core**
- **A PostgreSql database**

This document outlines the steps to install, configure, and launch the project.

---

## Table of Contents

- [Requirements](#requirements)
- [Configuration (optional)](#configuration-optional)
- [Building and Launching the Project](#building-and-launching-the-project)
- [How to use](#how-to-use)

---

## Requirements

-   Docker

---

## Configuration (optional)

**This release of TRAX LRS is delivered pre-configured and ready to use, so this configuration step is completely optional.**

If you still wish to personalise :
At the root of the application folder, make a copy of the `.env.example` file, rename it `.env` and set your custom values.

Here are the available variables and their default values :

```
APP_NAME="TRAX LRS"
APP_ENV=local
APP_KEY= #leave blank, will be generated automatically
APP_DEBUG=true
APP_URL=http://localhost:8000

#Identification for the Admin console
ADMIN_EMAIL=traxlrs@mimbus.com
ADMIN_PASSWORD=password123
# Identification for the endpoint
DEFAULT_ENDPOINT_USERNAME=traxlrs
DEFAULT_ENDPOINT_PASSWORD=password123

DB_CONNECTION=pgsql
DB_HOST=db
DB_PORT=5432
DB_DATABASE=trax
DB_USERNAME=traxuser
DB_PASSWORD=traxpass
```

---

## Building and Launching the Project

1. **Open a console and navigate to the root of the application folder**
    ```shell
    cd TRAXLRS
    ```

2. **Build the Project:**
   ```shell
   docker compose build
   ```

2. **Launch the Project:**
   ```shell
   docker compose up
   ```
   *Note: The initial launching step may take between 1 and 10 minutes depending on your hardware.*

---

## How to use

### Admin Console

The admin console is hosted by default at `http://localhost:8000`.
The admin account has the default following credentials:

-   Email: `traxlrs@mimbus.com`
-   Password: `password123`

You can define your own credentials in the `.env` file with the `ADMIN_EMAIL` and `ADMIN_PASSWORD` environment variables.


### xAPI endpoint

Assuming that TRAX LRS is hosted at `http://localhost:8000`, the xAPI endpoint is `http://localhost:8000/trax/api/gateway/clients/default/stores/default/xapi`.

The default Basic HTTP credentials are:

-   Username: `traxlrs`
-   Password: `password123`

You can define your own credentials in the `.env` file with the `DEFAULT_ENDPOINT_USERNAME` and `DEFAULT_ENDPOINT_PASSWORD` environment variables.
